namespace CapstoneUnit2

open System

type Location =
    | Home
    | Stadium
    | Office
    | GasStation

    override this.ToString() =
        match this with
        | Home -> "home"
        | Stadium -> "stadium"
        | Office -> "office"
        | GasStation -> "gas station"


[<RequireQualifiedAccess>]
module Location =
    let fromString location =
        let location = 
            Option.ofObj location 
            |> Option.map(fun (s: string) -> s.ToLower().Trim())

        match location with
        | Some "home" -> Some Home
        | Some "stadium" -> Some Stadium
        | Some "office" -> Some Office
        | Some "gas station" -> Some GasStation
        | _ -> None
