using System;
using System.Collections.Generic;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class SummonResponse {

        public long CurrentSummons { get; set; }
        public List<AccountHero> SummonedHeroes { get; set; }
    }
}
