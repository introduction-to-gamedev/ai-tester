namespace AiTester.Contest
{
    using System.Collections.Generic;

    public class PlayoffDuelProvider : IDuelsProvider
    {
        private readonly List<(ContestantConfig, ContestantConfig)> pairs;

        private readonly string stage;

        public PlayoffDuelProvider(string stage, List<(ContestantConfig, ContestantConfig)> pairs)
        {
            this.stage = stage;
            this.pairs = pairs;
        }

        public IEnumerable<DuelData> GetDuels()
        {
            foreach (var pair in pairs)
            {
                yield return new DuelData()
                {
                    Group = stage,
                    FirstContestant = pair.Item1,
                    SecondContestant = pair.Item2,
                    TotalMatchesToPlay = 3
                };
            }
        }
    }
}