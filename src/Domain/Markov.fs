namespace StudioDonder.SentenceGenerator.Domain

open System
open System.Collections.Generic
open Collections

module Markov =

    let private random = new Random()

    type ElementChain<'T> = 'T list    
    type ElementWithCount<'T> = { Element: 'T; Count: int }
    type ElementWithProbability<'T> = { Element: 'T; Probability: float }
    type ElementWithCumulativeProbability<'T> = { Element: 'T; CumulativeProbability: float }

    type ChainLink<'T> = { Chain: ElementChain<'T>; Successors: ElementChain<'T> }
    type ChainLinkWithCount<'T> = { Chain: ElementChain<'T>; SuccessorsWithCount: ElementWithCount<'T> list }
    type ChainLinkWithProbabilities<'T> = { Chain: ElementChain<'T>; SuccessorsWithProbabilities: ElementWithProbability<'T> list }
    type ChainLinkWithCumulativeProbabilities<'T> = { Chain: ElementChain<'T>; SuccessorsWithCumulativeProbabilities: ElementWithCumulativeProbability<'T> list }
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

    let pickSuccessorBasedOnProbability (successors: ElementWithCumulativeProbability<'T> list) =
        let probability = float (random.NextDouble())
        List.find (fun successor -> probability <= successor.CumulativeProbability) successors

    let createChain chainSize list first last =        
        let chainLinks = createChainLinksWithCumulativeProbability chainSize list
        let chainLinksAsDictionary = chainLinks |> List.map (fun chainLink -> chainLink.Chain, chainLink.SuccessorsWithCumulativeProbabilities) |> dict
        let currentChainQueue = new FixedSizeQueue<string>(first chainLinks)
        let rec createChainHelper index =
            if last currentChainQueue.Items index || not (chainLinksAsDictionary.ContainsKey(currentChainQueue.Items)) then
                []
            else
                let successor = pickSuccessorBasedOnProbability chainLinksAsDictionary.[currentChainQueue.Items]
                currentChainQueue.Enqueue successor.Element |> ignore
                successor.Element :: createChainHelper (index + 1)
        currentChainQueue.Items @ createChainHelper 1
