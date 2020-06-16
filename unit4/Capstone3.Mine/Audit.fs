module Capstone3.Audit

open Capstone3

type Summary =
    { Customer: Customer
      BeforeBalance: decimal
      AfterBalance: decimal
      Transaction: Transaction<Request> }

type Log = Summary -> unit

type Handle = Request -> Account -> Account

let auditWith loggers handle (account: Account) request =
    let customer = account.Customer
    let beforeBalance = Account.balance account
    let newAccount = handle request account
    let log summary = Seq.iter ((|>) summary) loggers

    newAccount
    |> Account.lastTransaction
    |> Option.iter(fun transaction ->
        log { Customer = customer
              BeforeBalance = beforeBalance
              AfterBalance = Account.balance newAccount
              Transaction = transaction })

    newAccount
