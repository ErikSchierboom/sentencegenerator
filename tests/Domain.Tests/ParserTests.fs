namespace Domain.Tests

open Domain.Parser
open Xunit
open Xunit.Extensions

type ParserTests() = 

    [<Theory>]
    [<InlineData(' ')>]
    [<InlineData('\t')>]
    [<InlineData('\n')>]
    [<InlineData(';')>]
    [<InlineData(',')>]
    [<InlineData('.')>]
    [<InlineData('!')>]
    [<InlineData('?')>]
    member this.isWordSeparatorOnSeparatorCharacterReturnsTrue(c) =
        Assert.True(isWordSeparator c)

    [<Theory>]
    [<InlineData('<')>]
    [<InlineData('>')>]
    [<InlineData('/')>]
    [<InlineData('\\')>]
    [<InlineData('a')>]
    [<InlineData('1')>]
    member this.isWordSeparatorOnNonSeparatorCharacterReturnsFalse(c) =
        Assert.False(isWordSeparator c)

    [<Theory>]
    [<InlineData("")>]
    [<InlineData(" ")>]
    [<InlineData("  ")>]
    [<InlineData("\t")>]
    [<InlineData("\n")>]
    member this.splitWordsOnWhiteSpaceStringReturnsEmptyList(wordsString) =
        let words = splitWords wordsString        
        Assert.True(words.IsEmpty)

    [<Theory>]
    [<InlineData("hello there world")>]
    [<InlineData("hello\nthere\nworld")>]
    [<InlineData("hello\tthere\tworld")>]
    [<InlineData("hello;there;world")>]
    [<InlineData("hello,there,world")>]
    [<InlineData("hello.there.world")>]
    [<InlineData("hello!there!world")>]    
    [<InlineData("hello?there?world")>]
    member this.splitWordsReturnsListOfWords(wordsString) =
        let words = splitWords wordsString
        Assert.True(["hello"; "there"; "world"] = words)

    [<Fact>]  
    member this.sanitizeWordReturnsLowerCaseString() =
        Assert.Equal<string>("hello there world", (sanitizeWord "HELLO THERE woRLD"))

    [<Fact>]
    member this.parseReturnsInputSplitIntoWordsAndSanitized() =
        let parsedWords = parse " HELLO!THERE;   woRLD "
        Assert.True(["hello"; "there"; "world"] = parsedWords)