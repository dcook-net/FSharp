module PrimeMaths

open System

let rec private divisibleBy number count max =

    match count <= max with
    | false -> true
    | true
        -> 
        if number % count = 0 then
            false
        else
            count
            |> (+) 2
            |> divisibleBy number max

let private ceilingOf (number:int) =
    Convert.ToInt32(Math.Floor(Math.Sqrt(Convert.ToDouble(number))))

let private evaluateNumber number = 
     number
     |> ceilingOf
     |> divisibleBy number 3

let public isPrime number =
    match number with
    | n when n <= 1 -> false
    | n when n = 2 -> true
    | n when n % 2 = 0 -> false
    | _
        -> number
        |> evaluateNumber