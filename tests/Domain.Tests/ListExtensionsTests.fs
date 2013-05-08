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
        Assert.Equal(2, List.take 2 ["hello"; "there"; "world"; "!"] |> List.length)
    
    [<Fact>]
    member this.takeReturnsLengthElementsFromStartOfList() =        
        Assert.Equal<string list>(["hello"; "there"], List.take 2 ["hello"; "there"; "world"; "!"])

    [<Fact>]
    member this.takeWithLengthIsOneReturnsHeadOfList() =
        Assert.Equal<string list>(["hello"], List.take 1 ["hello"; "there"; "world"; "!"])

    [<Fact>]
    member this.takeWithLengthGreaterThanNumberOfElementsReturnsMaximumNumberOfElements() =  
        Assert.Equal<string list>(["hello"; "there"; "world"; "!"], List.take 4 ["hello"; "there"; "world"; "!"])

    [<Theory>]
    [<InlineData(0)>]
    [<InlineData(-1)>]
    member this.takeWithLengthIsInvalidThrowsException(chainSize) =  
        Assert.Throws<Exception>(fun() -> List.take chainSize ["hello"; "there"; "world"; "!"] |> ignore)
        
    [<Fact>]
    member this.takeRandomWillReturnRandomElement() =
        let list = ["hello"; "there"; "world"]
        let randomElements = List.init 50 (fun _ -> List.takeRandom list) |> set
        Assert.Equal<Set<string>>(set list, randomElements)

    [<Fact>]
    member this.takeRandomOnListWithOneElementReturnsThatElement() =
        let list = ["hello"]
        let randomElements = List.init 50 (fun _ -> List.takeRandom list) |> set
        Assert.Equal<Set<string>>(set list, randomElements)

    [<Fact>]
    member this.takeRandomOnEmptyListThrowsException() =
        Assert.Throws<Exception>(fun() -> List.takeRandom [] |> ignore)

    [<Fact>]
    member this.partitionByLengthWithLengthIsOneReturnsCorrectPartionedBlocks() =  
        Assert.Equal<string list list>([["hello"]; ["there"]; ["world"]; ["!"]], List.partitionByLength 1 ["hello"; "there"; "world"; "!"])

    [<Fact>]
    member this.partitionByLengthWithLengthGreaterThanOneReturnsCorrectPartionedBlocks() =   
        Assert.Equal<string list list>([["hello"; "there"; "world"]; ["there"; "world"; "!"]], List.partitionByLength 3 ["hello"; "there"; "world"; "!"])

    [<Fact>]
    member this.withSingleTailElementOnListOfLengthTwoReturnsCorrectState() =    
        Assert.Equal<string list * string>((["hello"], "there"), List.withSingleTailElement ["hello"; "there"])

    [<Fact>]
    member this.withSingleTailElementOnListOfLengthGreaterThanTwoReturnsCorrectState() =  
        Assert.Equal<string list * string>((["hello"; "there"], "world"), List.withSingleTailElement ["hello"; "there"; "world"])

    [<Fact>]
    member this.withSingleTailElementOnEmptyListThrowsException() =  
        Assert.Throws<Exception>(fun() -> List.withSingleTailElement [] |> ignore)

    [<Fact>]
    member this.withSingleTailElementOnListWithListWithOneItemThrowsException() =  
        Assert.Throws<Exception>(fun() -> List.withSingleTailElement ["hello"] |> ignore)

    [<Fact>]
    member this.pairsOnEmptyListReturnsEmptyList() =
        Assert.Equal<(string * string) list>([], List.pairs [])

    [<Fact>]
    member this.pairsOnListWithOneItemReturnsEmptyList() =
        Assert.Equal<(string * string) list>([], List.pairs ["hello"])

    [<Fact>]
    member this.pairsOnListWithTwoItemsReturnsPairs() =
        Assert.Equal<(string * string) list>(["hello", "there"], List.pairs ["hello"; "there"])

    [<Fact>]
    member this.pairsOnListWithEvenItemsReturnsPairs() =
        Assert.Equal<(string * string) list>(["hello", "there"; "there", "world"; "world", "!"], List.pairs ["hello"; "there"; "world"; "!"])

    [<Fact>]
    member this.pairsOnListWithOddItemsReturnsPairs() =
        Assert.Equal<(string * string) list>(["hello", "there"; "there", "world"], List.pairs ["hello"; "there"; "world"])