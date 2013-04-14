namespace StudioDonder.SentenceGenerator.Domain

open StudioDonder.SentenceGenerator.Domain.Word
open StudioDonder.SentenceGenerator.Domain.Markov
open System

module SentenceGenerator = 

    let selectChainLinkToStartSentenceWith chainLinks =
        let punctuationWordChainLinks = List.filter (fun x -> isPunctuation (List.last (fst x))) chainLinks    
        match punctuationWordChainLinks with    
        | [x] -> 
            let randomIndex = (new System.Random()).Next(List.length punctuationWordChainLinks)
            punctuationWordChainLinks.Item randomIndex
        | _  -> List.head chainLinks

    let sentenceIsComplete list =
        List.last list |> isPunctuation

    let chainLinksToSentence chainLinks =    
        let (words:string list) = List.collect id chainLinks
        let sentence = List.fold (fun acc word -> if isPunctuation word then acc + word else acc + " " + word) "" words
        sentence.Substring(1)

    let generate chainSize (text:string) =
        let chainLinks = createMarkovChain chainSize (parse text)    
        let startChainLink = chainLinks
        ""
