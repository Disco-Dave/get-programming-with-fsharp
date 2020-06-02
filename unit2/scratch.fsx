module Scratch

open System

// We can bind normal values with let
let age = 25 // Inferred to be an `int`
let name = "David" // Inferred to be a `string`

// The `new` keyword is optional, by convention we only add
// the `new` keyword when the object implements `IDisposable`
let rng = System.Random()

// We may add explicit type annotations if needed
let (cat: string) = "Sagwa"
let (theGang: string array) = [| "Dennis"; "Dee"; "Charlie"; "Mac"; "Frank" |]

// An example using a double back ticks
let ``Some function does such and such`` = false

// You can also have access modifiers
let private secret = 123
let internal semiSecret = 456

// Note that all of these are immutable by default
(* The follow won't compile.
age <- 26
name <- name + " Burkett"
*)

// If you really need a variable to be mutable
let mutable size = 1
size <- 2

// Important to note that we can bind functions with let
let add (x, y) = x + y
let (randomInt: unit -> int) = rng.Next


let estimateAge =
    let age =
        let year = DateTime.Now.Year
        year - 1979
    sprintf "You are about %d years old!" age

let estimateAges familyName years = // familyName:string -> years:int seq -> string
    let calculateAge yearOfBirth =
        let year = DateTime.Now.Year
        year - yearOfBirth
    let averageAge = Seq.averageBy (calculateAge >> double) years
    //let averageAge =
    //    let ages = Seq.map (fun y -> double(calculateAge y)) years
    //    if Seq.isEmpty ages then
    //        0.0
    //    else
    //        let numberOfPeople = Seq.length ages
    //        let sumOfAges = Seq.sum ages
    //        sumOfAges / (double numberOfPeople)

    sprintf "Average age for family %s is %.0f" familyName averageAge

estimateAges "Burkett" [1995; 1991; 1987; 1966; 1961]





let someSimpleFunction a = sprintf "%A" a

let calculateAge birthYear =
    let currentYear = DateTime.Now.Year
    currentYear - birthYear

