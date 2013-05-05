namespace StudioDonder.SentenceGenerator.Domain.Tests

open StudioDonder.SentenceGenerator.Domain.Markov
open System
open Xunit
open Xunit.Extensions

type MarkovTests() =     

 [<Fact>]
    member this.elementsWithCountReturnsElementsWithCount() =        
        Assert.Equal<ElementWithCount<string>>([{ Element = "there"; Count = 2 }; 
                                                { Element = "hello"; Count = 1 }; 
                                                { Element = "world"; Count = 1 }], 
                                                elementsWithCount ["there"; "hello"; "there"; "world"])
    
    [<Fact>]
    member this.elementsWithCountOnEmptyListDoesNotThrowException() =  
        Assert.DoesNotThrow(fun() -> elementsWithCount [] |> ignore)

    [<Fact>]
    member this.elementsWithProbabilitiesReturnsElementsWithProbabilities() =        
        let elementsWithProbabilities = elementsWithProbabilities ["there"; "hello"; "there"; "world"]
        Assert.Equal<ElementWithProbability<string>>([{ Element = "there"; Probability = 0.5 }; 
                                                      { Element = "hello"; Probability = 0.25 }; 
                                                      { Element = "world"; Probability = 0.25 }],
                                                      elementsWithProbabilities)
    
    [<Fact>]
    member this.elementsWithProbabilitiesOnEmptyListDoesNotThrowException() =  
        Assert.DoesNotThrow(fun() -> elementsWithProbabilities [] |> ignore)

    [<Fact>]
    member this.createChainLinksWithChainSizeIsOneAndOnlyOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinks = createChainLinks 1 ["hello"; "there"; "world"; "!"]
        Assert.Equal<ChainLink<string> list>([{ Chain = ["hello"]; Successors = ["there"] }; 
                                              { Chain = ["there"];  Successors = ["world"] }; 
                                              { Chain = ["world"]; Successors = ["!"] }], 
                                              chainLinks)

    [<Fact>]
    member this.createChainLinksWithChainSizeIsGreaterThanOneAndOnlyOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinks = createChainLinks 2 ["hello"; "there"; "world"; "!"]
        Assert.Equal<ChainLink<string> list>([{ Chain = ["hello"; "there"]; Successors = ["world"] }; 
                                              { Chain = ["there"; "world"]; Successors = ["!"] }], 
                                              chainLinks)                

    [<Fact>]
    member this.createChainLinksWithChainSizeIsOneAndMoreThanOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinks = createChainLinks 1 ["hello"; "there"; "hello"; "!"]
        Assert.Equal<ChainLink<string> list>([{ Chain = ["hello"]; Successors = ["there"; "!"] }; 
                                              { Chain = ["there"];  Successors = ["hello"] }], 
                                              chainLinks)

    [<Fact>]
    member this.createChainLinksWithChainSizeIsGreaterThanOneAndMoreThanOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinks = createChainLinks 2 ["hello"; "there"; "hello"; "there"; "!"]
        Assert.Equal<ChainLink<string> list>([{ Chain = ["hello"; "there"]; Successors = ["hello"; "!"] }; 
                                              { Chain = ["there"; "hello"]; Successors = ["there"] }], 
                                              chainLinks)

    [<Fact>]
    member this.createChainLinksWithDuplicateSuccessorsReturnsDuplicates() =        
        let chainLinks = createChainLinks 1 ["hello"; "there"; "hello"; "there"]
        Assert.Equal<ChainLink<string> list>([{ Chain = ["hello"]; Successors = ["there"; "there"] }; 
                                              { Chain = ["there"];  Successors = ["hello"] }], 
                                              chainLinks)

    [<Theory>]
    [<InlineData(0)>]
    [<InlineData(-1)>]
    member this.createChainLinksWithChainSizeIsInvalidThrowsException(chainSize) =  
        Assert.Throws<Exception>(fun() -> createChainLinks chainSize ["hello"; "there"; "world"; "!"] |> ignore)

    [<Fact>]
    member this.createChainLinksWithCountWithChainSizeIsOneAndOnlyOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinksWithCount = createChainLinksWithCount 1 ["hello"; "there"; "world"; "!"]
        Assert.Equal<ChainLinkWithCount<string> list>([{ Chain = ["hello"]; SuccessorsWithCount = [{ Element = "there"; Count = 1 }] }; 
                                                       { Chain = ["there"]; SuccessorsWithCount = [{ Element = "world"; Count = 1 }] }; 
                                                       { Chain = ["world"]; SuccessorsWithCount = [{ Element = "!"; Count = 1 }] }], 
                                                       chainLinksWithCount)

    [<Fact>]
    member this.createChainLinksWithCountWithChainSizeIsOneAndMoreThanOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinksWithCount = createChainLinksWithCount 1 ["hello"; "there"; "hello"; "!"]
        Assert.Equal<ChainLinkWithCount<string> list>([{ Chain = ["hello"]; SuccessorsWithCount = [{ Element = "there"; Count = 1 }; { Element = "!"; Count = 1 }] }; 
                                                       { Chain = ["there"]; SuccessorsWithCount = [{ Element = "hello"; Count = 1 }] }], 
                                                       chainLinksWithCount)

    [<Fact>]
    member this.createChainLinksWithCountWithChainSizeIsOneAndDuplicateSuccessorsReturnsCorrectChainLinks() =        
        let chainLinksWithCount = createChainLinksWithCount 1 ["hello"; "there"; "hello"; "there"]
        Assert.Equal<ChainLinkWithCount<string> list>([{ Chain = ["hello"]; SuccessorsWithCount = [{ Element = "there"; Count = 2 }] }; 
                                                       { Chain = ["there"]; SuccessorsWithCount = [{ Element = "hello"; Count = 1 }] }], 
                                                       chainLinksWithCount)

    [<Fact>]
    member this.createChainLinksWithCountWithChainSizeIsTwoAndOnlyOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinksWithCount = createChainLinksWithCount 2 ["hello"; "there"; "world"; "!"]
        Assert.Equal<ChainLinkWithCount<string> list>([{ Chain = ["hello"; "there"]; SuccessorsWithCount = [{ Element = "world"; Count = 1 }] }; 
                                                       { Chain = ["there"; "world"]; SuccessorsWithCount = [{ Element = "!"; Count = 1 }] }], 
                                                       chainLinksWithCount)

    [<Fact>]
    member this.createChainLinksWithCountWithChainSizeIsTwoAndMoreThanOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinksWithCount = createChainLinksWithCount 2 ["hello"; "there"; "hello"; "there"; "!"]
        Assert.Equal<ChainLinkWithCount<string> list>([{ Chain = ["hello"; "there"]; SuccessorsWithCount = [{ Element = "hello"; Count = 1 }; { Element = "!"; Count = 1 }] }; 
                                                       { Chain = ["there"; "hello"]; SuccessorsWithCount = [{ Element = "there"; Count = 1 }] }], 
                                                       chainLinksWithCount)

    [<Fact>]
    member this.createChainLinksWithCountWithChainSizeIsTwoAndDuplicateSuccessorsReturnsCorrectChainLinks() =        
        let chainLinksWithCount = createChainLinksWithCount 2 ["hello"; "there"; "hello"; "there"; "hello"]
        Assert.Equal<ChainLinkWithCount<string> list>([{ Chain = ["hello"; "there"]; SuccessorsWithCount = [{ Element = "hello"; Count = 2 }] }; 
                                                       { Chain = ["there"; "hello"]; SuccessorsWithCount = [{ Element = "there"; Count = 1 }] }], 
                                                       chainLinksWithCount)                                

    [<Fact>]
    member this.createChainLinksWithProbabilityWithChainSizeIsOneAndOnlyOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinksWithProbability = createChainLinksWithProbability 1 ["hello"; "there"; "world"; "!"]
        Assert.Equal<ChainLinkWithProbabilities<string> list>([{ Chain = ["hello"]; SuccessorsWithProbabilities = [{ Element = "there"; Probability = 1.0 }] }; 
                                                               { Chain = ["there"]; SuccessorsWithProbabilities = [{ Element = "world"; Probability = 1.0 }] }; 
                                                               { Chain = ["world"]; SuccessorsWithProbabilities = [{ Element = "!"; Probability = 1.0 }] }], 
                                                               chainLinksWithProbability)

    [<Fact>]
    member this.createChainLinksWithProbabilityWithChainSizeIsOneAndMoreThanOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinksWithProbability = createChainLinksWithProbability 1 ["hello"; "there"; "hello"; "!"]
        Assert.Equal<ChainLinkWithProbabilities<string> list>([{ Chain = ["hello"]; SuccessorsWithProbabilities = [{ Element = "there"; Probability = 0.5 }; { Element = "!"; Probability = 0.5 }] }; 
                                                               { Chain = ["there"]; SuccessorsWithProbabilities = [{ Element = "hello"; Probability = 1.0 }] }], 
                                                               chainLinksWithProbability)

    [<Fact>]
    member this.createChainLinksWithProbabilityWithChainSizeIsOneAndDuplicateSuccessorsReturnsCorrectChainLinks() =        
        let chainLinksWithProbability = createChainLinksWithProbability 1 ["hello"; "there"; "hello"; "there"]
        Assert.Equal<ChainLinkWithProbabilities<string> list>([{ Chain = ["hello"]; SuccessorsWithProbabilities = [{ Element = "there"; Probability = 1.0 }] }; 
                                                               { Chain = ["there"]; SuccessorsWithProbabilities = [{ Element = "hello"; Probability = 1.0 }] }], 
                                                               chainLinksWithProbability)

    [<Fact>]
    member this.createChainLinksWithProbabilityWithChainSizeIsTwoAndOnlyOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinksWithProbability = createChainLinksWithProbability 2 ["hello"; "there"; "world"; "!"]
        Assert.Equal<ChainLinkWithProbabilities<string> list>([{ Chain = ["hello"; "there"]; SuccessorsWithProbabilities = [{ Element = "world"; Probability = 1.0 }] }; 
                                                               { Chain = ["there"; "world"]; SuccessorsWithProbabilities = [{ Element = "!"; Probability = 1.0 }] }], 
                                                               chainLinksWithProbability)

    [<Fact>]
    member this.createChainLinksWithProbabilityWithChainSizeIsTwoAndMoreThanOneSuccessorPerChainReturnsCorrectChainLinks() =        
        let chainLinksWithProbability = createChainLinksWithProbability 2 ["hello"; "there"; "hello"; "there"; "!"]
        Assert.Equal<ChainLinkWithProbabilities<string> list>([{ Chain = ["hello"; "there"]; SuccessorsWithProbabilities = [{ Element = "hello"; Probability = 0.5 }; { Element = "!"; Probability = 0.5 }] }; 
                                                               { Chain = ["there"; "hello"]; SuccessorsWithProbabilities = [{ Element = "there"; Probability = 1.0 }] }], 
                                                               chainLinksWithProbability)

    [<Fact>]
    member this.createChainLinksWithProbabilityWithChainSizeIsTwoAndDuplicateSuccessorsReturnsCorrectChainLinks() =        
        let chainLinksWithProbability = createChainLinksWithProbability 2 ["hello"; "there"; "hello"; "there"; "hello"]
        Assert.Equal<ChainLinkWithProbabilities<string> list>([{ Chain = ["hello"; "there"]; SuccessorsWithProbabilities = [{ Element = "hello"; Probability = 1.0 }] }; 
                                                               { Chain = ["there"; "hello"]; SuccessorsWithProbabilities = [{ Element = "there"; Probability = 1.0 }] }], 
                                                               chainLinksWithProbability)