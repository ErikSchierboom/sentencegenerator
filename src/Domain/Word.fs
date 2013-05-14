module StudioDonder.SentenceGenerator.Domain.Word

    open System

    type TextType = Normal | Punctuation | Separator

    type Character =
        | NormalCharacter of char        
        | PunctuationCharacter of char
        | SeparatorCharacter

        override self.ToString() =
            match self with
            | NormalCharacter x | PunctuationCharacter x -> x.ToString()
            | SeparatorCharacter -> " "

        member self.TextType with get() = match self with
                                          | NormalCharacter _      -> Normal
                                          | PunctuationCharacter _ -> Punctuation
                                          | SeparatorCharacter     -> Separator   

    type Word = 
        | NormalWord of string        
        | PunctuationWord of string
        | SeparatorWord

        override self.ToString() =
            match self with
            | NormalWord x | PunctuationWord x -> x
            | SeparatorWord -> " "

        member self.TextType with get() = match self with
                                          | NormalWord _      -> Normal
                                          | PunctuationWord _ -> Punctuation
                                          | SeparatorWord     -> Separator   

    type Characters = Character list
    type Words = Word list

    let private punctuationCharacters = set [';'; ','; '.'; '!'; '?']
    let private separatorCharacters = set [' '; '\r'; '\t'; '\n']    

    let isPunctuationCharacter (c:char) = Set.contains c punctuationCharacters
    let isSeparatorCharacter (c:char) = Set.contains c separatorCharacters
        
    let charactersToWord (characters:Characters) =        
        match characters with       
        | [] -> raise (new ArgumentException("The characters list must not be empty."))
        | x::xs -> 
            match x with
            | NormalCharacter _      -> NormalWord (String.Join("", List.map (fun c -> c.ToString()) characters))
            | PunctuationCharacter y -> PunctuationWord (x.ToString())
            | SeparatorCharacter     -> SeparatorWord
            
    let wordsToString (words:Words) = String.Join("", List.map (fun w -> w.ToString()) words)
    
        
    