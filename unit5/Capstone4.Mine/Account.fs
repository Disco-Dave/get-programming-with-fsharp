namespace Capstone3

type Request =
    | Withdraw of decimal
    | Deposit of decimal

type Account =
    { Customer: Customer
      Transactions: Transaction<Request> list }

module Account =
    let restore customer transactions =
        { Customer = customer
          Transactions = transactions }

    let lastTransaction { Transactions = transactions } =
        List.tryHead transactions

    let balance { Transactions = transactions } =
        let execute runningBalance { Request = request } =
            match request with
            | Withdraw amount -> runningBalance - amount
            | Deposit amount -> runningBalance + amount

        transactions
        |> List.filter(fun t -> t.Status = Accepted)
        |> List.sortBy(fun t -> t.Timestamp)
        |> List.fold execute 0M

    let handle request account =
        let amount =
            match request with
            | Withdraw a -> a
            | Deposit a -> a

        let transaction =
            if amount <= 0M then
                Transaction.reject "Negative or 0 amount" request
            else
                let currentBalance = balance account
                match request with
                | Withdraw _ when currentBalance < amount -> 
                    Transaction.reject "Insufficient funds" request
                | _ -> 
                    Transaction.accept request

        { account with Transactions = transaction :: account.Transactions }
