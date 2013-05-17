namespace StudioDonder.SentenceGenerator.Domain.Tests

open System
open StudioDonder.SentenceGenerator.Domain.Strings

open Xunit
open Xunit.Extensions

type StringsTests() = 

    [<Fact>]
    member this.concatOnEmptyListReturnsEmptyString() =        
        Assert.Equal<string>("", concat [])

    [<Fact>]
    member this.concatOnyListWithOneStringReturnsString() =        
        Assert.Equal<string>("hello", concat ["hello"])

    [<Fact>]
    member this.concatOnyListWithMoreThanOneStringReturnsStringsConcatenated() =       
        Assert.Equal<string>("hello there", concat ["hello"; " "; "there"])

    [<Fact>]
    member this.toCharacterListOnEmptyStringReturnsEmptyString() =  
        Assert.Equal<char list>([], toCharacterList "")

    [<Fact>]
    member this.toCharacterListReturnsCharactersAsList() =  
        Assert.Equal<char list>(['a'; ' '; 'b'], toCharacterList "a b")

    [<Fact>]
    member this.toCharacterListOnNullStringThrowsArgumentNullException() =  
        Assert.Throws<ArgumentNullException>(fun() -> toCharacterList null |> ignore)