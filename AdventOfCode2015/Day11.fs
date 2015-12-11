// ## Day 11: Corporate Policy
// 
// Santa's previous password expired, and he needs help choosing a new one.
// 
// To help him remember his new password after the old one expires, Santa has
// devised a method of coming up with a password based on the previous one.
// Corporate policy dictates that passwords must be exactly eight lowercase
// letters (for security reasons), so he finds his new password by
// _incrementing_ his old password string repeatedly until it is valid.
// 
// Incrementing is just like counting with numbers: `xx`, `xy`, `xz`, `ya`,
// `yb`, and so on. Increase the rightmost letter one step; if it was `z`, it
// wraps around to `a`, and repeat with the next letter to the left until one
// doesn't wrap around.
// 
// Unfortunately for Santa, a new Security-Elf recently started, and he has
// imposed some additional password requirements:
// - Passwords must include one increasing straight of at least three letters,
//   like `abc`, `bcd`, `cde`, and so on, up to `xyz`. They cannot skip letters;
//   `abd` doesn't count.
// - Passwords may not contain the letters `i`, `o`, or `l`, as these letters
//   can be mistaken for other characters and are therefore confusing.
// - Passwords must contain at least two different, non-overlapping pairs of
//   letters, like `aa`, `bb`, or `zz`.
// 
// For example:
// - `hijklmmn` meets the first requirement (because it contains the straight
//   `hij`) but fails the second requirement requirement (because it contains
//   `i` and `l`).
// - `abbceffg` meets the third requirement (because it repeats `bb` and `ff`)
//   but fails the first requirement.
// - `abbcegjk` fails the third requirement, because it only has one double
//   letter (`bb`).
// - The next password after `abcdefgh` is `abcdffaa`.
// - The next password after `ghijklmn` is `ghjaabcc`, because you eventually
//   skip all the passwords that start with `ghi...`, since `i` is not allowed.
// 
// Given Santa's current password (your puzzle input), what should his
// _next password_ be?
// 
// 
// Source: http://adventofcode.com/day/11, 2015-12-11
// 
// No specific personal day 11 goal


module DimitriVranken.AdventOfCode2015.Day11

open System
open System.Text.RegularExpressions 


let private invalidCharacters = [|'i';'o';'l'|]

let private IncrementChar (character : char) : char =
    (char)(int character + 1)

let rec private IncrementPassword (password : string) : string =
    let invalidCharIndex = password.IndexOfAny(invalidCharacters)
    if invalidCharIndex = -1 || invalidCharIndex = password.Length - 1 then
        let substring = password.[0..password.Length - 2]
        let lastChar = Seq.last password

        match lastChar with
        | 'z' -> IncrementPassword substring + "a"
        | _ -> substring + (IncrementChar lastChar).ToString()
    else
        // Increment the invalid char and reset chars to the right to 'a'
        // This saves a few increment and validation iterations
        let invalidCharValue = (int)(password.[invalidCharIndex])
        let nextValidChar = (char)(invalidCharValue + 1)

        let charsToTheLeftCount = invalidCharIndex
        let charsToTheRightCount =  password.Length - invalidCharIndex - 1
        password.[0..charsToTheLeftCount - 1]   // Keep chars on the left
        + nextValidChar.ToString()              // Replace invalid char
        + new String('a', charsToTheRightCount) // Reset chars on the right
        
let private IsPasswordValid (password : string) : bool =
    // May not contain the letters i, o, or l
    let doesNotContainInvalidChars = password.IndexOfAny(invalidCharacters) = -1

    // Must contain at least two different, non-overlapping pairs of letters
    let containsTwoPairs = Regex.IsMatch(password, "(.)\1.*(.)\2")

    // Must include one increasing straight of at least three letters
    let containsSequence = 
        password
        |> Seq.windowed 3
        |> Seq.exists (fun [|a;b;c|] ->
             IncrementChar a = b && IncrementChar b = c)

    doesNotContainInvalidChars
    && containsTwoPairs
    && containsSequence


let Solution (input : string) : (string * string) =
    if String.IsNullOrEmpty input then
        raise (ArgumentNullException "input")

    let solution (currentPassword : string) : string =
        let mutable nextPassword = IncrementPassword currentPassword
        while not (IsPasswordValid nextPassword) do
            nextPassword <- IncrementPassword nextPassword
        nextPassword

    let nextPassword = solution input
    let nextNextPassword = solution nextPassword
    (nextPassword, nextNextPassword)

let FormattedSolution (solution : (string * string)) : string =
    String.Format("Next password: {0}\n" +
                  "Next-next password 2: {1}",
                  fst solution, snd solution)
