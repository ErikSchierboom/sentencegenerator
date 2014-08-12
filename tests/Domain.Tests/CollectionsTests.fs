namespace SentenceGenerator.Domain.Tests

open SentenceGenerator.Domain.Collections
open System
open Xunit
open Xunit.Extensions

type CollectionsTests() =     

    [<Fact>]
    member this.constructorSetsItemsToCollectionParameter() =        
        let collection = [1; 2; 3]
        let fixedSizeQueue = new FixedSizeQueue<int>(collection)
        Assert.Equal<int list>(collection, fixedSizeQueue.Items)

    [<Fact>]
    member this.constructorSetsSizeToSizeOfCollectionParameter() =        
        let collection = [1; 2; 3]
        let fixedSizeQueue = new FixedSizeQueue<int>(collection)
        Assert.Equal<int>(3, fixedSizeQueue.Size)

    [<Fact>]
    member this.enqueueWillDequeueTheOldestItemAndEnqueueTheArgument() =        
        let collection = [1; 2; 3]
        let fixedSizeQueue = new FixedSizeQueue<int>(collection)
        fixedSizeQueue.Enqueue(4) |> ignore
        Assert.Equal<int list>([2; 3; 4], fixedSizeQueue.Items)