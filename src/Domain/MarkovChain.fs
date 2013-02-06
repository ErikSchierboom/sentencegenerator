module Domain.MarkovChain

open System
            
let createChainLink list =    
    match List.length list with
    | length when length < 2 -> failwith "The list must have at leat two items."
    | length -> 
        let precedingItems = List.slice (length - 1) list
        let nextItem = list.Item (length - 1)
        (precedingItems, nextItem)
            
let groupListsForChain chainSize list =    
    List.partitionByLength chainSize list
    |> List.map createChainLink
            
let groupListWithCount list =    
    list
    |> Seq.map (fun value -> (value, value))
    |> Seq.groupBy fst
    |> Seq.map (fun (key, values) -> (key, Seq.length values))
    |> List.ofSeq
            
let addCountToGroupedList groupedList =    
    groupedList
    |> Seq.groupBy fst
    |> Seq.map (fun (key, values) -> (key, groupListWithCount (List.tail (List.ofSeq values))))
    |> List.ofSeq

let createMarkovChain chainSize list =
    []