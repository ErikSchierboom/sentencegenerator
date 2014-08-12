module SentenceGenerator.Domain.SentenceGenerator

    open Word
    open Parser
    open Markov
    open System

    let newSentence (x, y) = wordToTextType x = Punctuation && wordToTextType y = Normal

    let numberOfSentences (words:Words) =         
        match words with
        | [] -> 0
        | _  -> List.pairs words |> List.fold (fun acc pair -> if newSentence pair then acc + 1 else acc) 1

    let numberOfWords (words:Words) = List.filter (fun x -> wordToTextType x = Normal) words |> List.length

    let findWordToStartWith (words:Words) =     
        match words with
        | [] -> invalidArg "words" "The words parameter must not be empty."
        | _  -> List.pairs words 
                |> List.fold (fun acc pair -> if newSentence pair then acc @ [snd pair] else acc) [List.head words]
                |> List.takeRandom

    let addSeparatorsToWords (words:Words) =
        List.pairs words
        |> List.map (fun (x, y) -> if wordToTextType y = Punctuation then [x; y] else [x; SeparatorWord; y])
        |> List.mapi (fun i normalizedWords -> if i = 0 then normalizedWords else List.tail normalizedWords)
        |> List.concat

    let generateSentences chainSize maximumNumberOfWords input =
        parseWords input
        |> List.filter (fun word -> not (wordToTextType word = Separator))
        |> createChain chainSize (fun elements -> List.head elements) (fun elements _ -> numberOfWords elements >= maximumNumberOfWords)
        |> addSeparatorsToWords
        |> wordsToString