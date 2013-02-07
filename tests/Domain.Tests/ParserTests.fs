namespace Domain.Tests

open Domain.Parser
open Xunit
open Xunit.Extensions

type ParserTests() =     

    [<Fact>]
    member this.parseReturnsTextSplitIntoWordsAndSanitized() =
        let parsedWords = parse " HELLO!THERE;   woRLD? "
        Assert.True(["hello"; "!"; "there"; ";"; "world"; "?"] = parsedWords)