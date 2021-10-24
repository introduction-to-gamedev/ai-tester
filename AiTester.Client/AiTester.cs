namespace IntroToGameDev.AiTester
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CommandLine;
    using NLog;
    using NLog.Config;
    using NLog.Targets;
    using Options;

    public abstract class AiTester<T> where T : IGameRunner, new()
    {
        protected static void ParseArgsAndStart(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(ExecuteTests);
        }

        private static async void ExecuteTests(CommandLineOptions options)
        {
            var logger = LogManager.LogFactory.GetLogger("Logger");

            try
            {
                SetUpLogging(options);

                if (options.SingleRun)
                {
                    logger.Log(LogLevel.Info, "Executing single run...");
                    var result = await new SingleTestExecutor(logger, new T()).Execute(options.RunCommand);
                    logger.Log(result.IsCompletedSuccessfully ? LogLevel.Info : LogLevel.Error,
                        $"{result.Type} {result.Error}");
                    return;
                }

                logger.Log(LogLevel.Info, "Executing full test...");

                var aggregated = new ResultsAggregator().Aggregate(await Task.WhenAll(Enumerable.Range(1, 100)
                    .Select(async index =>
                    {
                        logger.Log(LogLevel.Info, $"----------------------");
                        logger.Log(LogLevel.Info, $"Executing run #{index}");
                        var result = await new SingleTestExecutor(logger, new T()).Execute(options.RunCommand);
                        logger.Log(result.IsCompletedSuccessfully ? LogLevel.Info : LogLevel.Error,
                            $"{result.Type} {result.Error}");
                        return result;
                    })));

                Console.WriteLine(aggregated);
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e);
                throw;
            }
        }

        private static void SetUpLogging(CommandLineOptions commandLineOptions)
        {
            var config = new LoggingConfiguration();

            if (commandLineOptions.WriteLogsToFile)
            {
                var logFile = new FileTarget("logfile") { FileName = "run_results.log" };
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