using System;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.StateObjects {

    [Serializable]
    public class AccountState {
        
        public Guid Id { get; set; }
        public string PlayerName { get; set; }

        public long LastClaimTimeStamp { get; set; }

        public double CurrentGold { get; set; }
        public double CurrentSouls { get; set; }
        public int CurrentLevel { get; set; }
        public double CurrentExperience { get; set; }
        public long CurrentGems { get; set; }
        public long CurrentSummons { get; set; }

        public long CurrentBronzeSummons { get; set; }
        public long CurrentSilverSummons { get; set; }
        public long CurrentGoldSummons { get; set; }

        public int CurrentChapter { get; set; }
        public int CurrentMission { get; set; }

        public string LastCaveEntryDate { get; set; }
        public int CurrentCaveFloor { get; set; }

        public bool HasEnteredHub { get; set; }
        public bool HasEnteredSanctum { get; set; }
        public bool HasEnteredPortal { get; set; }
        public bool HasEnteredCampaign { get; set; }

        public List<AccountHero> AccountHeroes { get; set; }
        public List<AccountEquipment> AccountEquipment { get; set; }

        public Guid[] LastTeamSelection { get; set; }
        public Guid[] LastRaidSelection { get; set; }

        public void InitializeAccount() {
            Id = Guid.NewGuid();
            PlayerName = "Unregistered Account";

            LastClaimTimeStamp = EpochTime.CurrentTimeMillis();

            CurrentGold = 0;
            CurrentSouls = 0;
            CurrentLevel = 1;
            CurrentExperience = 0;
            CurrentGems = 0;
            CurrentSummons = 10;

            CurrentBronzeSummons = 0;
            CurrentSilverSummons = 0;
            CurrentGoldSummons = 0;

            CurrentChapter = 1;
            CurrentMission = 1;

            LastCaveEntryDate = null;
            CurrentCaveFloor = 1;

            AccountHeroes = new List<AccountHero>();
            AccountEquipment = new List<AccountEquipment>();

            LastTeamSelection = new Guid[5];
            for (int x = 0; x < LastTeamSelection.Length; x++) {
                LastTeamSelection[x] = Guid.Empty;
            }
            LastRaidSelection = new Guid[5];
            for (int x = 0; x < LastRaidSelection.Length; x++) {
                LastRaidSelection[x] = Guid.Empty;
            }
        }

        public void FixLevelsFromExperience() {
            while (CurrentExperience > LevelContainer.ExperienceRequirement(CurrentLevel)) {
                CurrentExperience -= LevelContainer.ExperienceRequirement(CurrentLevel);
                CurrentLevel++;
            }
        }

        public void RetrieveDataAfterLoad() {
            if (AccountHeroes == null) AccountHeroes = new List<AccountHero>();
            if (AccountEquipment == null) AccountEquipment = new List<AccountEquipment>();
            foreach (AccountHero hero in AccountHeroes) {
                hero.LoadBaseHero();
            }
            foreach (AccountEquipment equipment in AccountEquipment) {
                equipment.LoadBaseEquipment();
            }
        }

        public List<AccountEquipment> GetEquipmentForHero(AccountHero hero) {
            return AccountEquipment.FindAll((AccountEquipment matchable) => {
                return hero.Id.Equals(matchable.EquippedHeroId);
            });
        }

        public void ReceiveRewards(EarnedRewardsContainer rewards) {
            CurrentGold += rewards.Gold;
            CurrentSouls += rewards.Souls;
            CurrentExperience += rewards.PlayerExperience;
            CurrentGems += rewards.Gems;
            CurrentSummons += rewards.Summons;
            CurrentBronzeSummons += rewards.BronzeSummons;
            CurrentSilverSummons += rewards.SilverSummons;
            CurrentGoldSummons += rewards.GoldSummons;
            AccountEquipment.AddRange(rewards.EarnedEquipment);

            RetrieveDataAfterLoad();
            FixLevelsFromExperience();
            AccountEquipment.Sort();
        }
    }
}
