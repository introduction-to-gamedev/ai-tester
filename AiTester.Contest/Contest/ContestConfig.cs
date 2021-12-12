namespace AiTester.Contest
{
    using System.Collections.Generic;

    public class ContestConfig
    {
        public string RootFolder { get; set; }
        
        public List<ContestantConfig> Contestants { get; set; }
    }

    public class ContestantConfig
    {
        public string Id { get; init; }
        
        public string Command { get; init; }
    }
}