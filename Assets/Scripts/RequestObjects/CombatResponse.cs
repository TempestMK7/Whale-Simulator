using System;
using System.Collections.Generic;
using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class CombatResponse {

        public CombatReport Report { get; set; }
        public EarnedRewardsContainer Rewards { get; set; }
    }
}
