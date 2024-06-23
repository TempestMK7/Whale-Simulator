using System;
using System.Collections.Generic;

namespace Com.Tempest.Whale.GameObjects {

    public enum ItemEnum {
        // Basic currencies
        RED_CRYSTAL = 101,
        BLUE_CRYSTAL = 102,

        SILVER_DUST = 111,
        GOLD_DUST = 112,

        LUMBER = 121,

        STONE = 131,

        EDIBLE_FOOD = 141,
        GOOD_FOOD = 142,
        DELICIOUS_FOOD = 143,
        FANCY_FOOD = 144,

        // Fren modifiers
        LITTLE_TREAT = 201,
        YUMMY_TREAT = 202,
        SIZABLE_TREAT = 203,
        FANCY_TREAT = 204,

        OLD_PAGES = 211,
        OLD_BOOK = 212,
        ANCIENT_PAGES = 213,
        ANCIENT_BOOK = 214,

        // Rare drops
        WEAK_RUNE = 301,
        STRONG_RUNE = 302,
        POWERFUL_RUNE = 303,
        ANCIENT_RUNE = 304,
    }

    public class BaseInventory {

        public ItemEnum itemEnum { get; }
        public string name { get; }
        public string iconName { get; }
        public string description { get; }

        public BaseInventory(ItemEnum itemEnum, string name, string iconName, string description) {
            this.itemEnum = itemEnum;
            this.name = name;
            this.iconName = iconName;
            this.description = description;
        }
    }

    public class BaseInventoryContainer {

        public const string GOLD_ICON_PATH = "Icons/icon_gold_smaller";
        public const string GEM_ICON_PATH = "Icons/icon_gem";

        private static Dictionary<ItemEnum, BaseInventory> inventoryDictionary;

