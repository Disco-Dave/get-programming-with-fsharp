namespace Capstone3

open System

type TransactionStatus =
    | Accepted
    | Rejected of string

type Transaction<'request> =
    { Timestamp: DateTime
      Status: TransactionStatus
      Request: 'request }

[<RequireQualifiedAccess>]
module Transaction =
    let accept request =
        { Timestamp = DateTime.UtcNow
          Status = Accepted
          Request = request }

    let reject reason request =
        { Timestamp = DateTime.UtcNow
          Status = Rejected reason
          Request = request }
