module Capstone3.TransactionLog

open System
open System.IO
open Capstone3.Audit
open Thoth.Json.Net

module private Decode =
    let status =
        Decode.field "$tag" Decode.string
        |> Decode.andThen (function
            | "accepted" -> Decode.succeed Accepted
            | "rejected" -> 
                Decode.field "reason" Decode.string
                |> Decode.map Rejected
            | _ -> Decode.fail "Invalid $tag")

    let request =
        let decodeTag = 
            Decode.field "$tag" Decode.string
            |> Decode.andThen(function
                | "withdraw" -> Decode.succeed Withdraw
                | "deposit" -> Decode.succeed Deposit
                | _ -> Decode.fail "Invalid $tag")
        let decodeAmount = Decode.field "amount" Decode.decimal
        Decode.map2 (<|) decodeTag decodeAmount

    let transaction =
        Decode.map3 (fun t s r -> { Timestamp = t; Status = s; Request = r}) 
            (Decode.field "timestamp" Decode.datetime)
            (Decode.field "status" status)
            (Decode.field "request" request)

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

let private fileName env customer =
    let dataHome = ".store"
    FileSystem.createDirectory env dataHome |> ignore
    sprintf "%s/%s" dataHome (Customer.toString customer)
            
let getTransactionLog env customer =
    let fileName = fileName env customer

    if FileSystem.doesFileExist env fileName then
        let collectOk r xs = 
            match r with
            | Ok o -> o :: xs
            | _ -> xs

        let flip f a b = f b a

        FileSystem.readLines env fileName
        |> Seq.map (Decode.fromString Decode.transaction)
        |> flip (Seq.foldBack collectOk) []
    else
        FileSystem.createFile env fileName
        []

let log env {Transaction = transaction; Customer = customer} =
    Encode.transaction transaction
    |> Encode.toString 0
    |> FileSystem.appendText env (fileName env customer)
