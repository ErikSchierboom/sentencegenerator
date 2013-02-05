namespace Domain.Tests

open Domain.Parser
open Xunit
open Xunit.Extensions

type ParserTests() =     

    [<Fact>]
    member this.parseReturnsInputSplitIntoWordsAndSanitized() =
        let parsedWords = parse " HELLO!THERE;   woRLD? "
        Assert.True(["hello"; "!"; "there"; ";"; "world"; "?"] = parsedWords)