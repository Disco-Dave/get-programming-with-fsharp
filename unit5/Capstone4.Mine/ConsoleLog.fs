module Capstone3.ConsoleLog

open Capstone3
open Capstone3.Audit

let log print {Transaction = transaction} =
    let status =
        match transaction.Status with
        | Accepted -> "accepted"
        | Rejected reason -> sprintf "rejected (%s)" reason
    let request =
        match transaction.Request with
        | Deposit amount -> sprintf "Deposit of $%M" amount
        | Withdraw amount -> sprintf "Withdraw of $%M" amount
    sprintf "%O: %s %s" transaction.Timestamp request status
    |> print 
