module List

open System

let rec last = function    
    | x :: [] -> x
    | x :: xs -> last xs    
    | _ -> failwith "Cannot call List.last on empty list."

let slice length list =    
    if length < 1 then failwith "The length parameter must be greater than zero."
    list |> Seq.take length |> List.ofSeq

let rec partitionByLength length list =
    if length < 1 then failwith "The length parameter must be greater than zero."
    match List.length list with
    | x when x < length -> []
    | x -> slice length list :: partitionByLength length (List.tail list)