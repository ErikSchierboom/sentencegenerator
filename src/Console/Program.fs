namespace StudioDonder.SentenceGenerator.Console

open StudioDonder.SentenceGenerator.Domain.Parser
open StudioDonder.SentenceGenerator.Domain.Markov
open StudioDonder.SentenceGenerator.Domain.SentenceGenerator

module Main = 

    [<EntryPoint>]
    let main argv = 
        let chainSize = 1;
        let input = "hello there world! this is the input; for my markov chain generator."


        let chainLength = 10
        let chain = createChain 1 ["hello"; "there"; "hello"; "there"; "!"; "#"] (fun _ -> ["there"]) (fun _ index -> index = 10) 

        printfn "%A" chain
        printfn "%A" (generateSentences chainSize input)
        0 // return an integer exit code
