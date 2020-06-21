module Capstone4.Teller

open System

type AccountAlteration =
    | Withdraw
    | Deposit

type CustomerRequest =
    | Leave
    | AlterAccount of AccountAlteration

let tryParse parse (str: string) =
    let (isValid, a) = parse str
    if isValid then Some a else None

let tryParseCustomerRequest ch =
    match Char.ToLower ch with
    | 'q' -> Ok Leave
    | 'w' -> Ok <| AlterAccount Withdraw
    | 'd' -> Ok <| AlterAccount Deposit
    | _ -> Error "Invalid command"

let tryGetAlteration = function
    | AlterAccount alteration -> Some alteration
    | _ -> None

let tryParseAmount str =
    match tryParse Decimal.TryParse str with
    | None -> Error "Malformed decimal"
    | Some dec ->
        match Amount.make dec with
        | None -> Error "Amount may not be negative"
        | Some amount -> Ok amount
    
let alterAccount env ratedAccount (request, amount) =
    match request with
    | Deposit -> Ok <| Computer.deposit env amount ratedAccount
    | Withdraw ->
        match ratedAccount with
        | Overdrawn _ ->
            Error "Can not withdraw from overdrawn account"
        | InCredit account ->
            Ok <| Computer.withdraw env amount account

let interact env =
    let inputStream = seq { while true do Communicate.askChar env } 

    let prompt account = 
        (Account.account account).Balance
        |> sprintf "Current balance is %M"
        |> Communicate.sayLine env

        Communicate.say env "(w)ithdraw, (d)eposit, or (q)uit: "


    let hush = function
        | Ok a -> Some a
        | Error _ -> None

    let printErrorIfNeeded res =
        match res with
        | Error msg -> Communicate.sayLine env msg
        | _ -> ()
        res

    let addAmount input =
        Communicate.sayLine env ""
        Communicate.say env "Enter amount: "
        match Communicate.askLine env |> tryParseAmount with
        | Ok amount -> Ok (input, amount)
        | Error message -> Error message

    let alterAccount account res = 
        let newAccount =
            match Result.bind (alterAccount env account) res with
            | Error msg -> 
                Communicate.sayLine env msg
                account
            | Ok a -> a
        prompt newAccount
        newAccount

    let customer =
        let rec go () =
            Communicate.say env "Enter name: "
            match Communicate.askLine env |> Customer.make with
            | Ok c -> c
            | Error Customer.Error.IsEmpty ->
                Communicate.sayLine env "Name may not be empty"
                go ()
        go ()

    let account =
        Computer.retrieve env customer
        |> Option.defaultWith (fun () -> Account.make customer)
            
    prompt account

    inputStream
    |> Seq.map tryParseCustomerRequest
    |> Seq.takeWhile ((<>) (Ok Leave))
    |> Seq.choose (printErrorIfNeeded >> hush >> Option.bind tryGetAlteration)
    |> Seq.map addAmount
    |> Seq.fold alterAccount account
    |> Computer.save env
