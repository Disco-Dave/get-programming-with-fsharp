module Capstone3.FileStore

open System
open System.IO
open Capstone3.Audit
open Thoth.Json.Net

module private Decode =
    let status =
        Decode.object <| fun get ->
            let tag = get.Required.Field "$tag" Decode.string
            match tag with
            | "accepted" -> Accepted
            | "rejected" ->
                let reason = get.Required.Field "reason" Decode.string
                Rejected reason
            | _ -> failwith "Invalid $tag." 

    let request =
        Decode.object <| fun get ->
            let tag = get.Required.Field "$tag" Decode.string
            let request = 
                match tag with
                | "withdraw" -> Withdraw
                | "deposit" -> Deposit
                | _ -> failwith "Invalid $tag."
            let amount = get.Required.Field "amount" Decode.decimal
            request amount

    let transaction =
        Decode.object <| fun get ->
            { Timestamp = get.Required.Field "timestamp" Decode.datetime
              Status = get.Required.Field "status" status
              Request = get.Required.Field "request" request }

module private Encode =
    let status = function
        | Accepted -> Encode.object ["$tag", Encode.string "accepted"]
        | Rejected reason ->
            Encode.object
                [ "$tag", Encode.string "rejected"
                  "reason", Encode.string reason ]

    let request = function
        | Withdraw amount ->
            Encode.object
                [ "$tag", Encode.string "withdraw"
                  "amount", Encode.decimal amount ]
        | Deposit amount ->
            Encode.object
                [ "$tag", Encode.string "deposit"
                  "amount", Encode.decimal amount ]

    let transaction tran =
        Encode.object
            [ "timestamp", Encode.datetime tran.Timestamp
              "status", status tran.Status
              "request", request tran.Request ]

let private fileName customer =
    let dataHome = ".store"
    Directory.CreateDirectory dataHome |> ignore
    sprintf "%s/%s" dataHome (Customer.toString customer)
            
let getTransactionLog customer =
    let fileName = fileName customer

    if File.Exists fileName then
        let collectOk r xs = 
            match r with
            | Ok o -> o :: xs
            | _ -> xs

        let flip f a b = f b a

        File.ReadLines fileName
        |> Seq.map (Decode.fromString Decode.transaction)
        |> flip (Seq.foldBack collectOk) []
    else
        use _f = File.Create fileName
        []

let log {Transaction = transaction; Customer = customer} =
    use sw = File.AppendText(fileName customer)
    Encode.transaction transaction
    |> Encode.toString 0
    |> sw.WriteLine
