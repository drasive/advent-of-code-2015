// ## Day 10: Elves Look, Elves Say
// 
// Today, the Elves are playing a game called look-and-say. They take turns
// making sequences by reading aloud the previous sequence and using that
// reading as the next sequence. For example, `211` is read as
// "one two, two ones", which becomes `1221` (`1` `2`, `2` `1`s).
// 
// Look-and-say sequences are generated iteratively, using the previous value as
// input for the next step. For each step, take the previous value, and replace
// each run of digits (like `111`) with the number of digits (`3`) followed by
// the digit itself (`1`).
// 
// For example:
// - `1` becomes `11` (`1` copy of digit `1`).
// - `11` becomes `21` (`2` copies of digit `1`).
// - `21` becomes `1211` (one `2` followed by one `1`).
// - `1211` becomes `111221` (one `1`, one `2`, and two `1`s).
// - `111221` becomes `312211` (three `1`s, two `2`s, and one `1`).
// 
// Starting with the digits in your puzzle input, apply this process 40 times.
// What is _the length of the result_?
// 
// 
// Source: http://adventofcode.com/day/10, 2015-12-10
// 
// Personal day 10 goal: Use sequences extensively


module DimitriVranken.AdventOfCode2015.Day10

open System
open System.Text


let private GroupCharacters (str : string) : seq<char * int> =
    seq {
         let mutable groupCharacter = Seq.head str
         let mutable groupLength = 0

         for character in str do
            if character = groupCharacter then
                groupLength <- groupLength + 1
            else
                yield (groupCharacter, groupLength)

                groupCharacter <- character
                groupLength <- 1
            
         yield (groupCharacter, groupLength)
    }

let private LookAndSay (str : string) : string =
    let stringBuilder = new StringBuilder()
    
    str
    |> GroupCharacters
    |> Seq.iter(fun (character, length) ->
            stringBuilder.Append(length.ToString() + character.ToString())
            |> ignore)

    stringBuilder.ToString()
    
let SolutionCustomized (input : string) (steps : int) : int =
    if String.IsNullOrEmpty input then
        raise (ArgumentNullException "input")

    [1..steps]
    |> Seq.fold (fun acc _ -> LookAndSay acc) input
    |> String.length

let Solution (input : string) : (int * int) =
    (SolutionCustomized input 40, SolutionCustomized input 50)

let FormattedSolution (solution : (int * int)) : string =
    String.Format("40 steps: {0}\n" +
                  "50 steps: {1}",
                  fst solution, snd solution)
