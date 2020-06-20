module Capstone3.Teller

open System
open Capstone3

let rec getCustomer env =
    Communicate.say env "Enter name: "
    match Communicate.askLine env |> Customer.make with
    | Error Customer.IsEmpty ->
        Communicate.sayLine env "Name may not be empty."
        getCustomer env
    | Ok customer -> customer

let prompt env account =
    sprintf "Current balance is $%M." (Account.balance account) |> Communicate.sayLine env
    Communicate.say env "(d)eposit, (w)ithdraw, (q)uit: "

let userInputs env =
    seq {
        while true do
            Communicate.askChar env
    }

type AccountAction =
    | Withdraw
    | Deposit

type Input =
    | Exit
    | Action of AccountAction

let parseInput rawInput =
    match Char.ToLower rawInput with
    | 'w' -> Some <| Action Withdraw
    | 'd' -> Some <| Action Deposit
    | 'q' -> Some Exit
    | _ -> None

let handleAction env account action =
    Communicate.sayLine env ""

    let rec getAmount() =
        Communicate.say env "Enter amount: "
        let (isValid, amount) = Communicate.askLine env |> Decimal.TryParse
        if isValid then 
            amount 
        else 
            Communicate.sayLine env "Invalid amount."
            getAmount()

    let runAction req =
        getAmount()
        |> req
        |> Computer.alterAccount env account

    let newAccount =
        match action with
        | Withdraw -> runAction Request.Withdraw
        | Deposit -> runAction Request.Deposit

    prompt env newAccount

    newAccount

let interact env =
    let account = getCustomer env |> Computer.retrieveAccount env

    prompt env account

    let handleInput account = function
        | None ->
            Communicate.sayLine env "Invalid command."
            prompt env account
            account
        | Some (Action action) ->
            handleAction env account action
        | Some Exit ->
            account

    userInputs env
    |> Seq.map parseInput
    |> Seq.takeWhile((<>) (Some Exit))
    |> Seq.fold handleInput account
    |> ignore
