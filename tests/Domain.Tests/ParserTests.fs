namespace StudioDonder.SentenceGenerator.Domain.Tests

open StudioDonder.SentenceGenerator.Domain.Word
open StudioDonder.SentenceGenerator.Domain.Parser
open Xunit
open Xunit.Extensions

type ParserTests() =     

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
    member this.parseWordsIgnoresSeparatorWords(separatorWord) =
        Assert.Equal<Words>([], parseWords separatorWord)

    [<Theory>]
    [<InlineData(";")>]
    [<InlineData(",")>]
    [<InlineData(".")>]
    [<InlineData("!")>]
    [<InlineData("?")>]
    member this.parseWordsCorrectlyParsesPunctuationWords(punctuationWord) =
        Assert.Equal<Words>([PunctuationWord punctuationWord], parseWords punctuationWord)

    [<Fact>]    
    member this.parseWordsCorrectedParsesString() =
        Assert.Equal<Words>([NormalWord "hello"; PunctuationWord ";"; NormalWord "there"; PunctuationWord "!"], parseWords "hello; there!")