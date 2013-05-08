module List

open System

let private random = new Random()

let rec last = function    
    | x :: [] -> x
    | x :: xs -> last xs    
    | _ -> failwith "Cannot call List.last on empty list."
    
let take length list =    
    if length < 1 then failwith "The length parameter must be greater than zero."
    list |> Seq.take length |> List.ofSeq
    
let takeRandom (list:'T list) = 
    if list.IsEmpty then failwith "The list must not be empty."
    List.nth list (random.Next(list.Length))

let rec partitionByLength length (list:'T list)  =
    if length < 1 then failwith "The length parameter must be greater than zero."
    match list.Length with
    | x when x < length -> []
    | x -> take length list :: partitionByLength length list.Tail

let withSingleTailElement (list:'T list) =    
    match list.Length with
    | length when length < 2 -> failwith "The list must have at least two items."
    | length -> take (length - 1) list, list.Item (length - 1)

let pairs (list:'T list) =    
    match list.Length with
    | length when length < 2 -> []
    | length -> List.init (length - 1) (fun i -> List.nth list i, List.nth list (i + 1))