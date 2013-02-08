module Domain.SentenceGenerator

open Domain.Words
open System

let selectChainLinkToStartSentenceWith chainLinks =
    let punctuationWordChainLinks = List.filter (fun x -> isPunctuationWord (List.last (fst x))) chainLinks    
    match punctuationWordChainLinks with    
    | [x] -> 
        let randomIndex = (new System.Random()).Next(List.length punctuationWordChainLinks)
        punctuationWordChainLinks.Item randomIndex
    | _  -> List.head chainLinks

let sentenceIsComplete list =
    List.last list |> isPunctuationWord

let generate chainSize (text:string) =
    ""