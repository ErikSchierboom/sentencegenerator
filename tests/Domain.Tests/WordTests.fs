﻿namespace SentenceGenerator.Domain.Tests

open SentenceGenerator.Domain.Word
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
    member this.characterToTextTypeOnNormalCharacterReturnsNormalTextType() =         
        Assert.Equal<TextType>(Normal, characterToTextType (NormalCharacter 'a'))

    [<Fact>]
    member this.characterToTextTypeOnPunctuationCharacterReturnsPunctuationTextType() =         
        Assert.Equal<TextType>(Punctuation, characterToTextType (PunctuationCharacter '.'))

    [<Fact>]
    member this.characterToTextTypeOnPunctuationCharacterReturnsSeparatorTextType() =         
        Assert.Equal<TextType>(Separator, characterToTextType SeparatorCharacter)

    [<Fact>]
    member this.wordToTextTypeOnNormalWordReturnsNormalTextType() =         
        Assert.Equal<TextType>(Normal, wordToTextType (NormalWord "a"))

    [<Fact>]
    member this.wordToTextTypeOnPunctuationWordReturnsPunctuationTextType() =         
        Assert.Equal<TextType>(Punctuation, wordToTextType (PunctuationWord "."))

    [<Fact>]
    member this.wordToTextTypeOnPunctuationWordReturnsSeparatorTextType() =         
        Assert.Equal<TextType>(Separator, wordToTextType SeparatorWord)

    [<Fact>]
    member this.toStringOnNormalCharacterReturnSeparatorString() =         
        Assert.Equal<string>("a", (NormalCharacter 'a').ToString())

    [<Fact>]
    member this.toStringOnPunctuationCharacterReturnSeparatorString() =         
        Assert.Equal<string>(".", (PunctuationCharacter '.').ToString())

    [<Fact>]
    member this.toStringOnSeparatorCharacterReturnsSpace() =         
        Assert.Equal<string>(" ", SeparatorCharacter.ToString())

    [<Fact>]
    member this.characterToStringOnNormalCharacterReturnSeparatorString() =         
        Assert.Equal<string>("a", characterToString (NormalCharacter 'a'))

    [<Fact>]
    member this.characterToStringOnPunctuationCharacterReturnSeparatorString() =         
        Assert.Equal<string>(".", characterToString (PunctuationCharacter '.'))

    [<Fact>]
    member this.characterToStringOnSeparatorCharacterReturnsSpace() =         
        Assert.Equal<string>(" ", characterToString SeparatorCharacter)

    [<Fact>]
    member this.charactersToStringReturnsConcatenatedWords() =         
        Assert.Equal<string>("hey", charactersToString [NormalCharacter 'h'; NormalCharacter 'e'; NormalCharacter 'y'])

    [<Fact>]
    member this.charactersToStringOnEmptyListReturnsEmptyString() =         
        Assert.Equal<string>("", charactersToString [])

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
    member this.wordToStringOnSeparatorWordReturnsSpace() =         
        Assert.Equal<string>(" ", wordToString SeparatorWord)

    [<Fact>]
    member this.wordToStringOnPunctuationWordReturnsPunctuation() =         
        Assert.Equal<string>(".", wordToString (PunctuationWord "."))

    [<Fact>]
    member this.wordToStringOnNormalWordReturnsWord() =         
        Assert.Equal<string>("hey", wordToString (NormalWord "hey"))

    [<Fact>]
    member this.wordsToStringReturnsConcatenatedWords() =         
        Assert.Equal<string>("hello world!", wordsToString [NormalWord "hello"; SeparatorWord; NormalWord "world"; PunctuationWord "!"])

    [<Fact>]
    member this.wordsToStringOnEmptyListReturnsEmptyString() =         
        Assert.Equal<string>("", wordsToString [])