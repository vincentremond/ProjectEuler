let rec isprime x =
    primes
    |> Seq.takeWhile (fun i -> i * i <= x)
    |> Seq.forall (fun i -> x % i <> 0L)

and primes =
    seq {
        yield 2L

        yield!
            (Seq.unfold (fun i -> Some(i, i + 2L)) 3L)
            |> Seq.filter isprime
    }

//let mapFolder<'T, 'State, 'Result> (state: 'State) (item: 'T): ('Result * 'State option) =
let mapFold<'T, 'State, 'Result> (mapping: 'State -> 'T -> 'Result * 'State option) (state: 'State) (source: 'T seq): 'Result seq =
    seq {
        use enumerator = source.GetEnumerator()
        let mutable currentState = state
        let mutable loop = enumerator.MoveNext()

        while loop do
            let (resultItem, newState) = mapping currentState enumerator.Current
            yield resultItem

            loop <-
                match newState, enumerator.MoveNext() with
                | (Some s), true ->
                    currentState <- s
                    true
                | _, _ -> false
    }

let mapFolder state item =
    let rec div a b c =
        match a % b with
        | 0L -> div (a / b) (b) (c + 1L)
        | _ -> a, c

    match (div state item 0L) with
    | _, 0L -> (None, Some state)
    | newValue, count ->
        (Some(item, count),
         match newValue with
         | 1L -> None
         | _ -> Some newValue)

let testValue = 600851475143L

primes
|> mapFold mapFolder testValue
|> Seq.choose id
|> Seq.toArray
|> (printfn "%A")
