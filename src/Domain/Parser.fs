module StudioDonder.SentenceGenerator.Domain.Parser

    open System
    open System.IO
    open Word
    open Strings

    let private groupCharacters (characters:Characters) =    
        let key = ref 0
        let lastTextType = ref (None:Option<TextType>)
        characters
        |> Seq.groupBy (fun c ->
                if Option.isSome !lastTextType && characterToTextType c = Option.get !lastTextType then
                    key
                else
                    incr key
                    lastTextType := Some(characterToTextType c)
                    key)
        |> Seq.map (snd >> List.ofSeq)
        |> List.ofSeq

    let parseCharacter (c: char) : Character =
        match c with
        | x when isPunctuationCharacter x -> PunctuationCharacter x
        | x when isSeparatorCharacter x -> SeparatorCharacter
        | x -> NormalCharacter x

    let parseCharacters (input:string) : Characters =
        input
        |> toCharacterList
        |> List.map parseCharacter 
            
    let parseWords (input:string) : Words =
        parseCharacters input
        |> groupCharacters
        |> List.map charactersToWord