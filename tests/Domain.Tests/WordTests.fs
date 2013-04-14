namespace StudioDonder.SentenceGenerator.Domain.Tests

open StudioDonder.SentenceGenerator.Domain.Word
open Xunit
open Xunit.Extensions

type WordTests() = 

    [<Theory>]
    [<InlineData(";")>]
    [<InlineData(",")>]
    [<InlineData(".")>]
    [<InlineData("!")>]
    [<InlineData("?")>]
    member this.isPunctuationOnPunctuationCharacterReturnsTrue(word) =         
        Assert.True(isPunctuation word)

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
    member this.isPunctuationOnNonPunctuationCharacterReturnsFalse(word) =         
        Assert.False(isPunctuation word)

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
    member this.sanitizeTrimsWord() =  
        Assert.Equal<string>("hello", sanitize " hello ")

    [<Fact>]  
    member this.sanitizeReturnsLowerCaseString() =
        Assert.Equal<string>("hello there world", sanitize "HELLO THERE woRLD")

    [<Fact>]  
    member this.sanitizeMarksPunctuationCharactersAsWords() =
        Assert.Equal<string>("h ; e , l . l ! o ? ", sanitize "h;e,l.l!o?")

    [<Theory>]
    [<InlineData("hello world")>]
    [<InlineData("hello\tworld")>]
    [<InlineData("hello\nworld")>]
    member this.splitReturnsWordSplitBySeparator(wordsString) =
        Assert.True(["hello"; "world"] = split wordsString)

    [<Theory>]
    [<InlineData("")>]
    [<InlineData(" ")>]
    [<InlineData("  ")>]
    [<InlineData("\t")>]
    [<InlineData("\n")>]
    member this.parseOnWhiteSpaceStringReturnsEmptyList(wordsString) =
        Assert.True((parse wordsString).IsEmpty)

    [<Fact>]    
    member this.parseTreatsPunctuationCharactersAsWords() =
        Assert.True(["hello"; ";"; "there"; ","; "world"; "."; "hello"; "!"; "there"; "?"] = parse "hello;there,world.hello!there?")

    [<Theory>]
    [<InlineData("hello there world")>]
    [<InlineData("hello\nthere\nworld")>]
    [<InlineData("hello\tthere\tworld")>]
    member this.parseReturnsListOfWords(wordsString) =
        Assert.True(["hello"; "there"; "world"] = parse wordsString)

    [<Fact>]
    member this.parseDoesNotReturnEmptyWords() =
        Assert.True(["hello"; "there"; "world"; "?"] = parse " hello\t  there world?  ")