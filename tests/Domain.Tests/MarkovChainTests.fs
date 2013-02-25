namespace Domain.Tests

open Domain.MarkovChain
open System
open Xunit
open Xunit.Extensions

type MarkovChainTests() = 

    [<Fact>]
    member this.createChainLinkOfLengthTwoReturnsCorrectChainLink() =    
        Assert.True((["hello"], "there") = createChainLink ["hello"; "there"])

    [<Fact>]
    member this.createChainLinkOfLengthGreaterThanTwoReturnsCorrectChainLink() =  
        Assert.True((["hello"; "there"], "world") = createChainLink ["hello"; "there"; "world"])

    [<Fact>]
    member this.createChainLinkOnEmptyListThrowsException() =  
        Assert.Throws<Exception>(fun() -> createChainLink [] |> ignore)

    [<Fact>]
    member this.createChainLinkOnListWithListWithOneItemThrowsException() =  
        Assert.Throws<Exception>(fun() -> createChainLink ["hello"] |> ignore)

    [<Fact>]
    member this.createChainLinksWithChainSizeIsTwoReturnsCorrectChainLinks() =        
        let groupedLists = createChainLinks 2 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"], "there"; ["there"], "world"; ["world"], "!"] = groupedLists)

    [<Fact>]
    member this.createChainLinksWithChainSizeIsGreaterThatTwoReturnsCorrectChainLinks() =        
        let groupedLists = createChainLinks 3 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"; "there"], "world"; ["there"; "world"], "!"] = groupedLists)

    [<Theory>]
    [<InlineData(1)>]
    [<InlineData(0)>]
    [<InlineData(-1)>]
    member this.createChainLinksWithChainSizeIsInvalidThrowsException(chainSize) =  
        Assert.Throws<Exception>(fun() -> createChainLinks chainSize ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.groupChainLinksReturnsChainLinksGroupedCorrectly() =        
        let groupedChainLinks = groupChainLinks [["hello"], "world"; ["there"], "!"; ["hello"], "world";  ["hello"], "there"]
        Assert.True([["hello"], ["world"; "world"; "there"]; ["there"], ["!"]] = groupedChainLinks)

    [<Fact>]
    member this.groupChainLinksOnEmptyListReturnsEmptyList() =  
        Assert.True([] = groupChainLinks [])

//    [<Fact>]
//    member this.convertCountsToProbabilitiesReturnsCorrectlyConvertCountsToProbabilityIndexes() =        
//        Assert.True([("there", 0.0); ("hello", 0.7); ("world", 0.9)] = convertCountsToProbabilities [("there", 7); ("hello", 2); ("world", 1)])
    
    [<Fact>]
    member this.convertCountsToProbabilitiesOnEmptyListDoesNotThrowException() =  
        Assert.DoesNotThrow(fun() -> convertCountsToProbabilities [] |> ignore)

    [<Fact>]
    member this.createMarkovChainWithChainSizeIsTwoReturnsCorrectMarkovChain() =        
        let markovChain = createMarkovChain 2 ["hello"; "there"; "world"; "hello"; "hello"; "hello"]
        Assert.True([["hello"], [("there", 1); ("hello", 2)]; 
                     ["there"], [("world", 1)];
                     ["world"], [("hello", 1)]] = markovChain)

    [<Fact>]
    member this.createMarkovChainWithChainSizeIsGreaterThanTwoReturnsCorrectMarkovChain() =        
        let markovChain = createMarkovChain 3 ["hello"; "there"; "world"; "hello"; "there"; "hello"]
        Assert.True([["hello"; "there"], [("world", 1); ("hello", 1)]; 
                     ["there"; "world"], [("hello", 1)];
                     ["world"; "hello"], [("there", 1)]] = markovChain)

    [<Theory>]
    [<InlineData(1)>]
    [<InlineData(0)>]
    [<InlineData(-1)>]
    member this.createMarkovChainWithChainSizeIsInvalidThrowsException(chainSize) =  
        Assert.Throws<Exception>(fun() -> createMarkovChain chainSize ["hello"; "there"; "world"; "!"] |> ignore)
//
//    [<Fact>]
//    member this.createMarkovChainWithPropabilitiesWithChainSizeIsTwoReturnsCorrectMarkovChain() =        
//        let markovChain = createMarkovChainWithPropabilities 2 ["hello"; "there"; "world"; "hello"; "hello"; "hello"; "world"]
//        Assert.True([["hello"], [("there", 0.0); ("hello", 0.33); ("world", 0.33)]; 
//                     ["there"], [("world", 0.0)];
//                     ["world"], [("hello", 0.0)]] = markovChain)
//
//    [<Fact>]
//    member this.createMarkovChainWithPropabilitiesWithChainSizeIsGreaterThanTwoReturnsCorrectMarkovChain() =        
//        let markovChain = createMarkovChainWithPropabilities 3 ["hello"; "there"; "hello"; "there"; "hello"; "there"; "world"]
//        Assert.True([["hello"; "there"], [("hello", 0.0); ("world", 0.66)]; 
//                     ["there"; "hello"], [("there", 0.0)]] = markovChain)
//
//    [<Theory>]
//    [<InlineData(1)>]
//    [<InlineData(0)>]
//    [<InlineData(-1)>]
//    member this.createMarkovChainWithPropabilitiesWithChainSizeIsInvalidThrowsException(chainSize) =  
//        Assert.Throws<Exception>(fun() -> createMarkovChainWithPropabilities chainSize ["hello"; "there"; "world"; "!"] |> ignore)