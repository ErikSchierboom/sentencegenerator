module Domain.Words

open System

let isPunctuationWord (c:string) =
    match c with
    | ";" | "," | "." | "!" | "?" -> true
    | _ -> false

let markPunctuationCharactersAsWords (word:string) =
    word.Replace(";", " ; ")
        .Replace(",", " , ")
        .Replace(".", " . ")
        .Replace("!", " ! ")
        .Replace("?", " ? ")

let sanitizeWord (word:string) = 
    markPunctuationCharactersAsWords (word.Trim().ToLowerInvariant())        
            
let splitWord (word:string) = 
    Seq.toList (word.Split(' ', '\t', '\n'))
            
let splitWords (word:string) =
    sanitizeWord word 
    |> splitWord 
    |> List.map (fun w -> w.Trim())
    |> List.filter (fun w -> not (String.IsNullOrWhiteSpace(w)))
    