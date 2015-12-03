module DimitriVranken.AdventOfCode2015.Options

open CommandLine

type Options() = 

    [<Option('d', "day", Required = true, HelpText = "The puzzle to solve (1-25).")>]
    member val Day = 0 with get, set

    [<Option('f', "file", Required = true, HelpText = "The file containing the puzzle input.")>]
    member val File = "" with get, set

    [<Option('v', "verbose", HelpText = "Print verbose output.")>]
    member val Verbose = false with get, set


    [<ParserState>]
    member val LastParserState : IParserState = Unchecked.defaultof<IParserState> with get, set

    // TODO: Implement help text
    //[HelpOption]
    //public string GetUsage()
    //{
    //    // Build an automatic help and error message
    //    return HelpText.AutoBuild(this, (current) => HelpText.DefaultParsingErrorsHandler(this, current));
    //}
