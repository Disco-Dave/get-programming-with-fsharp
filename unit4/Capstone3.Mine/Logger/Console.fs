module Capstone3.Logger.Console

open Capstone3
open Capstone3.Audit
open Capstone3.Effect

let log env (summary: Summary) =
    let status =
        match summary.Transaction.Status with
        | Accepted -> "Accepted"
        | Rejected reason -> sprintf "Rejected (%s)" reason
    let request = 
        match summary.Transaction.Request with
        | Withdraw amount -> sprintf "Withdraw of %M" amount
        | Deposit amount -> sprintf "Deposit of %M" amount
    let transaction = sprintf "%s %s" status request
    let balance = sprintf "Before balance = %M, After balance = %M" summary.BeforeBalance summary.AfterBalance
    let timestamp = summary.Transaction.Timestamp

    let message =
        sprintf "%O : %s : %s" timestamp balance transaction

    ConsoleWriter.writeLine env message
