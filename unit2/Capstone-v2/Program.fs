open System


type PetrolUnits = PetrolUnits of int

type Car = Car of PetrolUnits

type Location =
    | Home
    | Office
    | Stadium
    | GasStation

module Location =
    let petrolRequired location =
        match location with
        | Home -> 25
        | Office -> 50
        | Stadium -> 25
        | GasStation -> 10
        |> PetrolUnits

    let petrolGained location =
        match location with
        | GasStation -> 50
        | _ -> 0
        |> PetrolUnits

    let tryParse rawLocation =
        if String.IsNullOrWhiteSpace(rawLocation) then
            None
        else
            match rawLocation.Trim().ToLower() with
            | "home" -> Some Home
            | "office" -> Some Office
            | "stadium" -> Some Stadium
            | "gas station" -> Some GasStation
            | _ -> None

module Car =
    let init =
        let petrol = PetrolUnits 100
        Car petrol

    type DriveResult =
        | NotEnoughPetrol
        | Driven of Car

    let drive location (Car(PetrolUnits currentPetrol)) =
        let (PetrolUnits petrolRequired) = Location.petrolRequired location
        if currentPetrol < petrolRequired then
            NotEnoughPetrol
        else
            let (PetrolUnits petrolGained) = Location.petrolGained location
            let newPetrol = currentPetrol - petrolRequired + petrolGained
            Driven(Car(PetrolUnits newPetrol))

    let isEmpty(Car(PetrolUnits currentPetrol)) = currentPetrol <= 0

[<Interface>]
type IConsoleEff =
    abstract WriteLine: string -> unit
    abstract ReadLine: unit -> string

let rec gameLoop (eff: IConsoleEff) car =
    if Car.isEmpty car then
        eff.WriteLine "You ran out of gas."
    else
        let (Car(PetrolUnits petrol)) = car

        eff.WriteLine(sprintf "Your car has %d petrol units." petrol)
        eff.WriteLine "Where would you like to drive? Home (25), Office (50), Stadium (25), or Gas Station (10)? "

        let response = eff.ReadLine()
        match Location.tryParse response with
        | None ->
            eff.WriteLine "Invalid location."
            car
        | Some location ->
            match Car.drive location car with
            | Car.NotEnoughPetrol ->
                eff.WriteLine "You don't have enough to drive there."
                car
            | Car.Driven newCar ->
                eff.WriteLine "You arrived at your location."
                newCar
        |> gameLoop eff

[<EntryPoint>]
let main _ =
    let car = Car.init

    let consoleEff =
        { new IConsoleEff with
            member __.ReadLine() = Console.ReadLine()
            member __.WriteLine str = Console.WriteLine str }

    gameLoop consoleEff car

    0
