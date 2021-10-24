namespace IntroToGameDev.AiTester
{
    using System;
    using CommandLine;
    using NLog;
    using NLog.Config;
    using NLog.Targets;
    using Options;

    public abstract class AiTester
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(ExecuteTests);
        }

        private static async void ExecuteTests(CommandLineOptions options)
        {
            var logger = LogManager.LogFactory.GetLogger("Logger");
            
            SetUpLogging(options);
        }
        
        private static void SetUpLogging(CommandLineOptions commandLineOptions)
        {
            var config = new LoggingConfiguration();

            if (commandLineOptions.WriteLogsToFile)
            {
                var logFile = new FileTarget("logfile") {FileName = "run_results.log"};
                config.AddRule(LogLevel.Info, LogLevel.Fatal, logFile);    
            }

            if (commandLineOptions.WriteLogsToConsole)
            {
                var logConsole = new ConsoleTarget("logconsole");
                config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
            }
            
            LogManager.Configuration = config;
        }
    }
}