namespace Domain

open System

module Markov =

    let random = new Random()

    let prepareListElementForProcessing list = list |> List.withSingleTailElement 
            
    let prepareListForProcessing chainSize list =    
        list
        |> List.partitionByLength chainSize 
        |> List.map prepareListElementForProcessing

    let elementsWithCount list =
        list
        |> Seq.countBy id
        |> List.ofSeq

    let elementsWithProbabilities list =         
        list
        |> elementsWithCount
        |> List.map (fun (x,y) -> (x, float y))
        |> List.map (fun (x,y) -> (x, y / (float (List.length list))))
            
    let groupNextStates list =    
        list
        |> Seq.groupBy fst
        |> Seq.map (fun (key, values) -> (key, Seq.map snd values |> List.ofSeq))
        |> List.ofSeq

    let createMarkovChain chainSize list =
        list 
        |> prepareListForProcessing chainSize
        |> groupNextStates
        |> Seq.map (fun (key, values) -> (key, elementsWithProbabilities values))
        |> List.ofSeq