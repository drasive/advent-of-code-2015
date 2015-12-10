module DimitriVranken.AdventOfCode2015.Test.Day10Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(4, Day10.SolutionCustomized "211" 1)

    Assert.Equal(2, Day10.SolutionCustomized "1" 1)
    Assert.Equal(2, Day10.SolutionCustomized "11" 1)
    Assert.Equal(4, Day10.SolutionCustomized "21" 1)
    Assert.Equal(6, Day10.SolutionCustomized "1211" 1)
    Assert.Equal(6, Day10.SolutionCustomized "111221" 1)

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day10.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((329356, 4666278), Day10.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day10.Solution null |> ignore)

[<Fact>]
let InputEmpty_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day10.Solution "" |> ignore)
