module Domain.MarkovChain

open System

let random = new Random()
            
let createChainLink list =    
    match List.length list with
    | length when length < 2 -> failwith "The list must have at least two items."
    | length -> List.take (length - 1) list, list.Item (length - 1)
            
let createChainLinks chainSize list =    
    List.partitionByLength chainSize list
    |> List.map createChainLink
            
let groupChainLinks chainLinks =    
    chainLinks
    |> Seq.groupBy fst
    |> Seq.map (fun (key, values) -> (key, Seq.map snd values |> List.ofSeq))
    |> List.ofSeq

let convertCountsToProbabilityIndexes list =
    list 
    |> List.fold (fun acc elem -> (fst acc + snd elem, snd acc @ [fst elem, fst acc])) (0, []) 
    |> snd

let createMarkovChain chainSize list =
    list 
    |> createChainLinks chainSize
    |> groupChainLinks
    |> Seq.map (fun (key, values) -> (key, Seq.countBy id values |> List.ofSeq))
    |> List.ofSeq

let createMarkovChainWithPropabilityIndexes chainSize list =
    createMarkovChain chainSize list
    |> List.map (fun (chainedElements, nextElements) -> (chainedElements, convertCountsToProbabilityIndexes nextElements))