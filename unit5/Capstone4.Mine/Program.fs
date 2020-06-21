open System
open Capstone4

type private Env () =
    interface Communicate.ISay with 
        member _.Say msg = printf "%s" msg
    interface Communicate.ISayLine with 
        member _.SayLine msg = printfn "%s" msg
    interface Communicate.IAskLine with 
        member _.AskLine () = Console.ReadLine()
    interface Communicate.IAskChar with 
        member _.AskChar () = (Console.ReadKey false).KeyChar
    
    interface Computer.IRetrieve with
        member _.Retrieve customer = FileStore.retrieve customer
    interface Computer.ISave with
        member _.Save account = FileStore.save account
    interface Computer.IDeposit with
        member _.Deposit amount account = Account.deposit amount account
    interface Computer.IWithdraw with
        member _.Withdraw amount account = Account.withdraw amount account

[<EntryPoint>]
let main _ =
    Teller.interact <| Env ()
    0
