using System;
using System.Collections.Generic;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class FuseHeroResponse {

        public bool FusionSuccessful { get; set; }
        public AccountHero FusedHero { get; set; }
    }
}
