namespace StudioDonder.SentenceGenerator.Domain.Tests

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
    member this.takeRandomReturnsRandomElement() =        
        let randomElements = List.fold (fun acc element -> (List.takeRandom ["hello"; "there"; "world"; "!"; "we"; "are"; "also"; "there"]) :: acc) [] [1..20]         
        Assert.True(Seq.distinct randomElements |> Seq.length > 1)

    [<Fact>]
    member this.takeRandomOnEmptyListThrowsArgumentException() =  
        Assert.Throws<Exception>(fun() -> List.takeRandom [] |> ignore)

    [<Fact>]
    member this.takeRandomProbabilityReturnsRandomElement() =        
        let randomElements = List.fold (fun acc element -> (List.takeRandomProbability ["hello", 0.9; "there", 0.1]) :: acc) [] [1..20]         
        Assert.True(Seq.distinct randomElements |> Seq.length > 1)

    [<Fact>]
    member this.takeRandomProbabilityOnEmptyListThrowsArgumentException() =  
        Assert.Throws<Exception>(fun() -> List.takeRandomProbability [] |> ignore)

    [<Fact>]
    member this.partitionByLengthWithLengthIsOneReturnsCorrectPartionedBlocks() =        
        let partitionBlock = List.partitionByLength 1 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"]; ["there"]; ["world"]; ["!"]] = partitionBlock)

    [<Fact>]
    member this.partitionByLengthWithLengthGreaterThanOneReturnsCorrectPartionedBlocks() =        
        let partitionBlock = List.partitionByLength 3 ["hello"; "there"; "world"; "!"]
        Assert.True([["hello"; "there"; "world"]; ["there"; "world"; "!"]] = partitionBlock)

    [<Fact>]
    member this.withSingleTailElementOnListOfLengthTwoReturnsCorrectState() =    
        Assert.True((["hello"], "there") = List.withSingleTailElement ["hello"; "there"])

    [<Fact>]
    member this.withSingleTailElementOnListOfLengthGreaterThanTwoReturnsCorrectState() =  
        Assert.True((["hello"; "there"], "world") = List.withSingleTailElement ["hello"; "there"; "world"])

    [<Fact>]
    member this.withSingleTailElementOnEmptyListThrowsException() =  
        Assert.Throws<Exception>(fun() -> List.withSingleTailElement [] |> ignore)

    [<Fact>]
    member this.withSingleTailElementOnListWithListWithOneItemThrowsException() =  
        Assert.Throws<Exception>(fun() -> List.withSingleTailElement ["hello"] |> ignore)