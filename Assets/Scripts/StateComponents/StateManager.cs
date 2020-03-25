using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class StateManager {

    private static string fileName = Application.persistentDataPath + "/WhaleState.txt";
    private static AccountState currentState;

    public static AccountState GetCurrentState() {
        LoadCurrentState();
        return currentState;
    }

    private static void LoadCurrentState() {
        if (currentState != null) return;

        if (!File.Exists(fileName)) {
            CreateEmptyContainer();
            return;
        }

        StreamReader reader = new StreamReader(fileName);
        try {
            currentState = JsonConvert.DeserializeObject<AccountState>(reader.ReadLine());
            currentState.RetrieveDataAfterLoad();
        } catch (Exception e) {
            UnityEngine.Debug.Log(e.Message);
        } finally {
            reader.Close();
        }

        if (currentState == null) {
            CreateEmptyContainer();
            currentState.RetrieveDataAfterLoad();
        }
    }

    private static void CreateEmptyContainer() {
        AccountState container = new AccountState();
        container.InitializeAccount();
        currentState = container;
        SaveState();
    }

    public static void SaveState() {
        currentState.AccountHeroes.Sort();
        currentState.AccountEquipment.Sort();

        StreamWriter writer = new StreamWriter(fileName, false);
        writer.WriteLine(JsonConvert.SerializeObject(currentState));
        writer.Close();
    }

    public static void NotifyHubEntered() {
        currentState.HasEnteredHub = true;
        SaveState();
    }

    public static void NotifyPortalEntered() {
        currentState.HasEnteredPortal = true;
        SaveState();
    }

    public static void NotifySanctumEntered() {
        currentState.HasEnteredSanctum = true;
        SaveState();
    }

    public static void NotifyCampaignEntered() {
        currentState.HasEnteredCampaign = true;
        SaveState();
    }

    public static void ClaimRewards(Action<object> handler) {
        GetCurrentState().ClaimMaterials();
        if (handler != null) handler.Invoke(null);
    }

    public static void CheatIdleCurrency(long millis) {
        var state = GetCurrentState();
        state.LastClaimTimeStamp -= millis;
        SaveState();
    }

    public static void CheatSummons(int summons) {
        GetCurrentState().CurrentSummons += summons;
        SaveState();
    }

    public static void RequestSummon(int numSummons, Action<List<AccountHero>> handler) {
        AccountState state = GetCurrentState();
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

    public static async Task<MissionReport> AttemptCurrentMissionWithTeam(AccountHero[] selectedTeam) {
        SetLastUsedTeam(selectedTeam);
        var missionInfo = MissionContainer.GetMission(currentState.CurrentChapter, currentState.CurrentMission);
        var enemyTeam = new AccountHero[missionInfo.MissionHeroes.Length];
        for (int x = 0; x < enemyTeam.Length; x++) {
            var accountHero = new AccountHero(missionInfo.MissionHeroes[x]) {
                AwakeningLevel = missionInfo.HeroAwakening,
                CurrentLevel = missionInfo.HeroLevel
            };
            enemyTeam[x] = accountHero;
        }
        var combatReport = await CombatEvaluator.GenerateCombatReport(selectedTeam, enemyTeam);

        // TODO: Remove these temp files when I'm done debugging.
        var fileName = "/CombatReport.txt";
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + fileName, false);
        writer.WriteLine(JsonConvert.SerializeObject(combatReport));
        writer.Close();

        // TODO: Remove these temp files when I'm done debugging.
        var readableReport = combatReport.ToHumanReadableReport();
        fileName = "/ReadableCombatReport.txt";
        writer = new StreamWriter(Application.persistentDataPath + fileName, false);
        foreach (string line in readableReport) {
            writer.WriteLine(line);
        }
        writer.Close();

        if (combatReport.alliesWon) {
            var earnedRewards = AddRewardsFromCurrentMission();
            IncrementCampaignPosition();
            return new MissionReport(combatReport, earnedRewards);
        }

        return new MissionReport(combatReport, null);
    }

    public static void IncrementCampaignPosition() {
        if (currentState.CurrentMission == 10) {
            ClaimRewards(null);
            currentState.CurrentMission = 1;
            currentState.CurrentChapter++;
        } else {
            currentState.CurrentMission++;
        }
        SaveState();
    }

    public static EarnedRewardsContainer AddRewardsFromCurrentMission() {
        var mission = MissionContainer.GetMission(currentState.CurrentChapter, currentState.CurrentMission);
        var rewards = mission.RewardsForMission();
        currentState.CurrentGold += rewards.Gold;
        currentState.CurrentSouls += rewards.Souls;
        currentState.CurrentExperience += rewards.PlayerExperience;
        currentState.CurrentSummons += rewards.Summons;
        if (currentState.CurrentMission == 10) {
            currentState.CurrentSummons += 10;
        }
        currentState.FixLevelsFromExperience();
        var equipmentRewards = new List<AccountEquipment>();
        var allEquipment = (EquipmentType[])Enum.GetValues(typeof(EquipmentType));
        for (int x = 0; x < rewards.NumberEquipment; x++) {
            var randomIndex = CombatMath.RandomInt(0, allEquipment.Length);
            var randomLevel = CombatMath.RandomInt(1, rewards.MaxEquipmentLevel + 1);
            var newEquipment = new AccountEquipment(allEquipment[randomIndex], randomLevel);
            currentState.AccountEquipment.Add(newEquipment);
            equipmentRewards.Add(newEquipment);
        }
        SaveState();
        return new EarnedRewardsContainer(rewards.Summons, rewards.Gold, rewards.Souls, rewards.PlayerExperience, equipmentRewards);
    }

    public static void SetLastUsedTeam(AccountHero[] team) {
        var state = GetCurrentState();
        state.LastTeamSelection = new Guid?[team.Length];
        for (int x = 0; x < team.Length; x++) {
            if (team[x] == null) state.LastTeamSelection[x] = null;
            else state.LastTeamSelection[x] = team[x].HeroGuid;
        }
    }

    public static AccountHero[] GetLastUsedTeam() {
        var state = GetCurrentState();
        var team = new AccountHero[5];
        if (state.LastTeamSelection == null) return team;
        for (int x = 0; x < state.LastTeamSelection.Length; x++) {
            var guid = state.LastTeamSelection[x];
            var matchingHero = state.AccountHeroes.Find((AccountHero hero) => {
                return hero.HeroGuid.Equals(state.LastTeamSelection[x]);
            });
            team[x] = matchingHero;
        }
        return team;
    }

    public static void UnequipHero(AccountHero hero) {
        var equipped = currentState.GetEquipmentForHero(hero);
        foreach (AccountEquipment equipment in equipped) {
            equipment.EquippedHeroGuid = null;
            equipment.EquippedSlot = null;
        }
        SaveState();
    }
}
