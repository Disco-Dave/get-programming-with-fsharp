module Car

open System

//TODO: Create helper functions to provide the building blocks to implement driveTo.

let getRequiredPetrol (destination: string) =
    let destination' = destination.ToUpper().Trim()
    if destination' = "HOME"  then
        25
    elif destination' = "OFFICE" then
        50
    elif destination' = "STADIUM" then
        25
    elif destination' = "GAS STATION" then
        10
    else
        failwith "Unknown destination"

let getGainedPetrol (destination: string) =
    let destination' = destination.ToUpper().Trim()
    if destination' = "GAS STATION"  then
        50
    else
        0        

/// Drives to a given destination given a starting amount of petrol
let driveTo (petrol: int, destination: string): int = 
    let requiredPetrol = getRequiredPetrol destination
    let petrolGained = getGainedPetrol destination
    if petrol < requiredPetrol then
        failwith "We don't have enough petrol"
    else
        petrol - requiredPetrol + petrolGained

