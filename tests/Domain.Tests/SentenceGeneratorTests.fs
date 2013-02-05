namespace Domain.Tests

open Domain.Parser
open Xunit
open Xunit.Extensions
(*
open Domain.SentenceGenerator

type SentenceGeneratorTests() = 

    [<Fact>]
    member this.partitionBlockWithBlockSizeGreaterThanNumberOfElementsInTailReturnsEmptyList() =
        let partitions = partitionBlock 3 ["hello"; "there"; "world"];
        Assert.True(partitions.IsEmpty)
(*
    [<Fact>]
    member this.partitionBlocksReturnsCorrectNumberOfBlocks() =
        let partitions = partitionBlocks 2 ["hello"; "there"; "world"; "from"];
        Assert.Equal(4, partitions.Length)

    [<Fact>]
    member this.partitionBlocksReturnsBlocksOfCorrectLength() =
        let partitions = partitionBlocks 2 ["hello"; "there"; "world"; "from"];
        Assert.True([("hello", "there"); ("there", "world"); ("world", "hello"); ("hello", "hello"); ("hello", "world")] = groupedWords)
*)
    [<Fact>]
    member this.groupWordsReturnsCorrectGroups() =
        let groupedWords = groupWords ["hello"; "there"; "world"; "hello"; "hello"; "world"]
        Assert.True([("hello", "there"); ("there", "world"); ("world", "hello"); ("hello", "hello"); ("hello", "world")] = groupedWords)
        *)