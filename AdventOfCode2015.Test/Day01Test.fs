module DimitriVranken.AdventOfCode2015.Test.Day01Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.Equal(0, fst (Day01.Solution "(())"))
    Assert.Equal(0, fst (Day01.Solution "()()"))

    Assert.Equal(3, fst (Day01.Solution "((("))
    Assert.Equal(3, fst (Day01.Solution "(()(()("))

    Assert.Equal(3, fst (Day01.Solution "))((((("))

    Assert.Equal(-1, fst (Day01.Solution "())"))
    Assert.Equal(-1, fst (Day01.Solution "))("))

    Assert.Equal(-3, fst (Day01.Solution ")))"))
    Assert.Equal(-3, fst (Day01.Solution ")())())"))

[<Fact>]
let Part2Examples_Correct() =
    Assert.Equal(Some 1, snd (Day01.Solution ")"))

    Assert.Equal(Some 5, snd (Day01.Solution "()())"))
    
[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day01.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal((232, Some 1783), Day01.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day01.Solution null |> ignore)

[<Fact>]
let InputEmpty_Correct() =
    Assert.Equal((0, None), Day01.Solution "")
