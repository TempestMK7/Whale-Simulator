using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsContainer {

    public int Summons { get; }
    public int Gold { get; }
    public int Souls { get; }
    public int PlayerExperience { get; }

    public RewardsContainer(int summons, int gold, int souls, int playerExperience) {
        Summons = summons;
        Gold = gold;
        Souls = souls;
        PlayerExperience = playerExperience;
    }
}
