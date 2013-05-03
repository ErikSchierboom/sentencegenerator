namespace StudioDonder.SentenceGenerator.Domain.Tests

open StudioDonder.SentenceGenerator.Domain.Word
open System
open Xunit
open Xunit.Extensions

type WordTests() = 

    [<Theory>]
    [<InlineData(';')>]
    [<InlineData(',')>]
    [<InlineData('.')>]
    [<InlineData('!')>]
    [<InlineData('?')>]
    member this.isPunctuationCharacterOnPunctuationCharacterReturnsTrue(punctuationCharacter) =         
        Assert.True(isPunctuationCharacter punctuationCharacter)

    [<Theory>]
    [<InlineData(' ')>]
    [<InlineData('\t')>]
    [<InlineData('\n')>]
    [<InlineData('a')>]
    [<InlineData('9')>]
    [<InlineData('-')>]
    [<InlineData('_')>]
    [<InlineData('/')>]
    [<InlineData('\\')>]
    [<InlineData('+')>]
    [<InlineData('[')>]
    [<InlineData(']')>]
    [<InlineData('|')>]
    member this.isPunctuationCharacterOnNonPunctuationCharacterReturnsFalse(nonPunctuationCharacter) =         
        Assert.False(isPunctuationCharacter nonPunctuationCharacter)

    [<Theory>]
    [<InlineData(' ')>]
    [<InlineData('\r')>]
    [<InlineData('\n')>]
    [<InlineData('\t')>]
    member this.isSeparatorCharacterOnSeparatorCharacterReturnsTrue(separatorCharacter) =         
        Assert.True(isSeparatorCharacter separatorCharacter)

    [<Theory>]
    [<InlineData(';')>]
    [<InlineData(',')>]
    [<InlineData('.')>]
    [<InlineData('!')>]
    [<InlineData('?')>]
    [<InlineData('a')>]
    [<InlineData('9')>]
    [<InlineData('-')>]
    [<InlineData('_')>]
    [<InlineData('/')>]
    [<InlineData('\\')>]
    [<InlineData('+')>]
    [<InlineData('[')>]
    [<InlineData(']')>]
    [<InlineData('|')>]
    member this.isSeparatorCharacterOnNonSeparatorCharacterReturnsFalse(nonSeparatorCharacter) =         
        Assert.False(isSeparatorCharacter nonSeparatorCharacter)

    [<Fact>]
    member this.characterToStringOnNormalCharacterReturnSeparatorString() =         
        Assert.Equal<string>("a", characterToString (NormalCharacter 'a'))

    [<Fact>]
    member this.characterToStringOnPunctuationCharacterReturnSeparatorString() =         
        Assert.Equal<string>(".", characterToString (PunctuationCharacter '.'))

    [<Fact>]
    member this.characterToStringOnSeparatorCharacterThrowsArgumentException() =         
        Assert.Throws<ArgumentException>(fun () -> characterToString SeparatorCharacter |> ignore)

    [<Theory>]
    [<InlineData(';')>]
    [<InlineData(',')>]
    [<InlineData('.')>]
    [<InlineData('!')>]
    [<InlineData('?')>]
    member this.parseCharacterCorrectlyParsesPunctuationCharacters(punctuationCharacter) =         
        Assert.Equal(PunctuationCharacter punctuationCharacter, parseCharacter punctuationCharacter)
        
    [<Theory>]
    [<InlineData(' ')>]
    [<InlineData('\r')>]
    [<InlineData('\n')>]
    [<InlineData('\t')>]
    member this.parseCharacterCorrectlyParsesSeparatorCharacters(separatorCharacter) =         
        Assert.Equal(SeparatorCharacter, parseCharacter separatorCharacter)
        
    [<Theory>]
    [<InlineData('a')>]
    [<InlineData('9')>]
    [<InlineData('-')>]
    [<InlineData('_')>]
    [<InlineData('/')>]
    [<InlineData('\\')>]
    [<InlineData('+')>]
    [<InlineData('[')>]
    [<InlineData(']')>]
    [<InlineData('|')>]
    member this.parseCharacterCorrectlyParsesNormalCharacters(character) =         
        Assert.Equal(NormalCharacter character, parseCharacter character)

    [<Fact>]
    member this.parseCharactersCorrectlyParsesCharacters() =         
        Assert.Equal<Character list>([NormalCharacter 'a'; PunctuationCharacter '!'; SeparatorCharacter], parseCharacters "a!\n")

    [<Theory>]
    [<InlineData(" ")>]
    [<InlineData("  ")>]
    [<InlineData("\t")>]
    [<InlineData("\n")>]
    [<InlineData("\r")>]
    member this.parseWordsCorrectlyParsesSeparatorWords(separatorWord) =
        Assert.True([SeparatorWord] = parseWords separatorWord)

    [<Theory>]
    [<InlineData(";")>]
    [<InlineData(",")>]
    [<InlineData(".")>]
    [<InlineData("!")>]
    [<InlineData("?")>]
    member this.parseWordsCorrectlyParsesPunctuationWords(punctuationWord) =
        Assert.True([PunctuationWord punctuationWord] = parseWords punctuationWord)

    [<Fact>]    
    member this.parseWordsCorrectedParsesString() =
        Assert.True([NormalWord "hello"; PunctuationWord ";"; SeparatorWord; NormalWord "there"; PunctuationWord "!"] = parseWords "hello; there!")