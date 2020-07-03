using System.Collections;
using System.Collections.Generic;

namespace Com.Tempest.Whale.GameObjects {

    public enum EquipmentSlot {
        NONE = 0,
        CHEST = 1, LEGS = 2, HEAD = 3,
        MAIN_HAND = 11, ONE_HAND = 12, OFF_HAND = 13, TWO_HAND = 14
    }

    public enum EquipmentType {
        // One Handed
        DAGGER = 11, SWORD = 12, AXE = 13,
        // Main Hand Only
        SCEPTER = 21,
        // Off Hand Only
        METAL_SHIELD = 31, CRYSTAL_SHIELD = 32, TOME = 33,
        // Two Handed
        GREAT_SWORD = 41, GREAT_AXE = 42, STAFF = 43,

        // Armor
        CLOTH_CHEST = 51, CLOTH_PANTS = 52, CLOTH_HAT = 53,
        LEATHER_CHEST = 61, LEATHER_PANTS = 62, LEATHER_HAT = 63,
        PLATE_CHEST = 71, PLATE_PANTS = 72, PLATE_HELMET = 73,
        CRYSTAL_CHEST = 81, CRYSTAL_PANTS = 82, CRYSTAL_HELMET = 83,
    }

    public class BaseEquipment {

        public EquipmentType Type { get; }
        public EquipmentSlot Slot { get; }
        public string Name { get; }
        public string IconPath { get; }
        public int BaseAttack { get; }
        public int BaseMagic { get; }
        public double BaseDefense { get; }
        public double DefensePerLevel { get; }
        public double BaseReflection { get; }
        public double ReflectionPerLevel { get; }
        public double BaseCrit { get; }
        public double BaseDeflect { get; }

        public BaseEquipment(EquipmentType type, EquipmentSlot slot, string name, string iconPath,
            int baseAttack, int baseMagic, double baseDefense, double defensePerLevel,
            double baseReflection, double reflectionPerLevel, double baseCrit, double baseDeflect) {

            Type = type;
            Slot = slot;
            Name = name;
            IconPath = iconPath;
            BaseAttack = baseAttack;
            BaseMagic = baseMagic;
            BaseDefense = baseDefense;
            DefensePerLevel = defensePerLevel;
            BaseReflection = baseReflection;
            ReflectionPerLevel = reflectionPerLevel;
            BaseCrit = baseCrit;
            BaseDeflect = baseDeflect;
        }
    }

    public class BaseEquipmentContainer {

        private static Dictionary<EquipmentType, BaseEquipment> equipDict;

