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

    public static void RequestSummon(int numSummons, Action<List<AccountHero>> handler) {
        AccountStateContainer state = GetCurrentState();
        System.Random rand = new System.Random((int)EpochTime.CurrentTimeMillis());
        // state.CurrentSummons -= numSummons;
        List<AccountHero> summonedHeroes = new List<AccountHero>();
        for (int x = 0; x < numSummons; x++) {
            AccountHero newHero = new AccountHero(ChooseRandomHero(rand));
            state.AccountHeroes.Add(newHero);
            summonedHeroes.Add(newHero);
        }
        // state.AccountHeroes.Sort();
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
}
