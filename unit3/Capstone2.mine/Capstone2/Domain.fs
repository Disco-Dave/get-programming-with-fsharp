namespace Captstone2.Domain

open System

type Customer = Customer of string

type AccountNumber = AccountNumber of Guid

type Account =
    { Id: AccountNumber
      Balance: Decimal
      Customer: Customer }

type Bank = private | Bank of Map<AccountNumber, Account>

module Bank =
    let init = Bank Map.empty

    let openAccount customerName openingBalance (Bank bank) =
        let accountNumber = AccountNumber <| Guid.NewGuid()

        let account =
            { Id = accountNumber
              Customer = customerName
              Balance = openingBalance }
        accountNumber, Bank <| Map.add accountNumber account bank

    let getAccount accountNumber (Bank bank) = Map.tryFind accountNumber bank

    let deposit accountNumber amount (Bank bank) =
        match Map.tryFind accountNumber bank with
        | Some account when amount > 0M ->
            let account = { account with Balance = account.Balance + amount }
            Map.add accountNumber account bank
        | _ -> bank
        |> Bank

    let withdraw accountNumber amount (Bank bank) =
        match Map.tryFind accountNumber bank with
        | Some account when account.Balance >= amount && amount > 0M ->
            let account = { account with Balance = account.Balance - amount }
            Map.add accountNumber account bank
        | _ -> bank
        |> Bank
