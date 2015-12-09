module DimitriVranken.AdventOfCode2015.Test.Day8Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    let input =
        "\"\"\n" +
        "\"abc\"\n" +
        "\"aaa\\\"aaa\"\n" +
        "\"\\x27\""

    Assert.Equal(12, fst (Day8.Solution input))

[<Fact>]
let Part2Examples_Correct() =
    let input =
        "\"\"\n" +
        "\"abc\"\n" +
        "\"aaa\\\"aaa\"\n" +
        "\"\\x27\""

    Assert.Equal(19, snd (Day8.Solution input))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day8.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((1333, 2046), Day8.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day8.Solution null |> ignore)

[<Fact>]
let InputEmpty_Correct() =
    Assert.Equal((0, 0), Day8.Solution "")
