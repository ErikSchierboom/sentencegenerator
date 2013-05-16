module List

    open System

    let private random = new Random()

    let last list = 
        match list with
        | [] -> invalidArg "list" "Cannot call List.last on empty list."
        | _  -> List.nth list (List.length list - 1)        
    
    let take length list =    
        match length with
        | x when x < 1 -> invalidArg "length" "The length parameter must be greater than zero."
        | _ -> list |> Seq.take length |> List.ofSeq
    
    let takeRandom (list:'T list) = 
        match list with
        | [] -> invalidArg "list" "The list must not be empty."
        | _  -> List.nth list (random.Next(list.Length))

    let rec partitionByLength length (list:'T list)  =
        match length with
        | x when x < 1 -> invalidArg "length" "The length parameter must be greater than zero."
        | x when x > List.length list -> []
        | _ -> take length list :: partitionByLength length list.Tail

    let withSingleTailElement (list:'T list) =    
        match List.length list with
        | length when length < 2 -> invalidArg "list" "The list must have at least two items."
        | length -> take (length - 1) list, last list

    let pairs (list:'T list) =    
        match list.Length with
        | length when length < 2 -> []
        | length -> List.init (length - 1) (fun i -> List.nth list i, List.nth list (i + 1))