module StudioDonder.SentenceGenerator.Domain.Strings

    open System

    let concat (strings:string list) = String.Join("", strings)

    let toCharacterList (str:string) = 
        match str with
        | null -> nullArg "str"
        | _    -> str.ToCharArray() |> List.ofArray