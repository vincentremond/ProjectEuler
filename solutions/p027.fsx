open PrimeLib

let expr a b n : int64 = (n * n) + (a * n) + (b) |> abs

let primes = (new PrimeList())

let getMaxN a b =
  0L
  |> Seq.unfold (fun n -> Some(n, n + 1L))
  |> Seq.map (fun n -> (n, (expr a b n)))
  |> Seq.map (fun (n, result) -> (n, (primes.IsPrime(result))))
  |> Seq.find (fun (_, prime) -> not prime)
  |> fst

let rec getMaxPrime =
  seq {
    for a in -999L .. 999L do
      printf "A=%A" a

      for b in -1000L .. 1000L do
        ((getMaxN a b), a, b)
  }

let (max, a, b) =
  getMaxPrime |> Seq.sortByDescending (fun (m, _, _) -> m) |> Seq.head

printfn "Max: %i A=%i B=%i A·B=%i" max a b (a * b)
