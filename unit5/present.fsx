module Unit5

// For loops

for number in 1 .. 10 do
    printfn "%d Hello!"  number

for number in 10 .. -1 .. 1 do
    printfn "%d Hello!" number

for number in 2 .. 2 .. 10 do
    printfn "%d Hello!" number


// Notice we can use the range syntax outside of loops
let oneToTen = [ 1 .. 10 ]
// also works for arrays
let oneToTenArray = [| 1.. 10 |]


// While loops
open System.IO
let reader = new StreamReader(File.OpenRead "File.txt")
while (not reader.EndOfStream) do
    printfn "%s" (reader.ReadLine())


open System
// Comprehensions
let arrayOfChars = [| for c in 'a' .. 'z' -> Char.ToUpper c|]
let listOfSquares = [ for i in 1 .. 10 -> i * i]
let seqOfStrings = seq { for i in 2 .. 4 .. 20 -> sprintf "Number %d" i}

// We can also put conditionals in there
let perfectSquares = [ for i in 1 .. 100 do if Math.Sqrt(double i) % 1.0 = 0.0 then yield i ]

// You can also nest them
[for i in 1 .. 10 do
    for j in 10 .. -1 .. 1 do
        yield (i, j)]

// If/else
let limit score years =
    if score = "medium" && years = 1 then 500
    elif score = "good" && (years = 0 || years = 1) then 750
    elif score = "good" && years = 2 then 1000
    elif score = "good" then 200
    else 250

// Pattern matching
let limit2 score years =
    match score, years with
    | "medium", 1 -> 500
    | "good", year when year < 2 -> 750
    | "good", 2 -> 1000
    | "good", _ -> 2000
    | _ -> 250

(*
* Pattern matching
    * Exhaustive checking
    * Warns us about unreachable patterns
    * Test against single source
    * Match using patterns that represent specific cases
    * Expression
    * Wild card is _
    * Guards
    * Top down
    * Pattern match against lists, arrays, records
    * Should prefer pattern matching
*)

// Discriminated unions
// Option
// Result
// Modelling business rules in types
// Parse don't validate
