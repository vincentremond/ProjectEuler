let div = 10
let findMaxRecurring num =
    let remainderIndexes = Map.empty

    let rec findMaxRecurring' prev curr i =
        let remainder = curr % num

        if remainder = 0 then
            0
        else
            match (prev |> Map.tryFind remainder) with
            | Some pi -> (i - pi)
            | None -> findMaxRecurring' (Map.add remainder i prev) (remainder * 10) (i + 1)

    let result = findMaxRecurring' remainderIndexes (div * 10) 1
    (num, result)

let max = { 2 .. 999 } |> Seq.map findMaxRecurring |> Seq.sortByDescending (fun (x,l) -> l) |> Seq.head
printfn "%A" max
