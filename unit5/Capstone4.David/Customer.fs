namespace Capstone4

type Customer = private | Customer of string

module Customer =
    let unwrap (Customer customer) = customer

    type Error = IsEmpty

    let private (|Trim|) (str: string) =
        Option.ofObj str
        |> Option.map (fun (s: string) -> s.Trim())
        |> Option.defaultWith (fun _ -> "")

    let make (Trim customer) =
        match customer with
        | "" -> Error IsEmpty
        | c -> Ok <| Customer c
