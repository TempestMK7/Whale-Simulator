using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AccountStateContainer {

    [SerializeField] public string PlayerName { get; set; }

    [SerializeField] public long LastClaimTimeStamp { get; set; }

    [SerializeField] public double CurrentGold { get; set; }
    [SerializeField] public double CurrentSouls { get; set; }
    [SerializeField] public int CurrentLevel { get; set; }
    [SerializeField] public double CurrentExperience { get; set; }
    [SerializeField] public double CurrentGems { get; set; }
    [SerializeField] public double CurrentSummons { get; set; }

    [SerializeField] public double GoldRate { get; set; }
    [SerializeField] public double SoulsRate { get; set; }
    [SerializeField] public double ExperienceRate { get; set; }
    [SerializeField] public double GemInterval { get; set; }
    [SerializeField] public double SummonInterval { get; set; }

    [SerializeField] public List<AccountHero> AccountHeroes { get; set; }

    public void InitializeAccount() {
        PlayerName = "Unregistered Account";

        LastClaimTimeStamp = EpochTime.CurrentTimeMillis();

        CurrentGold = 0;
        CurrentSouls = 0;
        CurrentLevel = 1;
        CurrentExperience = 0;
        CurrentGems = 0;
        CurrentSummons = 0;

        GoldRate = 2.0;
        SoulsRate = 1.0;
        ExperienceRate = 1.0;
        GemInterval = 60.0 * 60.0;
        SummonInterval = 60.0 * 60.0 * 24.0;

        AccountHeroes = new List<AccountHero>();
    }

    public void ClaimMaterials() {
        double timeElapsed = (EpochTime.CurrentTimeMillis() - LastClaimTimeStamp) / 1000.0;
        LastClaimTimeStamp = EpochTime.CurrentTimeMillis();

        CurrentGold += GoldRate * timeElapsed;
        CurrentSouls += SoulsRate * timeElapsed;
        CurrentExperience += ExperienceRate * timeElapsed;
        CurrentGems += (1.0 / GemInterval) * timeElapsed;
        CurrentSummons += (1.0 / SummonInterval) * timeElapsed;

        while (CurrentExperience > LevelContainer.experienceRequirement(CurrentLevel)) {
            CurrentExperience -= LevelContainer.experienceRequirement(CurrentLevel);
            CurrentLevel++;
        }

        StateManager.SaveState();
    }

    public void RetrieveDataAfterLoad() {
        foreach (AccountHero hero in AccountHeroes) {
            hero.LoadBaseHero();
        }
    }
}
