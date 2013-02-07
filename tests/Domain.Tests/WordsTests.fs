namespace Domain.Tests

open Domain.Words
open Xunit
open Xunit.Extensions

type WordsTests() = 

    [<Theory>]
    [<InlineData(";")>]
    [<InlineData(",")>]
    [<InlineData(".")>]
    [<InlineData("!")>]
    [<InlineData("?")>]
    member this.isPunctuationWordOnPunctuationCharacterReturnsTrue(word) =         
        Assert.True(isPunctuationWord word)

    [<Theory>]
    [<InlineData(" ")>]
    [<InlineData("\t")>]
    [<InlineData("\n")>]
    [<InlineData("a")>]
    [<InlineData("9")>]
    [<InlineData("-")>]
    [<InlineData("_")>]
    [<InlineData("/")>]
    [<InlineData("\\")>]
    [<InlineData("+")>]
    [<InlineData("[")>]
    [<InlineData("]")>]
    [<InlineData("|")>]
    member this.isPunctuationWordOnNonPunctuationCharacterReturnsFalse(word) =         
        Assert.False(isPunctuationWord word)

    [<Theory>]
    [<InlineData("hello;")>]
    [<InlineData("hello,")>]
    [<InlineData("hello.")>]
    [<InlineData("hello!")>]
    [<InlineData("hello?")>]
    member this.markPunctuationCharactersAsWordsSurroundsPunctuationCharactersWithSpaces(wordsString) = 
        let wordWithPunctuationCharactersMarked = markPunctuationCharactersAsWords wordsString
        Assert.Equal<string>("hello " + wordsString.Substring(5) + " ", wordWithPunctuationCharactersMarked)

    [<Fact>]
    member this.sanitizeWordTrimsWord() =  
        Assert.Equal<string>("hello", sanitizeWord " hello ")

    [<Fact>]  
    member this.sanitizeWordReturnsLowerCaseString() =
        Assert.Equal<string>("hello there world", sanitizeWord "HELLO THERE woRLD")

    [<Fact>]  
    member this.sanitizeWordMarksPunctuationCharactersAsWords() =
        Assert.Equal<string>("h ; e , l . l ! o ? ", sanitizeWord "h;e,l.l!o?")

    [<Theory>]
    [<InlineData("hello world")>]
    [<InlineData("hello\tworld")>]
    [<InlineData("hello\nworld")>]
    member this.splitWordReturnsWordSplitBySeparator(wordsString) =
        Assert.True(["hello"; "world"] = splitWord wordsString)

    [<Theory>]
    [<InlineData("")>]
    [<InlineData(" ")>]
    [<InlineData("  ")>]
    [<InlineData("\t")>]
    [<InlineData("\n")>]
    member this.splitWordsOnWhiteSpaceStringReturnsEmptyList(wordsString) =
        Assert.True((splitWords wordsString).IsEmpty)

    [<Fact>]    
    member this.splitWordsTreatsPunctuationCharactersAsWords() =
        Assert.True(["hello"; ";"; "there"; ","; "world"; "."; "hello"; "!"; "there"; "?"] = splitWords "hello;there,world.hello!there?")

    [<Theory>]
    [<InlineData("hello there world")>]
    [<InlineData("hello\nthere\nworld")>]
    [<InlineData("hello\tthere\tworld")>]
    member this.splitWordsReturnsListOfWords(wordsString) =
        Assert.True(["hello"; "there"; "world"] = splitWords wordsString)

    [<Fact>]
    member this.splitWordsDoesNotReturnEmptyWords() =
        Assert.True(["hello"; "there"; "world"; "?"] = splitWords " hello\t  there world?  ")