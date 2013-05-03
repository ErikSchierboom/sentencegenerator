namespace StudioDonder.SentenceGenerator.Domain

open System

module Word =

    type CharacterType = Normal | Punctuation | Separator

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

    let characterToCharacterType (c:Character) =
        match c with
        | NormalCharacter _      -> Normal
        | PunctuationCharacter _ -> Punctuation
        | SeparatorCharacter     -> Separator   

    let isPunctuationCharacter (c:char) = Set.contains c punctuationCharacters
    let isSeparatorCharacter (c:char) = Set.contains c separatorCharacters

    let characterToString (c:Character) =
        match c with
            | NormalCharacter x | PunctuationCharacter x -> x.ToString()
            | SeparatorCharacter -> raise (new ArgumentException("Cannot retrieve character value for SeparatorCharacter"))

    
        
    