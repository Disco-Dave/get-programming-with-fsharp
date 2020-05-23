open System

let countWords (text: string): int =
    if String.IsNullOrWhiteSpace text then
        0
    else
        text.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        |> Array.length

for text in [|""; "a b c"; "a   b  c "; "cat dog"|] do
    printfn "Text: %s; Result: %d" text (countWords text)
