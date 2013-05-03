namespace StudioDonder.SentenceGenerator.Domain.Tests

open StudioDonder.SentenceGenerator.Domain.Sentence
open Xunit
open Xunit.Extensions
(*
type SentenceTests() =    

    [<Fact>]    
    member this.parseOnWordsWithoutPunctuationCharacterReturnsWordsAsSentence() =
        Assert.True([["hello"; "there"]] = parse ["hello"; "there"])

    [<Fact>]    
    member this.parseOnWordsWithEachSentenceEndsWithPunctuationCharacterReturnsWordsAsSentencesEndingWithPunctuationCharacter() =
        Assert.True([["hello"; "there"; "!"]; ["and"; "hello"; ";"]] = parse ["hello"; "there"; "!"; "and"; "hello"; ";"])

    [<Fact>]    
    member this.parseOnWordsDoesNotRequirePunctuationForLastSentence() =
        Assert.True([["hello"; "there"; "!"]; ["and"; "hello"; ";"]; ["very"; "good"]] = parse ["hello"; "there"; "!"; "and"; "hello"; ";"; "very"; "good"])

    [<Fact>]
    member this.parseOnEmptyListReturnsEmptyList() =
        Assert.True([] = parse [])
        *)