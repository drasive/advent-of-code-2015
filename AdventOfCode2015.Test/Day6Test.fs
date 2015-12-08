module DimitriVranken.AdventOfCode2015.Test.Day6Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(1000000, fst (Day6.Solution "turn on 0,0 through 999,999"))
    
    Assert.Equal(1000, fst (Day6.Solution "toggle 0,0 through 999,0"))

    Assert.Equal(0, fst (Day6.Solution "turn off 499,499 through 500,500"))

[<Fact>]
let Part2Examples_Correct() =
    Assert.Equal(1, snd (Day6.Solution "turn on 0,0 through 0,0"))
    
    Assert.Equal(2000000, snd (Day6.Solution "toggle 0,0 through 999,999"))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day6.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((400410, 15343601), Day6.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day6.Solution null |> ignore)

[<Fact>]
let InputEmpty_Correct() =
    Assert.Equal((0, 0), Day6.Solution "")
