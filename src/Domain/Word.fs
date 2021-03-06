﻿module SentenceGenerator.Domain.Word

    open System
    open Strings

    type TextType = Normal | Punctuation | Separator

    type Character =
        | NormalCharacter of char        
        | PunctuationCharacter of char
        | SeparatorCharacter

        override self.ToString() =
            match self with
            | NormalCharacter x | PunctuationCharacter x -> x.ToString()
            | SeparatorCharacter -> " "

    type Word = 
        | NormalWord of string        
        | PunctuationWord of string
        | SeparatorWord

        override self.ToString() =
            match self with
            | NormalWord x | PunctuationWord x -> x
            | SeparatorWord -> " "

    type Characters = Character list
    type Words = Word list

    let private punctuationCharacters = set [';'; ','; '.'; '!'; '?']
    let private separatorCharacters = set [' '; '\r'; '\t'; '\n']    

    let isPunctuationCharacter (c:char) = Set.contains c punctuationCharacters
    let isSeparatorCharacter (c:char) = Set.contains c separatorCharacters

    let characterToTextType (character:Character) = 
        match character with
        | NormalCharacter _      -> Normal
        | PunctuationCharacter _ -> Punctuation
        | SeparatorCharacter     -> Separator   

    let characterToString (character:Character) = character.ToString()

    let charactersToString (characters:Characters) = List.map characterToString characters |> concat
    
    let charactersToWord (characters:Characters) =        
        match characters with       
        | [] -> invalidArg "characters" "The characters list must not be empty."
        | x::xs -> 
            match x with
            | NormalCharacter _      -> NormalWord (charactersToString characters)
            | PunctuationCharacter y -> PunctuationWord (characterToString x)
            | SeparatorCharacter     -> SeparatorWord

    let wordToTextType (word:Word) = 
        match word with
        | NormalWord _      -> Normal
        | PunctuationWord _ -> Punctuation
        | SeparatorWord     -> Separator   

    let wordToString (word:Word) = word.ToString()
    
    let wordsToString (words:Words) = List.map wordToString words |> concat
    
        
    