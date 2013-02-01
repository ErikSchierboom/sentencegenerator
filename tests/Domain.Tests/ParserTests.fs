namespace Domain.Tests

open Domain.Parser
open Xunit
open Xunit.Extensions

type ParserTests() = 

    [<Theory>]
    [<InlineData(';')>]
    [<InlineData(',')>]
    [<InlineData('.')>]
    [<InlineData('!')>]
    [<InlineData('?')>]
    member this.isPunctuationCharacterReturnsTrueForPunctuationCharacters(c) =
        Assert.True(isPunctuationCharacter c)