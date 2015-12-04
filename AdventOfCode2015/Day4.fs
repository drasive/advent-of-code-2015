// # Day 4: The Ideal Stocking Stuffer
// 
// Santa needs help mining some AdventCoins (very similar to bitcoins) to use as
// gifts for all the economically forward-thinking little girls and boys.
// 
// To do this, he needs to find MD5 hashes which, in hexadecimal, start with at
// least _five zeroes_. The input to the MD5 hash is some secret key
// (your puzzle input, given below) followed by a number in decimal. To mine
// AdventCoins, you must find Santa the lowest positive number (no leading
// zeroes: `1`, `2`, `3`, ...) that produces such a hash.
// 
// For example:
// - If your secret key is `abcdef`, the answer is `609043`, because the MD5
//   hash of `abcdef609043` starts with five zeroes (`000001dbbfa...`), and it
//   is the lowest such number to do so.
// - If your secret key is `pqrstuv`, the lowest number it combines with to make
//   an MD5 hash starting with five zeroes is `1048970`; that is, the MD5 hash
//   of `pqrstuv1048970` looks like `000006136ef...`.
// 
// ## Part Two
// 
// Now find one that starts with _six zeroes_.
// 
// 
// Source: http://adventofcode.com/day/4, 2015-12-04
// 
// Personal day 4 goal: Use sequences and parallelism


module DimitriVranken.AdventOfCode2015.Day4

open System


let private MD5Calculator = System.Security.Cryptography.MD5.Create()


let private ComputeMD5Hash (input : string) : byte[] =
    let inputBytes = System.Text.Encoding.ASCII.GetBytes input
    MD5Calculator.ComputeHash inputBytes
    
let private HasMD5FiveLeadingZeroes (hash : byte[]) : bool =
    hash.[0]     = (byte)0x00 // Characters 1 and 2 are "0" (zero)
    && hash.[1]  = (byte)0x00 // Characters 3 and 4 are "0" (zero)
    && hash.[2] <= (byte)0x0f // Character 5 is a "0" (zero)

let private HasMD5SixLeadingZeroes (hash : byte[]) : bool =
    hash.[0]    = (byte)0x00 // Characters 1 and 2 are "0" (zero)
    && hash.[1] = (byte)0x00 // Characters 3 and 4 are "0" (zero)
    && hash.[2] = (byte)0x00 // Characters 5 and 6 are "0" (zero)

let private MD5HashToHex (hash : byte[]) : string =
    let stringBuilder = new System.Text.StringBuilder()
    for hashIndex = 0 to hash.Length - 1 do
        stringBuilder.Append(hash.[hashIndex].ToString("x2")) |> ignore
    stringBuilder.ToString();


let Solve (input: string) : (int * int) =
    if input = null then
        raise (ArgumentNullException "input")

    let fiveZerosNumber =
        Seq.initInfinite(fun i -> i)
        |> Seq.find(fun i ->
            let hash = ComputeMD5Hash (input + i.ToString())
            HasMD5FiveLeadingZeroes hash)
    let sixZerosNumber =
        Seq.initInfinite(fun i ->
            // Continue from previous index
            i + fiveZerosNumber)
        |> Seq.find(fun i ->
            let hash = ComputeMD5Hash (input + i.ToString())
            HasMD5SixLeadingZeroes hash)

    (fiveZerosNumber, sixZerosNumber)

let FormatResult (input : string) (result : (int * int)) : string =
    let fiveZerosHash = ComputeMD5Hash (input + (fst result).ToString())
    let sixZerosHash = ComputeMD5Hash (input + (snd result).ToString())

    String.Format("5 leading zeros: {0} (hash {1})\n" +
                  "6 leading zeros: {2} (hash {3})",
                  fst result, MD5HashToHex fiveZerosHash,
                  snd result, MD5HashToHex sixZerosHash)
