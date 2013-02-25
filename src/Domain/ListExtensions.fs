module List

open System

let random = new Random()

let rec last = function    
    | x :: [] -> x
    | x :: xs -> last xs    
    | _ -> failwith "Cannot call List.last on empty list."

let take length list =    
    if length < 1 then failwith "The length parameter must be greater than zero."
    list |> Seq.take length |> List.ofSeq

let takeRandom list =    
    match list with
    | [] -> failwith "Cannot call List.takeRandom on empty list."
    | x -> 
        let index = random.Next(List.length list)
        list.Item index    

let rec partitionByLength length list =
    if length < 1 then failwith "The length parameter must be greater than zero."
    match List.length list with
    | x when x < length -> []
    | x -> take length list :: partitionByLength length (List.tail list)