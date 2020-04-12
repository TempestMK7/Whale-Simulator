using System;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class FactionSummonRequest {

        public FactionEnum ChosenFaction { get; set; }
        public int SummonCount { get; set; }
        public int SummonRarity { get; set; }
    }
}
