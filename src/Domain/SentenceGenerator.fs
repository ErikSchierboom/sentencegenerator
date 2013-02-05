namespace Domain

open System
(*
module SentenceGenerator =

    let partitionBlock blockSize list =
        if blockSize < 1 then raise (ArgumentException("The blockSize parameter must be greater than zero.", "blockSize"))

        match list with
        | []     -> []
        | [x]    -> []
        | [x:xs] -> 
            match List.length xs with
            | y when y < blockSize -> []
            | _ ->


    let rec partitionBlocks x y =
        y

    let rec groupWords x =
        match x with
        | []    -> []        
        | [y]   -> []
        | y::ys -> (y, List.head ys) :: groupWords ys

    let groupWordsInMap x =
        x |> groupWords |> Seq.groupBy fst

    
        *)