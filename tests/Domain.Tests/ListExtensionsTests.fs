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
    member this.sliceReturnsListOfSpecifiedSliceLength() =        
        let slicedList = List.slice 2 ["hello"; "there"; "world"; "!"]
        Assert.Equal(2, List.length slicedList)
    
    [<Fact>]
    member this.sliceReturnsSlicedElementsFromStartOfList() =        
        Assert.True(["hello"; "there"] = List.slice 2 ["hello"; "there"; "world"; "!"])

    [<Fact>]
    member this.sliceWithLengthIsOneReturnsHeadOfList() =
        Assert.True(["hello"] = List.slice 1 ["hello"; "there"; "world"; "!"])

    [<Fact>]
    member this.sliceWithLengthGreaterThanNumberOfElementsReturnsMaximumNumberOfElements() =  
        Assert.True(["hello"; "there"; "world"; "!"] = List.slice 4 ["hello"; "there"; "world"; "!"])

    [<Fact>]
    member this.sliceWithLengthLessThanZeroThrowsException() =  
        Assert.Throws<Exception>(fun() -> List.slice -1 ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.sliceWithLengthEqualToZeroThrowsException() =  
        Assert.Throws<Exception>(fun() -> List.slice 0 ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.partitionByLengthWithLengthIsOneReturnsCorrectPartionedBlocks() =        
        let partitionBlock = List.partitionByLength 1 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"]; ["there"]; ["world"]; ["!"]] = partitionBlock)

    [<Fact>]
    member this.partitionByLengthWithLengthGreaterThanOneReturnsCorrectPartionedBlocks() =        
        let partitionBlock = List.partitionByLength 3 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"; "there"; "world"]; ["there"; "world"; "!"]] = partitionBlock)