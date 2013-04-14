namespace StudioDonder.SentenceGenerator.Domain

open System

module Word =

    type T = string

    let isPunctuation (word:T) =
        match word with
        | ";" | "," | "." | "!" | "?" -> true
        | _ -> false

    let markPunctuationCharactersAsWords (word:T) =
        word.Replace(";", " ; ")
            .Replace(",", " , ")
            .Replace(".", " . ")
            .Replace("!", " ! ")
            .Replace("?", " ? ")

    let sanitize (word:T) = 
        markPunctuationCharactersAsWords (word.Trim().ToLowerInvariant())        
            
    let split (word:T) = 
        Seq.toList (word.Split(' ', '\t', '\n'))
            
    let parse (word:T) =
        sanitize word 
        |> split 
        |> List.map (fun w -> w.Trim())
        |> List.filter (fun w -> not (String.IsNullOrWhiteSpace(w)))
    