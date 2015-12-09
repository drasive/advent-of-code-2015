module DimitriVranken.AdventOfCode2015.Program

open System
open System.IO

open Logger
open Options


let Options = new Options()


let private parseOptions (arguments : string[]) : bool = 
    // Parse options
    CommandLine.Parser.Default.ParseArgumentsStrict(arguments, Options)
        |> ignore

    // Output options
    if Options.Verbose then
        Logger.Default.UpdateLogLevel NLog.LogLevel.Debug
    
    Logger.Default.Debug("day: {0}", Options.Day);
    Logger.Default.Debug("file: {0}", Options.File);
    Logger.Default.Debug("verbose: {0}", Options.Verbose);
    Logger.Default.Debug("\n");

    // Validate options
    let mutable optionsInvalid = false

    if Options.Day < 1 || Options.Day > 25 then
        optionsInvalid <- true
        Logger.Default.Fatal(
            "Error: The specified day ({0}) must be between 1 and 25",
            Options.Day)

    if not (File.Exists(Options.File)) then
        optionsInvalid <- true
        Logger.Default.Fatal(
            "Error: The specified input file ({0}) does not exist",
            Options.File)

    not optionsInvalid


[<EntryPoint>]
let main (argv : string[]) : int = 
    // Parse options
    if not (parseOptions argv) then
#if DEBUG
        Console.ReadLine() |> ignore
#endif
        Environment.Exit(1);

    // Solve and output puzzle
    let input = File.ReadAllText(Options.File)
    let stopwatch = System.Diagnostics.Stopwatch.StartNew()

    let solution =
        match Options.Day with
        | 1 -> Day1.FormattedSolution (Day1.Solution input)
        | 2 -> Day2.FormattedSolution (Day2.Solution input)
        | 3 -> Day3.FormattedSolution (Day3.Solution input)
        | 4 -> Day4.FormattedSolution input (Day4.Solution input)
        | 5 -> Day5.FormattedSolution (Day5.Solution input)
        | 6 -> Day6.FormattedSolution (Day6.Solution input)
        | 7 -> Day7.FormattedSolution (Day7.Solution input)
        | 8 -> Day8.FormattedSolution (Day8.Solution input)
        | _ -> "The solution for this puzzle is not yet implemented"

    Logger.Default.Info(solution)
    Logger.Default.Debug(
        "Solved in {0:0.00}ms",
        stopwatch.Elapsed.TotalMilliseconds)

#if DEBUG
    Console.ReadLine() |> ignore
#endif
    0
