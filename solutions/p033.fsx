// 1 − Ax/xB = A/B
// 2 − xA/xB = A/B
// 3 − xA/Bx = A/B
let dbg x = (printfn "%A" x)

let case1 a b x =
  (((a * 10.) + x) / ((b * 10.) + x)) = (a / b)

let case4 a b x =
  (((a * 10.) + x) / ((x * 10.) + b)) = (a / b)

let case2 a b x =
  (((x * 10.) + a) / ((x * 10.) + b)) = (a / b)

let case3 a b x =
  (((x * 10.) + a) / ((b * 10.) + x)) = (a / b)

let test a b x r =
  if r then
    printfn "%A %A %A" a b x

for a in 1..9 do
  for b in 1..9 do
    for x in 1..9 do
      case1 (float a) (float b) (float x) |> test a b x
      case2 (float a) (float b) (float x) |> test a b x
      case3 (float a) (float b) (float x) |> test a b x
      case4 (float a) (float b) (float x) |> test a b x

case4 4. 8. 9. |> dbg
