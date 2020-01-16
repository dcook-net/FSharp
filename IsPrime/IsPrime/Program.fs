namespace WebApplication

open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Giraffe
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open System
open PrimeMaths
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2
open System.Threading.Tasks

module Program =
    let exitCode = 0

    let isPrimeHandler (num : int) (next : HttpFunc) (ctx : HttpContext) : HttpFuncResult =
        if isPrime num then 
          text "true" next ctx
        else
          text "false" next ctx

          //HttpFuncResult : Task<HttpContext option>
    let finishEarly : (HttpContext -> HttpFuncResult) = 
      fun x -> Task.FromResult (Some x)
      //Some >> Task.FromResult

    let checkUserIsLoggedIn : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            if isNotNull ctx.User && ctx.User.Identity.IsAuthenticated
            then next ctx
            else (setStatusCode 401 >=> text "Please sign in.") finishEarly ctx


    let checkUserIsLoggedIn2 : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                if isNotNull ctx.User && ctx.User.Identity.IsAuthenticated
                then return! next ctx
                else
                    ctx.SetStatusCode 401
                    return Some ctx
            }

    type MaybeBuilder() =
      member this.Bind(x, f) = Option.bind f x
          //match x with
          //| None -> None
          //| Some a -> f a

      
      member this.Return(x) = 
          Some x
   
    let maybe = new MaybeBuilder()



    let optionsAreFun (opt : int option option) =
            maybe {
              let! a = opt
              let! b = a
              return b
            }

            //Option.bind(fun t -> t |> Option.bind(fun y -> Some y))

    

    let webApp = 
      setHttpHeader "X-Foo" "Bar"
      >=> choose [
        routef "/IsPrime/%i" (fun num -> isPrimeHandler num)
        route "/HelloWorld" >=> text "Hi"
        route "/secure" >=> checkUserIsLoggedIn >=> choose [
          route "/usersettings"
        ]
      ]

    let configureApp (app : IApplicationBuilder) = 
      app.UseDeveloperExceptionPage() |> ignore
      app.UseGiraffe webApp 

    let configureServices (services : IServiceCollection) = 
      services.AddGiraffe() |> ignore

    [<EntryPoint>]
    let main args =
        WebHostBuilder()
            .UseKestrel()
            .Configure(Action<IApplicationBuilder> configureApp)
            .ConfigureServices(configureServices)
            .UseUrls("http://localhost:9021/")
            .Build()
            .Run()
        exitCode
