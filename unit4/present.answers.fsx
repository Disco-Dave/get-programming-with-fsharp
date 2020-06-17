module Unit4

// HINT: https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/loops-for-in-expression#remarks
(* Declare function upto: int -> int list such that upto n = [1; 2; . . . ; n]. *)
let upto n = [ 1 .. n ]

(* Declare function downto1: int -> int list such that the value of downto1 n is the list [n; n − 1; . . . ; 1] *)
let downto1 n = [ n .. -1 .. 1 ]

(* Declare function evenN: int -> int list such that evenN n generates the list of the first n non-negative even numbers. *)
let evenN n = [ 2 .. 2 .. n ]

(* Declare an F# function rmodd removing the odd-numbered elements from a list *)
let rmodd xs =
    xs
    |> List.mapi(fun i v -> i % 2 = 0, v)
    |> List.filter fst
    |> List.map snd


(* Declare a function called length that gets the length of a list without using List.length *)
let lenghtV1 xs =
    let rec go n =
        function
        | [] -> n
        | _ :: t -> go (n + 1) t
    go 0 xs

let lengthV2 xs = List.fold (fun n _ -> n + 1) 0 xs

(* Delcare a function called reverse that reverses a list without using List.reverse *)
let reverseV1 xs =
    let rec go r =
        function
        | [] -> r
        | h :: t -> go (h :: r) t
    go [] xs

let reverse2 xs = List.fold (fun r h -> h :: r) [] xs


(* Delcare a function that calculates the min, max, and average of a sequence *)
type Summary =
    { Max: double
      Min: double
      Average: double }

let summarize xs =
    { Max = Seq.max xs
      Min = Seq.min xs
      Average = Seq.average xs }

(* Write a function that takes a word and returns a map with the letter as keys and its frequency as the value. string -> Map<char, int> *)
let letterCount word = Seq.countBy id word |> Map.ofSeq


(*
    Given students' names along with the grade that they are in, create a roster for the school.

    In the end, you should be able to:

        Add a student's name to the roster for a grade
            "Add Jim to grade 2."
            "OK."
        Get a list of all students enrolled in a grade
            "Which students are in grade 2?"
            "We've only got Jim just now."
        Get a sorted list of all students in all grades. Grades should sort as 1, 2, 3, etc., and students within a grade should be sorted alphabetically by name.
            "Who all is enrolled in school right now?"
            "Grade 1: Anna, Barb, and Charlie. Grade 2: Alex, Peter, and Zoe. Grade 3…"

    Note that all our students only have one name. (It's a small town, what do you want?)
 *)
type School = Map<int, string list>

module School =
    let grade (number: int) (school: School): string list = 
        Map.tryFind number school 
        |> Option.defaultWith(fun () -> List.empty)

    let add (student: string) (grade': int) (school: School): School =
        let students = grade grade' school
        Map.add grade' (student :: students) school

    let roster (school: School): string list = 
        Map.toSeq school
        |> Seq.sortBy fst
        |> Seq.collect (snd >> List.sort)
        |> Seq.toList

