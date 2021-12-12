namespace Quoridor.Contest
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AiTester.Contest;
    using NLog;
    using NLog.Config;
    using NLog.Targets;

    public class QuoridorContestRunner
    {
        static async Task Main(string[] args)
        {
            SetUpLogging();
            
            var config = PrepareConfig();
            await new Contest<QuoridorDuelRunner>(config, () => LogManager.LogFactory.GetLogger("Logger")).Execute();
        }

        private static ContestConfig PrepareConfig()
        {
            return new ContestConfig()
            {
                RootFolder = "G:\\Contest",
                Contestants = new List<ContestantConfig>()
                {
                    new()
                    {
                        Id = "alpha",
                        Command = "Quoridor.ConsoleClient.exe"
                    },
                    new()
                    {
                        Id = "bravo",
                        Command = "QuoridorConsole.exe"
                    }
                }
            };
        }
        
        private static void SetUpLogging()
        {
            var config = new LoggingConfiguration();

            // if (commandLineOptions.WriteLogsToFile)
            // {
                // var logFile = new FileTarget("logfile") { FileName = "run_results.log" };
                // config.AddRule(LogLevel.Info, LogLevel.Fatal, logFile);
            // }

            // if (commandLineOptions.WriteLogsToConsole)
            // {
                var logConsole = new ConsoleTarget("logconsole");
                config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
            // }

            LogManager.Configuration = config;
        }
    }
}