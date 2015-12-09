module DimitriVranken.AdventOfCode2015.Test.Day9Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    let input =
        "London to Dublin = 464\n" +
        "London to Belfast = 518\n" +
        "Dublin to Belfast = 141"

    Assert.Equal(605, fst (Day9.Solution input))

[<Fact>]
let Part2Examples_Correct() =
    let input =
        "London to Dublin = 464\n" +
        "London to Belfast = 518\n" +
        "Dublin to Belfast = 141"

    Assert.Equal(982, snd (Day9.Solution input))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day9.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((117, 909), Day9.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day9.Solution null |> ignore)

[<Fact>]
let InputEmpty_Correct() =
        Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day9.Solution "" |> ignore)
