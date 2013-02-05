module Domain.MarkovChain

open System
            
let groupListForChain list =    
    match List.length list with
    | length when length < 2 -> raise (ArgumentException("The list must have at leat two items.", "list"))
    | length -> 
        let precedingItems = List.slice 0 (length - 1) list
        let nextItem = list.Item (length - 1)
        (precedingItems, nextItem)
            
let groupListsForChain chainSize list =    
    List.partitionBlock chainSize list
    |> List.map groupListForChain

let createMarkovChain list =
    list