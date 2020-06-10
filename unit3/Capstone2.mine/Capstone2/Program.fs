module Capstone2.Program

open Capstone2.Audit
open Capstone2.Domain

[<EntryPoint>]
let main _ =

    let deposit = withFileAudit "$XDG_RUNTIME_DIR/bank.txt" "Deposit" Bank.deposit
    let withdraw = withFileAudit "$XDG_RUNTIME_DIR/bank.txt" "Withdraw" Bank.withdraw

    let rec go bank =
        ()

    go Bank.init
    0 // return an integer exit code
