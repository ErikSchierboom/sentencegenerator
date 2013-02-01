namespace Domain

open System

module Parser =

    let isWordSeparator x = 
        match x with
        | ' ' | '\t' | '\n' | ';' | ',' | '.'  | '!' | '?' -> true
        | _  -> false
            
    let splitWords (x:string) = 
        Seq.toList (x.Split (' ', '\t', '\n', ';', ',', '.', '!', '?'))
        |> List.filter (fun w -> not (String.IsNullOrWhiteSpace(w)))

    let sanitizeWord (x:string) = 
        x.ToLowerInvariant()

    let parse (x:string) =
        List.map sanitizeWord (splitWords x)