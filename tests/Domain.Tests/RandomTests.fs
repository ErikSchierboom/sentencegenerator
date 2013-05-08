namespace StudioDonder.SentenceGenerator.Domain.Tests

open StudioDonder.SentenceGenerator.Domain.Random
open System
open Xunit
open Xunit.Extensions

type RandomTests() = 

    [<Fact>]  
    member this.meetsProbabilityWithProbabilityIsOneReturnsTrue() =
        let probabilities = List.init 30 (fun _ -> meetsProbability 1.0)
        Assert.True(List.forall (fun p -> p) probabilities)

    [<Fact>]  
    member this.meetsProbabilityWithProbabilityIsZeroReturnsFalse() =
        let probabilities = List.init 30 (fun _ -> meetsProbability 0.0)
        Assert.True(List.forall (fun p -> not p) probabilities)

    [<Fact>]  
    member this.meetsProbabilityWithProbabilityIsLessThanZeroThrowsException() =
        Assert.Throws<Exception>(fun() -> meetsProbability -0.1 |> ignore)

    [<Fact>]  
    member this.meetsProbabilityWithProbabilityIsGreaterThanOneThrowsException() =
        Assert.Throws<Exception>(fun() -> meetsProbability 1.1 |> ignore)

    [<Fact>]  
    member this.doesNotMeetProbabilityWithProbabilityIsOneReturnsFalse() =
        let probabilities = List.init 30 (fun _ -> doesNotMeetProbability 1.0)
        Assert.True(List.forall (fun p -> not p) probabilities)

    [<Fact>]  
    member this.doesNotMeetProbabilityWithProbabilityIsZeroReturnsTrue() =
        let probabilities = List.init 30 (fun _ -> doesNotMeetProbability 0.0)
        Assert.True(List.forall (fun p -> p) probabilities)

    [<Fact>]  
    member this.doesNotMeetProbabilityWithProbabilityIsLessThanZeroThrowsException() =
        Assert.Throws<Exception>(fun() -> doesNotMeetProbability -0.1 |> ignore)

    [<Fact>]  
    member this.doesNotMeetProbabilityWithProbabilityIsGreaterThanOneThrowsException() =
        Assert.Throws<Exception>(fun() -> doesNotMeetProbability 1.1 |> ignore)
