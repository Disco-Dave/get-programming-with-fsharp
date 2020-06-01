open System
open CapstoneUnit2


[<EntryPoint>]
let main _ =
    let console =
        { new Game.IConsole with
            member __.WriteLine(msg) = Console.WriteLine(msg)
            member __.Write(msg) = Console.Write(msg)
            member __.ReadLine() = Console.ReadLine() }
    Game.start console
    0
