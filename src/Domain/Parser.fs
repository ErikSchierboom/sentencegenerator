module Domain.Parser

open System
open Domain.Word

let parse (text:string) =
    Domain.Word.parse text