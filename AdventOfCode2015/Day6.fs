//## Day 6: Probably a Fire Hazard
//
// Because your neighbors keep defeating you in the holiday house decorating
// contest year after year, you've decided to deploy one million lights in a
// 1000x1000 grid.
// 
// Furthermore, because you've been especially nice this year, Santa has mailed
// you instructions on how to display the ideal lighting configuration.
// 
// Lights in your grid are numbered from 0 to 999 in each direction; the lights
// at each corner are at `0,0`, `0,999`, `999,999`, and `999,0`. The
// instructions include whether to `turn on`, `turn off`, or `toggle` various
// inclusive ranges given as coordinate pairs. Each coordinate pair represents
// opposite corners of a rectangle, inclusive; a coordinate pair like
// `0,0 through 2,2` therefore refers to 9 lights in a 3x3 square. The lights
// all start turned off.
// 
// To defeat your neighbors this year, all you have to do is set up your lights
// by doing the instructions Santa sent you in order.
// 
// - `turn on 0,0 through 999,999` would turn on (or leave on) every light.
// - `toggle 0,0 through 999,0` would toggle the first line of 1000 lights,
//   turning off the ones that were on, and turning on the ones that were off.
// - `turn off 499,499 through 500,500` would turn off (or leave off) the middle
//   four lights.
// 
// After following the instructions, _how many lights are lit_?
// 
// ## Part Two
// 
// You just finish implementing your winning light pattern when you realize you
// mistranslated Santa's message from Ancient Nordic Elvish.
// 
// The light grid you bought actually has individual brightness controls; each
// light can have a brightness of zero or more. The lights all start at zero.
// 
// The phrase `turn on` actually means that you should increase the brightness
// of those lights by `1`.
// 
// The phrase `turn off` actually means that you should decrease the brightness
// of those lights by `1`, to a minimum of zero.
// 
// The phrase `toggle` actually means that you should increase the brightness of
// those lights by `2`.
// 
// What is the _total brightness_ of all lights combined after following Santa's
// instructions?
// 
// For example:
// - `turn on 0,0 through 0,0` would increase the total brightness by `1`.
// - `toggle 0,0 through 999,999` would increase the total brightness by
//   `2000000`.
// 
// 
// Source: http://adventofcode.com/day/6, 2015-12-06
// 
// Personal day 6 goal: Use functional style


module DimitriVranken.AdventOfCode2015.Day6

open System
open System.Text.RegularExpressions


type Coordinates = (int * int)

type private InstructionType =
    | TurnOn
    | TurnOff
    | Toggle

type Instruction = (InstructionType * Coordinates * Coordinates)

let private ParseInstruction (instruction : string) : Instruction =
    let pattern = @"(turn on|turn off|toggle) (\d+),(\d+) through (\d+),(\d+)"
    let matchedGroups = Regex.Match(instruction, pattern).Groups

    let instructionType =
        match matchedGroups.[1].Value with
        | "turn on" -> InstructionType.TurnOn
        | "turn off" -> InstructionType.TurnOff
        | _ -> InstructionType.Toggle
    let coordinatesStart =
        (int matchedGroups.[2].Value, int matchedGroups.[3].Value)
    let coordinatesEnd =
        (int matchedGroups.[4].Value, int matchedGroups.[5].Value)

    (instructionType, coordinatesStart, coordinatesEnd)

let private CoordinatesInRange (rangeStart : Coordinates) (rangeEnd : Coordinates) =
    [for x = fst rangeStart to fst rangeEnd do
     for y = snd rangeStart to snd rangeEnd do
         yield x, y]

let private CalculateLightsOn (lines : string[])
    (actionMapper : (InstructionType -> int -> int)) : int = 
    let lights = Array2D.create 1000 1000 0

    lines
    |> Seq.map ParseInstruction
    |> Seq.iter (fun (instructionType, coordinatesStart, coordinatesEnd) ->
        // TODO: How to use top-level piping instead?
        CoordinatesInRange coordinatesStart coordinatesEnd
        |> Seq.iter (fun (x, y) ->
            lights.[x, y] <- (actionMapper instructionType) lights.[x, y]))
    |> ignore

    lights |> Seq.cast<int> |> Seq.sum

let private ActionMapperRuleSet1 (instructionType) =
    match instructionType with
    | TurnOn -> (fun status -> 1)
    | TurnOff -> (fun status -> 0)
    | Toggle -> (fun status -> if status = 0 then 1 else 0)

let private ActionMapperRuleSet2 (instructionType) =
    match instructionType with
    | TurnOn -> (fun status -> status + 1)
    | TurnOff -> (fun status -> Math.Max(status - 1, 0))
    | Toggle -> (fun status -> status + 2)


let Solution (input : string) : (int * int) =
    if input = null then
        raise (ArgumentNullException "input")

    if not (String.IsNullOrEmpty input) then
        let lines = input.Split('\n')

        (CalculateLightsOn lines ActionMapperRuleSet1,
         CalculateLightsOn lines ActionMapperRuleSet2)
    else
        (0, 0)

let FormattedSolution (solution : (int * int)) : string =
    String.Format("Rule set 1: {0}\n" +
                  "Rule set 2: {1}",
                  fst solution, snd solution)
