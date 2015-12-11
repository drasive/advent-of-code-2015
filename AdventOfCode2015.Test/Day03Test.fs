module DimitriVranken.AdventOfCode2015.Test.Day03Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(2, fst (Day03.Solution ">"))

    Assert.Equal(4, fst (Day03.Solution "^>v<"))

    Assert.Equal(2, fst (Day03.Solution "^v^v^v^v^v"))

[<Fact>]
let Part2Examples_Correct() =
    Assert.Equal(3, snd (Day03.Solution "^v"))

    Assert.Equal(11, snd (Day03.Solution "^v^v^v^v^v"))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day03.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((2592, 2360), Day03.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day03.Solution null |> ignore)

[<Fact>]
let InputEmpty_Correct() =
    Assert.Equal((1, 1), (Day03.Solution ""))
