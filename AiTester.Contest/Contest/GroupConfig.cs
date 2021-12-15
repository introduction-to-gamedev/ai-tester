namespace AiTester.Contest
{
    using System;
    using System.Collections.Generic;

    public class GroupConfig
    {
        public string GroupLetter { get; set; }
        
        public List<ContestantConfig> Contestants { get; set; }

        public IEnumerable<(ContestantConfig, ContestantConfig)> GetDuelPairs()
        {
            for (var i = 0; i < Contestants.Count - 1; i++)
            {
                for (var j = i + 1; j < Contestants.Count; j++)
                {
                    yield return (Contestants[i], Contestants[j]);
                }
            }
        }
    }
}