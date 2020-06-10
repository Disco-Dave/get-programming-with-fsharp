module Unit3

(*
    Overview:
    * Tuples
    * Records
    * Partial Application
    * Forward-Pipe operator (|>)
    * Reverse composition operator (>>)
    * Namespaces
    * Modules
    * High-order functions
*)

open System

// Tuples - a grouping of unnamed but ordered values, possibly of different types.
// Official Documentation: https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/tuples
// NOTE: The type signature of tuples is done with an asterisk. `int * string` `string * double * bool`

let a = (1, 2)
let (x, y) = (12, 33)

let parseInt(str: string): int option =
    // NOTE: Int32.TryParse takes an out parameter as it's second argument, but F# let's us omit it and instead return that out value as a tuple.
    // :) Very nice
    let (didWork, parsedInt) = Int32.TryParse str
    if didWork then Some parsedInt else None

let parsePlayer(player: string): string * string * int =
    let parts =
        if String.IsNullOrWhiteSpace player then [||] else player.Split ' '

    let getPart index parser def =
        Array.tryItem index parts
        |> Option.bind parser
        |> Option.defaultWith(fun () -> def)

    let name = getPart 0 Some "unknown"
    let game = getPart 1 Some "unknown"
    let score = getPart 2 parseInt 0
    name, game, score

let (name, game, score) = parsePlayer "Chase Dragon-Ball 9001"

// F# has had tuple support longer than C#. If you want to interop with C#'s tuple we have to use a struct tuple
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/tuples#interoperation-with-c-tuples
let struct (foo, bar) = struct (11, 22)




// Records - represent simple aggregates of named values, optionally with members.
// Official Docmentation: https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/records
//
// In functional programming these are sometimes called Product Types. https://en.wikipedia.org/wiki/Product_type

type Point =
    { X: float
      Y: float
      Z: float }

type Customer =
    { First: string
      Last: string
      SSN: uint32
      AccountNumber: uint32 }

// Benefits of records over classes
// 1. Immutable by default
// 2. Automatic structural equality of the entire object graph (Value object)
// 3. Enforces that all fields are set
// 4. Easy to create new records from old ones with the `with` "copy-and-update" syntax

// Notice how the compiler infers what type the record based on its properties.
// If you're ever in a situation where you need to explicit about the type you have two options
// 1. Specify the type on the left hand side of the =
// 2. Fully qualify one or all of the properties

let ourCustomer =
    { First = "Joe"
      Last = "Shmoe"
      SSN = 123u
      AccountNumber = 1234u }

let ourOtherCustomer =
    { First = "Joe"
      Last = "Shmoe"
      SSN = 123u
      AccountNumber = 1234u }

ourCustomer = ourOtherCustomer // true

let improvedCustomer = { ourCustomer with First = "Joey" }



// Partial Application - the process of fixing a number of arguments to a function, producing another function of smaller arity.
// Official Documentation: https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/functions/#partial-application-of-arguments

// Two ways to write a function

// 1 Tupled form
let tupledAdd (a,b) = a + b
// 2 Curried form
let curriedAdd a b = a + b

let add5 = curriedAdd 5

let buildDate year month day = DateTime (year, month, day)
let buildDateThisYear = buildDate DateTime.UtcNow.Year
let buildDateThisMonth = buildDateThisYear DateTime.UtcNow.Month

// Forward-Pipe operator (|>) - plays nicely with partial application
// Allows use to make a pipeline. Pipe the output of the previous function into the next function.
let (|>) v f = f v

open System.IO

Directory.GetCurrentDirectory()
|> Directory.GetCreationTime
|> printf "%A"

let add l r = l + l
let sub l r = l - l
let multiply l r = l * l

10
|> add 30
|> sub 2
|> multiply 3

// Reverse composition operator (>>) - allows use to compose two functions together to make a new function
let (>>) f g = fun a -> g (f a)

let add30Subtract2AndTriple: int -> int = add 30 >> sub 2 >> multiply 3
add30Subtract2AndTriple 20


// Namespaces: https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/namespaces 
// Modules: https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/modules 


// Higher-order functions is a function does one or both of the following:
// 1. Takes one or more functions as arguments
// 2. Returns a function

let twice f = f >> f
let add3 = (+) 3
twice add3 7
