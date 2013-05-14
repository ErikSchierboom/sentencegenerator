module StudioDonder.SentenceGenerator.Domain.Random

    open System

    let private random = new Random()

    let meetsProbability probability = 
        if probability < 0.0 || probability > 1.0  then invalidArg "probability" "The probability must be in the range [0.0 .. 1.0]."
        random.NextDouble() <= probability

    let doesNotMeetProbability probability = 
        if probability < 0.0 || probability > 1.0  then invalidArg "probability" "The probability must be in the range [0.0 .. 1.0]."
        random.NextDouble() > probability

