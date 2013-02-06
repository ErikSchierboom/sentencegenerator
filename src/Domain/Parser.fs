module Domain.Parser

open System
open Domain.Words

let parse (word:string) =
    List.map sanitizeWord (splitWords word)