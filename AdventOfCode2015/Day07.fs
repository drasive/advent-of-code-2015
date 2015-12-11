// ## Day 7: Some Assembly Required
// 
// This year, Santa brought little Bobby Tables a set of wires and bitwise logic
// gates! Unfortunately, little Bobby is a little under the recommended age
// range, and he needs help assembling the circuit.
// 
// Each wire has an identifier (some lowercase letters) and can carry a 16-bit
// signal (a number from `0` to `65535`). A signal is provided to each wire by a
// gate, another wire, or some specific value. Each wire can only get a signal
// from one source, but can provide its signal to multiple destinations. A gate
// provides no signal until all of its inputs have a signal.
// 
// The included instructions booklet describes how to connect the parts
// together: `x AND y -> z` means to connect wires `x` and `y` to an AND gate,
// and then connect its output to wire `z`.
// 
// For example:
// - `123 -> x` means that the signal `123` is provided to wire `x`.
// - `x AND y -> z` means that the bitwise AND of wire `x` and wire `y` is
//   provided to wire `z`.
// - `p LSHIFT 2 -> q` means that the value from wire `p` is left-shifted by `2`
//   and then provided to wire `q`.
// - `NOT e -> f` means that the bitwise complement of the value from wire `e`
//   is provided to wire `f`.
// 
// Other possible gates include `OR` (bitwise OR) and `RSHIFT` (right-shift).
// If, for some reason, you'd like to _emulate_ the circuit instead, almost all
// programming languages (for example, C, JavaScript, or Python) provide
// operators for these gates.
// 
// For example, here is a simple circuit:
//   123 -> x
//   456 -> y
//   x AND y -> d
//   x OR y -> e
//   x LSHIFT 2 -> f
//   y RSHIFT 2 -> g
//   NOT x -> h
//   NOT y -> i
// 
// After it is run, these are the signals on the wires:
//   d: 72
//   e: 507
//   f: 492
//   g: 114
//   h: 65412
//   i: 65079
//   x: 123
//   y: 456
// 
// In little Bobby's kit's instructions booklet (provided as your puzzle input),
// what signal is ultimately provided to _wire `a`_?
// 
// 
// Source: http://adventofcode.com/day/7, 2015-12-07
// 
// Personal day 7 goal: Use functional style


module DimitriVranken.AdventOfCode2015.Day07

open System
open System.Collections.Generic
open System.Text.RegularExpressions


[<AbstractClass>]
type Wire() = 
    abstract member Value : UInt16

type private ValueWire(value : UInt16) =
    inherit Wire()
    override this.Value = value

type private AndWire(input1 : Wire, input2 : Wire) =
    inherit Wire()
    override this.Value = input1.Value &&& input2.Value

type private OrWire(input1 : Wire, input2 : Wire) =
    inherit Wire()
    override this.Value = input1.Value ||| input2.Value

type private ShiftWire(input : Wire, shiftLeft : bool, shiftAmount : int) =
    inherit Wire()
    override this.Value =
        if shiftLeft then
            input.Value <<< shiftAmount
        else
            input.Value >>> shiftAmount

type private NotWire(input : Wire) =
    inherit Wire()
    override this.Value = ~~~ input.Value
    
let private ParseInstruction (nodes : Dictionary<string, Wire>) (line : string)
    : (string * Wire) =
    // TODO: What to do when input node doesn't exist yet?
    let node =
        if line.Contains("AND") || line.Contains("OR") then
            let pattern = @"^(.+) (AND|OR) (.+) ->"
            let matchedGroups = Regex.Match(line, pattern).Groups

            let input1 = nodes.Item matchedGroups.[1].Value
            let isAndWire = matchedGroups.[2].Value = "AND"
            let input2 = nodes.Item matchedGroups.[3].Value

            if isAndWire then
                new AndWire(input1, input2) :> Wire
            else
                new OrWire(input1, input2) :> Wire
        elif line.Contains("NOT") then
            let pattern = @"^NOT (.+) ->"
            let matchedGroups = Regex.Match(line, pattern).Groups

            let input = nodes.Item matchedGroups.[1].Value

            new NotWire(input) :> Wire
        elif line.Contains("SHIFT") then
            let pattern = @"^(.+) (LSHIFT|RSHIFT) (.+) ->"
            let matchedGroups = Regex.Match(line, pattern).Groups

            let input = nodes.Item matchedGroups.[1].Value
            let shiftLeft = matchedGroups.[2].Value = "LSHIFT"
            let shiftAmount = Convert.ToInt32(matchedGroups.[3].Value)

            new ShiftWire(input, shiftLeft, shiftAmount) :> Wire
        else
            let pattern = @"^(.+) ->"
            let matchedGroups = Regex.Match(line, pattern).Groups

            let value = Convert.ToUInt16(matchedGroups.[1].Value)

            new ValueWire(value) :> Wire

    let targetWirePattern = @"-> (.+)$"
    let targetWire = Regex.Match(line, targetWirePattern).Groups.[1].Value

    (targetWire, node)


let Solution (input : string) : (Dictionary<string, Wire>) =
    if String.IsNullOrEmpty input then
        raise (ArgumentNullException "input")

    let nodes = new Dictionary<string, Wire>()

    let lines = input.Split('\n')
    lines
    |> Seq.iter (fun line ->
        nodes.Add(ParseInstruction nodes line))
    |> ignore

    nodes

let FormattedSolution (solution : Dictionary<string, Wire>) : string =
    String.Format("Wire a: {0}\n" +
                  "Wire b: {1}",
                  solution.Item("a").Value, solution.Item("a").Value)
