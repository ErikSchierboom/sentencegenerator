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
         SuccessorsWithCount: ElementWithCount<'T> list}  

    type ChainLinkWithProbabilities<'T> = 
        {Chain: ElementChain<'T>
         SuccessorsWithProbabilities: ElementWithProbability<'T> list}

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
            
    let createChainLinks chainSize list =    
        list
        |> List.partitionByLength (chainSize + 1)
        |> List.map List.withSingleTailElement 
        |> Seq.groupBy fst
        |> Seq.map (fun (chain, successor) -> { ChainLink.Chain = chain; ChainLink.Successors = Seq.map snd successor |> List.ofSeq })
        |> List.ofSeq

    let createChainLinksWithCount chainSize list =    
        list
        |> createChainLinks chainSize       
