namespace StudioDonder.SentenceGenerator.Domain

open System

module Word =

    type TextType = Normal | Punctuation | Separator

    type Character =
        | NormalCharacter of char        
        | PunctuationCharacter of char
        | SeparatorCharacter

    type Word = 
        | NormalWord of string        
        | PunctuationWord of string
        | SeparatorWord

    type Characters = Character list
    type Words = Word list

    let private punctuationCharacters = set [';'; ','; '.'; '!'; '?']
    let private separatorCharacters = set [' '; '\r'; '\t'; '\n']    

    let isPunctuationCharacter (c:char) = Set.contains c punctuationCharacters
    let isSeparatorCharacter (c:char) = Set.contains c separatorCharacters

    let characterToTextType (c:Character) =
        match c with
        | NormalCharacter _      -> Normal
        | PunctuationCharacter _ -> Punctuation
        | SeparatorCharacter     -> Separator   

    let wordToTextType (word:Word) =
        match word with
        | NormalWord _      -> Normal
        | PunctuationWord _ -> Punctuation
        | SeparatorWord     -> Separator   

    let characterToString (c:Character) =
        match c with
            | NormalCharacter x | PunctuationCharacter x -> x.ToString()
            | SeparatorCharacter -> raise (new ArgumentException("Cannot retrieve character value for SeparatorCharacter"))

    let charactersToWord (characters:Characters) =        
        match characters with       
        | [] -> raise (new ArgumentException("The characters list must not be empty."))
        | x::xs -> 
            match x with
            | NormalCharacter _      -> NormalWord (String.Join("", List.map characterToString characters))
            | PunctuationCharacter y -> PunctuationWord (characterToString x)
            | SeparatorCharacter     -> SeparatorWord

    let wordToString (word:Word) = 
        match word with
        | NormalWord x      -> x
        | PunctuationWord x -> x
        | SeparatorWord     -> " "

    let wordsToString (words:Words) =        
        match words with       
        | [] -> ""
        | _  ->        
            let numberOfWords = List.length words
            let convertWord index word = 
                match word with
                | SeparatorWord -> ""
                | PunctuationWord x -> x
                | NormalWord x -> if index = 0 then x else " " + x      
            let isNoSeparatorWord word = not (wordToTextType word = Separator)
            let wordsAsStrings = List.filter isNoSeparatorWord words
                                |> List.mapi convertWord
                                |> Array.ofList
            String.Join("", wordsAsStrings)
    
        
    