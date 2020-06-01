module CapstoneUnit2.Game

type EventResult =
    | GameOver of string
    | UserError of string
    | Advance of Car

let handleEvent location car =
    let driveResult =
        Location.fromString location
        |> Option.map (fun l -> Car.drive l car)
     
    match driveResult with
    | None ->
        UserError "Unrecognized location."
    | Some (Error AlreadyAtLocation) ->
        UserError "The car is already at this location."
    | Some (Error NotEnoughPetrol) ->
        UserError "The car does not have enough petrol to reach this location."
    | Some (Error OutOfPetrol) ->
        GameOver "The car has ran out of petrol."
    | Some (Ok newCar) ->
        Advance newCar

type IConsole =
    abstract member Write : string -> unit
    abstract member WriteLine : string -> unit
    abstract member ReadLine : unit -> string

let start (console: IConsole) =
    let initialCar = Car.init

    let rec loop car = 
        console.Write("Enter destination: ")
        let location = console.ReadLine()
        console.WriteLine(sprintf "Trying to drive to %s" location)
        match handleEvent location car with
        | GameOver msg ->
            console.WriteLine msg
        | UserError msg ->
            console.WriteLine msg
            loop car
        | Advance car ->
            let currentLocation =
                match car.Location with
                | None -> "no where"
                | Some l -> l.ToString()
            console.WriteLine (sprintf "Made it to %s! You have %d petrol left" currentLocation car.RemainingPetrol)
            loop car

    loop initialCar
