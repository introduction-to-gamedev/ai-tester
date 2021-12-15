namespace AiTester.Contest
{
    using System.Collections.Generic;
    using Contestants;

    public interface IDuelsProvider
    {
        IEnumerable<DuelData> GetDuels();
    }

    public class DuelData
    {
        public string Group { get; init; }
        
        public ContestantConfig FirstContestant { get; init; }
        
        public ContestantConfig SecondContestant { get; init; }
        
        public int TotalMatchesToPlay { get; init; }
    }
}