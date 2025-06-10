let rec fibonacci =
  let generator state =
    let (current, previous1) = state
    let nextState = (current + previous1, current)
    Some(current, nextState)

  (1, 1) |> Seq.unfold generator

fibonacci
|> Seq.takeWhile (fun x -> x < 4000000)
|> Seq.filter (fun x -> x % 2 = 0)
|> Seq.sum
|> (printfn "%A")
