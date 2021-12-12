namespace AiTester.Contest
{
    using System;
    using System.Threading.Tasks;
    using Contestants;
    using NLog;

    public class Contest<TDuel> where TDuel : DuelRunner, new()
    {
        private readonly ContestConfig config;

        private readonly Func<ILogger> loggerCreator;

        public Contest(ContestConfig config, Func<ILogger> loggerCreator)
        {
            this.config = config;
            this.loggerCreator = loggerCreator;
        }

        public Task Execute()
        {
            var duelRunner = new TDuel();
            duelRunner.SetLogger(loggerCreator());

            var factory = new ContestantFactory();
            var contestantA = factory.Create(config.RootFolder, config.Contestants[0]);
            var contestantB = factory.Create(config.RootFolder, config.Contestants[1]);

            return duelRunner.RunDuel(contestantA, contestantB);
        }
    }
}