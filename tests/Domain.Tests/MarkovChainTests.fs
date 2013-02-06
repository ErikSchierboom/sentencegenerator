namespace Domain.Tests

open Domain.MarkovChain
open System
open Xunit
open Xunit.Extensions

type MarkovChainTests() = 

    [<Fact>]
    member this.createChainLinkOfLengthTwoCorrectlyGroupsList() =        
        let groupedList = createChainLink ["hello"; "there"]
        Assert.True((["hello"], "there") = groupedList)

    [<Fact>]
    member this.createChainLinkOfLengthGreaterThanTwoCorrectlyGroupsList() =        
        let groupedList = createChainLink ["hello"; "there"; "world"]
        Assert.True((["hello"; "there"], "world") = groupedList)

    [<Fact>]
    member this.createChainLinkCorrectlyGroupsListWithEmptyListThrowsException() =  
        Assert.Throws<Exception>(fun() -> createChainLink [] |> ignore)

    [<Fact>]
    member this.createChainLinkCorrectlyGroupsListWithListWithOneItemThrowsException() =  
        Assert.Throws<Exception>(fun() -> createChainLink ["hello"] |> ignore)

    [<Fact>]
    member this.groupListsForChainCorrectlyGroupsLists() =        
        let groupedLists = groupListsForChain 3 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"; "there"], "world"; ["there"; "world"], "!"] = groupedLists)

    [<Fact>]
    member this.groupListWithCountCorrectlyGroupsList() =        
        let groupedListWithCount = groupListWithCount ["hello"; "there"; "hello"]
        Assert.True(["hello", 2; "there", 1] = groupedListWithCount)

    [<Fact>]
    member this.addCountToGroupedListCorrectlyGroupsList() =        
        let groupedListWithCount = addCountToGroupedList [["hello"], "there"; ["hello"], "there";["hello"], "world"]
        Assert.False(true)
        (*Assert.True([["hello"], [("there", 2); ("world", 1)]] = groupedListWithCount)*)

    [<Fact>]
    member this.createMarkovChainReturnsMarkovChainForInput() =        
        let markovChain = createMarkovChain 2 ["hello"; "there"; "world"; "hello"; "hello"; "hello"]
        Assert.True([["hello"], [("there", 1); ("hello", 2)]; 
                     ["there"], [("world", 1)];
                     ["world"], [("hello", 1)]] = markovChain)