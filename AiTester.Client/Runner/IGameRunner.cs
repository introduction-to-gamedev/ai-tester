namespace IntroToGameDev.AiTester
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using NLog;

    public interface IGameRunner
    {
        Task<SingleTestResult> Play(ILogger logger, StreamWriter input, StreamReader output);
    }

    public abstract class GameRunner : IGameRunner
    {
        protected readonly CommandFetcher Fetcher = new();

        public abstract Task<SingleTestResult> Play(ILogger logger, StreamWriter input, StreamReader output);
    }
}