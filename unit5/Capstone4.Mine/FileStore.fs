module Capstone4.FileStore

open System.IO
open Capstone4
open Thoth.Json.Net

module private Encode =
    let account ratedAccount = 
        let unratedAccount = Account.account ratedAccount
        Encode.object
            [ "customer", Encode.string (Customer.unwrap unratedAccount.Customer)
              "balance", Encode.decimal unratedAccount.Balance ]

module private Decode =
    let private customer =
        Decode.string
        |> Decode.andThen (fun customer ->
            match Customer.make customer with
            | Ok c -> Decode.succeed c
            | Error Customer.Error.IsEmpty -> Decode.fail "Customer is empty")

    let account =
        Decode.map2 (fun c b -> { Customer = c; Balance = b })
            (Decode.field "customer" customer)
            (Decode.field "balance" Decode.decimal)
        |> Decode.map Account.rateAccount

let private getLocation customer =
    sprintf ".store/%s" (Customer.unwrap customer)

let retrieve customer =
    let location = getLocation customer

    if File.Exists location then
        File.ReadAllText location
        |> Decode.fromString Decode.account
        |> function
            | Ok a -> Some a
            | Error _ -> None
    else
        None

let save account =
    Directory.CreateDirectory ".store" |> ignore
    use sw = 
        getLocation (Account.account account).Customer
        |> File.CreateText
    Encode.toString 4 (Encode.account account)
    |> sw.Write
