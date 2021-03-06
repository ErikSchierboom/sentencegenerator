﻿namespace SentenceGenerator.Domain.Tests

open SentenceGenerator.Domain.SentenceGenerator
open SentenceGenerator.Domain.Word
open System
open Xunit
open Xunit.Extensions

type SentenceGeneratorTests() = 

    [<Fact>]
    member this.newSentenceOnPunctuationWordAndNormalWordPairReturnsTrue() =
        Assert.True(newSentence (PunctuationWord "!", NormalWord "a"))

    [<Fact>]
    member this.newSentenceOnPunctuationWordAndPunctuationWordPairReturnsFalse() =
        Assert.False(newSentence (PunctuationWord "!", PunctuationWord "!"))

    [<Fact>]
    member this.newSentenceOnPunctuationWordAndSeparatorWordPairReturnsFalse() =
        Assert.False(newSentence (PunctuationWord "!", SeparatorWord))

    [<Fact>]
    member this.newSentenceOnSeparatorWordAndNormalWordPairReturnsFalse() =
        Assert.False(newSentence (SeparatorWord, NormalWord "a"))

    [<Fact>]
    member this.newSentenceOnSeparatorWordAndPunctuationWordPairReturnsFalse() =
        Assert.False(newSentence (SeparatorWord, PunctuationWord "!"))

    [<Fact>]
    member this.newSentenceOnSeparatorWordAndSeparatorWordPairReturnsFalse() =
        Assert.False(newSentence (SeparatorWord, SeparatorWord))

    [<Fact>]
    member this.newSentenceOnNormalWordAndNormalWordPairReturnsFalse() =
        Assert.False(newSentence (NormalWord "a", NormalWord "a"))

    [<Fact>]
    member this.newSentenceOnNormalWordAndPunctuationWordPairReturnsFalse() =
        Assert.False(newSentence (NormalWord "a", PunctuationWord "!"))

    [<Fact>]
    member this.newSentenceOnNormalWordAndSeparatorWordPairReturnsFalse() =
        Assert.False(newSentence (NormalWord "a", SeparatorWord))

    [<Fact>]
    member this.numberOfWordsOnEmptyListReturnsZero() =
        Assert.Equal<int>(0, numberOfWords [])

    [<Fact>]
    member this.numberOfWordsReturnsNumberOfNormalWords() =
        Assert.Equal<int>(2, numberOfWords [NormalWord "hello"; PunctuationWord "!"; SeparatorWord; PunctuationWord "?"; PunctuationWord ";"; NormalWord "there"])

    [<Fact>]
    member this.numberOfSentencesOnEmptyListReturnsZero() =
        Assert.Equal<int>(0, numberOfSentences [])

    [<Fact>]
    member this.numberOfSentencesOnWordsWithoutPunctuationCharacterReturnsOne() =
        Assert.Equal<int>(1, numberOfSentences [NormalWord "hello"; NormalWord "there"])
        
    [<Fact>]
    member this.numberOfSentencesOnWordsWithOnePunctuationCharacterAtTheEndReturnsOne() =
        Assert.Equal<int>(1, numberOfSentences [NormalWord "hello"; NormalWord "there"])
        
    [<Fact>]
    member this.numberOfSentencesOnWordsWithOnePunctuationCharacterInTheMiddleReturnsTwo() =
        Assert.Equal<int>(2, numberOfSentences [NormalWord "hello"; PunctuationWord "!"; NormalWord "there"])
        
    [<Fact>]
    member this.numberOfSentencesOnWordsWithOnePunctuationCharacterInTheMiddleAndOneAtTheEndReturnsTwo() =
        Assert.Equal<int>(2, numberOfSentences [NormalWord "hello"; PunctuationWord "!"; NormalWord "there"; PunctuationWord "?"])
        
    [<Fact>]
    member this.numberOfSentencesOnWordsWillIgnoreConsecutivePunctuationWords() =
        Assert.Equal<int>(2, numberOfSentences [NormalWord "hello"; PunctuationWord "!"; PunctuationWord "?"; PunctuationWord ";"; NormalWord "there"])

    [<Fact>]
    member this.findWordToStartWithOnListWithoutPunctuationCharactersReturnsNormalWordAtBegin() =
        Assert.Equal<Word>(NormalWord "hello", findWordToStartWith [NormalWord "hello"; NormalWord "there"])

    [<Fact>]
    member this.findWordToStartWithOnListWithOnePunctuationCharacterWithNoNormalWordAfterItReturnsNormalWordAtBegin() =
        Assert.Equal<Word>(NormalWord "hello", findWordToStartWith [NormalWord "hello"; PunctuationWord "!"])

    [<Fact>]
    member this.findWordToStartWithOnListWithOnePunctuationCharacterWithAPunctuationCharcerAfterItReturnsNormalWordAtBegin() =
        Assert.Equal<Word>(NormalWord "hello", findWordToStartWith [NormalWord "hello"; PunctuationWord "!"; PunctuationWord "?"])

    [<Fact>]
    member this.findWordToStartWithOnListWithOnePunctuationCharacterWithNormalWordAfterItWillReturnNormalWordAtBeginOrWordAfterPunctuationWord() =
        let validWords = [NormalWord "hello"; NormalWord "there"] |> set
        let foundWords = List.init 50 (fun _ -> findWordToStartWith [NormalWord "hello"; PunctuationWord "!"; NormalWord "there"]) |> set
        Assert.Equal<Set<Word>>(validWords, foundWords)

    [<Fact>]
    member this.findWordToStartWithOnListWithMoreThanOnePunctuationCharacterWithNormalWordAfterItWillReturnNormalWordAtBeginOrWordAfterPunctuationWord() =
        let validWords = [NormalWord "hello"; NormalWord "there"; NormalWord "world"] |> set
        let foundWords = List.init 50 (fun _ -> findWordToStartWith [NormalWord "hello"; PunctuationWord "!"; NormalWord "there"; PunctuationWord "!"; NormalWord "world"]) |> set
        Assert.Equal<Set<Word>>(validWords, foundWords)

    [<Fact>]
    member this.findWordToStartWithWillNotReturnPunctuationWord() =
        let validWords = [NormalWord "hello"] |> set
        let foundWords = List.init 50 (fun _ -> findWordToStartWith [NormalWord "hello"; PunctuationWord "!"; PunctuationWord "?"]) |> set
        Assert.Equal<Set<Word>>(validWords, foundWords)

    [<Fact>]
    member this.findWordToStartWithOnEmptyListThrowsArgumentException() =
        Assert.Throws<ArgumentException>(fun () -> findWordToStartWith [] |> ignore)

    [<Fact>]
    member this.addSeparatorsToWordsOnEmptyListReturnsEmptyList() =
        Assert.Equal<Word list>([], addSeparatorsToWords [])

    [<Fact>]
    member this.addSeparatorsToWordsWillAddSeparatorBetweenTwoNormalWords() =
        Assert.Equal<Word list>([NormalWord "hello"; SeparatorWord; NormalWord "there"], addSeparatorsToWords [NormalWord "hello"; NormalWord "there"])

    [<Fact>]
    member this.addSeparatorsToWordsWillNotAddSeparatorBetweenTwoPunctuationWords() =
        Assert.Equal<Word list>([PunctuationWord "!"; PunctuationWord "!"], addSeparatorsToWords [PunctuationWord "!"; PunctuationWord "!"])

    [<Fact>]
    member this.addSeparatorsToWordsWillNotAddSeparatorBetweenNormalWordAndPunctuationWord() =
        Assert.Equal<Word list>([NormalWord "hello"; PunctuationWord "!"], addSeparatorsToWords [NormalWord "hello"; PunctuationWord "!"])

    [<Fact>]
    member this.addSeparatorsToWordsWillAddSeparatorBetweenPunctuationWordAndNormalWord() =
        Assert.Equal<Word list>([PunctuationWord "!"; SeparatorWord; NormalWord "hello"], addSeparatorsToWords [PunctuationWord "!"; NormalWord "hello"])

    [<Fact>]
    member this.addSeparatorsToWordsWillAddSeparators() =
        Assert.Equal<Word list>([NormalWord "hello"; SeparatorWord; NormalWord "there"; PunctuationWord "!"; SeparatorWord; NormalWord "okay"; PunctuationWord "?"],
                                addSeparatorsToWords [NormalWord "hello"; NormalWord "there"; PunctuationWord "!"; NormalWord "okay"; PunctuationWord "?"])

    [<Fact>]
    member this.generateSentencesWillGenerateCorrectSentence() =
        Assert.Equal<string>("hello there",  generateSentences 1 2 "hello there world! or not?")