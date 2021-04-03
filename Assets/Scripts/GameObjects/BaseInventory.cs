using System;
using System.Collections.Generic;

namespace Com.Tempest.Whale.GameObjects {

    public enum InventoryEnum {
        SINGLE_SUMMON = 1, TEN_SUMMON = 2,
        SINGLE_BRONZE = 11, SINGLE_SILVER = 12, SINGLE_GOLD = 13,
        SMALL_HERO_EXPERIENCE = 21, LARGE_HERO_EXPERIENCE = 22,
        SINGLE_ARENA_TICKET = 31, FIVE_ARENA_TICKET = 32, 
        SMALL_MONSTER_COINS = 41, LARGE_MONSTER_COINS = 42,
        SMALL_GEM_PACKAGE = 51, LARGE_GEM_PACKAGE = 52
    }

    public class BaseInventory {

        public InventoryEnum Inventory { get; }
        public string DisplayName { get; }
        public string IconPath { get; }
        public int QuantityBadge { get; }
        public int? GoldCost { get; }
        public int? GemCost { get; }
        public int Rarity { get; }

        public BaseInventory(InventoryEnum inventory, string displayName, string iconPath, int quantityBadge, int? goldCost, int? gemCost, int rarity) {
            Inventory = inventory;
            DisplayName = displayName;
            IconPath = iconPath;
            QuantityBadge = quantityBadge;
            GoldCost = goldCost;
            GemCost = gemCost;
            Rarity = rarity;
        }

        public static BaseInventory GetBaseInventory(InventoryEnum inventoryEnum) {

            switch (inventoryEnum) {
                case InventoryEnum.SINGLE_SUMMON:
                    return new BaseInventory(InventoryEnum.SINGLE_SUMMON, "Summoning Stone", "Icons/StoneGUI/complete_coin", 1, 50000, 100, 20);
                case InventoryEnum.TEN_SUMMON:
                    return new BaseInventory(InventoryEnum.TEN_SUMMON, "Summoning Stone Package", "Icons/StoneGUI/complete_coin", 1, null, 900, 5);
                case InventoryEnum.SINGLE_BRONZE:
                    return new BaseInventory(InventoryEnum.SINGLE_BRONZE, "Bronze Relic", "Icons/StoneGUI/complete_coin", 1, 40000, 75, 10);
                case InventoryEnum.SINGLE_SILVER:
                    return new BaseInventory(InventoryEnum.SINGLE_SILVER, "Silver Relic", "Icons/StoneGUI/complete_coin", 1, 100000, 200, 5);
                case InventoryEnum.SINGLE_GOLD:
                    return new BaseInventory(InventoryEnum.SINGLE_GOLD, "Gold Relic", "Icons/StoneGUI/complete_coin", 1, null, 1000, 1);
                case InventoryEnum.SMALL_HERO_EXPERIENCE:
                    return new BaseInventory(InventoryEnum.SMALL_HERO_EXPERIENCE, "Hero Experience Package", "Icons/StoneGUI/complete_coin", 50000, 75000, 100, 20);
                case InventoryEnum.LARGE_HERO_EXPERIENCE:
                    return new BaseInventory(InventoryEnum.LARGE_HERO_EXPERIENCE, "Hero Experience Package", "Icons/StoneGUI/complete_coin", 200000, 300000, 350, 10);
                case InventoryEnum.SINGLE_ARENA_TICKET:
                    return new BaseInventory(InventoryEnum.SINGLE_ARENA_TICKET, "Arena Ticket", "Icons/StoneGUI/complete_coin", 1, 10000, 15, 20);
                case InventoryEnum.FIVE_ARENA_TICKET:
                    return new BaseInventory(InventoryEnum.FIVE_ARENA_TICKET, "Arena Ticket Package", "Icons/StoneGUI/complete_coin", 1, 50000, 70, 10);
                case InventoryEnum.SMALL_MONSTER_COINS:
                    return new BaseInventory(InventoryEnum.SMALL_MONSTER_COINS, "Monster Coin Package", "Icons/StoneGUI/complete_coin", 1, 1000, 100, 10);
                case InventoryEnum.LARGE_MONSTER_COINS:
                    return new BaseInventory(InventoryEnum.LARGE_MONSTER_COINS, "Monster Coin Package", "Icons/StoneGUI/complete_coin", 1, 1000, 100, 5);
                case InventoryEnum.SMALL_GEM_PACKAGE:
                    return new BaseInventory(InventoryEnum.SMALL_GEM_PACKAGE, "Gem Package", "Icons/StoneGUI/complete_coin", 100, 60000, null, 5);
                case InventoryEnum.LARGE_GEM_PACKAGE:
                    return new BaseInventory(InventoryEnum.LARGE_GEM_PACKAGE, "Gem Package", "Icons/StoneGUI/complete_coin", 500, 300000, null, 2);

                default:
                    return new BaseInventory(InventoryEnum.SINGLE_SUMMON, "Summoning Stone", "Icons/StoneGUI/complete_coin", 1, 50000, 100, 20);
            }
        }

        public class BaseInventoryContainer {

            private static Dictionary<InventoryEnum, BaseInventory> allInventory;
            private static int rarityTotal;

            public static void Initialize() {
                allInventory = new Dictionary<InventoryEnum, BaseInventory>();
                rarityTotal = 0;
                foreach (InventoryEnum i in Enum.GetValues(typeof(InventoryEnum))) {
                    var row = GetBaseInventory(i);
                    allInventory.Add(i, row);
                    rarityTotal += row.Rarity;
                }
            }

            public static InventoryEnum ChooseRandomInventory(Random rand) {
                if (allInventory == null || rarityTotal == 0) Initialize();
                int roll = rand.Next(rarityTotal);
                foreach (InventoryEnum key in allInventory.Keys) {
                    if (roll < allInventory[key].Rarity) return key;
                    roll -= allInventory[key].Rarity;
                }
                throw new KeyNotFoundException("Something went wrong when trying to choose a random inventory.");
            }
        }
    }
}
