module DimitriVranken.AdventOfCode2015.Test.Day2Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(58, fst (Day2.Solve "2x3x4"))

    Assert.Equal(43, fst (Day2.Solve "1x1x10"))

[<Fact>]
let Part2Examples_Correct() =
    Assert.Equal(34, snd (Day2.Solve "2x3x4"))

    Assert.Equal(14, snd (Day2.Solve "1x1x10"))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeDataDirectory")
            + @"\day2.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((901, 1297), Day2.Solve input)


[<Fact>]
let InputEmpty_0_0() =
    Assert.Equal((0, 0), Day2.Solve "")

[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day2.Solve(null) |> ignore)

[<Fact>]
let InputWithComments_Correct() =
    let input = "Comment\n" +
                "2x3x4"

    Assert.Equal(58, fst (Day2.Solve input))
