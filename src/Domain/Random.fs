namespace StudioDonder.SentenceGenerator.Domain

open System

module Random =

    let private random = new Random()

    let meetsProbability probability = 
        if probability < 0.0 || probability > 1.0  then failwith "The probability must be in the range [0.0 .. 1.0]."
        random.NextDouble() <= probability

    let doesNotMeetProbability probability = 
        if probability < 0.0 || probability > 1.0  then failwith "The probability must be in the range [0.0 .. 1.0]."
        random.NextDouble() > probability

