using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class StateManager {

    private static string fileName = Application.persistentDataPath + "/WhaleState.dat";
    private static AccountStateContainer currentState;

    public static AccountStateContainer GetCurrentState() {
        LoadCurrentState();
        return currentState;
    }

    private static void LoadCurrentState() {
        if (currentState != null) return;

        if (!File.Exists(fileName)) {
            CreateEmptyContainer();
            return;
        }

        FileStream stream = new FileStream(fileName, FileMode.Open);
        try {
            BinaryFormatter formatter = new BinaryFormatter();
            currentState = (AccountStateContainer)formatter.Deserialize(stream);
        } catch (Exception e) {
            UnityEngine.Debug.Log(e.Message);
        } finally {
            stream.Close();
        }

        if (currentState == null) {
            CreateEmptyContainer();
        }
        currentState.RetrieveDataAfterLoad();
    }

    private static void CreateEmptyContainer() {
        AccountStateContainer container = new AccountStateContainer();
        container.InitializeAccount();
        currentState = container;
        SaveState();
    }

    public static void SaveState() {
        currentState.AccountHeroes.Sort();

        FileStream stream = new FileStream(fileName, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        try {
            formatter.Serialize(stream, currentState);
        } finally {
            stream.Close();
        }
    }

    public static void ClaimRewards(Action<object> handler) {
        GetCurrentState().ClaimMaterials();
        handler.Invoke(null);
    }

    public static void CheatIdleCurrency(long millis) {
        var state = GetCurrentState();
        state.LastClaimTimeStamp -= millis;
        state.ClaimMaterials();
    }

    public static void CheatSummons(int summons) {
        GetCurrentState().CurrentSummons += summons;
        SaveState();
    }

    public static void RequestSummon(int numSummons, Action<List<AccountHero>> handler) {
        AccountStateContainer state = GetCurrentState();
        if (state.CurrentSummons < numSummons) return;
        state.CurrentSummons -= numSummons;
        System.Random rand = new System.Random((int)EpochTime.CurrentTimeMillis());
        List<AccountHero> summonedHeroes = new List<AccountHero>();
        for (int x = 0; x < numSummons; x++) {
            AccountHero newHero = new AccountHero(ChooseRandomHero(rand));
            state.AccountHeroes.Add(newHero);
            summonedHeroes.Add(newHero);
        }
        SaveState();
        handler.Invoke(summonedHeroes);
    }

    private static HeroEnum ChooseRandomHero(System.Random rand) {
        double roll = rand.NextDouble();
        if (roll <= 0.3) {
            return ChooseHeroFromList(rand, BaseHeroContainer.rarityOne);
        } else if (roll <= 0.6) {
            return ChooseHeroFromList(rand, BaseHeroContainer.rarityTwo);
        } else if (roll <= 0.8) {
            return ChooseHeroFromList(rand, BaseHeroContainer.rarityThree);
        } else if (roll <= 0.95) {
            return ChooseHeroFromList(rand, BaseHeroContainer.rarityFour);
        } else {
            return ChooseHeroFromList(rand, BaseHeroContainer.rarityFive);
        }
    }

    private static HeroEnum ChooseHeroFromList(System.Random rand, List<HeroEnum> choices) {
        int choice = rand.Next(choices.Count);
        return choices[choice];
    }

    public static void LevelUpHero(AccountHero hero, Action<bool> handler) {
        if (hero.CurrentLevel >= LevelContainer.MaxLevelForAwakeningValue(hero.AwakeningLevel)) {
            handler.Invoke(false);
            return;
        }

        long cost = LevelContainer.HeroExperienceRequirement(hero.CurrentLevel);
        if (currentState.CurrentGold < cost || currentState.CurrentSouls < cost || hero.CurrentLevel >= 200) {
            handler.Invoke(false);
            return;
        }

        currentState.CurrentGold -= cost;
        currentState.CurrentSouls -= cost;
        hero.CurrentLevel += 1;
        SaveState();
        handler.Invoke(true);
    }

    public static void FuseHero(AccountHero fusedHero, List<AccountHero> destroyedHeroes, Action<bool> handler) {
        if (!FusionIsLegal(fusedHero, destroyedHeroes)) {
            handler.Invoke(false);
            return;
        }

        var accountHeroes = GetCurrentState().AccountHeroes;
        fusedHero.AwakeningLevel++;
        foreach (AccountHero destroyed in destroyedHeroes) {
            accountHeroes.Remove(destroyed);
        }
        accountHeroes.Sort();
        SaveState();
        handler.Invoke(true);
    }

    public static bool FusionIsLegal(AccountHero fusedHero, List<AccountHero> destroyedHeroes) {
        FusionRequirement? requirement = LevelContainer.GetFusionRequirementForLevel(fusedHero.AwakeningLevel);
        if (requirement == null) return false;

        int selectedSameHeroes = 0;
        int selectedFactionHeroes = 0;
        foreach (AccountHero destroyed in destroyedHeroes) {
            if (destroyed == fusedHero) return false;
            if (destroyed.GetBaseHero().Hero == fusedHero.GetBaseHero().Hero && destroyed.AwakeningLevel == requirement?.SameHeroLevel && selectedSameHeroes != requirement?.SameHeroRequirement) {
                selectedSameHeroes++;
            } else if (destroyed.AwakeningLevel == requirement?.FactionHeroLevel) {
                if (requirement?.RequireSameFaction == false || destroyed.GetBaseHero().Faction == fusedHero.GetBaseHero().Faction) selectedFactionHeroes++;
                else return false;
            } else {
                return false;
            }
        }
        return selectedSameHeroes == requirement?.SameHeroRequirement && selectedFactionHeroes == requirement?.FactionHeroRequirement;
    }
}
