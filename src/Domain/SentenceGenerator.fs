namespace StudioDonder.SentenceGenerator.Domain

open Word
open Parser
open Markov
open System

module SentenceGenerator = 

    let generateSentences chainSize input =
        parseWords input
        |> List.filter (fun word -> not (wordToTextType word = Separator))
        |> createChainLinksWithProbability chainSize
