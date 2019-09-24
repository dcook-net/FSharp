module IsPrime.Tests

open NUnit.Framework
open PrimeMaths

[<SetUp>]
let Setup () =
    ()

[<TestCase(2)>]
[<TestCase(3)>]
[<TestCase(5)>]
[<TestCase(7)>]
[<TestCase(53)>]
[<TestCase(89)>]
let ``Is a Prime Number`` number =
    isPrime number |> Assert.IsTrue

[<TestCase(0)>]
[<TestCase(1)>]
[<TestCase(4)>]
[<TestCase(6)>]
[<TestCase(52)>]
[<TestCase(99)>]
let ``Is not a Prime Number`` number =
    isPrime number |> Assert.IsFalse

