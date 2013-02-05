module Domain.Parser

open System
open Domain.StringHelpers

let parse (word:string) =
    List.map sanitizeWord (splitWords word)