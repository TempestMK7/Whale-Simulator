using System;
using UnityEngine;

[Serializable]
public class AccountStateContainer {

    [SerializeField] public string playerName;

    [SerializeField] public long lastClaimTimeStamp;

    [SerializeField] public double currentGold;
    [SerializeField] public double currentSouls;
    [SerializeField] public int currentLevel;
    [SerializeField] public double currentExperience;
    [SerializeField] public double currentGems;
    [SerializeField] public double currentScrolls;

    [SerializeField] public double goldRate;
    [SerializeField] public double soulsRate;
    [SerializeField] public double experienceRate;
    [SerializeField] public double gemInterval;
    [SerializeField] public double scrollInterval;

    public void InitializeAccount() {
        playerName = "Unregistered Account";

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
        gemInterval = 60.0 * 60.0;
        scrollInterval = 60.0 * 60.0 * 24.0;
    }

    public void ClaimMaterials() {
        double timeElapsed = (EpochTime.CurrentTimeMillis() - lastClaimTimeStamp) / 1000.0;
        lastClaimTimeStamp = EpochTime.CurrentTimeMillis();

        currentGold += goldRate * timeElapsed;
        currentSouls += soulsRate * timeElapsed;
        currentExperience += experienceRate * timeElapsed;
        currentGems += (1.0 / gemInterval) * timeElapsed;
        currentScrolls += (1.0 / scrollInterval) * timeElapsed;

        while (currentExperience > LevelContainer.experienceRequirement(currentLevel)) {
            currentExperience -= LevelContainer.experienceRequirement(currentLevel);
            currentLevel++;
        }

        StateManager.SaveState();
    }
}
