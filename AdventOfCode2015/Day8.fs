// ## Day 8: Matchsticks
// 
// Space on the sleigh is limited this year, and so Santa will be bringing his
// list as a digital copy. He needs to know how much space it will take up when
// stored.
// 
// It is common in many programming languages to provide a way to escape special
// characters in strings. For example, C, JavaScript, Perl, Python, and even PHP
// handle special characters in very similar ways.
// 
// However, it is important to realize the difference between the number of
// characters _in the code representation of the string literal_ and the number
// of characters _in the in-memory string itself_.
// 
// For example:
// - `""` is `2` characters of code (the two double quotes), but the string
// contains zero characters.
// - `"abc"` is `5` characters of code, but `3` characters in the string data.
// - `"aaa\"aaa"` is `10` characters of code, but the string itself contains six
//   "a" characters and a single, escaped
//   quote character, for a total of `7` characters in the string data.
// - `"\x27"` is `6` characters of code, but the string itself contains just
//   one - an apostrophe (`'`), escaped using hexadecimal notation.
// 
// Santa's list is a file that contains many double-quoted string literals, one
// on each line. The only escape sequences used are `\\` (which represents a
// single backslash), `\"` (which represents a lone double-quote character), and
//  `\x` plus two hexadecimal characters (which represents a single character
// with that ASCII code).
// 
// Disregarding the whitespace in the file, what is _the number of characters of
// code for string literals_ minus _the number of characters in memory for the
// values of the strings_ in total for the entire file?
// 
// For example, given the four strings above, the total number of characters of
// string code (`2 + 5 + 10 + 6 = 23`) minus the total number of characters in
// memory for string values (`0 + 3 + 7 + 1 = 11`) is `23 - 11 = 12`.
// 
// ## Part Two
// 
// Now, let's go the other way. In addition to finding the number of characters
// of code, you should now _encode each code representation as a new string_ and
// find the number of characters of the new encoded representation, including
// the surrounding double quotes.
// 
// For example:
// - `""` encodes to `"\"\""`, an increase from `2` characters to `6`.
// - `"abc"` encodes to `"\"abc\""`, an increase from `5` characters to `9`.
// - `"aaa\"aaa"` encodes to `"\"aaa\\\"aaa\""`, an increase from `10`
//   characters to `16`.
// - `"\x27"` encodes to `"\"\\x27\""`, an increase from `6` characters to `11`.
// 
// Your task is to find _the total number of characters to represent the newly
// encoded strings_ minus _the number of characters of code in each original
// string literal_. For example, for the strings above, the total encoded length
// (`6 + 9 + 16 + 11 = 42`) minus the characters in the original code
// representation (`23`, just like in the first part of this puzzle) is
// `42 - 23 = 19`.
// 
// 
// Source: http://adventofcode.com/day/8, 2015-12-09
// 
// Personal day 8 goal: Short development time


module DimitriVranken.AdventOfCode2015.Day8

open System
open System.Text.RegularExpressions


let private UnescapedStringMemoryDifference (line : string) : int =
    let lineLength = line.Length

    let rec unescapeHexChars (str : string) : string =
        let regex = new Regex(@"\\x([0-9a-f]{2})")
        let matchedGroups = regex.Match(str).Groups

        if matchedGroups.Count > 1 then // String contains escaped hex char
            let number =
                Int32.Parse(matchedGroups.[1].Value,
                            System.Globalization.NumberStyles.HexNumber)
            let character = ((char)number).ToString()

            unescapeHexChars(regex.Replace(str, character, 1))
        else
            str

    let mutable unescapedLine =
        line
            .Substring(1, line.Length - 2) // Remove leading and trailing quotes
            .Replace("\\\"", "\"")         // Replace \" by "
            .Replace(@"\\", @"\")          // Replace \\ by \
            
    if unescapedLine.Contains(@"\x") then // Line may contain escaped hex chars
        unescapedLine <- unescapeHexChars unescapedLine

    lineLength - unescapedLine.Length

let private EscapedStringMemoryDifference (line : string) : int =
    let lineLength = line.Length

    let encodedLength =
        2 +
        (line |> Seq.sumBy(fun char ->
            if char = '\"' || char = '\\' then 2 else 1))

    encodedLength - lineLength


let Solution (input : string) : (int * int) =
    if input = null then
        raise (ArgumentNullException "input")

    if not (String.IsNullOrEmpty input) then
        let lines = input.Split('\n')
        let unescapedCharacters = 
            lines
            |> Seq.map UnescapedStringMemoryDifference
            |> Seq.sum
        let escapedCharacters = 
            lines
            |> Seq.map EscapedStringMemoryDifference
            |> Seq.sum
    
        (unescapedCharacters, escapedCharacters)
    else
        (0, 0)

let FormattedSolution (solution : (int * int)) : string =
    String.Format("Unescaped: {0}\n" +
                  "Escaped: {1}",
                  fst solution, snd solution)
