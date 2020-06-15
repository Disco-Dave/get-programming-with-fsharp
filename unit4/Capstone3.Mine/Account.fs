namespace Capstone3

open System

type Currency = decimal

type Request =
    | Withdraw of Currency
    | Deposit of Currency

type Status =
    | Accepted
    | Rejected of reason: string

type Transaction =
    { Timestamp: DateTime
      Status: Status
      Request: Request }

[<RequireQualifiedAccess>]
module Transaction =
    let accept request =
        { Timestamp = DateTime.UtcNow
          Status = Accepted
          Request = request }

    let reject reason request =
        { Timestamp = DateTime.UtcNow
          Status = Rejected reason
          Request = request }

type Customer = private | Customer of string

[<RequireQualifiedAccess>]
module Customer =
    type Error = IsEmpty

    let make customer =
        if String.IsNullOrWhiteSpace customer 
            then Error IsEmpty 
            else Ok <| Customer (customer.Trim())

    let toString (Customer customer) = customer


type Account = 
    { Customer: Customer
      History: Transaction list }

    member this.Balance =
        let runAction runningBalance { Request = req } =
            match req with
            | Withdraw amount -> runningBalance - amount
            | Deposit amount -> runningBalance + amount

        this.History
        |> List.sortBy(fun t -> t.Timestamp)
        |> List.filter(fun t -> t.Status = Accepted)
        |> List.fold runAction 0M


[<RequireQualifiedAccess>]
module Account =
    let ``open`` customer =
        { Customer = customer
          History = [] }

    let lastTransaction {History = history} =
        List.tryHead history

    let handle request (account: Account) =
        let transaction =
            match request with
            | Deposit _  ->
                Transaction.accept request
            | Withdraw amount when account.Balance >= amount ->
                Transaction.accept request
            | Withdraw _ -> 
                Transaction.reject "Insufficient funds." request
        { account with History = transaction :: account.History }
