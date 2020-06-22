namespace Capstone4

type Account =
    { Customer: Customer
      Balance: decimal }

type CreditAccount = private | CreditAccount of Account

type RatedAccount =
    | Overdrawn of Account
    | InCredit of CreditAccount

module Account =
    let account =
        function
        | Overdrawn a -> a
        | InCredit (CreditAccount a) -> a

    let rateAccount account =
        if account.Balance >= 0M 
            then InCredit <| CreditAccount account 
            else Overdrawn account

    let make customer =
        { Customer = customer
          Balance = 0M }
        |> rateAccount

    let private updateBalance op amount account =
        { account with Balance = op (account.Balance) (PositiveAmount.unwrap amount) } 
        |> rateAccount

    let deposit amount = 
        account >> updateBalance (+) amount

    let withdraw amount (CreditAccount account) = 
        updateBalance (-) amount account
