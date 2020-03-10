using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AccountState {

    [SerializeField] public string PlayerName { get; set; }

    [SerializeField] public long LastClaimTimeStamp { get; set; }

    [SerializeField] public double CurrentGold { get; set; }
    [SerializeField] public double CurrentSouls { get; set; }
    [SerializeField] public int CurrentLevel { get; set; }
    [SerializeField] public double CurrentExperience { get; set; }
    [SerializeField] public double CurrentGems { get; set; }
    [SerializeField] public double CurrentSummons { get; set; }

    [SerializeField] public int CurrentChapter { get; set; }
    [SerializeField] public int CurrentMission { get; set; }

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

        CurrentChapter = 1;
        CurrentMission = 1;

        AccountHeroes = new List<AccountHero>();
    }

    public void ClaimMaterials() {
        double timeElapsed = (EpochTime.CurrentTimeMillis() - LastClaimTimeStamp);
        LastClaimTimeStamp = EpochTime.CurrentTimeMillis();
        var generation = MissionContainer.GetGenerationInfo();

        CurrentGold += GenerationInfo.GenerationPerMillisecond(generation.GoldPerMinute) * timeElapsed;
        CurrentSouls += GenerationInfo.GenerationPerMillisecond(generation.SoulsPerMinute) * timeElapsed;
        CurrentExperience += GenerationInfo.GenerationPerMillisecond(generation.ExperiencePerMinute) * timeElapsed;

        while (CurrentExperience > LevelContainer.ExperienceRequirement(CurrentLevel)) {
            CurrentExperience -= LevelContainer.ExperienceRequirement(CurrentLevel);
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
