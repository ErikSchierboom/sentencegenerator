module Domain.Parser

open System
open Domain.StringHelpers

let parse (x:string) =
    List.map sanitizeWord (splitWords x)