using System;
using System.Collections;
using System.Collections.Generic;
using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.GameObjects {

    public class PotentialRewardsContainer {

        public List<int> DefeatedEnemyLevels { get; set; }
        public int GoldMin { get; set; }
        public int GoldMax { get; set; }
        public int RedCrystalsMin { get; set; }
        public int RedCrystalsMax { get; set; }
        public int BlueCrystalsMin { get; set; }
        public int BlueCrystalsMax { get; set; }
        public int SilverDustMin { get; set; }
        public int SilverDustMax { get; set; }
        public int GoldDustMin { get; set; }
        public int GoldDustMax { get; set; }
        public int OldPagesMin { get; set; }
        public int OldPagesMax { get; set; }
        public int AncientPagesMin { get; set; }
        public int AncientPagesMax { get; set; }
        public int TreatsMin { get; set; }
        public int TreatsMax { get; set; }
        public int TreatsMaxSize { get; set; }
        public int NumberEquipmentMin { get; set; }
        public int NumberEquipmentMax { get; set; }
        public int EquipmentLevelMin { get; set; }
        public int EquipmentLevelMax { get; set; }

        public PotentialRewardsContainer() {
            DefeatedEnemyLevels = new List<int>();
        }

        public EarnedRewardsContainer GenerateRewards() {
            var earnedEquipment = new List<AccountEquipment>();
            var earnedInventory = new List<AccountInventory>();
            var gold = CombatMath.RandomInt(GoldMin, GoldMax + 1);

            var redCrystals = CombatMath.RandomInt(RedCrystalsMin, RedCrystalsMax + 1);
            if (redCrystals > 0) {
                earnedInventory.Add(new AccountInventory() {
                    Id = new Guid(),
                    ItemType = ItemEnum.RED_CRYSTAL,
                    Quantity = redCrystals
                });
            }

            var blueCrystals = CombatMath.RandomInt(BlueCrystalsMin, BlueCrystalsMax + 1);
            if (blueCrystals > 0) {
                earnedInventory.Add(new AccountInventory() {
                    Id = new Guid(),
                    ItemType = ItemEnum.BLUE_CRYSTAL,
                    Quantity = blueCrystals
                });
            }

            var silverDust = CombatMath.RandomInt(SilverDustMin, SilverDustMax + 1);
            if (silverDust > 0) {
                earnedInventory.Add(new AccountInventory() {
                    Id = new Guid(),
                    ItemType = ItemEnum.SILVER_DUST,
                    Quantity = silverDust
                });
            }

            var goldDust = CombatMath.RandomInt(GoldDustMin, GoldDustMax + 1);
            if (goldDust > 0) {
                earnedInventory.Add(new AccountInventory() {
                    Id = new Guid(),
                    ItemType = ItemEnum.GOLD_DUST,
                    Quantity = goldDust
                });
            }

            var oldPages = CombatMath.RandomInt(OldPagesMin, OldPagesMax + 1);
            if (oldPages > 0) {
                earnedInventory.Add(new AccountInventory() {
                    Id = new Guid(),
                    ItemType = ItemEnum.OLD_PAGES,
                    Quantity = oldPages
                });
            }

            var ancientPages = CombatMath.RandomInt(AncientPagesMin, AncientPagesMax + 1);
            if (ancientPages > 0) {
                earnedInventory.Add(new AccountInventory() {
                    Id = new Guid(),
                    ItemType = ItemEnum.ANCIENT_PAGES,
                    Quantity = ancientPages
                });
            }

            if (TreatsMaxSize >= 0) {
                var littleTreats = CombatMath.RandomInt(TreatsMin * (int)Math.Pow(2, TreatsMaxSize), TreatsMax * (int)Math.Pow(2, TreatsMaxSize) + 1);
                if (littleTreats > 0) {
                    earnedInventory.Add(new AccountInventory() {
                        Id = new Guid(),
                        ItemType = ItemEnum.LITTLE_TREAT,
                        Quantity = littleTreats
                    });
                }
            }

            if (TreatsMaxSize >= 1) {
                var yummyTreats = CombatMath.RandomInt(TreatsMin * (int)Math.Pow(2, TreatsMaxSize - 1), TreatsMax * (int)Math.Pow(2, TreatsMaxSize - 1) + 1);
                if (yummyTreats > 0) {
                    earnedInventory.Add(new AccountInventory() {
                        Id = new Guid(),
                        ItemType = ItemEnum.YUMMY_TREAT,
                        Quantity = yummyTreats
                    });
                }
            }

            if (TreatsMaxSize >= 2) {
                var sizableTreats = CombatMath.RandomInt(TreatsMin * (int)Math.Pow(2, TreatsMaxSize - 2), TreatsMax * (int)Math.Pow(2, TreatsMaxSize - 2) + 1);
                if (sizableTreats > 0) {
                    earnedInventory.Add(new AccountInventory() {
                        Id = new Guid(),
                        ItemType = ItemEnum.SIZABLE_TREAT,
                        Quantity = sizableTreats
                    });
                }
            }

            if (TreatsMaxSize >= 3) {
                var fancyTreats = CombatMath.RandomInt(TreatsMin * (int)Math.Pow(2, TreatsMaxSize - 3), TreatsMax * (int)Math.Pow(2, TreatsMaxSize - 3) + 1);
                if (fancyTreats > 0) {
                    earnedInventory.Add(new AccountInventory() {
                        Id = new Guid(),
                        ItemType = ItemEnum.FANCY_TREAT,
                        Quantity = fancyTreats
                    });
                }
            }

            var numEquipment = CombatMath.RandomInt(NumberEquipmentMin, NumberEquipmentMax + 1);
            for (int x = 0; x < numEquipment; x++) {
                int level = CombatMath.RandomInt(EquipmentLevelMin, EquipmentLevelMax + 1);
                earnedEquipment.Add(BaseEquipmentContainer.GenerateRandomEquipment(level));
            }

            return new EarnedRewardsContainer() {
                Gold = gold,
                Gems = 0,
                DefeatedEnemyLevels = DefeatedEnemyLevels,
                EarnedEquipment = earnedEquipment,
                EarnedInventory = earnedInventory
            };
        }
    }

    public class EarnedRewardsContainer {

        public int Gold { get; set; }
        public int Gems { get; set; }

        public List<int> DefeatedEnemyLevels { get; set; }
        public List<AccountEquipment> EarnedEquipment { get; set; }
        public List<AccountInventory> EarnedInventory { get; set; }

        public EarnedRewardsContainer() {
            DefeatedEnemyLevels = new List<int>();
            EarnedEquipment = new List<AccountEquipment>();
            EarnedInventory = new List<AccountInventory>();
        }
    }
}
