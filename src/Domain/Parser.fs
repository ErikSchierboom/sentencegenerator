namespace StudioDonder.SentenceGenerator.Domain

open System
open StudioDonder.SentenceGenerator.Domain.Word

module Parser = 

    let groupCharacters (characters:Characters) =        
        let key = ref 0
        let lastCharacterType = ref (None:Option<CharacterType>)
        characters
        |> Seq.groupBy (fun c ->
                if lastCharacterType.Value.IsSome && characterToCharacterType c = lastCharacterType.Value.Value then
                    key
                else
                    incr key
                    lastCharacterType := Some(characterToCharacterType c)
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
        