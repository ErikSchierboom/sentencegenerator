namespace StudioDonder.SentenceGenerator.Domain

open Word
open Parser
open Markov
open System

module SentenceGenerator = 

    let generateSentences chainSize input =
        let parsedWords = parseWords input
        "" 
