open System

(*
    The "try this" section at the end of the lesson said to
    enhance the application by printing out the length of 
    the array as well as the items supplied. But the example
    in book already does this?
*)

[<EntryPoint>]
let main argv =
    let items = argv.Length
    printfn "Passed in %d items: %A" items argv
    0 // return an integer exit code
