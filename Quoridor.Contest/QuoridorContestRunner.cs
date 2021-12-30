namespace Quoridor.Contest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AiTester.Contest;
    using NLog;
    using NLog.Config;
    using NLog.Layouts;
    using NLog.Targets;

    public class QuoridorContestRunner
    {
        static async Task Main(string[] args)
        {
            var config = PrepareConfig();
            var groups = GetGroups(config).ToList();
            await new Contest<QuoridorDuelRunner>(config, new GroupDuelsProvider(groups),
                data =>
                {
                    SetUpLoggingFor(data);
                    return LogManager.LogFactory.GetLogger("Logger");
                }).Execute();
        }


        private static void SetUpLoggingFor(DuelData duelData)
        {
            var config = new LoggingConfiguration();

            Layout fileName =
                $"{duelData.Group}/{duelData.FirstContestant.Id}-vs-{duelData.SecondContestant.Id}.log";
            var logFile = new FileTarget("logfile") { FileName = fileName };
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logFile);

            var logConsole = new ConsoleTarget("logconsole");
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);

            LogManager.Configuration = config;
        }

        private static IEnumerable<GroupConfig> GetGroups(ContestConfig config)
        {
            // yield return new GroupConfig()
            // {
            // GroupLetter = "A",
            // Contestants = new List<ContestantConfig>()
            // {
            // config.GetContestantById("casablanca"),
            // config.GetContestantById("mike"),
            // config.GetContestantById("whiskey"),
            // config.GetContestantById("toronto"),
            // config.GetContestantById("yankee"),
            // config.GetContestantById("yokohama"),
            // }
            // };

            // yield return new GroupConfig()
            // {
            //     GroupLetter = "B",
            //     Contestants = new List<ContestantConfig>()
            //     {
            //         config.GetContestantById("miku"),
            //         config.GetContestantById("foxtrot"),
            //         config.GetContestantById("xmas"),
            //         config.GetContestantById("oslo"),
            //         config.GetContestantById("king"),
            //         config.GetContestantById("madagaskar"),
            //     }
            // };
            //
            yield return new GroupConfig()
            {
                GroupLetter = "C",
                Contestants = new List<ContestantConfig>()
                {
                    // config.GetContestantById("paris"),
                    // config.GetContestantById("romeo"),
                    config.GetContestantById("kilo"),
                    // config.GetContestantById("juilett"),
                    // config.GetContestantById("bravo"),
                    config.GetContestantById("oscar"),
                }
            };

            // yield return new GroupConfig()
            // {
            // GroupLetter = "D",
            // Contestants = new List<ContestantConfig>()
            // {
            // config.GetContestantById("charlie"),
            // config.GetContestantById("golf"),
            // config.GetContestantById("india"),
            // config.GetContestantById("sierra"),
            // config.GetContestantById("hotel"),
            // config.GetContestantById("kilo"),
            // }
            // };

            // yield return new GroupConfig()
            // {
            //     GroupLetter = "E",
            //     Contestants = new List<ContestantConfig>()
            //     {
            //         config.GetContestantById("santiago"),
            //         config.GetContestantById("jerusalem"),
            //         config.GetContestantById("oscar"),
            //         config.GetContestantById("november"),
            //         config.GetContestantById("queen"),
            //         config.GetContestantById("delta"),
            //     }
            // };
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
                    },
                    new()
                    {
                        Id = "casablanca",
                        Command = "main.exe"
                    },
                    new()
                    {
                        Id = "charlie",
                        Command = "Quoridor.AiTestClient.exe"
                    },
                    new()
                    {
                        Id = "csharp",
                        Command = "Bot.exe"
                    },
                    new()
                    {
                        Id = "delta",
                        Command = "QuoridorConsole.exe"
                    },
                    new()
                    {
                        Id = "florida",
                        Command = "QuoridorConsole.exe"
                    },
                    new()
                    {
                        Id = "foxtrot",
                        Command = "ConsoleTestProgram.exe"
                    },
                    new()
                    {
                        Id = "hotel",
                        Command = "GameDev.exe"
                    },
                    new()
                    {
                        Id = "india",
                        Command = "main.exe"
                    },
                    new()
                    {
                        Id = "jerusalem",
                        Command = "QuorridorAI.exe"
                    },
                    new()
                    {
                        Id = "juilett",
                        Command = "Quoridor.Console.App.PvAI.exe"
                    },
                    new()
                    {
                        Id = "kilo",
                        Command = "QuoridorGame.exe"
                    },
                    new()
                    {
                        Id = "king",
                        Command = "Qouridor.ConsoleAI.exe"
                    },
                    new()
                    {
                        Id = "madagaskar",
                        Command = "start-bot.bat"
                    },
                    new()
                    {
                        Id = "mike",
                        Command = "QuoridorCmd.exe"
                    },
                    new()
                    {
                        Id = "miku",
                        Command = "lima.exe \"set TIME_PER_MOVE=4500\""
                    },
                    new()
                    {
                        Id = "november",
                        Command = "Quoridor.Console.exe"
                    },
                    new()
                    {
                        Id = "oscar",
                        Command = "Quoridor.exe"
                    },
                    new()
                    {
                        Id = "oslo",
                        Command = "Quoridor.exe"
                    },
                    new()
                    {
                        Id = "paris",
                        Command = "lab2.exe"
                    },
                    new()
                    {
                        Id = "queen",
                        Command = "Quoridor.Console.App.exe"
                    },
                    new()
                    {
                        Id = "romeo",
                        Command = "Controller.exe"
                    },
                    new()
                    {
                        Id = "sierra",
                        Command = "Quoridor.exe"
                    },
                    new()
                    {
                        Id = "victor",
                        Command = "Quoridor.exe"
                    },
                    new()
                    {
                        Id = "whiskey",
                        Command = "Quoridor_AI.exe"
                    },
                    new()
                    {
                        Id = "xmas",
                        Command = "Quoridor.Cnsl.App.exe"
                    },
                    new()
                    {
                        Id = "yankee",
                        Command = "Quoridor.exe"
                    },
                    new()
                    {
                        Id = "yokohama",
                        Command = "Quoridor.exe"
                    },
                    new()
                    {
                        Id = "zurich",
                        Command = "Controller.exe"
                    },
                    new()
                    {
                        Id = "toronto",
                        Command = "Quoridor.exe"
                    },
                    new()
                    {
                        Id = "tango",
                        Command = "ConsoleChessApp2.exe"
                    },
                    new()
                    {
                        Id = "golf",
                        Command = "QuoridorBotGolf.exe"
                    },
                    new()
                    {
                        Id = "santiago",
                        Command = "QuoridorAI.exe"
                    },
                }
            };
        }
    }
}