module SentenceGenerator.Console.Main

    open SentenceGenerator.Domain.Parser
    open SentenceGenerator.Domain.Markov
    open SentenceGenerator.Domain.SentenceGenerator

    open System.IO;

    [<EntryPoint>]
    let main argv = 
        let file = @"the-divine-comedy.txt"

        let input = File.ReadAllText(file)
        let chainSize = 1
        let numberOfWords = 20
        
        (*let input = "hello there world! hello there monde. hello there world wereld monde hello"*)

        printfn "%A" (generateSentences chainSize numberOfWords input)
        0 // return an integer exit code
