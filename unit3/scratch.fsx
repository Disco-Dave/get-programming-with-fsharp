module Unit3

open System

let parseInt(str: string): int option =
    let (didWork, parsedInt) = Int32.TryParse str
    if didWork then Some parsedInt else None

// Exercise on page 104
// NOTE: Tuples are represented with asterisks
let parse person =
    let parts =
        if String.IsNullOrWhiteSpace person 
            then [||] 
            else person.Split ' '

    let getPart index parser def =
        Array.tryItem index parts
        |> Option.bind parser
        |> Option.defaultWith(fun () -> def)

    let name = getPart 0 Some "unknown"
    let game = getPart 1 Some "unknown"
    let score = getPart 2 parseInt 0
    name, game, score
