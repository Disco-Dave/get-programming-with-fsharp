module Capstone2.Tests.DomainTests

open Captstone2.Domain
open FsUnit.Xunit
open System
open Xunit

[<Fact>]
let ``Get an account returns None when an account can't be found``() =
    Bank.init
    |> Bank.getAccount(AccountNumber <| Guid.NewGuid())
    |> should equal None

[<Fact>]
let ``Get an account returns Some account when found``() =
    let (accountNumber, bank) = Bank.openAccount (Customer "David") 1000M Bank.init
    Bank.getAccount accountNumber bank
    |> should equal
           (Some
               { Id = accountNumber
                 Balance = 1000M
                 Customer = Customer "David" })

[<Fact>]
let ``Can deposit positive amount of money``() =
    let (accountNumber, bank) = Bank.openAccount (Customer "David") 1000M Bank.init
    bank
    |> Bank.deposit accountNumber 200M
    |> Bank.getAccount accountNumber
    |> should equal
           (Some
               { Id = accountNumber
                 Balance = 1200M
                 Customer = Customer "David" })

[<Fact>]
let ``Can not deposit negative amount of money``() =
    let (accountNumber, bank) = Bank.openAccount (Customer "David") 1000M Bank.init
    bank
    |> Bank.deposit accountNumber -200M
    |> Bank.getAccount accountNumber
    |> should equal
           (Some
               { Id = accountNumber
                 Balance = 1000M
                 Customer = Customer "David" })

[<Fact>]
let ``Can withdraw positive amount of money``() =
    let (accountNumber, bank) = Bank.openAccount (Customer "David") 1000M Bank.init
    bank
    |> Bank.withdraw accountNumber 200M
    |> Bank.getAccount accountNumber
    |> should equal
           (Some
               { Id = accountNumber
                 Balance = 800M
                 Customer = Customer "David" })

[<Fact>]
let ``Can withdraw all of the money``() =
    let (accountNumber, bank) = Bank.openAccount (Customer "David") 1000M Bank.init
    bank
    |> Bank.withdraw accountNumber 1000M
    |> Bank.getAccount accountNumber
    |> should equal
           (Some
               { Id = accountNumber
                 Balance = 0M
                 Customer = Customer "David" })

[<Fact>]
let ``Can not withdraw negative amount of money``() =
    let (accountNumber, bank) = Bank.openAccount (Customer "David") 1000M Bank.init
    bank
    |> Bank.withdraw accountNumber -200M
    |> Bank.getAccount accountNumber
    |> should equal
           (Some
               { Id = accountNumber
                 Balance = 1000M
                 Customer = Customer "David" })
