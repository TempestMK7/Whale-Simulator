using System;
using Com.Tempest.Whale.Combat;

namespace Com.Tempest.Whale.GameObjects {

    public class MissionReport {

        public CombatReport Combat { get; }
        public EarnedRewardsContainer EarnedRewards { get; }

        public MissionReport(CombatReport combat, EarnedRewardsContainer earnedRewards) {
            Combat = combat;
            EarnedRewards = earnedRewards;
        }
    }
}