module Unit3

open System

let parseInt(str: string): int option =
    let (didWork, parsedInt) = Int32.TryParse str
    if didWork then Some parsedInt else None

// Exercise on page 104
// NOTE: Tuples are represented with asterisks
let parse person =
    let parts =
        if String.IsNullOrWhiteSpace person then [||] else person.Split ' '

    let getPart index parser def =
        Array.tryItem index parts
        |> Option.bind parser
        |> Option.defaultWith(fun () -> def)

    let name = getPart 0 Some "unknown"
    let game = getPart 1 Some "unknown"
    let score = getPart 2 parseInt 0
    name, game, score

// Exercise on page 117
type Car =
    { Manufacturer: string
      EngineSize: double
      NumberOfDoors: int
      HasAutomaticWindows: bool }

let myCar =
    { Manufacturer = "Hyundai"
      EngineSize = 1.5
      NumberOfDoors = 5
      HasAutomaticWindows = true }

// Exercise on page 120
type Address =
    { HouseNumber: string
      StreetName: string
      City: string
      State: string
      ZipCode: string }

let mcdonalds =
    { HouseNumber = "4230"
      StreetName = "Trindle Road"
      City = "Camp Hill"
      State = "Pennsylvania"
      ZipCode = "17011" }

let mcdonalds2 =
    { HouseNumber = "4230"
      StreetName = "Trindle Road"
      City = "Camp Hill"
      State = "Pennsylvania"
      ZipCode = "17011" }

mcdonalds = mcdonalds2
mcdonalds.Equals mcdonalds2
System.Object.ReferenceEquals(mcdonalds, mcdonalds2)

type Customer =
    { Name: string
      Age: int }

let randomizeAge customer =
    let newAge = Random().Next(18, 48)
    printfn "Old age %d, new age %d" customer.Age newAge
    { customer with Age = newAge }

randomizeAge
    { Name = "Bob"
      Age = 30 }
