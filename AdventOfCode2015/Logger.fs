module DimitriVranken.AdventOfCode2015.Logger

open NLog

type Logger() = 

    static let _default = 
        NLog.LogManager.GetLogger("Default")


    static member public Default =
        _default


    static member public UpdateLogLevel (loggerPatter : string) (logLevel : LogLevel) : unit =
        for rule in LogManager.Configuration.LoggingRules do
            if rule.NameMatches(loggerPatter)
            then rule.EnableLoggingForLevel(logLevel)
        
        LogManager.ReconfigExistingLoggers()
    
    type NLog.Logger with
        member this.UpdateLogLevel (logLevel : LogLevel) : unit =
            Logger.UpdateLogLevel this.Name logLevel
