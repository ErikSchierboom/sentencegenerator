namespace StudioDonder.SentenceGenerator.Domain.Tests

open StudioDonder.SentenceGenerator.Domain.Markov
open System
open Xunit
open Xunit.Extensions

type MarkovTests() =     

    [<Fact>]
    member this.prepareListElementForProcessingOnListOfLengthTwoReturnsCorrectState() =    
        Assert.True((["hello"], "there") = prepareListElementForProcessing ["hello"; "there"])

    [<Fact>]
    member this.prepareListElementForProcessingOnListOfLengthGreaterThanTwoReturnsCorrectState() =  
        Assert.True((["hello"; "there"], "world") = prepareListElementForProcessing ["hello"; "there"; "world"])

    [<Fact>]
    member this.prepareListElementForProcessingOnEmptyListThrowsException() =  
        Assert.Throws<Exception>(fun() -> prepareListElementForProcessing [] |> ignore)

    [<Fact>]
    member this.prepareListElementForProcessingOnListWithListWithOneItemThrowsException() =  
        Assert.Throws<Exception>(fun() -> prepareListElementForProcessing ["hello"] |> ignore)

    [<Fact>]
    member this.prepareListForProcessingWithChainSizeIsTwoReturnsCorrectChainLinks() =        
        let groupedLists = prepareListForProcessing 2 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"], "there"; ["there"], "world"; ["world"], "!"] = groupedLists)

    [<Fact>]
    member this.prepareListForProcessingWithChainSizeIsGreaterThatTwoReturnsCorrectChainLinks() =        
        let groupedLists = prepareListForProcessing 3 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"; "there"], "world"; ["there"; "world"], "!"] = groupedLists)

    [<Theory>]
    [<InlineData(1)>]
    [<InlineData(0)>]
    [<InlineData(-1)>]
    member this.prepareListForProcessingWithChainSizeIsInvalidThrowsException(chainSize) =  
        Assert.Throws<Exception>(fun() -> prepareListForProcessing chainSize ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.elementsWithCountReturnsElementsWithCount() =        
        Assert.True([("there", 2); ("hello", 1); ("world", 1)] = elementsWithCount ["there"; "hello"; "there"; "world"])
    
    [<Fact>]
    member this.elementsWithCountOnEmptyListDoesNotThrowException() =  
        Assert.DoesNotThrow(fun() -> elementsWithCount [] |> ignore)

    [<Fact>]
    member this.elementsWithProbabilitiesReturnsElementsWithProbabilities() =        
        let bla = elementsWithProbabilities ["there"; "hello"; "there"; "world"]
        Assert.True([("there", 0.5); ("hello", 0.25); ("world", 0.25)] = bla)
    
    [<Fact>]
    member this.elementsWithProbabilitiesOnEmptyListDoesNotThrowException() =  
        Assert.DoesNotThrow(fun() -> elementsWithProbabilities [] |> ignore)

    [<Fact>]
    member this.groupNextStatesReturnsCorrectlyGroupedStates() =        
        let groupedStates = groupNextStates [["hello"], "world"; ["there"], "!"; ["hello"], "world";  ["hello"], "there"]
        Assert.True([["hello"], ["world"; "world"; "there"]; ["there"], ["!"]] = groupedStates)

    [<Fact>]
    member this.groupNextStatesOnEmptyListReturnsEmptyList() =  
        Assert.True([] = groupNextStates [])

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