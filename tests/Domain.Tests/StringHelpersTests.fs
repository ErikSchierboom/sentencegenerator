namespace Domain.Tests

open Domain.StringHelpers
open Xunit
open Xunit.Extensions

type StringHelpersTests() = 

    [<Theory>]
    [<InlineData("hello;")>]
    [<InlineData("hello,")>]
    [<InlineData("hello.")>]
    [<InlineData("hello!")>]
    [<InlineData("hello?")>]
    member this.convertPunctuationCharactersToWords(wordsString) =        
        let convertedWord = convertPunctuationCharactersToWords wordsString        
        Assert.Equal<string>("hello " + wordsString.Substring(5) + " ", convertedWord)

    [<Theory>]
    [<InlineData("hello world")>]
    [<InlineData("hello\tworld")>]
    [<InlineData("hello\nworld")>]
    member this.splitWordReturnsWordSplitBySeparator(wordsString) =
        let words = splitWord wordsString        
        Assert.True(["hello"; "world"] = words)

    [<Theory>]
    [<InlineData("")>]
    [<InlineData(" ")>]
    [<InlineData("  ")>]
    [<InlineData("\t")>]
    [<InlineData("\n")>]
    member this.splitWordsOnWhiteSpaceStringReturnsEmptyList(wordsString) =
        let words = splitWords wordsString        
        Assert.True(words.IsEmpty)

    [<Fact>]    
    member this.splitWordsTreatsPunctuationCharactersAsWords() =
        let words = splitWords "hello;there,world.hello!there?"
        Assert.True(["hello"; ";"; "there"; ","; "world"; "."; "hello"; "!"; "there"; "?"] = words)

    [<Theory>]
    [<InlineData("hello there world")>]
    [<InlineData("hello\nthere\nworld")>]
    [<InlineData("hello\tthere\tworld")>]
    member this.splitWordsReturnsListOfWords(wordsString) =
        let words = splitWords wordsString
        Assert.True(["hello"; "there"; "world"] = words)

    [<Fact>]
    member this.splitWordsDoesNotReturnEmptyWords() =
        let words = splitWords " hello\t there world  "        
        Assert.True(["hello"; "there"; "world"] = words)

    [<Fact>]  
    member this.sanitizeWordReturnsLowerCaseString() =
        Assert.Equal<string>("hello there world", (sanitizeWord "HELLO THERE woRLD"))