module DimitriVranken.AdventOfCode2015.Test.Day5Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(1, fst (Day5.Solution "ugknbfddgicrmopn"))
    
    Assert.Equal(1, fst (Day5.Solution "aaa"))

    Assert.Equal(0, fst (Day5.Solution "jchzalrnumimnmhp"))

    Assert.Equal(0, fst (Day5.Solution "haegwjzuvuyypxyu"))

    Assert.Equal(0, fst (Day5.Solution "dvszwmarrgswjxmb"))

[<Fact>]
let Part2Examples_Correct() =
    Assert.Equal(1, snd (Day5.Solution "qjhvhtzxzqqjkmpb"))
    
    Assert.Equal(1, snd (Day5.Solution "xxyxx"))

    Assert.Equal(0, snd (Day5.Solution "uurcxstgmygtbstg"))

    Assert.Equal(0, snd (Day5.Solution "ieodomkazucvgmuy"))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day5.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((238, 69), Day5.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day5.Solution null |> ignore)

[<Fact>]
let InputEmpty_Correct() =
    Assert.Equal((0, 0), Day5.Solution "")
