module Domain.Parser

open System
open Domain.Words

let parse (text:string) =
    splitWords text