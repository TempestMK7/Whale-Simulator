using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionReport {

    public CombatReport Combat { get; }
    public EarnedRewardsContainer EarnedRewards { get; }

    public MissionReport(CombatReport combat, EarnedRewardsContainer earnedRewards) {
        Combat = combat;
        EarnedRewards = earnedRewards;
    }
}