using System;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.RequestObjects;
using Com.Tempest.Whale.StateObjects;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public AccountState CurrentAccountState { get; private set; }
    public UserState CurrentUserState { get; private set; }

    public void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    #region Internal state handling.

    private void ConsolidateState() {
        if (CurrentAccountState.AccountHeroes == null) CurrentAccountState.AccountHeroes = new List<AccountHero>();
        if (CurrentAccountState.AccountEquipment == null) CurrentAccountState.AccountEquipment = new List<AccountEquipment>();
        foreach (AccountHero hero in CurrentAccountState.AccountHeroes) {
            hero.LoadBaseHero();
        }
        CurrentAccountState.AccountHeroes.Sort();
        CurrentAccountState.AccountEquipment.Sort();
    }

    #endregion

    #region Server response handling.

    public void HandleUserInfoResponse(GetUserInfoResponse response) {
        CurrentUserState = new UserState();
        CurrentUserState.AccountCreated = response.AccountCreated;
        CurrentUserState.EmailVerified = response.EmailVerified;
    }

    public void HandleCreateLoginResponse(CreateLoginResponse response) {
        CurrentUserState.AccountCreated = response.Success;
    }

    public void HandleVerifyEmailResponse(VerifyEmailResponse response) {
        CurrentUserState.EmailVerified = response.Success;
    }

    public void HandleAccountStateResponse(AccountState newState) {
        CurrentAccountState = newState;
        ConsolidateState();
    }

    public void HandleUpdateTutorialsResponse(UpdateTutorialsResponse response) {
        CurrentAccountState.HasEnteredHub = response.HasEnteredHub;
        CurrentAccountState.HasEnteredSanctum = response.HasEnteredSanctum;
        CurrentAccountState.HasEnteredPortal = response.HasEnteredPortal;
        CurrentAccountState.HasEnteredCampaign = response.HasEnteredCampaign;
        ConsolidateState();
    }

    public void HandleClaimResourcesResponse(ClaimResourcesResponse response) {
        CurrentAccountState.LastClaimTimeStamp = response.LastClaimTimeStamp;
        CurrentAccountState.CurrentGold = response.CurrentGold;
        CurrentAccountState.CurrentSouls = response.CurrentSouls;
        CurrentAccountState.CurrentExperience = response.CurrentExperience;
        CurrentAccountState.CurrentLevel = response.CurrentLevel;
        ConsolidateState();
    }

    public void HandleSummonResponse(SummonResponse response) {
        foreach (AccountHero hero in response.SummonedHeroes) {
            hero.LoadBaseHero();
        }
        CurrentAccountState.CurrentSummons = response.CurrentSummons;
        CurrentAccountState.AccountHeroes.AddRange(response.SummonedHeroes);
        CurrentAccountState.RetrieveDataAfterLoad();
        ConsolidateState();
    }

    public void HandleSummonResponse(FactionSummonResponse response) {
        foreach (AccountHero hero in response.SummonedHeroes) {
            hero.LoadBaseHero();
        }
        CurrentAccountState.CurrentBronzeSummons = response.CurrentBronzeSummons;
        CurrentAccountState.CurrentSilverSummons = response.CurrentSilverSummons;
        CurrentAccountState.CurrentGoldSummons = response.CurrentGoldSummons;
        CurrentAccountState.AccountHeroes.AddRange(response.SummonedHeroes);
        CurrentAccountState.RetrieveDataAfterLoad();
        ConsolidateState();
    }

    public void HandleLevelupResponse(LevelupHeroResponse response, AccountHero leveledHero) {
        if (!response.LevelupSuccessful) return;
        leveledHero.CurrentLevel = response.HeroLevel;
        CurrentAccountState.CurrentGold = response.CurrentGold;
        CurrentAccountState.CurrentSouls = response.CurrentSouls;
        ConsolidateState();
    }

    public void HandleFuseResponse(FuseHeroResponse response, AccountHero fusedHero, List<AccountHero> destroyedHeroes) {
        var accountHeroes = CurrentAccountState.AccountHeroes;
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

    public void HandleFuseResponse(FuseEquipmentResponse response, AccountEquipment fusedEquipment, List<AccountEquipment> destroyedEquipment) {
        var accountEquipment = CurrentAccountState.AccountEquipment;
        accountEquipment.Remove(fusedEquipment);
        foreach (AccountEquipment destroyed in destroyedEquipment) {
            accountEquipment.Remove(destroyed);
        }
        var newFusedEquipment = response.FusedEquipment;
        accountEquipment.Add(newFusedEquipment);
        ConsolidateState();
    }

    public void HandleEquipResponse(EquipResponse response, AccountEquipment equipment, AccountHero hero) {
        Guid? heroId = null;
        if (hero != null) heroId = hero.Id;
        equipment.EquippedHeroId = heroId;

        if (response.UnequippedIds.Count == 0) return;
        var unequippedList = CurrentAccountState.AccountEquipment.FindAll((AccountEquipment e) => {
            return response.UnequippedIds.Contains(e.Id);
        });
        foreach (AccountEquipment unequipped in unequippedList) {
            unequipped.EquippedHeroId = null;
        }
        ConsolidateState();
    }

    public void HandleUnequipResponse(AccountHero hero) {
        var equipped = CurrentAccountState.AccountEquipment.FindAll((AccountEquipment e) => {
            return hero.Id.Equals(e.EquippedHeroId);
        });
        foreach (AccountEquipment e in equipped) {
            e.EquippedHeroId = null;
        }
        ConsolidateState();
    }

    public void HandleCombatResponse(BattleEnum battleType, CombatResponse response) {
        if (!response.Report.alliesWon) return;
        switch (battleType) {
            case BattleEnum.CAMPAIGN:
                if (CurrentAccountState.CurrentMission == 10) {
                    CurrentAccountState.CurrentMission = 1;
                    CurrentAccountState.CurrentChapter++;
                } else {
                    CurrentAccountState.CurrentMission++;
                }
                break;
            case BattleEnum.LOOT_CAVE:
                CurrentAccountState.CurrentCaveFloor += 1;
                break;
        }
        CurrentAccountState.ReceiveRewards(response.Rewards);
        ConsolidateState();
    }

    public void HandleCaveEncounterResponse(LootCaveEncounterResponse response) {
        var encounter = response.Encounter;
        CurrentAccountState.LastCaveEntryDate = encounter.Date;
        CurrentAccountState.CurrentCaveFloor = encounter.Floor;
        ConsolidateState();
    }

    #endregion

    #region Allowable state altering, mostly for displaying info to user.

    public int GetLootCavePosition() {
        var currentDate = EpochTime.GetCurrentDate();
        if (!currentDate.Equals(CurrentAccountState.LastCaveEntryDate)) {
            CurrentAccountState.LastCaveEntryDate = EpochTime.GetCurrentDate();
            CurrentAccountState.CurrentCaveFloor = 1;
        }
        return CurrentAccountState.CurrentCaveFloor;
    }

    public void SetLastUsedTeam(AccountHero[] team) {
        CurrentAccountState.LastTeamSelection = new Guid[team.Length];
        for (int x = 0; x < team.Length; x++) {
            if (team[x] == null) CurrentAccountState.LastTeamSelection[x] = Guid.Empty;
            else CurrentAccountState.LastTeamSelection[x] = team[x].Id;
        }
    }

    public AccountHero[] GetLastUsedTeam() {
        var team = new AccountHero[5];
        if (CurrentAccountState.LastTeamSelection == null) return team;
        for (int x = 0; x < CurrentAccountState.LastTeamSelection.Length; x++) {
            var guid = CurrentAccountState.LastTeamSelection[x];
            var matchingHero = CurrentAccountState.AccountHeroes.Find((AccountHero hero) => {
                return hero.Id.Equals(CurrentAccountState.LastTeamSelection[x]);
            });
            team[x] = matchingHero;
        }
        return team;
    }

    #endregion
}
