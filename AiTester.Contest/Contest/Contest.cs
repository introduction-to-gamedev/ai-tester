namespace AiTester.Contest
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contestants;
    using NLog;

    public class Contest<TDuel> where TDuel : DuelRunner, new()
    {
        private readonly ContestConfig config;
        
        private readonly IDuelsProvider duelsProvider;

        private readonly Func<DuelData, ILogger> loggerCreator;

        private readonly IContestantFactory factory = new ContestantFactory();

        public Contest(ContestConfig config, IDuelsProvider duelsProvider, Func<DuelData, ILogger> loggerCreator)
        {
            this.config = config;
            this.duelsProvider = duelsProvider;
            this.loggerCreator = loggerCreator;
        }

        public async Task Execute()
        {
            foreach (var duelData in duelsProvider.GetDuels())
            {
                await PerformDuel(duelData);
            }

            async Task PerformDuel(DuelData duelData)
            {
                var logger = loggerCreator(duelData);
                
                var firstContestantId = duelData.FirstContestant.Id;
                var secondContestantId = duelData.SecondContestant.Id;
                
                logger.Log(LogLevel.Info, $"{firstContestantId} VS {secondContestantId}");
                
                var wins = new Dictionary<string, int>()
                {
                    [firstContestantId] = 0,
                    [secondContestantId] = 0,
                };
                
                for (var match = 0; match < duelData.TotalMatchesToPlay; match++)
                {
                    var duelRunner = new TDuel();
                    

                    duelRunner.SetLogger(logger);  
                
                    var contestantA = factory.Create(config.RootFolder, duelData.FirstContestant);
                    if (contestantA == null)
                    {
                        logger.Log(LogLevel.Error, $"Can not crete process for {duelData.FirstContestant.Id}, aborting");
                        return;
                    }
                    
                    var contestantB = factory.Create(config.RootFolder, duelData.SecondContestant);
                    if (contestantB == null)
                    {
                        logger.Log(LogLevel.Error, $"Can not crete process for {duelData.SecondContestant.Id}, aborting");
                        return;
                    }
                    
                    var winner = await duelRunner.RunDuel(contestantA, contestantB);
                    wins[winner]++;
                    
                    logger.Log(LogLevel.Info, $"{firstContestantId} {wins[firstContestantId]} : {wins[secondContestantId]} {secondContestantId}");
                }
            }
        }
    }
}