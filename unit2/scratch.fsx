module Scratch

open System

// We can bind normal values with let
let age = 25 // Inferred to be an `int`
let name = "David" // Invered to be a `string`

// The `new` keyword is optional, by convetion we only add
// the `new` keyword when the object implments `IDisposable`
let rng = System.Random()

// We may add explicit type annotations if needed
let (cat: string) = "Sagwa"
let (theGang: string array) = [| "Dennis"; "Dee"; "Charlie"; "Mac"; "Frank" |]

// Note that all of these are immutable by default
(* The follow won't compile.
age <- 26
name <- name + " Burkett"
*)
// if you really need to a variable to be mutable
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





