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
        public double CurrentGems { get; set; }
        public double CurrentSummons { get; set; }

        public int CurrentChapter { get; set; }
        public int CurrentMission { get; set; }

        public bool HasEnteredHub { get; set; }
        public bool HasEnteredSanctum { get; set; }
        public bool HasEnteredPortal { get; set; }
        public bool HasEnteredCampaign { get; set; }

        public List<AccountHero> AccountHeroes { get; set; }
        public List<AccountEquipment> AccountEquipment { get; set; }

        public Guid?[] LastTeamSelection { get; set; }
        public Guid?[] LastRaidSelection { get; set; }

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

            CurrentChapter = 1;
            CurrentMission = 1;

            AccountHeroes = new List<AccountHero>();
            AccountEquipment = new List<AccountEquipment>();
        }

        public void FixLevelsFromExperience() {
            while (CurrentExperience > LevelContainer.ExperienceRequirement(CurrentLevel)) {
                CurrentExperience -= LevelContainer.ExperienceRequirement(CurrentLevel);
                CurrentLevel++;
            }
        }

        public void RetrieveDataAfterLoad() {
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
    }
}
