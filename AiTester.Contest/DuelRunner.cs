namespace AiTester.Contest
{
    using System.Threading.Tasks;
    using Contestants;
    using NLog;

    public interface IDuelRunner
    {
        Task RunDuel(Contestant contestantA, Contestant contestantB);
    }

    public abstract class DuelRunner : IDuelRunner
    {
        protected ILogger Logger;

        public void SetLogger(ILogger logger)
        {
            Logger = logger;
        }
        
        public abstract Task RunDuel(Contestant contestantA, Contestant contestantB);
    }
}