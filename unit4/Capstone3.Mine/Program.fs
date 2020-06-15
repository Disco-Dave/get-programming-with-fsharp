open System
open Capstone3
open Capstone3.Effect
open Capstone3.Logger

type private IEnv =
    inherit IConsoleWriter
    inherit IConsoleReader

let private env =
    { new IEnv with
        member _.Write msg = Console.Write msg
        member _.WriteLine msg = Console.WriteLine msg
        member _.ReadLine() = Console.ReadLine()
        member _.ReadKey() = (Console.ReadKey true).KeyChar }

[<EntryPoint>]
let main _ =
    Teller.start env [| Console.log env; FileStore.log |]
    0
