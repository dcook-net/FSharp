namespace WebApplication.Controllers

open Microsoft.AspNetCore.Mvc
open PrimeMaths

[<ApiController>]
type MathsController () =
    inherit ControllerBase()
    
    [<HttpGet("IsPrime/{number}")>]
    member __.Get number =
         number
         |> isPrime
         |> __.Ok 
