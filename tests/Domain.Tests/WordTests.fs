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
    member this.textTypeOnNormalCharacterReturnsNormalTextType() =         
        Assert.Equal<TextType>(Normal, (NormalCharacter 'a').TextType)

    [<Fact>]
    member this.textTypeOnPunctuationCharacterReturnsPunctuationTextType() =         
        Assert.Equal<TextType>(Punctuation, (PunctuationCharacter '.').TextType)

    [<Fact>]
    member this.textTypeOnPunctuationCharacterReturnsSeparatorTextType() =         
        Assert.Equal<TextType>(Separator, SeparatorCharacter.TextType)

    [<Fact>]
    member this.textTypeOnNormalWordReturnsNormalTextType() =         
        Assert.Equal<TextType>(Normal, (NormalWord "a").TextType)

    [<Fact>]
    member this.textTypeOnPunctuationWordReturnsPunctuationTextType() =         
        Assert.Equal<TextType>(Punctuation, (PunctuationWord ".").TextType)

    [<Fact>]
    member this.textTypeOnPunctuationWordReturnsSeparatorTextType() =         
        Assert.Equal<TextType>(Separator, SeparatorWord.TextType)

    [<Fact>]
    member this.toStringOnNormalCharacterReturnSeparatorString() =         
        Assert.Equal<string>("a", (NormalCharacter 'a').ToString())

    [<Fact>]
    member this.yoStringOnPunctuationCharacterReturnSeparatorString() =         
        Assert.Equal<string>(".", (PunctuationCharacter '.').ToString())

    [<Fact>]
    member this.yoStringOnSeparatorCharacterReturnsSpace() =         
        Assert.Equal<string>(" ", SeparatorCharacter.ToString())

    [<Fact>]
    member this.charactersToWordOnNormalCharactersReturnsNormalWord() =         
        Assert.Equal<Word>(NormalWord "hey", charactersToWord [NormalCharacter 'h'; NormalCharacter 'e'; NormalCharacter 'y'])

    [<Fact>]
    member this.charactersToWordOnPunctuationCharacterReturnsPunctuationWord() =         
        Assert.Equal<Word>(PunctuationWord ".", charactersToWord [PunctuationCharacter '.'])

    [<Fact>]
    member this.charactersToWordOnSeparatorCharacterReturnsSeparatorWord() =         
        Assert.Equal<Word>(SeparatorWord, charactersToWord [SeparatorCharacter])

    [<Fact>]
    member this.charactersToWordOnSeparatorCharactersReturnsSeparatorWord() =         
        Assert.Equal<Word>(SeparatorWord, charactersToWord [SeparatorCharacter; SeparatorCharacter; SeparatorCharacter])  

    [<Fact>]  
    member this.charactersToWordOnEmptyListThrowsArgumentException() =
        Assert.Throws<ArgumentException>(fun() -> charactersToWord [] |> ignore)

    [<Fact>]
    member this.toStringOnSeparatorWordReturnsSpace() =         
        Assert.Equal<string>(" ", SeparatorWord.ToString())

    [<Fact>]
    member this.toStringOnPunctuationWordReturnsPunctuation() =         
        Assert.Equal<string>(".", (PunctuationWord ".").ToString())

    [<Fact>]
    member this.toStringOnNormalWordReturnsWord() =         
        Assert.Equal<string>("hey", (NormalWord "hey").ToString())

    [<Fact>]
    member this.wordsToStringReturnsConcatenatedWords() =         
        Assert.Equal<string>("hello world!", wordsToString [NormalWord "hello"; SeparatorWord; NormalWord "world"; PunctuationWord "!"])

    [<Fact>]
    member this.wordsToStringOnEmptyListReturnsEmptyString() =         
        Assert.Equal<string>("", wordsToString [])