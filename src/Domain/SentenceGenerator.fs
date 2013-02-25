module Domain.SentenceGenerator

open Domain.Word
open System

let selectChainLinkToStartSentenceWith chainLinks =
    let punctuationWordChainLinks = List.filter (fun x -> isPunctuation (List.last (fst x))) chainLinks    
    match punctuationWordChainLinks with    
    | [x] -> 
        let randomIndex = (new System.Random()).Next(List.length punctuationWordChainLinks)
        punctuationWordChainLinks.Item randomIndex
    | _  -> List.head chainLinks

let sentenceIsComplete list =
    List.last list |> isPunctuation

let generate chainSize (text:string) =
    ""