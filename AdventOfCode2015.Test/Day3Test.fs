module DimitriVranken.AdventOfCode2015.Test.Day3Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(2, fst (Day3.Solve ">"))

    Assert.Equal(4, fst (Day3.Solve "^>v<"))

    Assert.Equal(2, fst (Day3.Solve "^v^v^v^v^v"))

[<Fact>]
let Part2Examples_Correct() =
    Assert.Equal(3, snd (Day3.Solve "^v"))

    Assert.Equal(11, snd (Day3.Solve "^v^v^v^v^v"))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day3.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((2592, 2360), Day3.Solve input)


[<Fact>]
let InputEmpty_1_1() =
    Assert.Equal((1, 1), (Day3.Solve ""))

[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day3.Solve(null) |> ignore)

[<Fact>]
let InputWithComments_Correct() =
    let input = "Comment^>Tevs<t"

    Assert.Equal(4, fst (Day3.Solve input))
