namespace StudioDonder.SentenceGenerator.Domain

open System

module Markov =

    let private random = new Random()

    type Element<'T> = 'T
    type ElementChain<'T> = Element<'T> list    
    type ElementWithCount<'T> = { Element: Element<'T>; Count: int }
    type ElementWithProbability<'T> = { Element: Element<'T>; Probability: float }
    type ElementWithCumulativeProbability<'T> = { Element: Element<'T>; CumulativeProbability: float }

    type ChainLink<'T> = {Chain: ElementChain<'T>; Successors: ElementChain<'T> }
    type ChainLinkWithCount<'T> = {Chain: ElementChain<'T>; SuccessorsWithCount: ElementWithCount<'T> list }
    type ChainLinkWithProbabilities<'T> = {Chain: ElementChain<'T>; SuccessorsWithProbabilities: ElementWithProbability<'T> list }
    type ChainLinkWithCumulativeProbabilities<'T> = {Chain: ElementChain<'T>; SuccessorsWithCumulativeProbabilities: ElementWithCumulativeProbability<'T> list }
    type Chain<'T> = ChainLinkWithProbabilities<'T> list

    let elementsWithCount list =
        list     
        |> Seq.countBy id
        |> Seq.map (fun (element, count) -> { Element = element; Count = count; })
        |> List.ofSeq

    let elementsWithProbabilities list =
        list
        |> elementsWithCount
        |> List.map (fun elementWithCount -> { Element = elementWithCount.Element; Probability = float elementWithCount.Count / (float (List.length list)) })

    let elementsWithCumulativeProbabilities list =         
        let cumulativeProbability = ref (float 0.0)
        list
        |> elementsWithProbabilities
        |> List.map (fun elementWithProbability -> 
                         cumulativeProbability := !cumulativeProbability + elementWithProbability.Probability
                         { Element = elementWithProbability.Element; CumulativeProbability = !cumulativeProbability })
            
    let createChainLinks chainSize list =    
        list
        |> List.partitionByLength (chainSize + 1)
        |> List.map List.withSingleTailElement 
        |> Seq.groupBy fst
        |> Seq.map (fun (chain, successor) -> { Chain = chain; Successors = Seq.map snd successor |> List.ofSeq })
        |> List.ofSeq

    let createChainLinksWithCount chainSize list =    
        list
        |> createChainLinks chainSize     
        |> List.map (fun chainLink -> { Chain = chainLink.Chain; SuccessorsWithCount = elementsWithCount chainLink.Successors })

    let createChainLinksWithProbability chainSize list =    
        list
        |> createChainLinks chainSize     
        |> List.map (fun chainLink ->  { Chain = chainLink.Chain; SuccessorsWithProbabilities = elementsWithProbabilities chainLink.Successors })

    let createChainLinksWithCumulativeProbability chainSize list =    
        list
        |> createChainLinks chainSize     
        |> List.map (fun chainLink ->  { Chain = chainLink.Chain; SuccessorsWithCumulativeProbabilities = elementsWithCumulativeProbabilities chainLink.Successors })

    let pickSuccessorBasedOnProbability (chainLink:ChainLinkWithCumulativeProbabilities<'T>) =
        let probability = float (random.NextDouble())
        List.find (fun successor -> probability <= successor.CumulativeProbability) chainLink.SuccessorsWithCumulativeProbabilities