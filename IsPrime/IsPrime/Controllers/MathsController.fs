module PrimeMaths 

open System

let rec private divisibleBy number count max =

    match count <= max with
    | false -> true
    | true -> 
    
    if number % count = 0 then
        false
    else
        (count + 2) |> divisibleBy number max

let private getBoundary (number:int) =
    Convert.ToInt32(Math.Floor(Math.Sqrt(Convert.ToDouble(number))))

let private evaluateNumber number = 
     getBoundary number |> divisibleBy number 3

let public isPrime number =
    match number with
    | n when n <= 1 -> false
    | n when n = 2 -> true
    | n when n % 2 = 0 -> false
    | _ -> evaluateNumber number
    
//namespace IsPrimeFSharp.Controllers

//open IsPrimeMaths
open Microsoft.AspNetCore.Mvc

[<ApiController>]
type MathsController () =
    inherit ControllerBase()
    
    [<HttpGet("IsPrime/{number}")>]
    member __.Get number =
        isPrime number |>
        Ok