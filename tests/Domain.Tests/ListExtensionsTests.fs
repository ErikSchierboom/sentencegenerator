namespace Domain.Tests

open System
open Xunit
open Xunit.Extensions

type ListExtensionsTests() = 

    [<Fact>]
    member this.lastReturnsLastElementOfList() =        
        Assert.Equal<string>("world", List.last ["hello"; "there"; "world"])               

    [<Fact>]
    member this.lastOnEmptyListThrowsException() =  
        Assert.Throws<Exception>(fun() -> List.last [] |> ignore)

    [<Fact>]
    member this.takeReturnsListOfSpecifiedSliceLength() =        
        let slicedList = List.take 2 ["hello"; "there"; "world"; "!"]
        Assert.Equal(2, List.length slicedList)
    
    [<Fact>]
    member this.takeReturnsLengthElementsFromStartOfList() =        
        Assert.True(["hello"; "there"] = List.take 2 ["hello"; "there"; "world"; "!"])

    [<Fact>]
    member this.takeWithLengthIsOneReturnsHeadOfList() =
        Assert.True(["hello"] = List.take 1 ["hello"; "there"; "world"; "!"])

    [<Fact>]
    member this.takeWithLengthGreaterThanNumberOfElementsReturnsMaximumNumberOfElements() =  
        Assert.True(["hello"; "there"; "world"; "!"] = List.take 4 ["hello"; "there"; "world"; "!"])

    [<Theory>]
    [<InlineData(0)>]
    [<InlineData(-1)>]
    member this.takeWithLengthIsInvalidThrowsException(chainSize) =  
        Assert.Throws<Exception>(fun() -> List.take chainSize ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.partitionByLengthWithLengthIsOneReturnsCorrectPartionedBlocks() =        
        let partitionBlock = List.partitionByLength 1 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"]; ["there"]; ["world"]; ["!"]] = partitionBlock)

    [<Fact>]
    member this.partitionByLengthWithLengthGreaterThanOneReturnsCorrectPartionedBlocks() =        
        let partitionBlock = List.partitionByLength 3 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"; "there"; "world"]; ["there"; "world"; "!"]] = partitionBlock)