namespace StudioDonder.SentenceGenerator.Domain

open StudioDonder.SentenceGenerator.Domain.Word
open System

module Sentence =

    type Sentence = Word list 
    type Sentences = Sentence list   
            
    let parse (words:Words) : Sentences =
        []
    