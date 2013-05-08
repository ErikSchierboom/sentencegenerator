namespace StudioDonder.SentenceGenerator.Domain.Tests

open StudioDonder.SentenceGenerator.Domain.SentenceGenerator
open StudioDonder.SentenceGenerator.Domain.Word
open Xunit
open Xunit.Extensions

type SentenceGeneratorTests() = 

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
        Assert.Equal<string>("hello there world! or not?",  generateSentences 1 "hello there world! or not?")