module Presentation

(*
    Overview:
    * Let bindings
    * Functions
    * Access modifiers
    * Scope
    * Type Inference
    * Mutability vs Immutability
    * Statements vs Expressions
*)



// LET BINDINGS
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/functions/let-bindings 

// We can use the let keyword assign values, functions, or objects to an identifier
let numberOfCats = 3
let ``We can also use double back ticks`` = false
let rng = System.Random()
let boyz4Now = ["Griffin"; "Matt"; "Boo Boo"; "Allen"]

// Notice all of these have inferred types, but we can be explicit if we'd like
let numberOfDogs: int = 0
let ``Some name with spaces``: bool = true
let randomInt: unit -> int = rng.Next

// NOTE: The default is immutable
let apples = 3
apples <- apples + 1 // COMPILE ERROR: 

let mutable oranges = 3
oranges <- oranges + 1



// FUNCTIONS
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/functions/
// NOTE: No return keyword
let greet name = sprintf "Hello, %s" name

let yell (name: string): string = 
    let bigName = name.ToUpper()
    sprintf "Hello, %s" bigName

let add (x, y) = x + y
let sub x y = x - y



// Access Modifiers
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/access-control
// NOTE: Things default to public, and there is no protected.
// Although you can still implement things that were protected in C#
let private secret = 123
let internal notAsSecret = 555



// SCOPE
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/functions/#scope
// Scoping is done with white space, we can nest functions and variables arbitrarily deep

let estimateAges familyName years =
    let calculateAge yearOfBirth =
        let year = System.DateTime.Now.Year
        year - yearOfBirth
    //let averageAge = Seq.averageBy (calculateAge >> double) years
    let averageAge =
        // NOTE: F# does not have implicit casting
        let ages = Seq.map (fun y -> double(calculateAge y)) years
        if Seq.isEmpty ages then
            0.0
        else
            let numberOfPeople = Seq.length ages
            let sumOfAges = Seq.sum ages
            sumOfAges / (double numberOfPeople)

    sprintf "Average age for family %s is %.0f" familyName averageAge

estimateAges "Burkett" [1995; 1991; 1987; 1966; 1961]



// Type Inference
// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/type-inference
// Unlike C#, F# can infer return types and function parameters.
// F# also takes into account how a value is used to infer its type.

// Infers the most generic signature it can
let tupleToList (a,b,c) = [a;b;c] // a:'a * b:'a * c:'a -> 'a list

// Sometimes has trouble inferring types when only using methods
let toUpper str = str.ToUpper() // COMPILE ERROR
let toUpper' (str: string) = str.ToUpper() // COMPILE ERROR



// Expressions
// Statements have no return value and have side effects
// Expressions have a return value and may have side effects

// Almost everything in F# is an expression
// All expressions have to return something
// If you need a statement then you need to return unit
// F# also warns you about discarded results, to get rid of this warning you
// have to explicitly use the ignore function.
let ignore _ = ()

// If statements are expressions unlike C#
let test x y =
  if x = y then 
      "equals"
  elif x < y then 
      "is less than"
  else 
      "is greater than"
      
      
// Opening namespaces or modules
open System.IO

let writeTextToDisk text =
    let path = Path.GetTempFileName()
    System.IO.File.WriteAllText(path, text)    

let createManyFiles() =
    writeTextToDisk "The quick brown fox jumped over the lazy dog"
    writeTextToDisk "The quick brown fox jumped over the lazy dog"
    writeTextToDisk "The quick brown fox jumped over the lazy dog"

createManyFiles()
