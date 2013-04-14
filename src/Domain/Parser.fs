namespace StudioDonder.SentenceGenerator.Domain

open System
open StudioDonder.SentenceGenerator.Domain.Word

module Parser = 

    let parse (text:string) =
        StudioDonder.SentenceGenerator.Domain.Word.parse text