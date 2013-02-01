namespace Domain

module Parser =
            
    let splitWords (x:string) = 
        Seq.toList (x.Split (' ', '\t', '\n'))

    let isPunctuationCharacter x = 
        match x with
        | ';' -> true
        | ',' -> true
        | '.' -> true
        | '!' -> true
        | '?' -> true
        | _   -> false

    let removePunctuationCharacters (x:string) = 
        let chars = Seq.toList (x.ToCharArray())
        let charsWithoutPunctuationCharacters = List.filter (fun c -> not (isPunctuationCharacter c)) chars
        charsWithoutPunctuationCharacters.ToString

    let sanitizeWord (x:string) = 
        removePunctuationCharacters x

    let parse (x:string) =
        List.map sanitizeWord (splitWords x)