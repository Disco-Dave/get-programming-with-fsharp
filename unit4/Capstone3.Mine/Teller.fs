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
            Communicate.askChar env |> Char.ToLower
    }

let handleInput env account input =
    Communicate.sayLine env ""

    let rec getAmount() =
        Communicate.say env "Enter amount: "
        let (isValid, amount) = Communicate.askLine env |> Decimal.TryParse
        if isValid then amount else getAmount()

    let runAction req =
        getAmount()
        |> req
        |> Computer.alterAccount env account

    let newAccount =
        match input with
        | 'w' -> runAction Withdraw
        | 'd' -> runAction Deposit
        | _ -> account

    prompt env newAccount

    newAccount

let interact env =
    let account = getCustomer env |> Computer.retrieveAccount env

    prompt env account

    userInputs env
    |> Seq.takeWhile((<>) 'q')
    |> Seq.fold (handleInput env) account
    |> ignore
