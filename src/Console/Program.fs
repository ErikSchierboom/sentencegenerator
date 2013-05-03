// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open StudioDonder.SentenceGenerator.Domain.Parser

[<EntryPoint>]
let main argv = 
    let input = "hello there world! this is the input; for my markov chain generator."

    printfn "%A" (parseWords input)
    0 // return an integer exit code
