module List

open System

let slice index length list =    
    if index < 0 then raise (ArgumentException("The index parameter must be greater than or equal to zero.", "index"))
    if length < 1 then raise (ArgumentException("The length parameter must be greater than zero.", "length"))
    list |> Seq.skip index |> Seq.take length |> List.ofSeq

let rec partitionBlock blockSize list =
    if blockSize < 1 then raise (ArgumentException("The blockSize parameter must be greater than zero.", "blockSize"))
    match List.length list with
    | y when y < blockSize -> []
    | y -> slice 0 blockSize list :: partitionBlock blockSize (List.tail list)