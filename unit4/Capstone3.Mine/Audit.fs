module Capstone3.Audit

type Summary =
    { Customer: Customer
      BeforeBalance: Currency
      AfterBalance: Currency
      Transaction: Transaction }

type Log = Summary -> unit

type Handle = Request -> Account -> Account

let auditWith (loggers: Log seq) (handle: Handle) (request: Request) (account: Account) =
    let customer = account.Customer
    let beforeBalance = account.Balance
    let newAccount = handle request account
    let log summary = Seq.iter ((|>) summary) loggers

    newAccount
    |> Account.lastTransaction
    |> Option.iter(fun transaction ->
        log { Customer = customer
              BeforeBalance = beforeBalance
              AfterBalance = newAccount.Balance
              Transaction = transaction })

    newAccount
