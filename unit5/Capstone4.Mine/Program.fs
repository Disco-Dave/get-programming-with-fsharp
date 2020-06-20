open System
open System.IO
open Capstone3

type Env () =
    // A bank tellers computer.
    interface Computer.IRetrieveAccount with
        member this.Retrieve customer = 
            TransactionLog.getTransactionLog this customer
            |> Account.restore customer
    interface Computer.IAlterAccount with
        member this.Alter account request = 
            let loggers = [ConsoleLog.log (printfn "%s"); TransactionLog.log this]
            let handle = Audit.auditWith loggers Account.handle
            handle account request

    // A way for the bank teller to communicate with the customer.
    interface Communicate.ISay with
        member _.Say message = printf "%s" message
    interface Communicate.ISayLine with
        member _.SayLine message = printfn "%s" message
    interface Communicate.IAskChar with
        member _.AskChar () = Console.ReadKey(true).KeyChar
    interface Communicate.IAskLine with
        member _.AskLine () = Console.ReadLine()

    // How to interact with a file system
    interface FileSystem.ICreateDirectory with
        member _.CreateDirectory dir =
            Directory.CreateDirectory dir |> ignore
    interface FileSystem.IDoesFileExist with
        member _.DoesFileExist path =
            File.Exists path
    interface FileSystem.IReadLines with
        member _.ReadLines path =
            File.ReadLines path
    interface FileSystem.ICreateFile with
        member _.CreateFile path =
            (File.Create path).Dispose()
    interface FileSystem.IAppendText with
        member _.AppendText path text =
            use sw = File.AppendText path
            sw.WriteLine text


[<EntryPoint>]
let main _ =
    Teller.interact <| Env ()
    0
