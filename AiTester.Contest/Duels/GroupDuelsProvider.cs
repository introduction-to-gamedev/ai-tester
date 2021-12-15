namespace AiTester.Contest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GroupDuelsProvider : IDuelsProvider
    {
        private readonly IList<GroupConfig> groups;

        public GroupDuelsProvider(IList<GroupConfig> groups)
        {
            this.groups = groups;
        }

        public IEnumerable<DuelData> GetDuels()
        {
            foreach (var group in groups)
            {
                foreach (var duelPair in group.GetDuelPairs())
                {
                    yield return new DuelData()
                    {
                        Group = group.GroupLetter,
                        FirstContestant = duelPair.Item1,
                        SecondContestant = duelPair.Item2,
                        TotalMatchesToPlay = 3
                    };
                }
            }
        }
    }
}