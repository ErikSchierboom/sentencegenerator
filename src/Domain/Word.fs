namespace StudioDonder.SentenceGenerator.Domain

open System

module Word =

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
        
    type private CharacterType = Normal | Punctuation | Separator | None

    let private punctuationCharacters = set [';'; ','; '.'; '!'; '?']
    let private separatorCharacters = set [' '; '\r'; '\t'; '\n']    

    let private characterType (c:Character) =
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

    let groupCharacters (characters:Characters) =        
        let key = ref 0
        let lastCharacterType = ref None
        characters
        |> Seq.groupBy (fun c ->
                if characterType c = lastCharacterType.Value then
                    key
                else
                    incr key
                    lastCharacterType := characterType c
                    key)
        |> Seq.map (snd >> List.ofSeq)
        |> List.ofSeq
    
    let groupedCharactersToWord (characters:Characters) =        
        match characters with       
        | [] -> raise (new ArgumentException("The characters list must not be empty."))
        | x::xs -> 
            match x with
            | NormalCharacter _ -> NormalWord (String.Join("", List.map characterToString characters))
            | PunctuationCharacter y -> PunctuationWord (characterToString x)
            | SeparatorCharacter -> SeparatorWord

    let parseCharacter (c: char) : Character =
        if isPunctuationCharacter c then PunctuationCharacter c
        elif isSeparatorCharacter c then SeparatorCharacter
        else NormalCharacter c

    let parseCharacters (input:string) : Characters =
        input.ToCharArray()
        |> List.ofArray
        |> List.map parseCharacter 
            
    let parseWords (input:string) : Words =
        parseCharacters input
        |> groupCharacters
        |> List.map groupedCharactersToWord
        
    