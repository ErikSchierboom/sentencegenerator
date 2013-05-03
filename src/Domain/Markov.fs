namespace StudioDonder.SentenceGenerator.Domain

open System

module Markov =

    type Element<'T> = 'T
    type ElementChain<'T> = Element<'T> list

    type ElementWithCount<'T> =
        {Element: Element<'T>
         Count: int}

    type ElementWithProbability<'T> =
        {Element: Element<'T>
         Probability: float}    

    type ChainLink<'T> = 
        {Chain: ElementChain<'T>
         Successors: ElementChain<'T>}    

    type ChainLinkWithCount<'T> = 
        {Chain: ElementChain<'T>
         Successors: ElementWithCount<'T> list}  

    type ChainLinkWithProbabilities<'T> = 
        {Chain: ElementChain<'T>
         Successors: ElementWithProbability<'T> list}

    type Chain<'T> = ChainLinkWithProbabilities<'T> list

    let elementsWithCount list =
        list     
        |> Seq.countBy id
        |> Seq.map (fun (x, y) -> { Element = x; Count = y; })
        |> List.ofSeq

    let elementsWithProbabilities list =         
        list
        |> elementsWithCount
        |> List.map (fun x -> { Element = x.Element; Probability = float x.Count / (float (List.length list)) })
        
         
(*
    let prepareListElementForProcessing list = list |> List.withSingleTailElement 
            
    let prepareListForProcessing chainSize list =    
        list
        |> List.partitionByLength chainSize 
        |> List.map prepareListElementForProcessing
            
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

        *)