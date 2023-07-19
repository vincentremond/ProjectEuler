let primes = [| 2L; 3L; 5L; 7L; 11L; 13L; 17L; 19L |]

let mapFolder2 state item =
    let rec div a b c =
        match a % b with
        | 0L -> div (a / b) (b) (c + 1L)
        | _ -> a, c

    match (div state item 0L) with
    | _, 0L -> (None, state)
    | newValue, count -> (Some(item, count), newValue)

let testValue = 600851475143L

primes
|> Seq.mapFold mapFolder2 testValue
|> fst
|> Seq.choose id
|> Seq.toArray
|> (printfn "%A")
