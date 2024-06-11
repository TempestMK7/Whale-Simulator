using System;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.RequestObjects;
using Com.Tempest.Whale.StateObjects;

public class StateManager {

    private static AccountState currentState;

    #region Internal state handling.

    public static AccountState GetCurrentState() {
        return currentState;
    }

    public static void OverrideState(AccountState newState) {
        if (newState == null) return;
        currentState = newState;
        currentState.RetrieveDataAfterLoad();
        ConsolidateState();
    }

    public static void ConsolidateState() {
        currentState.AccountHeroes.Sort();
        currentState.AccountEquipment.Sort();
    }

    #endregion

    #region Server response handling.

    public static void HandleUpdateTutorialsResponse(UpdateTutorialsResponse response) {
        currentState.HasEnteredHub = response.HasEnteredHub;
        currentState.HasEnteredSanctum = response.HasEnteredSanctum;
        currentState.HasEnteredPortal = response.HasEnteredPortal;
        currentState.HasEnteredCampaign = response.HasEnteredCampaign;
        ConsolidateState();
    }

    public static void HandleClaimResourcesResponse(ClaimResourcesResponse response) {
        currentState.LastClaimTimeStamp = response.LastClaimTimeStamp;
        currentState.CurrentGold = response.CurrentGold;
        currentState.CurrentSouls = response.CurrentSouls;
        currentState.CurrentExperience = response.CurrentExperience;
        currentState.CurrentLevel = response.CurrentLevel;
        ConsolidateState();
    }

    public static void HandleSummonResponse(SummonResponse response) {
        foreach (AccountHero hero in response.SummonedHeroes) {
            hero.LoadBaseHero();
        }
        currentState.CurrentSummons = response.CurrentSummons;
        currentState.AccountHeroes.AddRange(response.SummonedHeroes);
        currentState.RetrieveDataAfterLoad();
        ConsolidateState();
    }

    public static void HandleSummonResponse(FactionSummonResponse response) {
        foreach (AccountHero hero in response.SummonedHeroes) {
            hero.LoadBaseHero();
        }
        currentState.CurrentBronzeSummons = response.CurrentBronzeSummons;
        currentState.CurrentSilverSummons = response.CurrentSilverSummons;
        currentState.CurrentGoldSummons = response.CurrentGoldSummons;
        currentState.AccountHeroes.AddRange(response.SummonedHeroes);
        currentState.RetrieveDataAfterLoad();
        ConsolidateState();
    }

    public static void HandleLevelupResponse(LevelupHeroResponse response, AccountHero leveledHero) {
        if (!response.LevelupSuccessful) return;
        leveledHero.CurrentLevel = response.HeroLevel;
        currentState.CurrentGold = response.CurrentGold;
        currentState.CurrentSouls = response.CurrentSouls;
        ConsolidateState();
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
        ConsolidateState();
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
        ConsolidateState();
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
        ConsolidateState();
    }

    public static void HandleUnequipResponse(AccountHero hero) {
        var equipped = currentState.AccountEquipment.FindAll((AccountEquipment e) => {
            return hero.Id.Equals(e.EquippedHeroId);
        });
        foreach (AccountEquipment e in equipped) {
            e.EquippedHeroId = null;
            e.EquippedSlot = null;
        }
        ConsolidateState();
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
        ConsolidateState();
    }

    public static void HandleCaveEncounterResponse(LootCaveEncounterResponse response) {
        var encounter = response.Encounter;
        currentState.LastCaveEntryDate = encounter.Date;
        currentState.CurrentCaveFloor = encounter.Floor;
        ConsolidateState();
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
        state.LastTeamSelection = new Guid[team.Length];
        for (int x = 0; x < team.Length; x++) {
            if (team[x] == null) state.LastTeamSelection[x] = Guid.Empty;
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
}