        public static void Initialize() {
            inventoryDictionary ??= new Dictionary<ItemEnum, BaseInventory>() {
                [ItemEnum.RED_CRYSTAL] = new BaseInventory(ItemEnum.RED_CRYSTAL, "Red Crystal", "Icons/Inventory/CrystalRed", "Used to summon new Gachafrens in the portal."),
                [ItemEnum.BLUE_CRYSTAL] = new BaseInventory(ItemEnum.BLUE_CRYSTAL, "Blue Crystal", "Icons/Inventory/CrystalBlue", "Used to summon new Gachafrens of a specific type in the portal."),
                [ItemEnum.SILVER_DUST] = new BaseInventory(ItemEnum.SILVER_DUST, "Silver Dust", "Icons/Inventory/DustSilver", "Used to create new equipment in the Forge."),
                [ItemEnum.GOLD_DUST] = new BaseInventory(ItemEnum.GOLD_DUST, "Gold Dust", "Icons/Inventory/DustGold", "Used to upgrade equipment in the Forge."),
                [ItemEnum.LUMBER] = new BaseInventory(ItemEnum.LUMBER, "Lumber", "Icons/Inventory/Lumber", "Used to upgrade buildings."),
                [ItemEnum.STONE] = new BaseInventory(ItemEnum.STONE, "Stone", "Icons/Inventory/Stone", "Used to upgrade buildings."),
                [ItemEnum.EDIBLE_FOOD] = new BaseInventory(ItemEnum.EDIBLE_FOOD, "Edible Food", "Icons/Inventory/FoodEdible", "This food can definitely be eaten.  It can be given to a building to increase its resource generation by 10% for 4 hours."),
                [ItemEnum.GOOD_FOOD] = new BaseInventory(ItemEnum.GOOD_FOOD, "Good Food", "Icons/Inventory/FoodGood", "Now made with real flavor!  It can be given to a building to increase its resource generation by 25% for 8 hours."),
                [ItemEnum.DELICIOUS_FOOD] = new BaseInventory(ItemEnum.DELICIOUS_FOOD, "Delicious Food", "Icons/Inventory/FoodDelicious", "I bet your Gachafrens will really like this stuff.  It can be given to a building to increase its resource generation by 50% for 12 hours."),
                [ItemEnum.FANCY_FOOD] = new BaseInventory(ItemEnum.FANCY_FOOD, "Fancy Food", "Icons/Inventory/FoodFancy", "Show your Gachafrens that you really care about them.  It can be given to a building to increase its resource generation by 50% for 24 hours."),
                [ItemEnum.LITTLE_TREAT] = new BaseInventory(ItemEnum.LITTLE_TREAT, "Little Treat", "Icons/Inventory/TreatLittle", "Your Gachafrens have earned a little treat.  Awards 100 experience."),
                [ItemEnum.YUMMY_TREAT] = new BaseInventory(ItemEnum.YUMMY_TREAT, "Yummy Treat", "Icons/Inventory/TreatYummy", "Your Gachafrens will definitely appreciate this.  Awards 1000 experience."),
                [ItemEnum.SIZABLE_TREAT] = new BaseInventory(ItemEnum.SIZABLE_TREAT, "Sizable Treat", "Icons/Inventory/TreatSizable", "This is what your Gachafrens deserve after all their hard work.  Awards 10,000 experience."),
                [ItemEnum.FANCY_TREAT] = new BaseInventory(ItemEnum.FANCY_TREAT, "Fancy Treat", "Icons/Inventory/TreatFancy", "After a treat this excellent, your Gachafrens will be able to take on the world.  Awards 100,000 experience."),
                [ItemEnum.OLD_PAGES] = new BaseInventory(ItemEnum.OLD_PAGES, "Old Pages", "Icons/Inventory/PagesOld", "100 of these can be combined into an old book.  If a Gachafren reads that book, they will learn a new intermediate move."),
                [ItemEnum.OLD_BOOK] = new BaseInventory(ItemEnum.OLD_BOOK, "Old Book", "Icons/Inventory/BookOld", "Who knows what secrets it contains?  Give to a Gachafren to make them learn a new intermediate move."),
                [ItemEnum.ANCIENT_PAGES] = new BaseInventory(ItemEnum.ANCIENT_PAGES, "Ancient Pages", "Icons/Inventory/PagesAncient", "100 of these can be combined into an ancient book.  If a Gachafren reads that book, they will learn a new complex move."),
                [ItemEnum.ANCIENT_BOOK] = new BaseInventory(ItemEnum.ANCIENT_BOOK, "Ancient Book", "Icons/Inventory/BookAncient", "Probably full of forbidden knowledge.  Give it to a Gachafren to make them learn a new complex move."),
                [ItemEnum.WEAK_RUNE] = new BaseInventory(ItemEnum.WEAK_RUNE, "Weak Rune", "Icons/Inventory/RuneWeak", "It's just not very strong.  Using this rune when making a new piece of equipment will allow you to select which primary stat appears on the equipment."),
                [ItemEnum.STRONG_RUNE] = new BaseInventory(ItemEnum.STRONG_RUNE, "Strong Rune", "Icons/Inventory/RuneStrong", "This one is actually pretty strong.  Using this rune when making a new piece of equipment will allow you to select which primary and secondary stats appear on the equipment."),
                [ItemEnum.POWERFUL_RUNE] = new BaseInventory(ItemEnum.POWERFUL_RUNE, "Powerful Rune", "Icons/Inventory/RunePowerful", "This rune is so powerful that it can defy the gods of RNG almost completely.  Using this rune when making a new piece of equipment will allow you to select all three stats that appear on the equipment."),
                [ItemEnum.ANCIENT_RUNE] = new BaseInventory(ItemEnum.ANCIENT_RUNE, "Ancient Rune", "Icons/Inventory/RuneAncient", "The gods of RNG really won't like this.  Using this rune when making a new piece of equipment will allow you to select all three stats that appear on the equipment.  It will also guarantee a very high level of quality for all three of those stats.")
            };
        }

        public static BaseInventory GetBaseInventory(ItemEnum itemEnum) {
            if (inventoryDictionary == null) {
                Initialize();
            }
            return inventoryDictionary[itemEnum];
        }
    }
}
