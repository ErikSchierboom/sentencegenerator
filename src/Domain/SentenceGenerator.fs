namespace StudioDonder.SentenceGenerator.Domain

open Word
open Parser
open Markov
open System

module SentenceGenerator = 

    let numberOfSentences (words:Words) =         
        List.pairs words
        |> List.fold (fun acc (x, y) -> if x.TextType = Punctuation && y.TextType = Normal then acc + 1 else acc) 1

    let findWordToStartWith (words:Words) =         
        if words.Length < 1 then failwith "The words parameter must not be empty."
        List.pairs words 
        |> List.fold (fun acc (x, y) -> if x.TextType = Punctuation && y.TextType = Normal then acc @ [y] else acc) [List.head words]
        |> List.takeRandom

    let addSeparatorsToWords (words:Words) =
        List.pairs words
        |> List.map (fun (x, y) -> if y.TextType = Punctuation then [x; y] else [x; SeparatorWord; y])
        |> List.mapi (fun i normalizedWords -> if i = 0 then normalizedWords else List.tail normalizedWords)
        |> List.concat

    let generateSentences chainSize input =
        parseWords input
        |> List.filter (fun word -> not (word.TextType = Separator))
        |> createChain chainSize (fun elements -> List.head elements) (fun _ index -> index = 10)
        |> addSeparatorsToWords
        |> wordsToString