namespace AiTester.Contest
{
    using System.Collections.Generic;
    using System.Linq;
    using Contestants;

    public class ContestConfig
    {
        public string RootFolder { get; set; }
        
        public List<ContestantConfig> Contestants { get; set; }

        public ContestantConfig GetContestantById(string id)
        {
            return Contestants.First(config => config.Id == id);
        }
    }
}