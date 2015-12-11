module DimitriVranken.AdventOfCode2015.Test.Day04Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(609043, fst (Day04.Solution "abcdef"))
    
    Assert.Equal(1048970, fst (Day04.Solution "pqrstuv"))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day04.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((282749, 9962624), Day04.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day04.Solution null |> ignore)

[<Fact>]
let InputEmpty_Correct() =
    Assert.Equal((1803305, 20412333), Day04.Solution "")
