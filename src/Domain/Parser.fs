namespace StudioDonder.SentenceGenerator.Domain

open System
open Word

module Parser = 

    let private groupCharacters (characters:Characters) =        
        let key = ref 0
        let lastTextType = ref (None:Option<TextType>)
        characters
        |> Seq.groupBy (fun c ->
                if lastTextType.Value.IsSome && characterToTextType c = lastTextType.Value.Value then
                    key
                else
                    incr key
                    lastTextType := Some(characterToTextType c)
                    key)
        |> Seq.map (snd >> List.ofSeq)
        |> List.ofSeq

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
        |> List.filter (fun c -> not (characterToTextType c = Separator))
        |> groupCharacters        
        |> List.map charactersToWord