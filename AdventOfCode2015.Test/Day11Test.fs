module DimitriVranken.AdventOfCode2015.Test.Day11Test

open System
open System.Configuration
open System.IO

open Xunit

open DimitriVranken.AdventOfCode2015


[<Fact>]
let Part1Examples_Correct() =
    Assert.True("hijklmmn" <> fst (Day11.Solution "hijklmmn"))
    Assert.True("abbceffg" <> fst (Day11.Solution "abbceffg"))
    Assert.True("abbcegjk" <> fst (Day11.Solution "abbcegjk"))

    Assert.Equal("abcdffaa", fst (Day11.Solution "abcdefgh"))
    Assert.Equal("ghjaabcc", fst (Day11.Solution "ghijklmn"))

[<Fact>]
let MyInput_Correct() =
    let filePath =
        Path.GetFullPath(
            System.AppDomain.CurrentDomain.BaseDirectory
            + ConfigurationManager.AppSettings.Item("relativeInputDirectory")
            + @"\day11.txt")
    let input = File.ReadAllText(filePath)

    Assert.Equal(("hepxxyzz", ""), Day11.Solution input)


[<Fact>]
let InputNull_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day11.Solution null |> ignore)

[<Fact>]
let InputEmpty_Exception() =
    Assert.ThrowsAny<ArgumentNullException>(fun() ->
        Day11.Solution "" |> ignore)
