using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.RequestObjects;
using Com.Tempest.Whale.StateObjects;

public class StateManager {

    private static string fileName = Application.persistentDataPath + "/WhaleState.txt";
    private static AccountState currentState;

    #region Internal state handling.

    public static AccountState GetCurrentState() {
        LoadCurrentState();
        return currentState;
    }

    public static bool Initialized() {
        return currentState != null || File.Exists(fileName);
    }

    public static void OverrideState(AccountState newState) {
        currentState = newState;
        currentState.RetrieveDataAfterLoad();
        SaveState(false);
    }

    private static void LoadCurrentState() {
        if (currentState != null) return;

        StreamReader reader = new StreamReader(fileName);
        try {
            currentState = JsonConvert.DeserializeObject<AccountState>(reader.ReadLine());
            currentState.RetrieveDataAfterLoad();
        } catch (Exception e) {
            Debug.LogError(e);
        } finally {
            reader.Close();
        }
    }

    public static async void SaveState(bool uploadToServer = true) {
        currentState.AccountHeroes.Sort();
        currentState.AccountEquipment.Sort();

        StreamWriter writer = new StreamWriter(fileName, false);
        writer.WriteLine(JsonConvert.SerializeObject(currentState));
        writer.Close();

        if (uploadToServer) await UnityEngine.Object.FindObjectOfType<CredentialsManager>().UploadStateToServer();
    }

    #endregion

    #region Server response handling.

    public static void HandleClaimResourcesResponse(ClaimResourcesResponse response) {
        currentState.LastClaimTimeStamp = response.LastClaimTimeStamp;
        currentState.CurrentGold = response.CurrentGold;
        currentState.CurrentSouls = response.CurrentSouls;
        currentState.CurrentExperience = response.CurrentExperience;
        currentState.CurrentLevel = response.CurrentLevel;
        SaveState(false);
    }

    public static void HandleSummonResponse(SummonResponse response) {
        foreach (AccountHero hero in response.SummonedHeroes) {
            hero.LoadBaseHero();
        }
        currentState.CurrentSummons = response.CurrentSummons;
        currentState.AccountHeroes.AddRange(response.SummonedHeroes);
        currentState.RetrieveDataAfterLoad();
        SaveState(false);
    }

    public static void HandleLevelupResponse(LevelupHeroResponse response, AccountHero leveledHero) {
        if (!response.LevelupSuccessful) return;
        leveledHero.CurrentLevel = response.HeroLevel;
        currentState.CurrentGold = response.CurrentGold;
        currentState.CurrentSouls = response.CurrentSouls;
        SaveState(false);
    }

    public static void HandleFuseResponse(FuseHeroResponse response, AccountHero fusedHero, List<AccountHero> destroyedHeroes) {
        var accountHeroes = currentState.AccountHeroes;
        accountHeroes.Remove(fusedHero);
        foreach (AccountHero destroyed in destroyedHeroes) {
            HandleUnequipResponse(destroyed);
            accountHeroes.Remove(destroyed);
        }
        var newFusedHero = response.FusedHero;
        newFusedHero.LoadBaseHero();
        accountHeroes.Add(newFusedHero);
        SaveState(false);
    }

    public static void HandleFuseResponse(FuseEquipmentResponse response, AccountEquipment fusedEquipment, List<AccountEquipment> destroyedEquipment) {
        var accountEquipment = currentState.AccountEquipment;
        accountEquipment.Remove(fusedEquipment);
        foreach (AccountEquipment destroyed in destroyedEquipment) {
            accountEquipment.Remove(destroyed);
        }
        var newFusedEquipment = response.FusedEquipment;
        newFusedEquipment.LoadBaseEquipment();
        accountEquipment.Add(newFusedEquipment);
        SaveState(false);
    }

