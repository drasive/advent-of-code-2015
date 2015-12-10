module DimitriVranken.AdventOfCode2015.Test.Day7Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    let input =
        "123 -> x\n" +
        "456 -> y\n" +
        "x AND y -> d\n" +
        "x OR y -> e\n" +
        "x LSHIFT 2 -> f\n" +
        "y RSHIFT 2 -> g\n" +
        "NOT x -> h\n" +
        "NOT y -> i"

    let solution = Day7.Solution input

    // Assert
    Assert.Equal(8, solution.Count)

    let node (key : string) : int =
        Convert.ToInt32(solution.Item(key).Value)
    Assert.Equal(72, node "d")
    Assert.Equal(507, node "e")
    Assert.Equal(492, node "f")
    Assert.Equal(114, node "g")
    Assert.Equal(65412, node "h")
    Assert.Equal(65079, node "i")
    Assert.Equal(123, node "x")
    Assert.Equal(456, node "y")


//[<Fact>]
//let Part2Examples_Correct() =
//    Assert.Equal(1, snd (Day6.Solution "turn on 0,0 through 0,0"))
//    
//    Assert.Equal(2000000, snd (Day6.Solution "toggle 0,0 through 999,999"))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day7.txt")
    let input = File.ReadAllText(filePath)

    let solution = Day7.Solution input

    Assert.Equal(0, Convert.ToInt32(solution.Item("a").Value))


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day7.Solution null |> ignore)

[<Fact>]
let InputEmpty_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day7.Solution "" |> ignore)
