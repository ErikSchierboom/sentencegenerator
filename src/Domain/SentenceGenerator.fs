namespace StudioDonder.SentenceGenerator.Domain

open Word
open Parser
open Markov
open System

module SentenceGenerator = 

    let addSeparatorsToWords (words:Words) =
        List.pairs words
        |> List.map (fun (x, y) -> if y.TextType = Punctuation then [x; y] else [x; SeparatorWord; y])
        |> List.mapi (fun i normalizedWords -> if i = 0 then normalizedWords else List.tail normalizedWords)
        |> List.concat

    let generateSentences chainSize input =
        parseWords input
        |> List.filter (fun word -> not (word.TextType = Separator))
        |> createChain chainSize (fun chainLinks -> List.head chainLinks) (fun _ index -> index = 10)
        |> addSeparatorsToWords
        |> wordsToString