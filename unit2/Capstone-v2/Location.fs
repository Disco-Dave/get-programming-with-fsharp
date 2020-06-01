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
        | Stadium -> "stadiuam"
        | Office -> "office"
        | GasStation -> "gas station"
        

[<RequireQualifiedAccess>]
module Location =
    let fromString str =
        let str =
            if String.IsNullOrWhiteSpace(str)
                then ""
                else str.Trim().ToLower()
        match str with
        | "home" -> Some Home
        | "stadium" -> Some Stadium
        | "office" -> Some Office
        | "gas station" -> Some GasStation
        | _ -> None
