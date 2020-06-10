namespace Capstone2.Domain

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

    type DepositError =
        | AccountNotFound
        | NegativeAmount

    let deposit accountNumber amount (Bank bank) =
        match Map.tryFind accountNumber bank with
        | Some account when amount >= 0M ->
            let account = { account with Balance = account.Balance + amount }
            let bank = Map.add accountNumber account bank
            Ok bank
        | Some _ -> Error NegativeAmount
        | _ -> Error AccountNotFound
        |> Result.map Bank

    type WithdrawError =
        | AccountNotFound
        | NegativeAmount
        | InsufficientFunds

    let withdraw accountNumber amount (Bank bank) =
        match Map.tryFind accountNumber bank with
        | None -> Error AccountNotFound
        | Some _ when amount < 0M -> Error NegativeAmount
        | Some account when account.Balance < amount -> Error InsufficientFunds
        | Some account ->
            let account = { account with Balance = account.Balance - amount }
            let bank = Map.add accountNumber account bank
            Ok bank
        |> Result.map Bank
