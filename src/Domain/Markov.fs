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
        |> Seq.map (fun (element, count) -> { Element = element; Count = count; })
        |> List.ofSeq

    let elementsWithProbabilities list =         
        list
        |> elementsWithCount
        |> List.map (fun elementWithCount -> { Element = elementWithCount.Element; Probability = float elementWithCount.Count / (float (List.length list)) })
            
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
        |> List.map (fun chainLink -> 
                    { Chain = chainLink.Chain; 
                      SuccessorsWithCount = Seq.countBy id chainLink.Successors
                                            |> Seq.map (fun (successor, count) -> { Element = successor; Count = count })
                                            |> List.ofSeq 
                    })

    let createChainLinksWithProbability chainSize list =    
        list
        |> createChainLinksWithCount chainSize     
        |> List.map (fun chainLink -> 
                    { Chain = chainLink.Chain; 
                      SuccessorsWithProbabilities = let totalCount = List.sumBy (fun x -> float x.Count) chainLink.SuccessorsWithCount
                                                    List.map (fun (c:ElementWithCount<'T>) -> { Element = c.Element; Probability = float c.Count / totalCount }) chainLink.SuccessorsWithCount                                                    
                    })