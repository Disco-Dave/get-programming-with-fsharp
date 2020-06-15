module Capstone3.Console

open Capstone3.Effect
open Capstone3.Audit

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
    let timestamp = summary.Transaction.Timestamp.ToString()

    let message =
        sprintf "%s : %s : %s" timestamp balance transaction

    ConsoleWriter.writeLine env message
