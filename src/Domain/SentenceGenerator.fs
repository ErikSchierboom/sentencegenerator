module Domain.SentenceGenerator

open System

let selectChainLinkToStartSentenceWith chainLinks =
    List.head chainLinks |> snd

let generate chainSize (text:string) =
    ""