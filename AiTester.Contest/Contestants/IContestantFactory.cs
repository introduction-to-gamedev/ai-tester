namespace AiTester.Contest.Contestants
{
    using IntroToGameDev.AiTester.Utils;

    public interface IContestantFactory
    {
        Contestant Create(string rootPath, ContestantConfig config);
    }

    public class ContestantFactory : IContestantFactory
    {
        public Contestant Create(string rootPath, ContestantConfig config)
        {
            var fullPath = $"{rootPath}/{config.Id}/{config.Command}";
            var pc = new ProcessCreator(fullPath);
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