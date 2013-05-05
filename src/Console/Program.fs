namespace StudioDonder.SentenceGenerator.Console

open StudioDonder.SentenceGenerator.Domain.SentenceGenerator

module Main = 

    [<EntryPoint>]
    let main argv = 
        let input = "hello there world! this is the input; for my markov chain generator."

        printfn "%A" (generateSentences input)
        0 // return an integer exit code