    public static void HandleEquipResponse(EquipResponse response, AccountEquipment equipment, AccountHero hero, EquipmentSlot? slot) {
        Guid? heroId = null;
        if (hero != null) heroId = hero.Id;
        equipment.EquippedHeroId = heroId;
        equipment.EquippedSlot = slot;

        if (response.UnequippedIds.Count == 0) return;
        var unequippedList = currentState.AccountEquipment.FindAll((AccountEquipment e) => {
            return response.UnequippedIds.Contains(e.Id);
        });
        foreach (AccountEquipment unequipped in unequippedList) {
            unequipped.EquippedHeroId = null;
            unequipped.EquippedSlot = null;
        }
        SaveState(false);
    }

    public static void HandleUnequipResponse(AccountHero hero) {
        var equipped = currentState.AccountEquipment.FindAll((AccountEquipment e) => {
            return hero.Id.Equals(e.EquippedHeroId);
        });
        foreach (AccountEquipment e in equipped) {
            e.EquippedHeroId = null;
            e.EquippedSlot = null;
        }
        SaveState(false);
    }

    public static void HandleCombatResponse(BattleEnum battleType, CombatResponse response) {
        if (!response.Report.alliesWon) return;
        switch (battleType) {
            case BattleEnum.CAMPAIGN:
                if (currentState.CurrentMission == 10) {
                    currentState.CurrentMission = 1;
                    currentState.CurrentChapter++;
                } else {
                    currentState.CurrentMission++;
                }
                break;
            case BattleEnum.LOOT_CAVE:
                currentState.CurrentCaveFloor += 1;
                break;
        }
        currentState.ReceiveRewards(response.Rewards);
        SaveState(false);
    }

    public static void HandleCaveEncounterResponse(LootCaveEncounterResponse response) {
        var encounter = response.Encounter;
        currentState.LastCaveEntryDate = encounter.Date;
        currentState.CurrentCaveFloor = encounter.Floor;
        SaveState(false);
    }

    #endregion

    #region Allowable state altering, mostly for displaying info to user.

    public static int GetLootCavePosition() {
        var currentDate = EpochTime.GetCurrentDate();
        if (!currentDate.Equals(currentState.LastCaveEntryDate)) {
            currentState.LastCaveEntryDate = EpochTime.GetCurrentDate();
            currentState.CurrentCaveFloor = 1;
        }
        return currentState.CurrentCaveFloor;
    }

    public static void SetLastUsedTeam(AccountHero[] team) {
        var state = GetCurrentState();
        state.LastTeamSelection = new Guid?[team.Length];
        for (int x = 0; x < team.Length; x++) {
            if (team[x] == null) state.LastTeamSelection[x] = null;
            else state.LastTeamSelection[x] = team[x].Id;
        }
    }

    public static AccountHero[] GetLastUsedTeam() {
        var state = GetCurrentState();
        var team = new AccountHero[5];
        if (state.LastTeamSelection == null) return team;
        for (int x = 0; x < state.LastTeamSelection.Length; x++) {
            var guid = state.LastTeamSelection[x];
            var matchingHero = state.AccountHeroes.Find((AccountHero hero) => {
                return hero.Id.Equals(state.LastTeamSelection[x]);
            });
            team[x] = matchingHero;
        }
        return team;
    }

    #endregion

    #region Direct state altering, to be removed.

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

    public static MissionReport AttemptCurrentMissionWithTeam(AccountHero[] selectedTeam) {
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
        var combatReport = CombatEvaluator.GenerateCombatReport(selectedTeam, enemyTeam, currentState.AccountEquipment, currentState.AccountEquipment);

        if (combatReport.alliesWon) {
            var earnedRewards = AddRewardsFromCurrentMission();
            IncrementCampaignPosition();
            return new MissionReport(combatReport, earnedRewards);
        }

        return new MissionReport(combatReport, null);
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

    public static void IncrementCampaignPosition() {
        if (currentState.CurrentMission == 10) {
            currentState.CurrentMission = 1;
            currentState.CurrentChapter++;
        } else {
            currentState.CurrentMission++;
        }
        SaveState();
    }

    #endregion
}
