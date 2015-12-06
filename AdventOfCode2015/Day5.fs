// # Day 5: Doesn't He Have Intern-Elves For This?
// 
// Santa needs help figuring out which strings in his text file are naughty or
// nice.
// 
// A _nice string_ is one with all of the following properties:
// - It contains at least three vowels (`aeiou` only), like `aei`, `xazegov`, or
//   `aeiouaeiouaeiou`.
// - It contains at least one letter that appears twice in a row, like `xx`,
//   `abcdde` (`dd`), or `aabbccdd` (`aa`, `bb`, `cc`, or `dd`).
// - It does _not_ contain the strings `ab`, `cd`, `pq`, or `xy`, even if they
//   are part of one of the other requirements.
// 
// For example:
// - `ugknbfddgicrmopn` is nice because it has at least three vowels
//   (`u...i...o...`), a double letter (`...dd...`), and none of the disallowed
//   substrings.
// - `aaa` is nice because it has at least three vowels and a double letter,
//   even though the letters used by different rules overlap.
// - `jchzalrnumimnmhp` is naughty because it has no double letter.
// - `haegwjzuvuyypxyu` is naughty because it contains the string `xy`.
// - `dvszwmarrgswjxmb` is naughty because it contains only one vowel.
// 
// How many strings are nice?
// 
// ## Part Two
// 
// Realizing the error of his ways, Santa has switched to a better model of
// determining whether a string is naughty or nice. None of the old rules apply,
// as they are all clearly ridiculous.
// 
// Now, a nice string is one with all of the following properties:
// - It contains a pair of any two letters that appears at least twice in the
//   string without overlapping, like `xyxy` (`xy`) or `aabcdefgaa` (`aa`), but
//   not like `aaa` (`aa`, but it overlaps).
// - It contains at least one letter which repeats with exactly one letter
//   between them, like `xyx`, `abcdefeghi` (`efe`), or even `aaa`.
// 
// For example:
// - `qjhvhtzxzqqjkmpb` is nice because is has a pair that appears twice (`qj`)
//   and a letter that repeats with exactly one letter between them (`zxz`).
// - `xxyxx` is nice because it has a pair that appears twice and a letter that
//   repeats with one between, even though the letters used by each rule
//   overlap.
// - `uurcxstgmygtbstg` is naughty because it has a pair (`tg`) but no repeat
//   with a single letter between them.
// - `ieodomkazucvgmuy` is naughty because it has a repeating letter with one
//   between (`odo`), but no pair that appears twice.
// 
// How many strings are nice under these new rules?
// 
// 
// Source: http://adventofcode.com/day/5, 2015-12-05
// 
// Personal day 5 goal: Use sequences extensively, write short code


module DimitriVranken.AdventOfCode2015.Day5

open System
open System.Text.RegularExpressions 


let IsStringNiceRuleSet1 (string : string) : bool =
        // It contains at least three vowels
        let containsVowel (str : string) : bool =
            let vowels = ['a';'e';'i';'o';'u'] |> Set.ofList
            
            str
            |> Seq.filter (fun char -> vowels.Contains char)
            |> Seq.length
                >= 3

        // Does not contain the strings `ab`, `cd`, `pq`, or `xy`
        let doesNotContainBadSequence (str : string) : bool =
            let badSequences = ["ab";"cd";"pq";"xy"]

            badSequences
            |> Seq.forall(fun badSequence -> not (str.Contains badSequence))
    
        // Contains at least one letter that appears twice in a row
        let containsLetterAppearingTwice (str : string) : bool =
            str
            |> Seq.pairwise
            |> Seq.exists (fun (a, b) -> a = b)

        containsVowel string
        && doesNotContainBadSequence string
        && containsLetterAppearingTwice string

let IsStringNiceRuleSet2 (string : string) : bool =
    // Contains a pair of any two letters that appears at least twice in the
    // string without overlapping
    let containsTwoLetterPair (str : string) : bool =
        Regex.IsMatch(str, @"(..).*\1")

    // Contains at least one letter which repeats with exactly one letter
    // between them
    let containsRepeatedLetter (str : string) : bool =
        str
        |> Seq.windowed 3
        |> Seq.exists (fun [|a;b;c|] -> a = c)

    containsTwoLetterPair string 
    && containsRepeatedLetter string


let Solution (input: string) : (int * int) =
    if input = null then
        raise (ArgumentNullException "input")
    
    let lines = input.Split('\n')
    let ruleSet1Solution =
        lines
        |> Seq.filter IsStringNiceRuleSet1
        |> Seq.length
    let ruleSet2Solution =
        lines
        |> Seq.filter IsStringNiceRuleSet2
        |> Seq.length
        
    (ruleSet1Solution, ruleSet2Solution)

let FormattedResult (result : (int * int)) : string =
    String.Format("Rule set 1: {0}\n" +
                  "Rule set 2: {1}",
                  fst result, snd result)
