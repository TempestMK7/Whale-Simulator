using System;
using UnityEngine;

[Serializable]
public class AccountStateContainer {

    [SerializeField] public string playerName;

    [SerializeField] public long lastClaimTimeStamp;

    [SerializeField] public double currentGold;
    [SerializeField] public double currentSouls;
    [SerializeField] public double currentLevel;
    [SerializeField] public double currentExperience;
    [SerializeField] public double currentGems;
    [SerializeField] public double currentScrolls;

    [SerializeField] public double goldRate;
    [SerializeField] public double soulsRate;
    [SerializeField] public double experienceRate;
    [SerializeField] public double gemRate;
    [SerializeField] public double scrollRate;

    public void InitializeAccount() {
        lastClaimTimeStamp = EpochTime.CurrentTimeMillis();

        currentGold = 0;
        currentSouls = 0;
        currentLevel = 1;
        currentExperience = 0;
        currentGems = 0;
        currentScrolls = 0;

        goldRate = 2.0;
        soulsRate = 1.0;
        experienceRate = 1.0;
        gemRate = 0f;
        scrollRate = 0f;
    }

    public void ClaimMaterials() {
        double timeElapsed = (EpochTime.CurrentTimeMillis() - lastClaimTimeStamp) / 1000.0;
        lastClaimTimeStamp = EpochTime.CurrentTimeMillis();

        currentGold += goldRate * timeElapsed;
        currentSouls += goldRate * timeElapsed;
        currentExperience += goldRate * timeElapsed;
        currentGems += goldRate * timeElapsed;
        currentScrolls += goldRate * timeElapsed;

        StateManager.SaveState();
    }
}
