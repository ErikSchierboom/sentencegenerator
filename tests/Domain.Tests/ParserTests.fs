namespace Domain.Tests

open Domain.Parser
open Xunit
open Xunit.Extensions

type ParserTests() = 

    [<Theory>]
    [<InlineData(' ')>]
    [<InlineData('\t')>]
    [<InlineData('\n')>]
    member this.isWordSeparatorOnSeparatorCharacterReturnsTrue(c) =
        Assert.True(isWordSeparator c)

    [<Theory>]
    [<InlineData('<')>]
    [<InlineData('>')>]
    [<InlineData('/')>]
    [<InlineData('\\')>]
    [<InlineData('a')>]
    [<InlineData('1')>]
    [<InlineData(';')>]
    [<InlineData(',')>]
    [<InlineData('.')>]
    [<InlineData('!')>]
    [<InlineData('?')>]
    member this.isWordSeparatorOnNonSeparatorCharacterReturnsFalse(c) =
        Assert.False(isWordSeparator c)

    [<Theory>]
    [<InlineData("hello;")>]
    [<InlineData("hello,")>]
    [<InlineData("hello.")>]
    [<InlineData("hello!")>]
    [<InlineData("hello?")>]
    member this.convertPunctuationCharactersToWords(wordsString) =        
        let convertedWord = convertPunctuationCharactersToWords wordsString        
        Assert.Equal<string>("hello " + wordsString.Substring(5) + " ", convertedWord)

    [<Fact>]
    member this.splitOnWithNoMatchForPassedFuncReturnsStringArgumentInList() =
        let words = splitOn (fun c -> c = ' ') "hello" System.StringSplitOptions.RemoveEmptyEntries
        Assert.True(["hello"] = words)

    [<Fact>]
    member this.splitOnWithMatchesForPassedFuncReturnsListWithStringArgumentSplitUsingFunc() =
        let words = splitOn (fun c -> c = ' ') "hello there world " System.StringSplitOptions.RemoveEmptyEntries
        Assert.True(["hello"; "there"; "world"] = words)

    [<Fact>]
    member this.splitOnWithStringSplitOptionsIsNoneReturnsEmptyEntries() =
        let words = splitOn (fun c -> c = ' ') "hello " System.StringSplitOptions.None
        Assert.True(["hello"; ""] = words)

    [<Fact>]
    member this.splitOnWithStringSplitOptionsIsRemoveEmptyEntriesDoesNotReturnEmptyEntries() =
        let words = splitOn (fun c -> c = ' ') "hello " System.StringSplitOptions.RemoveEmptyEntries
        Assert.True(["hello"] = words)

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
    member this.sanitizeWordReturnsLowerCaseString() =
        Assert.Equal<string>("hello there world", (sanitizeWord "HELLO THERE woRLD"))

    [<Fact>]
    member this.parseReturnsInputSplitIntoWordsAndSanitized() =
        let parsedWords = parse " HELLO!THERE;   woRLD "
        Assert.True(["hello"; "!"; "there"; ";"; "world"] = parsedWords)