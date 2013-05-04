namespace StudioDonder.SentenceGenerator.Domain.Tests

open StudioDonder.SentenceGenerator.Domain.Markov
open System
open Xunit
open Xunit.Extensions

type MarkovTests() =     

 [<Fact>]
    member this.elementsWithCountReturnsElementsWithCount() =        
        Assert.True([{ Element = "there"; Count = 2 }; { Element = "hello"; Count = 1 }; { Element = "world"; Count = 1 }] = elementsWithCount ["there"; "hello"; "there"; "world"])
    
    [<Fact>]
    member this.elementsWithCountOnEmptyListDoesNotThrowException() =  
        Assert.DoesNotThrow(fun() -> elementsWithCount [] |> ignore)

    [<Fact>]
    member this.elementsWithProbabilitiesReturnsElementsWithProbabilities() =        
        let elementsWithProbabilities = elementsWithProbabilities ["there"; "hello"; "there"; "world"]
        Assert.True([{ Element = "there"; Probability = 0.5 }; { Element = "hello"; Probability = 0.25 }; { Element = "world"; Probability = 0.25 }] = elementsWithProbabilities)
    
    [<Fact>]
    member this.elementsWithProbabilitiesOnEmptyListDoesNotThrowException() =  
        Assert.DoesNotThrow(fun() -> elementsWithProbabilities [] |> ignore)

    [<Fact>]
    member this.createChainLinksWithChainSizeIsOneAndOnlyOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinks = createChainLinks 1 ["hello"; "there"; "world"; "!"]
        Assert.True([{ Chain = ["hello"]; Successors = ["there"] }; { Chain = ["there"];  Successors = ["world"] }; { Chain = ["world"]; Successors = ["!"] }] = chainLinks)

    [<Fact>]
    member this.createChainLinksWithChainSizeIsGreaterThanOneAndOnlyOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinks = createChainLinks 2 ["hello"; "there"; "world"; "!"]
        Assert.True([{ Chain = ["hello"; "there"]; Successors = ["world"] }; { Chain = ["there"; "world"]; Successors = ["!"] }] = chainLinks)                

    [<Fact>]
    member this.createChainLinksWithChainSizeIsOneAndMoreThanOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinks = createChainLinks 1 ["hello"; "there"; "hello"; "!"]
        Assert.True([{ Chain = ["hello"]; Successors = ["there"; "!"] }; { Chain = ["there"];  Successors = ["hello"] }] = chainLinks)

    [<Fact>]
    member this.createChainLinksWithChainSizeIsGreaterThanOneAndMoreThanOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinks = createChainLinks 2 ["hello"; "there"; "hello"; "there"; "!"]
        Assert.True([{ Chain = ["hello"; "there"]; Successors = ["hello"; "!"] }; { Chain = ["there"; "hello"]; Successors = ["there"] }] = chainLinks)

    [<Fact>]
    member this.createChainLinksWithDuplicateSuccessorsReturnsDuplicates() =        
        let chainLinks = createChainLinks 1 ["hello"; "there"; "hello"; "there"]
        Assert.True([{ Chain = ["hello"]; Successors = ["there"; "there"] }; { Chain = ["there"];  Successors = ["hello"] }] = chainLinks)

    [<Theory>]
    [<InlineData(0)>]
    [<InlineData(-1)>]
    member this.createChainLinksWithChainSizeIsInvalidThrowsException(chainSize) =  
        Assert.Throws<Exception>(fun() -> createChainLinks chainSize ["hello"; "there"; "world"; "!"] |> ignore)

    
    (*
    


    [<Fact>]
    member this.createMarkovChainWithChainSizeIsTwoReturnsCorrectMarkovChain() =        
        let markovChain = createMarkovChain 2 ["hello"; "there"; "world"; "hello"; "hello"; "hello"; "there"]
        Assert.True([["hello"], [("there", 0.5); ("hello", 0.5)]; 
                     ["there"], [("world", 1.0)];
                     ["world"], [("hello", 1.0)]] = markovChain)

    [<Fact>]
    member this.createMarkovChainWithChainSizeIsGreaterThanTwoReturnsCorrectMarkovChain() =        
        let markovChain = createMarkovChain 3 ["hello"; "there"; "world"; "hello"; "there"; "hello"]
        Assert.True([["hello"; "there"], [("world", 0.5); ("hello", 0.5)]; 
                     ["there"; "world"], [("hello", 1.0)];
                     ["world"; "hello"], [("there", 1.0)]] = markovChain)

    [<Theory>]
    [<InlineData(1)>]
    [<InlineData(0)>]
    [<InlineData(-1)>]
    member this.createMarkovChainWithChainSizeIsInvalidThrowsException(chainSize) =  
        Assert.Throws<Exception>(fun() -> createMarkovChain chainSize ["hello"; "there"; "world"; "!"] |> ignore)

        *)