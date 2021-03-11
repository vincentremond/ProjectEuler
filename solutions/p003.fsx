let rec isprime x =
    primes
    |> Seq.takeWhile (fun i -> i * i <= x)
    |> Seq.forall (fun i -> x % i <> 0)

and primes =
    seq {
        yield 2

        yield!
            (Seq.unfold (fun i -> Some(i, i + 2)) 3)
            |> Seq.filter isprime
    }

// TODO
