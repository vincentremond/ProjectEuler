let rec fill target coins prev: (int * int) list seq =
    seq {
        match coins with
        | coinValue :: remainingCoins ->
            for value in 0 .. coinValue .. target do
                yield! fill (target - value) remainingCoins ((coinValue, value) :: prev)
        | [] -> yield ((1, target) :: prev)
    }

let s = System.Diagnostics.Stopwatch.StartNew()
fill 200 [ 200; 100; 50; 20; 10; 5; 2 ] [] |> Seq.length |> printfn "%A %A" (s.Elapsed)
