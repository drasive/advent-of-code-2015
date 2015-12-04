module DimitriVranken.AdventOfCode2015.Test.Day4Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(609043, fst (Day4.Solve "abcdef"))
    
    Assert.Equal(1048970, fst (Day4.Solve "pqrstuv"))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeDataDirectory")
            + @"\day4.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((282749, 9962624), Day4.Solve input)


[<Fact>]
let InputEmpty_Correct() =
    Assert.Equal((1803305, 20412333), Day4.Solve "")

[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day4.Solve(null) |> ignore)
