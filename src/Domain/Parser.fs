module StudioDonder.SentenceGenerator.Domain.Parser

    open System
    open System.IO
    open Word

    let private groupCharacters (characters:Characters) =        
        let key = ref 0
        let lastTextType = ref (None:Option<TextType>)
        characters
        |> Seq.groupBy (fun c ->
                if Option.isSome !lastTextType && c.TextType = Option.get !lastTextType then
                    key
                else
                    incr key
                    lastTextType := Some(c.TextType)
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
        |> groupCharacters
        |> List.map charactersToWord

    let parseFile (file:string) : Words =
        File.ReadAllText(file) 
        |> parseWords