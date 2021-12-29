namespace AiTester.Contest.Contestants
{
    using System.IO;
    using IntroToGameDev.AiTester.Utils;

    public interface IContestantFactory
    {
        Contestant Create(string rootPath, ContestantConfig config);
    }

    public class ContestantFactory : IContestantFactory
    {
        public Contestant Create(string rootPath, ContestantConfig config)
        {
            var folder = Path.Combine(rootPath, config.Id);
            var pc = new ProcessCreator(folder, config.Command);
            var process = pc.Create(error => { });
            if (process == null)
            {
                return null;
            }

            return new Contestant(process.StandardInput, process.StandardOutput)
            {
                Id = config.Id,
            };
        }
    }
}