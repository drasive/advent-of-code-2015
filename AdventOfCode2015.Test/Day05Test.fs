module DimitriVranken.AdventOfCode2015.Test.Day05Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(1, fst (Day05.Solution "ugknbfddgicrmopn"))
    
    Assert.Equal(1, fst (Day05.Solution "aaa"))

    Assert.Equal(0, fst (Day05.Solution "jchzalrnumimnmhp"))

    Assert.Equal(0, fst (Day05.Solution "haegwjzuvuyypxyu"))

    Assert.Equal(0, fst (Day05.Solution "dvszwmarrgswjxmb"))

[<Fact>]
let Part2Examples_Correct() =
    Assert.Equal(1, snd (Day05.Solution "qjhvhtzxzqqjkmpb"))
    
    Assert.Equal(1, snd (Day05.Solution "xxyxx"))

    Assert.Equal(0, snd (Day05.Solution "uurcxstgmygtbstg"))

    Assert.Equal(0, snd (Day05.Solution "ieodomkazucvgmuy"))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day05.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((238, 69), Day05.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day05.Solution null |> ignore)

[<Fact>]
let InputEmpty_Correct() =
    Assert.Equal((0, 0), Day05.Solution "")
