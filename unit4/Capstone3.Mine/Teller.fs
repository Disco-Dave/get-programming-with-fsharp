module Capstone3.Teller

open System
open Capstone3.Audit
open Capstone3.Effect

let private userInputs env = seq {
    while true do
        ConsoleWriter.write env "(w)ithdraw, (d)eposit, or (q)uit: "
        let input = ConsoleReader.readKey env |> Char.ToLower
        ConsoleWriter.writeLine env ""
        input }

let rec private getAccount env =
    ConsoleWriter.write env "Enter your name: "
    match ConsoleReader.readLine env |> Customer.make with
    | Error Customer.IsEmpty ->
        ConsoleWriter.writeLine env "The name may not be empty"
        getAccount env
    | Ok customer -> Account.``open`` customer

let rec private getAmount env =
    ConsoleWriter.write env "Enter amount: "
    let (isValid, amount) = ConsoleReader.readLine env |> Decimal.TryParse
    if isValid then amount else getAmount env

let private handleInput env loggers account input =
    let makeRequest =
        match input with
        | 'w' -> Some Withdraw
        | 'd' -> Some Deposit
        | _ -> None

    match makeRequest with
    | None -> account
    | Some req ->
        getAmount env
        |> req
        |> auditWith loggers Account.handle account

let start env loggers =
    let account = getAccount env

    account
    |> Account.balance
    |> (ConsoleWriter.writeLine env << sprintf "Current balance: %M")

    userInputs env
    |> Seq.takeWhile((<>) 'q')
    |> Seq.fold (handleInput env loggers) account
    |> printf "%A"
