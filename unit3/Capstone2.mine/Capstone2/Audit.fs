module Capstone2.Audit

open Capstone2.Domain
open System.IO

let private summarizeAccount accountNumber bank =
    match Bank.getAccount accountNumber bank with
    | Some { Balance = balance } -> sprintf "Balance: %M" balance
    | None -> "Account not found!"

let withAudit audit operationName operation bank accountNumber amount =
    audit <| sprintf "%s: Attempting %s for $%M" (accountNumber.ToString()) operationName amount
    match operation accountNumber amount bank with
    | Error _ ->
        audit <| sprintf "%s: %s failed" (accountNumber.ToString()) operationName
        bank
    | Ok newBank ->
        sprintf "%s: %s succeeded. %s" (accountNumber.ToString()) operationName (summarizeAccount accountNumber bank)
        |> audit
        newBank

let withConsoleAudit operationName operation = withAudit (printfn "%s") operationName operation

let withFileAudit fileName operationName operation =
    let writeToFile(msg: string) =
        if File.Exists fileName then
            use sw = File.AppendText fileName
            sw.WriteLine msg
        else
            File.WriteAllText(fileName, msg)

    withAudit writeToFile operationName operation
