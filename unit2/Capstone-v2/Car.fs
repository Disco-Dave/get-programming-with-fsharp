namespace CapstoneUnit2

type Car =
    { Location: Location option
      RemainingPetrol: int }

type DriveError =
    | AlreadyAtLocation
    | NotEnoughPetrol
    | OutOfPetrol

[<RequireQualifiedAccess>]
module Car =
    let init =
        { Location = None
          RemainingPetrol = 100 }

    let drive location car =
        let isAlreadyAtLocation =
            car.Location
            |> Option.map((=) location)
            |> Option.defaultWith(fun () -> false)

        let destinationInfo = DestinationInfo.fromLocation location
        let hasEnoughPetrol = car.RemainingPetrol >= destinationInfo.PetrolReqiured

        if isAlreadyAtLocation then
            Error AlreadyAtLocation
        elif not hasEnoughPetrol then
            Error NotEnoughPetrol
        else
            let newPetrol = car.RemainingPetrol - destinationInfo.PetrolReqiured + destinationInfo.PetrolGained
            if newPetrol <= 0 then
                Error OutOfPetrol
            else
                Ok
                    { RemainingPetrol = newPetrol
                      Location = Some location }

