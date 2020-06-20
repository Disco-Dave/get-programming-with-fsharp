namespace Capstone3

open System

type Customer = private | Customer of string

[<RequireQualifiedAccess>]
module Customer =
    type Error = IsEmpty

    let make name =
        if String.IsNullOrWhiteSpace name then
            Error IsEmpty
        else
            name.Trim()
            |> Customer
            |> Ok

    let toString (Customer customer) = customer
