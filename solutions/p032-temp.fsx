let expected =
    { '1' .. '9' }
    |> Seq.map (fun i -> (i, 1))
    |> Seq.toArray

let dbg x = (printfn "%A" x)

let rec results =
    seq {
        for a in 1 .. 9999 do
            dbg a
            for b in a .. 9999 do
                let p = a * b

                let concat =
                    a.ToString() + b.ToString() + p.ToString()

                if concat.Length = 9 then
                    let test =
                        concat
                        |> Seq.groupBy (fun c -> c)
                        |> Seq.map (fun (c, m) -> (c, m |> Seq.length))
                        |> Seq.sort
                        |> Seq.toArray

                    if expected = test then yield p
    }

results |> Seq.distinct |> Seq.sum |> dbg
//open System
//
//let displayStats () =
//    let len x =
//        if x < 10 then 1
//        elif x < 100 then 2
//        elif x < 1000 then 3
//        elif x < 10000 then 4
//        elif x < 100000 then 5
//        elif x < 1000000 then 6
//        elif x < 10000000 then 7
//        elif x < 100000000 then 8
//        elif x < 1000000000 then 9
//        else raise (InvalidOperationException())
//
//    let rec maplen =
//        seq {
//
//            for a in 1 .. 10000 do
//                printfn "%A" a
//
//                for b in a .. 10000 do
//                    let la = len a
//                    let lb = len b
//                    let lp = len (a * b)
//                    if (la + lb + lp) = 9 then yield (la, lb, lp)
//        }
//
//    maplen
//    |> Seq.groupBy (fun x -> x)
//    |> Seq.iter (fun ((la, lb, lp), lst) -> printfn "%A %A %A %A " la lb lp (lst |> Seq.length))
////
////let rec genAll (prev: int list) (chars: int list) =
////    seq {
////        for (c, others) in (chars
////                            |> Seq.map (fun c ->
////                                (c,
////                                 chars
////                                 |> Seq.filter (fun x -> x <> c)
////                                 |> Seq.toList))) do
////            let nl = (c :: prev)
////
////            match others with
////            | _ :: _ -> yield! (genAll others nl)
////            | [] -> yield nl
////    }
////let rec genAll2 (used: int list) (chars: int list) =
////
////    seq {
////        for c in chars do
////            if List.contains c used then
////                ()
////            else
////                yield! getAll2 (c :: used) chars
////    }
//
////let rec genAll3 (chars: int list) =
////    let sdmflkjdsh (chr:int seq) : (int * int list) = chr |> Seq.map (fun c -> (c , chr |> Seq.filter (fun x -> x<>c) |> Seq.toList))
////    let rec listchars chr : int seq =
////        seq {
////            for (c,others) in chr |> sdmflkjdsh do
////
////        }
//
//
////            if
////        for (c, others) in (chars
////                            |> Seq.map (fun c ->
////                                (c,
////                                 chars
////                                 |> Seq.filter (fun x -> x <> c)
////                                 |> Seq.toList))) do
////            let nl = (c :: prev)
////
////            match others with
////            | _ :: _ -> yield! (genAll others nl)
////            | [] -> yield nl
////    }
//
//let mapList (list: int array): (int * int * int) =
//    let rec sum start length acc =
//        let value = acc + list.[start]
//
//        match length with
//        | 1 -> value
//        | _ -> sum (start + 1) (length - 1) (value * 10)
//
//    let a' = sum 0 2 0
//    let b' = sum 2 3 0
//    let p' = sum 5 4 0
//
//    a', b', p'
//
//let rec genAll (used: int list) (values: int list): int array seq =
//    seq {
//        if used.Length = values.Length then
//            yield (used |> List.toArray)
//        else
//            for c in values do
//                if used |> List.contains c then () else yield! genAll (c :: used) values
//    }
//
//let dbg x = (printfn "%A" x)
//
////[ 1; 2; 3; 4; 5; 6; 7; 8; 9 ]
////|> genAll []
////|> Seq.toList
////|> dbg
////|> Seq.map mapList
////|> Seq.filter (fun (a, b, p) -> a * b = p)
////|> Seq.iter (printfn "%A")
//
////ClassLibrary1.Class1.Test()
//[ 1; 2; 3; 4; 5; 6; 7; 8; 9 ] |> genAll []
//|> Seq.map mapList
//|> Seq.filter (fun (a, b, p) -> a * b = p)
//|> Seq.iter (printfn "%A")

