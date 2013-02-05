namespace Domain.Tests

open System
open Xunit
open Xunit.Extensions

type ListExtensionsTests() = 

    [<Fact>]
    member this.sliceReturnsCorrectSlice() =        
        let slicedList = List.slice 1 2 ["hello"; "there"; "world"; "!"]
        Assert.True(["there"; "world"] = slicedList)

    [<Fact>]
    member this.sliceWithLengthLessThanZeroThrowsArgumentException() =  
        Assert.Throws<ArgumentException>(fun() -> List.slice 1 0 ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.sliceWithLengthEqualToZeroThrowsArgumentException() =  
        Assert.Throws<ArgumentException>(fun() -> List.slice 1 0 ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.sliceWithIndexLessThanZeroThrowsArgumentException() =  
        Assert.Throws<ArgumentException>(fun() -> List.slice -1 1 ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.sliceWithIndexEqualToNumberOfElementsThrowsInvalidOperationException() =  
        Assert.Throws<InvalidOperationException>(fun() -> List.slice 4 1 ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.sliceWithIndexGreaterThanNumberOfElementsThrowsInvalidOperationException() =  
        Assert.Throws<InvalidOperationException>(fun() -> List.slice 5 1 ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.sliceWithLengthGreaterThanNumberOfElementsThrowsInvalidOperationException() =  
        Assert.Throws<InvalidOperationException>(fun() -> List.slice 1 4 ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.partitionBlockReturnsCorrectPartionBlock() =        
        let partitionBlock = List.partitionBlock 3 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"; "there"; "world"]; ["there"; "world"; "!"]] = partitionBlock)