module Unit4

type SoccerResult =
    { HomeTeam: string
      AwayTeam: string
      HomeGoals: int
      AwayGoals: int }

let results =
    [ { HomeTeam = "Missiville"
        AwayTeam = "Ronaldo City"
        HomeGoals = 1
        AwayGoals = 2 }
      { HomeTeam = "Missiville"
        AwayTeam = "Bale Town"
        HomeGoals = 1
        AwayGoals = 3 }
      { HomeTeam = "Bale Town"
        AwayTeam = "Ronaldo City"
        HomeGoals = 3
        AwayGoals = 2 }
      { HomeTeam = "Bale Town"
        AwayTeam = "Messiville"
        HomeGoals = 2
        AwayGoals = 1 }
      { HomeTeam = "Ronaldo City"
        AwayTeam = "Messiville"
        HomeGoals = 4
        AwayGoals = 2 }
      { HomeTeam = "Ronaldo City"
        AwayTeam = "Bale Town"
        HomeGoals = 1
        AwayGoals = 2 } ]

// Write a function to pick the team who won the most away games in the season
let mostAwayGamesWon soccerResults =
    let awayWins =
        soccerResults
        |> Seq.filter(fun { HomeGoals = hg; AwayGoals = ag } -> ag > hg)
        |> Seq.countBy(fun { AwayTeam = at } -> at)
    if Seq.isEmpty awayWins then
        None
    else
        awayWins
        |> Seq.maxBy snd
        |> fst
        |> Some

mostAwayGamesWon results = Some "Bale Town"


(*
    Write a function that when given a file path returns a list of records containing the
    folder name, size, number of files, average file size, and distinct set of file extensions within
    the folder sorted by folder size.

    You don't need to recursively search the directory.
    Hint: Using DirectoryInfo and FileInfo make this pretty easy.
*)

open System.IO

type FolderSummary =
    { Name: string
      Size: int64
      NumberOfFiles: int
      AverageFileSize: decimal
      FileExtensions: Set<string> }

let getFolderSummary(folder: string) =
    let summarize(directoryInfo: DirectoryInfo) =
        let files = directoryInfo.GetFiles()
        let getFileLength(file: FileInfo) = file.Length
        let getFileExtension(file: FileInfo) = file.Extension
        { Name = directoryInfo.Name
          Size = Array.sumBy getFileLength files
          NumberOfFiles = Array.length files
          FileExtensions = Array.map getFileExtension files |> Set.ofArray
          AverageFileSize =
              if Array.isEmpty files
              then 0M
              else Array.averageBy (getFileLength >> decimal) files }
    (DirectoryInfo folder).GetDirectories()
    |> Array.map summarize
    |> Array.sortByDescending(fun summary -> summary.Size)

open System

Environment.GetEnvironmentVariable "HOME" |> getFolderSummary




(* Create a function to calculate the length of a list without using `List.length` *)

let listLength xs =
    let rec go n xs' =
        match xs' with
        | [] -> n
        | _ :: t -> go (n + 1) t
    go 0 xs

let listLength' xs = List.fold (fun n _ -> n + 1) 0 xs

listLength [ 1 .. 10 ] = 10
listLength' [ 1 .. 10 ] = 10
listLength [] = 0
listLength' [] = 0


(* Create a function to calculate the max value of a list without using `List.max` *)

let listMax xs =
    let rec go currentMax xs' =
        match xs' with
        | [] -> currentMax
        | h :: t when h > currentMax -> go h t
        | _ :: t -> go currentMax t

    match xs with
    | [] -> None
    | h :: t -> Some <| go h t

let listMax' = function
    | [] -> None
    | h :: t ->
        let testMaxValue currentMax currentValue =
            if currentValue > currentMax then
                currentValue
            else
                currentMax
        Some <| List.fold testMaxValue h t

(* Implement fold for lists using tail recursion *)
let rec fold (f: 'state -> 'a -> 'state) (state: 'state) (xs: 'a list): 'state = 
    match xs with
    | [] -> state
    | h :: t -> fold f (f state h) t

let rec foldS f state xs =
    match Seq.tryHead xs with
    | None -> state
    | Some h -> foldS f (f state h) (Seq.tail xs)


let validate rules convert input =
    ([], rules)
    ||> Seq.fold (fun errors (isValid, msg) ->
            if isValid input then errors else msg :: errors)
    |> function
    | [] -> Ok (convert input)
    | errors -> Error errors
