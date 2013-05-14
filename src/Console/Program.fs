module StudioDonder.SentenceGenerator.Console.Main

open StudioDonder.SentenceGenerator.Domain.Parser
open StudioDonder.SentenceGenerator.Domain.Markov
open StudioDonder.SentenceGenerator.Domain.SentenceGenerator

    [<EntryPoint>]
    let main argv = 
        let chainSize = 1;
        let input = "hello there world! hello there monde. hello there world wereld monde hello"

        printfn "%A" (generateSentences chainSize input)
        0 // return an integer exit code
