open System
open CapstoneUnit2


type RealConsole() =
    interface Game.IConsole with
        member __.WriteLine(msg) = Console.WriteLine(msg)
        member __.Write(msg) = Console.Write(msg)
        member __.ReadLine() = Console.ReadLine() 

[<EntryPoint>]
let main _ =
    Game.start (RealConsole())
    0