        public static void Initialize() {
            if (equipDict != null) return;
            equipDict = new Dictionary<EquipmentType, BaseEquipment>();

            // Cloth Armor
            equipDict[EquipmentType.CLOTH_CHEST] = new BaseEquipment(
                EquipmentType.CLOTH_CHEST, EquipmentSlot.CHEST, "Cloth Chest", "Icons/Equipment/ClothChest",
                0, 30, 0, 0, 0, 0, 0, 0);
            equipDict[EquipmentType.CLOTH_PANTS] = new BaseEquipment(
                EquipmentType.CLOTH_PANTS, EquipmentSlot.LEGS, "Cloth Pants", "Icons/Equipment/ClothPants",
                0, 30, 0, 0, 0, 0, 0, 0);
            equipDict[EquipmentType.CLOTH_HAT] = new BaseEquipment(
                EquipmentType.CLOTH_HAT, EquipmentSlot.HEAD, "Cloth Hat", "Icons/Equipment/ClothHelm",
                0, 30, 0, 0, 0, 0, 0, 0);

            // Leather Armor
            equipDict[EquipmentType.LEATHER_CHEST] = new BaseEquipment(
                EquipmentType.LEATHER_CHEST, EquipmentSlot.CHEST, "Leather Chest", "Icons/Equipment/LeatherChest",
                30, 0, 0, 0, 0, 0, 0, 0);
            equipDict[EquipmentType.LEATHER_PANTS] = new BaseEquipment(
                EquipmentType.LEATHER_PANTS, EquipmentSlot.LEGS, "Leather Pants", "Icons/Equipment/LeatherPants",
                30, 0, 0, 0, 0, 0, 0, 0);
            equipDict[EquipmentType.LEATHER_HAT] = new BaseEquipment(
                EquipmentType.LEATHER_HAT, EquipmentSlot.HEAD, "Leather Hat", "Icons/Equipment/LeatherHelm",
                30, 0, 0, 0, 0, 0, 0, 0);

            // Plate Armor
            equipDict[EquipmentType.PLATE_CHEST] = new BaseEquipment(
                EquipmentType.PLATE_CHEST, EquipmentSlot.CHEST, "Plate Chest", "Icons/Equipment/PlateChest",
                0, 0, 0.05, 0.01, 0, 0, 0, 0);
            equipDict[EquipmentType.PLATE_PANTS] = new BaseEquipment(
                EquipmentType.PLATE_PANTS, EquipmentSlot.LEGS, "Plate Pants", "Icons/Equipment/PlatePants",
                0, 0, 0.05, 0.01, 0, 0, 0, 0);
            equipDict[EquipmentType.PLATE_HELMET] = new BaseEquipment(
                EquipmentType.PLATE_HELMET, EquipmentSlot.HEAD, "Plate Hat", "Icons/Equipment/PlateHelm",
                0, 0, 0.05, 0.01, 0, 0, 0, 0);

            // Crystal Armor
            equipDict[EquipmentType.CRYSTAL_CHEST] = new BaseEquipment(
                EquipmentType.CRYSTAL_CHEST, EquipmentSlot.CHEST, "Crystal Chest", "Icons/Equipment/CrystalChest",
                0, 0, 0, 0, 0.05, 0.01, 0, 0);
            equipDict[EquipmentType.CRYSTAL_PANTS] = new BaseEquipment(
                EquipmentType.CRYSTAL_PANTS, EquipmentSlot.LEGS, "Crystal Pants", "Icons/Equipment/CrystalPants",
                0, 0, 0, 0, 0.05, 0.01, 0, 0);
            equipDict[EquipmentType.CRYSTAL_HELMET] = new BaseEquipment(
                EquipmentType.CRYSTAL_HELMET, EquipmentSlot.HEAD, "Crystal Hat", "Icons/Equipment/CrystalHelm",
                0, 0, 0, 0, 0.05, 0.01, 0, 0);

            // One Handed
            equipDict[EquipmentType.DAGGER] = new BaseEquipment(
                EquipmentType.DAGGER, EquipmentSlot.ONE_HAND, "Dagger", "Icons/Equipment/Dagger",
                20, 0, 0, 0, 0, 0, 0.2, 0);
            equipDict[EquipmentType.SWORD] = new BaseEquipment(
                EquipmentType.SWORD, EquipmentSlot.ONE_HAND, "Sword", "Icons/Equipment/OneHandedSword",
                40, 0, 0, 0, 0, 0, 0, 0);
            equipDict[EquipmentType.AXE] = new BaseEquipment(
                EquipmentType.AXE, EquipmentSlot.ONE_HAND, "Axe", "Icons/Equipment/OneHandedAxe",
                30, 0, 0, 0, 0, 0, 0.1, 0);

            // Main Hand
            equipDict[EquipmentType.SCEPTER] = new BaseEquipment(
                EquipmentType.SCEPTER, EquipmentSlot.MAIN_HAND, "Scepter", "Icons/Equipment/Scepter",
                0, 40, 0, 0, 0, 0, 0, 0);

            // Off Hand
            equipDict[EquipmentType.METAL_SHIELD] = new BaseEquipment(
                EquipmentType.METAL_SHIELD, EquipmentSlot.OFF_HAND, "Metal Shield", "Icons/Equipment/MetalShield",
                0, 0, 0.05, 0.01, 0, 0, 0, 0.1);
            equipDict[EquipmentType.CRYSTAL_SHIELD] = new BaseEquipment(
                EquipmentType.CRYSTAL_SHIELD, EquipmentSlot.OFF_HAND, "Crystal Shield", "Icons/Equipment/CrystalShield",
                0, 0, 0, 0, 0.05, 0.01, 0, 0.1);
            equipDict[EquipmentType.TOME] = new BaseEquipment(
                EquipmentType.TOME, EquipmentSlot.OFF_HAND, "Tome", "Icons/Equipment/Tome",
                0, 20, 0, 0, 0, 0, 0.2, 0);

            // Two Handed
            equipDict[EquipmentType.GREAT_SWORD] = new BaseEquipment(
                EquipmentType.GREAT_SWORD, EquipmentSlot.TWO_HAND, "Greatsword", "Icons/Equipment/TwoHandedSword",
                80, 0, 0, 0, 0, 0, 0, 0);
            equipDict[EquipmentType.GREAT_AXE] = new BaseEquipment(
                EquipmentType.GREAT_AXE, EquipmentSlot.TWO_HAND, "Greataxe", "Icons/Equipment/TwoHandedAxe",
                60, 0, 0, 0, 0, 0, 0.2, 0);
            equipDict[EquipmentType.STAFF] = new BaseEquipment(
                EquipmentType.STAFF, EquipmentSlot.TWO_HAND, "Staff", "Icons/Equipment/Staff",
                0, 80, 0, 0, 0, 0, 0, 0);
        }

        public static BaseEquipment GetBaseEquipment(EquipmentType type) {
            if (equipDict == null) Initialize();
            return equipDict[type];
        }
    }
}
