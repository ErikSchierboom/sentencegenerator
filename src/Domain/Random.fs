module StudioDonder.SentenceGenerator.Domain.Random

    open System

    let private random = new Random()

    let private verifyProbability comparison probability =
        match probability with 
        | _ when probability < 0.0 || probability > 1.0 -> invalidArg "probability" "The probability must be in the range [0.0 .. 1.0]."
        | _ -> comparison (random.NextDouble()) probability

    let meetsProbability probability = verifyProbability (<=) probability

    let doesNotMeetProbability probability = verifyProbability (>) probability