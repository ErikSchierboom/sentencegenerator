module Domain.StringHelpers

open System

let convertPunctuationCharactersToWords (word:string) =
    word.Replace(";", " ; ").Replace(",", " , ").Replace(".", " . ").Replace("!", " ! ").Replace("?", " ? ")
            
let splitWord (word:string) = 
    let convertedWord = convertPunctuationCharactersToWords word
    Seq.toList (convertedWord.Split(' ', '\t', '\n'))
            
let splitWords (word:string) =
    splitWord (convertPunctuationCharactersToWords word)
    |> List.filter (fun w -> not (String.IsNullOrWhiteSpace(w)))

let sanitizeWord (word:string) = 
    word.ToLowerInvariant()