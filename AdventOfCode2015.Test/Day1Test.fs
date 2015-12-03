module DimitriVranken.AdventOfCode2015.Test.Day1Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(0, fst (Day1.Solve "(())"))
    Assert.Equal(0, fst (Day1.Solve "()()"))

    Assert.Equal(3, fst (Day1.Solve "((("))
    Assert.Equal(3, fst (Day1.Solve "(()(()("))

    Assert.Equal(3, fst (Day1.Solve "))((((("))

    Assert.Equal(-1, fst (Day1.Solve "())"))
    Assert.Equal(-1, fst (Day1.Solve "))("))

    Assert.Equal(-3, fst (Day1.Solve ")))"))
    Assert.Equal(-3, fst (Day1.Solve ")())())"))

[<Fact>]
let Part2Examples_Correct() =
    Assert.Equal(Some 1, snd (Day1.Solve ")"))

    Assert.Equal(Some 5, snd (Day1.Solve "()())"))
    
[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeDataDirectory")
            + @"\day1.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((232, Some 1783), Day1.Solve input)


[<Fact>]
let InputEmpty_0_None() =
    Assert.Equal((0, None), Day1.Solve "")

[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day1.Solve(null) |> ignore)

[<Fact>]
let InputWithComments_Correct() =
    Assert.Equal(3, fst (Day1.Solve "Comment((Te(st"))
