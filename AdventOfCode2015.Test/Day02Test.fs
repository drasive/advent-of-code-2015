module DimitriVranken.AdventOfCode2015.Test.Day02Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(58, fst (Day02.Solution "2x3x4"))

    Assert.Equal(43, fst (Day02.Solution "1x1x10"))

[<Fact>]
let Part2Examples_Correct() =
    Assert.Equal(34, snd (Day02.Solution "2x3x4"))

    Assert.Equal(14, snd (Day02.Solution "1x1x10"))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day02.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((901, 1297), Day02.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day02.Solution null |> ignore)

[<Fact>]
let InputEmpty_Correct() =
    Assert.Equal((0, 0), Day02.Solution "")
