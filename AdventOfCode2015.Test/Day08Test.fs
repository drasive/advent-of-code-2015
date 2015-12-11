module DimitriVranken.AdventOfCode2015.Test.Day08Test

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

    Assert.Equal(12, fst (Day08.Solution input))

[<Fact>]
let Part2Examples_Correct() =
    let input =
        "\"\"\n" +
        "\"abc\"\n" +
        "\"aaa\\\"aaa\"\n" +
        "\"\\x27\""

    Assert.Equal(19, snd (Day08.Solution input))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day08.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((1333, 2046), Day08.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day08.Solution null |> ignore)

[<Fact>]
let InputEmpty_Correct() =
    Assert.Equal((0, 0), Day08.Solution "")
