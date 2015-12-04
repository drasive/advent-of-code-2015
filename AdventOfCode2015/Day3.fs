// # Day 3: Perfectly Spherical Houses in a Vacuum
// 
// Santa is delivering presents to an infinite two-dimensional grid of houses.
// 
// He begins by delivering a present to the house at his starting location, and
// then an elf at the North Pole calls him via radio and tells him where to move
// next. Moves are always exactly one house to the north (`^`), south (`v`),
// east (`>`), or west (`<`). After each move, he delivers another present to
// the house at his new location.
// 
// However, the elf back at the north pole has had a little too much eggnog, and
// so his directions are a little off, and Santa ends up visiting some houses
// more than once. How many houses receive _at least one present_?
// 
// For example:
// - `>` delivers presents to `2` houses: one at the starting location, and one
//   to the east.
// - `^>v<` delivers presents to `4` houses in a square, including twice to the
//   house at his starting/ending location.
// - `^v^v^v^v^v` delivers a bunch of presents to some very lucky children at
//   only `2` houses.
//
// # Part Two
// 
// The elves are also running low on ribbon. Ribbon is all the same width, so
// they only have to worry about the length they need to order, which they would
// again like to be exact.
// 
// The ribbon required to wrap a present is the shortest distance around its
// sides, or the smallest perimeter of any one face. Each present also requires
// a bow made out of ribbon as well; the feet of ribbon required for the perfect
// bow is equal to the cubic feet of volume of the present. Don't ask how they
// tie the bow, though; they'll never tell.
// 
// For example:
// - A present with dimensions `2x3x4` requires `2+2+3+3 = 10` feet of ribbon to
//   wrap the present plus `2*3*4 = 24` feet of ribbon for the bow, for a total
//   of `34` feet.
// - A present with dimensions `1x1x10` requires `1+1+1+1 = 4` feet of ribbon to
//   wrap the present plus `1*1*10 = 10` feet of ribbon for the bow, for a total
//   of `14` feet.
// 
// How many total _feet of ribbon_ should they order?
// 
// 
// Source: http://adventofcode.com/day/3, 2015-12-03
// 
// Personal day 3 goal: Use classes, create unit tests


module DimitriVranken.AdventOfCode2015.Day3

open System
open System.Collections.Generic


type Deliverer() =
    member val positionX = 0 with get, set
    member val positionY = 0 with get, set

    // TODO: Removed side effects?
    member this.UpdateLocation (instruction : char) : unit =
        match instruction with
        | '<' -> this.positionX <- this.positionX - 1
        | '^' -> this.positionY <- this.positionY + 1
        | '>' -> this.positionX <- this.positionX + 1
        | 'v' -> this.positionY <- this.positionY - 1
        | _ -> () // Ignore any other characters

let CalculateSantaOnly (input : string) : int =
    let housesVisited = new Dictionary<(int * int), int>()
    let santa = new Deliverer()

     // Starting house is always visited
    housesVisited.Add((0, 0), 1)

    for index = 0 to input.Length - 1 do
        // Update location
        let instruction = input.[index]
        santa.UpdateLocation instruction

        // Update house visits
        let key = (santa.positionX, santa.positionY)
        if housesVisited.ContainsKey(key) then
            housesVisited.Item(key) <- housesVisited.Item(key) + 1
        else
            housesVisited.Add(key, 1)

    housesVisited.Count

let CalculateSantaAndRoboSanta (input : string) : int =
    let housesVisited = new Dictionary<(int * int), int>()
    let santa = new Deliverer()
    let roboSanta = new Deliverer()

     // Starting house is always visited (by all santas)
    housesVisited.Add((0, 0), 2)

    for index = 0 to input.Length - 1 do
        // Update location
        let instruction = input.[index]

        let deliverer = if index % 2 = 0 then santa else roboSanta
        deliverer.UpdateLocation instruction

        // Update house visits
        let key = (deliverer.positionX, deliverer.positionY)
        if housesVisited.ContainsKey(key) then
            housesVisited.Item(key) <- housesVisited.Item(key) + 1
        else
            housesVisited.Add(key, 1)

    housesVisited.Count


let Solve (input: string) : (int * int) =
    if input = null then
        raise (ArgumentNullException "input")

    let resultSantaOnly = CalculateSantaOnly input
    let resultSantaAndRoboSanta = CalculateSantaAndRoboSanta input
    
    (resultSantaOnly, resultSantaAndRoboSanta)

let FormatResult (result : (int * int)) : string =
    String.Format("Santa alone: {0}\n" +
                  "Santa and Robo-Santa: {1}",
                  fst result, snd result)
