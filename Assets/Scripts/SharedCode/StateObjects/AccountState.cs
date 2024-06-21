using System;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.StateObjects {

    [Serializable]
    public class AccountState {
        
        public Guid Id { get; set; }
        public string PlayerName { get; set; }

        public double CurrentGold { get; set; }
        public long CurrentGems { get; set; }

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
        public List<AccountInventory> Inventory { get; set; }

        public Guid[] LastTeamSelection { get; set; }
        public Guid[] LastRaidSelection { get; set; }

        public void InitializeAccount() {
            Id = Guid.NewGuid();
            PlayerName = "Unregistered Account";

            CurrentGold = 0;
            CurrentGems = 0;

            CurrentChapter = 1;
            CurrentMission = 1;

            LastCaveEntryDate = null;
            CurrentCaveFloor = 1;

            AccountHeroes = new List<AccountHero>();
            AccountEquipment = new List<AccountEquipment>();
            Inventory = new List<AccountInventory>() {
                new AccountInventory() {
                    Id = new Guid(),
                    ItemType = ItemEnum.RED_CRYSTAL,
                    Quantity = 10
                }
            };

            LastTeamSelection = new Guid[5];
            for (int x = 0; x < LastTeamSelection.Length; x++) {
                LastTeamSelection[x] = Guid.Empty;
            }
            LastRaidSelection = new Guid[5];
            for (int x = 0; x < LastRaidSelection.Length; x++) {
                LastRaidSelection[x] = Guid.Empty;
            }
        }

        public void RetrieveDataAfterLoad() {
            AccountHeroes ??= new List<AccountHero>();
            AccountEquipment ??= new List<AccountEquipment>();
            Inventory ??= new List<AccountInventory>();

            foreach (AccountHero hero in AccountHeroes) {
                hero.LoadBaseHero();
            }
        }

        public List<AccountEquipment> GetEquipmentForHero(AccountHero hero) {
            return AccountEquipment.FindAll((AccountEquipment matchable) => {
                return hero.Id.Equals(matchable.EquippedHeroId);
            });
        }

        public void ReceiveRewards(EarnedRewardsContainer rewards, Guid[] selectedHeroes) {
            CurrentGold += rewards.Gold;
            CurrentGems += rewards.Gems;

            AccountEquipment.AddRange(rewards.EarnedEquipment);
            RetrieveDataAfterLoad();
            AccountEquipment.Sort();

            foreach (AccountInventory inventory in rewards.EarnedInventory) {
                var existingInventory = Inventory.Find(matchable => matchable.ItemType == inventory.ItemType);
                if (existingInventory != null) {
                    existingInventory.Quantity += inventory.Quantity;
                } else {
                    Inventory.Add(inventory);
                }
            }

            foreach (int defeatedEnemyLevel in rewards.DefeatedEnemyLevels) {
                foreach (Guid selectedHeroId in selectedHeroes) {
                    var selectedHero = AccountHeroes.Find(matchable => matchable.Id.Equals(selectedHeroId));
                    if (selectedHero != null) {
                        selectedHero.AwardExperience(selectedHero.ExperienceReward(defeatedEnemyLevel));
                    }
                }
            }
        }
    }
}
