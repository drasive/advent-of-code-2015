// # Day 1: Not Quite Lisp
// 
// [...]
// 
// Santa is trying to deliver presents in a large apartment building, but he
// can't find the right floor - the directions he got are a little confusing.
// He starts on the ground floor (floor `0`) and then follows the instructions
// one character at a time.
// 
// An opening parenthesis, `(`, means he should go up one floor, and a closing
// parenthesis, `)`, means he should go down one floor.
// 
// The apartment building is very tall, and the basement is very deep; he will
// never find the top or bottom floors.
// 
// For example:
// - `(())` and `()()` both result in floor `0`.
// - `(((` and `(()(()(` both result in floor `3`.
// - `))(((((` also results in floor `3`.
// - `())` and `))(` both result in floor `-1` (the first basement level).
// - `)))` and `)())())` both result in floor `-3`.
// 
// To _what floor_ do the instructions take Santa?
// 
// # Part Two
// 
// Now, given the same instructions, find the position of the first character
// that causes him to enter the basement (floor `-1`). The first character in
// the instructions has position `1`, the second character has position `2`, and
// so on.
// 
// For example:
// - `)` causes him to enter the basement at character position `1`.
// - `()())` causes him to enter the basement at character position `5`.
// 
// What is the _position_ of the character that causes Santa to first enter the
// basement?
// 
// 
// Source: http://adventofcode.com/day/1, 2015-12-01
//
// Personal day 1 goal: Get F# application running, use command line parameters


module DimitriVranken.AdventOfCode2015.Day1

open System


let Solution (input : string) : (int * Option<int>) =
    if input = null then
        raise (ArgumentNullException "input")

    let floor = ref 0
    let mutable basementEncounter = false
    let mutable basementEncounterPosition = 0

    for index = 0 to input.Length - 1 do
        let instruction = input.[index]
        match instruction with
        | '(' -> incr floor
        | ')' -> decr floor
        | _ -> () // Ignore any other characters
        
        if basementEncounter = false && !floor = -1 then
            basementEncounter <- true
            basementEncounterPosition <- index + 1

    (!floor, if basementEncounter then Some basementEncounterPosition else None)

let FormattedSolution (solution : (int * Option<int>)) : string =
    let firstBasementEncounter =
        match snd solution with
        | Some r -> r.ToString()
        | None -> "-"

    String.Format("End floor: {0}\n" +
                  "First basement encounter: {1}",
                  fst solution, firstBasementEncounter)
