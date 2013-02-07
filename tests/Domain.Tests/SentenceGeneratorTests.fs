﻿namespace Domain.Tests

open Domain.SentenceGenerator
open Xunit
open Xunit.Extensions

type SentenceGeneratorTests() = 

    [<Fact>]
    member this.selectChainLinkToStartSentenceWithOnePunctuationCharacterChainLinkReturnsThatChainLink() =
        Assert.True((["."], [("hello", 1); ("welcome", 1)]) = selectChainLinkToStartSentenceWith [["hello"], [("there", 1)]; ["."], [("hello", 1); ("welcome", 1)]])

    [<Fact>]
    member this.selectChainLinkToStartSentenceWithNoPunctuationCharacterChainLinkReturnsFirstChainLink() =
        Assert.True((["hello"], [("there", 1)]) = selectChainLinkToStartSentenceWith [["hello"], [("there", 1)]; ["world"], [("hello", 1); ("welcome", 1)]])

    [<Fact>]
    member this.generateReturnsRandomlyGeneratedSentence() =
        Assert.Equal<string>("asdasd", generate 2 "hello there world!")