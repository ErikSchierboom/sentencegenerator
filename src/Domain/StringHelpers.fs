module Domain.StringHelpers

open System

let convertPunctuationCharactersToWords (x:string) =
    x.Replace(";", " ; ").Replace(",", " , ").Replace(".", " . ").Replace("!", " ! ").Replace("?", " ? ")
            
let splitWord (x:string) = 
    let convertedWord = convertPunctuationCharactersToWords x
    Seq.toList (convertedWord.Split(' ', '\t', '\n'))
            
let splitWords (x:string) =
    splitWord (convertPunctuationCharactersToWords x)
    |> List.filter (fun w -> not (String.IsNullOrWhiteSpace(w)))

let sanitizeWord (x:string) = 
    x.ToLowerInvariant()