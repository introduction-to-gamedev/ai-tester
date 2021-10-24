namespace IntroToGameDev.AiTester
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IGameRunner
    {
        Task<SingleTestResult> Play(StreamReader input, StreamWriter output);
    }
}