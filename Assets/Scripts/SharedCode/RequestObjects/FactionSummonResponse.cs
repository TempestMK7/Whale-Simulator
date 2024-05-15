using System;
using System.Collections.Generic;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class FactionSummonResponse {

        public long CurrentBronzeSummons { get; set; }
        public long CurrentSilverSummons { get; set; }
        public long CurrentGoldSummons { get; set; }
        public List<AccountHero> SummonedHeroes { get; set; }
    }
}
