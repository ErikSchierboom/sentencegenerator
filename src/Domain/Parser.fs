namespace Domain

open System

module Parser =

    let isWordSeparator x = 
        match x with
        | ' ' | '\t' | '\n' -> true
        | _  -> false        

    let convertPunctuationCharactersToWords (x:string) =
        x.Replace(";", " ; ").Replace(",", " , ").Replace(".", " . ").Replace("!", " ! ").Replace("?", " ? ")

    let rec splitOn x (y:string) (o:StringSplitOptions) =
        let chars = Seq.toList (y.ToCharArray())
        let splitStr = 
            match List.tryFindIndex x chars with
            | None -> [y]
            | Some i -> y.Substring(0, i) :: (splitOn x (y.Substring(i + 1)) o)        
        let isEmpty z = String.IsNullOrWhiteSpace(z)
        match o with
        | StringSplitOptions.None -> splitStr
        | _ -> List.filter (fun z -> not (isEmpty z)) splitStr
            
    let splitWords (x:string) = 
        let convertedWord = convertPunctuationCharactersToWords x
        Seq.toList (convertedWord.Split(' ', '\t', '\n'))        
        |> List.filter (fun w -> not (String.IsNullOrWhiteSpace(w)))

    let sanitizeWord (x:string) = 
        x.ToLowerInvariant()

    let parse (x:string) =
        List.map sanitizeWord (splitWords x)