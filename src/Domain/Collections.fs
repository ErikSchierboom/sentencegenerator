namespace StudioDonder.SentenceGenerator.Domain

open System
open System.Collections.Generic

module Collections =

    type FixedSizeQueue<'T>(collection: 'T list) =
        let queue = new Queue<'T>(collection);
        
        member val Size = collection.Length with get
        member this.Items with get() = List.ofSeq queue        
        
        member q.Enqueue(item) = 
            queue.Dequeue() |> ignore
            queue.Enqueue(item)
    
        
    