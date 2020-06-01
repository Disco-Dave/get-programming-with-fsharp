namespace CapstoneUnit2

type DestinationInfo =
    { PetrolReqiured: int
      PetrolGained: int }

[<RequireQualifiedAccess>]
module DestinationInfo =
    let fromLocation location =
        match location with
        | Home ->
            { PetrolReqiured = 25
              PetrolGained = 0 }
        | Stadium ->
            { PetrolReqiured = 25
              PetrolGained = 0 }
        | Office ->
            { PetrolReqiured = 50
              PetrolGained = 0 }
        | GasStation ->
            { PetrolReqiured = 10
              PetrolGained = 50 }
