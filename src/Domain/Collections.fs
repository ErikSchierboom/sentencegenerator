module SentenceGenerator.Domain.Collections

    open System
    open System.Collections.Generic

    type FixedSizeQueue<'T when 'T : equality>(collection: 'T list) =
        let queue = new Queue<'T>(collection);
        
        member val Size = collection.Length with get
        member this.Items with get() = List.ofSeq queue : 'T list       
        
        member q.Enqueue(item) = 
            queue.Dequeue() |> ignore
            queue.Enqueue(item)
    
        
    