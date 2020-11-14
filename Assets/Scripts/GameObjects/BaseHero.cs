using System;
using System.Collections.Generic;

namespace Com.Tempest.Whale.GameObjects {

    public enum HeroEnum {
        // Water
        VAPOR_MAGE = 1, RAIN_MAN = 2, FEESH = 3, MIST_CALLER = 4, ORACLE = 5,
        // Grass
        HAPPY_FLOWER = 6, BUSH_WHACKER = 7, BALL_OF_ROOTS = 8, DRYAD = 9, ANGERY_TREANT = 10,
        // Fire
        CANDLE_MAN = 11, EMBER = 12, TORCH = 13, LAVA_GOLEM = 14, INFERNOMANCER = 15,
        // Ice
        SNOW_MAN = 16, ICE_CUBE = 17, ICICLE_FLINGER = 18, BLIZZARD_WIZZARD = 19, REFLECTOR = 20,
        // Earth
        DUST_ELEMENTAL = 21, BOULDER = 22, PEBBLE_FLINGER = 23, LIVING_WALL = 24, EARTHZERKER = 25,
        // Electric
        STATIC_CLING = 26, BATTERY = 27, SPARK_ELEMENTAL = 28, LIGHTNING_WRAITH = 29, NEUROMANCER = 30
    }

    public class BaseHero {

        public HeroEnum Hero { get; }
        public string HeroName { get; }
        public string HeroIconPath { get; }
        public string AnimatorPath { get; }
        public string HarshPath { get; }

        public RoleEnum Role { get; }
        public FactionEnum Faction { get; }
        public int Rarity { get; }

        public double BaseHealth { get; }
        public double BaseAttack { get; }
        public double BaseMagic { get; }
        public double BaseDefense { get; }
        public double BaseReflection { get; }
        public double BaseSpeed { get; }
        public double BaseCritChance { get; }
        public double BaseDeflectionChance { get; }

        public AttackEnum BasicAttack { get; }
        public AttackEnum SpecialAttack { get; }
        public AbilityEnum PassiveAbility { get; }

        public EquipmentType? PreferredMainHand { get; }
        public EquipmentType? PreferredOffHand { get; }
        public EquipmentType? PreferredTwoHand { get; }
        public EquipmentType PreferredChest { get; }
        public EquipmentType PreferredLegs { get; }
        public EquipmentType PreferredHead { get; }

        public BaseHero(HeroEnum hero, string heroName, string heroIconPath, string animatorPath, string harshPath,
            RoleEnum role, FactionEnum faction, int rarity,
            double baseHealth, double baseAttack, double baseMagic,
            double baseDefense, double baseReflection, double baseSpeed, double baseCritChance, double baseDeflectionChance,
            AttackEnum basicAttack, AttackEnum specialAttack, AbilityEnum passiveAbility,
            EquipmentType? preferredMainHand, EquipmentType? preferredOffHand, EquipmentType? preferredTwoHand,
            EquipmentType preferredChest, EquipmentType preferredLegs, EquipmentType preferredHead) {

            Hero = hero;
            HeroName = heroName;
            HeroIconPath = heroIconPath;
            AnimatorPath = animatorPath;
            HarshPath = harshPath;

            Role = role;
            Faction = faction;
            Rarity = rarity;

            BaseHealth = baseHealth;
            BaseAttack = baseAttack;
            BaseMagic = baseMagic;
            BaseDefense = baseDefense;
            BaseReflection = baseReflection;
            BaseSpeed = baseSpeed;
            BaseCritChance = baseCritChance;
            BaseDeflectionChance = baseDeflectionChance;

            BasicAttack = basicAttack;
            SpecialAttack = specialAttack;
            PassiveAbility = passiveAbility;

            PreferredMainHand = preferredMainHand;
            PreferredOffHand = preferredOffHand;
            PreferredTwoHand = preferredTwoHand;
            PreferredChest = preferredChest;
            PreferredLegs = preferredLegs;
            PreferredHead = preferredHead;
        }

        public static BaseHero GetHero(HeroEnum hero) {
            switch (hero) {
                // Water heroes.
                case HeroEnum.VAPOR_MAGE:
                    return new BaseHero(hero, "Vapor Mage", "Icons/Element02_256_04", "Characters/WaterOverrideController", "Characters/VaporMage/VaporMagePrefab",
                        RoleEnum.DAMAGE, FactionEnum.WATER, 1,
                        70, 60, 85, 0.1, 0.25, 70, 0.2, 0,
                        AttackEnum.CRYSTAL_SMASH, AttackEnum.WATER_SHOT, AbilityEnum.WATER_BODY,
                        EquipmentType.SCEPTER, EquipmentType.TOME, null,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
                case HeroEnum.RAIN_MAN:
                    return new BaseHero(hero, "Rain Man", "Icons/Element02_256_04", "Characters/WaterOverrideController", null,
                        RoleEnum.SUPPORT, FactionEnum.WATER, 2,
                        80, 60, 80, 0.1, 0.25, 75, 0, 0.1,
                        AttackEnum.VAPOR_CLOUD, AttackEnum.DRENCHING_WAVE, AbilityEnum.VAPORIZE,
                        null, null, EquipmentType.STAFF,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
                case HeroEnum.FEESH:
                    return new BaseHero(hero, "Feesh", "Icons/Element02_256_04", "Characters/WaterOverrideController", null,
                        RoleEnum.PROTECTION, FactionEnum.WATER, 3,
                        85, 70, 60, 0.35, 0.2, 50, 0, 0.2,
                        AttackEnum.FISH_SLAP, AttackEnum.ENSCALE_TEAM, AbilityEnum.NONE,
                        EquipmentType.SWORD, EquipmentType.METAL_SHIELD, null,
                        EquipmentType.PLATE_CHEST, EquipmentType.PLATE_PANTS, EquipmentType.PLATE_HELMET);
                case HeroEnum.MIST_CALLER:
                    return new BaseHero(hero, "Mist Caller", "Icons/Element02_256_04", "Characters/WaterOverrideController", null,
                        RoleEnum.DAMAGE, FactionEnum.WATER, 4,
                        55, 55, 90, 0.1, 0.15, 80, 0.15, 0,
                        AttackEnum.VAPOR_CLOUD, AttackEnum.TSUNAMI, AbilityEnum.VAPORIZE,
                        null, null, EquipmentType.STAFF,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
                case HeroEnum.ORACLE:
                    return new BaseHero(hero, "Oracle", "Icons/Element02_256_04", "Characters/Oracle/OracleController", "Characters/Oracle/OraclePrefab", 
                        RoleEnum.SUPPORT, FactionEnum.WATER, 5,
                        80, 55, 95, 0.1, 0.2, 60, 0, 0,
                        AttackEnum.HEALING_MIST, AttackEnum.HEALING_WAVE, AbilityEnum.CLEANSING_RAIN,
                        null, null, EquipmentType.STAFF,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);

                // Grass heroes.
                case HeroEnum.HAPPY_FLOWER:
                    return new BaseHero(hero, "Happy Flower", "Icons/Element02_256_10", "Characters/GrassOverrideController", null,
                        RoleEnum.SUPPORT, FactionEnum.GRASS, 1,
                        80, 70, 80, 0.2, 0.2, 60, 0, 0.1,
                        AttackEnum.PETAL_SLAP, AttackEnum.HEALING_SUN, AbilityEnum.ABSORB_RAIN,
                        EquipmentType.SWORD, EquipmentType.METAL_SHIELD, null,
                        EquipmentType.PLATE_CHEST, EquipmentType.PLATE_PANTS, EquipmentType.PLATE_HELMET);
                case HeroEnum.BUSH_WHACKER:
                    return new BaseHero(hero, "Needler", "Icons/Element02_256_10", "Characters/GrassOverrideController", "Characters/Needler/NeedlerPrefab",
                        RoleEnum.DAMAGE, FactionEnum.GRASS, 2,
                        60, 90, 70, 0.1, 0, 90, 0.3, -0.1,
                        AttackEnum.NEEDLE_STAB, AttackEnum.WEED_WHACKER, AbilityEnum.NONE,
                        EquipmentType.DAGGER, EquipmentType.DAGGER, null,
                        EquipmentType.LEATHER_CHEST, EquipmentType.LEATHER_PANTS, EquipmentType.LEATHER_HAT);
                case HeroEnum.BALL_OF_ROOTS:
                    return new BaseHero(hero, "Ball Of Roots", "Icons/Element02_256_10", "Characters/GrassOverrideController", null,
                        RoleEnum.PROTECTION, FactionEnum.GRASS, 3,
                        80, 70, 60, 0.3, 0.2, 55, 0, 0.25,
                        AttackEnum.PETAL_SLAP, AttackEnum.ENTANGLING_ROOTS, AbilityEnum.BARK_SKIN,
                        EquipmentType.SWORD, EquipmentType.METAL_SHIELD, null,
                        EquipmentType.PLATE_CHEST, EquipmentType.PLATE_PANTS, EquipmentType.PLATE_HELMET);
                case HeroEnum.DRYAD:
                    return new BaseHero(hero, "Dryad", "Icons/Element02_256_10", "Characters/GrassOverrideController", null,
                        RoleEnum.SUPPORT, FactionEnum.GRASS, 4,
                        75, 60, 95, 0.2, 0.2, 70, 0, 0,
                        AttackEnum.MOONLIGHT, AttackEnum.RITUAL_OF_THE_SUN, AbilityEnum.NONE,
                        null, null, EquipmentType.STAFF,
                        EquipmentType.LEATHER_CHEST, EquipmentType.LEATHER_PANTS, EquipmentType.LEATHER_HAT);
                case HeroEnum.ANGERY_TREANT:
                    return new BaseHero(hero, "Angery Treant", "Icons/Element02_256_10", "Characters/GrassOverrideController", null,
                        RoleEnum.PROTECTION, FactionEnum.GRASS, 5,
                        100, 60, 50, 0.3, 0.2, 50, 0, 0.1,
                        AttackEnum.BRANCH_SLAM, AttackEnum.GIFT_OF_THORNS, AbilityEnum.DEEP_ROOTS,
                        EquipmentType.SWORD, EquipmentType.CRYSTAL_SHIELD, null,
                        EquipmentType.PLATE_CHEST, EquipmentType.PLATE_PANTS, EquipmentType.PLATE_HELMET);

                // Fire heroes.
                case HeroEnum.CANDLE_MAN:
                    return new BaseHero(hero, "Candle Man", "Icons/Element02_256_01", "Characters/FireOverrideController", "Characters/Candle/CandlePrefab",
                        RoleEnum.DAMAGE, FactionEnum.FIRE, 1,
                        60, 55, 90, 0.1, 0.2, 85, 0.2, 0,
                        AttackEnum.FIRE_BOLT, AttackEnum.TWIN_FLAME, AbilityEnum.KINDLING,
                        EquipmentType.SCEPTER, EquipmentType.TOME, null,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
                case HeroEnum.EMBER:
                    return new BaseHero(hero, "Ember", "Icons/Element02_256_01", "Characters/FireOverrideController", null,
                        RoleEnum.SUPPORT, FactionEnum.FIRE, 2,
                        75, 60, 80, 0.2, 0.25, 60, 0.1, 0.1,
                        AttackEnum.SCORCH, AttackEnum.TURN_UP_THE_HEAT, AbilityEnum.HOT_BLOODED,
                        null, null, EquipmentType.STAFF,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
                case HeroEnum.TORCH:
                    return new BaseHero(hero, "Torch", "Icons/Element02_256_01", "Characters/FireOverrideController", "Characters/Torch/TorchPrefab",
                        RoleEnum.DAMAGE, FactionEnum.FIRE, 3,
                        65, 55, 95, 0.05, 0.1, 80, 0.3, 0,
                        AttackEnum.FIRE_BOLT, AttackEnum.IMMOLATE, AbilityEnum.KINDLING,
                        EquipmentType.SCEPTER, EquipmentType.TOME, null,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
                case HeroEnum.LAVA_GOLEM:
                    return new BaseHero(hero, "Lava Golem", "Icons/Element02_256_01", "Characters/FireOverrideController", null,
                        RoleEnum.PROTECTION, FactionEnum.FIRE, 4,
                        65, 50, 70, 0.3, 0.25, 50, 0, 0.3,
                        AttackEnum.FIRE_PUNCH, AttackEnum.GIFT_OF_LAVA, AbilityEnum.HOT_BLOODED,
                        EquipmentType.SCEPTER, EquipmentType.METAL_SHIELD, null,
                        EquipmentType.PLATE_CHEST, EquipmentType.PLATE_PANTS, EquipmentType.PLATE_HELMET);
                case HeroEnum.INFERNOMANCER:
                    return new BaseHero(hero, "Infernomancer", "Icons/Element02_256_01", "Characters/FireOverrideController", null,
                        RoleEnum.DAMAGE, FactionEnum.FIRE, 5,
                        55, 50, 100, 0.05, 0.1, 90, 0.1, 0,
                        AttackEnum.SCORCH, AttackEnum.FIRE_STORM, AbilityEnum.FEED_THE_INFERNO,
                        EquipmentType.SCEPTER, EquipmentType.TOME, null,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);

                // Ice heroes.
                case HeroEnum.SNOW_MAN:
                    return new BaseHero(hero, "Snow Man", "Icons/Element02_256_19", "Characters/IceOverrideController", null,
                        RoleEnum.SUPPORT, FactionEnum.ICE, 1,
                        80, 80, 60, 0.2, 0.3, 60, 0, 0.1,
                        AttackEnum.ICE_PUNCH, AttackEnum.CHILLY_WIND, AbilityEnum.COLD_BLOODED,
                        EquipmentType.SWORD, EquipmentType.METAL_SHIELD, null,
                        EquipmentType.PLATE_CHEST, EquipmentType.PLATE_PANTS, EquipmentType.PLATE_HELMET);
                case HeroEnum.ICE_CUBE:
                    return new BaseHero(hero, "Ice Cube", "Icons/Element02_256_19", "Characters/IceOverrideController", null,
                        RoleEnum.PROTECTION, FactionEnum.ICE, 2,
                        70, 80, 60, 0.2, 0.4, 50, 0, 0.3,
                        AttackEnum.ICE_PUNCH, AttackEnum.ENCASE_IN_ICE, AbilityEnum.COLD_BLOODED,
                        EquipmentType.SWORD, EquipmentType.METAL_SHIELD, null,
                        EquipmentType.PLATE_CHEST, EquipmentType.PLATE_PANTS, EquipmentType.PLATE_HELMET);
                case HeroEnum.ICICLE_FLINGER:
                    return new BaseHero(hero, "Icicle Flinger", "Icons/Element02_256_19", "Characters/IceOverrideController", null,
                        RoleEnum.DAMAGE, FactionEnum.ICE, 3,
                        60, 95, 55, 0.1, 0.2, 80, 0.2, 0,
                        AttackEnum.ICICLE_THROW, AttackEnum.FLINGING_SPREE, AbilityEnum.COLD_BLOODED,
                        null, null, EquipmentType.GREAT_AXE,
                        EquipmentType.LEATHER_CHEST, EquipmentType.LEATHER_PANTS, EquipmentType.LEATHER_HAT);
                case HeroEnum.BLIZZARD_WIZZARD:
                    return new BaseHero(hero, "Blizzard Wizard", "Icons/Element02_256_19", "Characters/IceOverrideController", null,
                        RoleEnum.SUPPORT, FactionEnum.ICE, 4,
                        65, 60, 75, 0.05, 0.15, 70, 0.1, 0,
                        AttackEnum.SNOWY_WIND, AttackEnum.BLIZZARD, AbilityEnum.NONE,
                        null, null, EquipmentType.STAFF,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
                case HeroEnum.REFLECTOR:
                    return new BaseHero(hero, "Reflector", "Icons/Element02_256_19", "Characters/IceOverrideController", null,
                        RoleEnum.PROTECTION, FactionEnum.ICE, 5,
                        70, 70, 50, 0.2, 0.5, 50, 0, 0.15,
                        AttackEnum.ICE_PUNCH, AttackEnum.GIFT_OF_ICE, AbilityEnum.MIRROR_ICE,
                        EquipmentType.SWORD, EquipmentType.METAL_SHIELD, null,
                        EquipmentType.CRYSTAL_CHEST, EquipmentType.CRYSTAL_PANTS, EquipmentType.CRYSTAL_HELMET);

                // Earth heroes.
                case HeroEnum.DUST_ELEMENTAL:
                    return new BaseHero(hero, "Dust Elemental", "Icons/Element02_256_22", "Characters/EarthOverrideController", null,
                        RoleEnum.DAMAGE, FactionEnum.EARTH, 1,
                        70, 80, 60, 0.2, 0.1, 80, 0.2, 0,
                        AttackEnum.TWISTER, AttackEnum.DUST_STORM, AbilityEnum.JAGGED_SURFACE,
                        EquipmentType.SWORD, EquipmentType.METAL_SHIELD, null,
                        EquipmentType.PLATE_CHEST, EquipmentType.PLATE_PANTS, EquipmentType.PLATE_HELMET);
                case HeroEnum.BOULDER:
                    return new BaseHero(hero, "Boulder", "Icons/Element02_256_22", "Characters/EarthOverrideController", null,
                        RoleEnum.PROTECTION, FactionEnum.EARTH, 2,
                        80, 80, 50, 0.3, 0.1, 60, 0, 0.3,
                        AttackEnum.ROCK_SLAM, AttackEnum.ENCASE_IN_ROCK, AbilityEnum.JAGGED_SURFACE,
                        EquipmentType.SWORD, EquipmentType.METAL_SHIELD, null,
                        EquipmentType.PLATE_CHEST, EquipmentType.PLATE_PANTS, EquipmentType.PLATE_HELMET);
                case HeroEnum.PEBBLE_FLINGER:
                    return new BaseHero(hero, "Pebble Flinger", "Icons/Element02_256_22", "Characters/EarthOverrideController", null,
                        RoleEnum.DAMAGE, FactionEnum.EARTH, 3,
                        60, 90, 60, 0.2, 0.1, 80, 0.2, 0,
                        AttackEnum.GRAVEL_SHOT, AttackEnum.PEBBLE_SHOWER, AbilityEnum.NONE,
                        null, null, EquipmentType.GREAT_SWORD,
                        EquipmentType.LEATHER_CHEST, EquipmentType.LEATHER_PANTS, EquipmentType.LEATHER_HAT);
                case HeroEnum.LIVING_WALL:
                    return new BaseHero(hero, "Living Wall", "Icons/Element02_256_22", "Characters/EarthOverrideController", null,
                        RoleEnum.PROTECTION, FactionEnum.EARTH, 4,
                        70, 70, 50, 0.4, 0.2, 50, 0, 0.2,
                        AttackEnum.ROCK_SLAM, AttackEnum.GIFT_OF_EARTH, AbilityEnum.JAGGED_SURFACE,
                        EquipmentType.SWORD, EquipmentType.METAL_SHIELD, null,
                        EquipmentType.PLATE_CHEST, EquipmentType.PLATE_PANTS, EquipmentType.PLATE_HELMET);
                case HeroEnum.EARTHZERKER:
                    return new BaseHero(hero, "Earthzerker", "Icons/Element02_256_22", "Characters/EarthOverrideController", null,
                        RoleEnum.DAMAGE, FactionEnum.EARTH, 5,
                        60, 100, 50, 0.1, 0, 80, 0.4, -0.2,
                        AttackEnum.AXE_SLASH, AttackEnum.SPLIT_SKULL, AbilityEnum.MOUNTING_RAGE,
                        null, null, EquipmentType.GREAT_AXE,
                        EquipmentType.PLATE_CHEST, EquipmentType.PLATE_PANTS, EquipmentType.PLATE_HELMET);

                // Electric heroes.
                case HeroEnum.STATIC_CLING:
                    return new BaseHero(hero, "Static Cling", "Icons/Element02_256_16", "Characters/ElectricOverrideController", null,
                        RoleEnum.DAMAGE, FactionEnum.ELECTRIC, 1,
                        70, 60, 80, 0.1, 0.1, 90, 0.2, 0,
                        AttackEnum.SPARK, AttackEnum.FLASH_OF_LIGHT, AbilityEnum.CONDUCTIVITY,
                        EquipmentType.SCEPTER, EquipmentType.TOME, null,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
                case HeroEnum.BATTERY:
                    return new BaseHero(hero, "Battery", "Icons/Element02_256_16", "Characters/ElectricOverrideController", null,
                        RoleEnum.SUPPORT, FactionEnum.ELECTRIC, 2,
                        70, 50, 80, 0.2, 0.3, 90, 0, 0,
                        AttackEnum.ENERGY_DRAIN, AttackEnum.CHARGE_TEAM, AbilityEnum.CONDUCTIVITY,
                        null, null, EquipmentType.STAFF,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
                case HeroEnum.SPARK_ELEMENTAL:
                    return new BaseHero(hero, "Spark Elemental", "Icons/Element02_256_16", "Characters/ElectricOverrideController", null,
                        RoleEnum.DAMAGE, FactionEnum.ELECTRIC, 3,
                        60, 50, 90, 0.1, 0.1, 90, 0.3, 0,
                        AttackEnum.LIGHTNING_BOLT, AttackEnum.OVERCHARGED_BOLT, AbilityEnum.CONDUCTIVITY,
                        EquipmentType.SCEPTER, EquipmentType.TOME, null,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
                case HeroEnum.LIGHTNING_WRAITH:
                    return new BaseHero(hero, "Lightning Wraith", "Icons/Element02_256_16", "Characters/ElectricOverrideController", null,
                        RoleEnum.DAMAGE, FactionEnum.ELECTRIC, 4,
                        60, 50, 80, 0.1, 0.1, 90, 0.2, 0,
                        AttackEnum.LIGHTNING_BOLT, AttackEnum.LIGHTNING_FLASH, AbilityEnum.CONDUCTIVITY,
                        null, null, EquipmentType.STAFF,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
                case HeroEnum.NEUROMANCER:
                    return new BaseHero(hero, "Neuromancer", "Icons/Element02_256_16", "Characters/ElectricOverrideController", null,
                        RoleEnum.SUPPORT, FactionEnum.ELECTRIC, 5,
                        70, 50, 80, 0.05, 0.15, 90, 0.1, 0,
                        AttackEnum.FORKED_LIGHTNING, AttackEnum.BRAIN_STORM, AbilityEnum.MENTAL_GYMNASTICS,
                        null, null, EquipmentType.STAFF,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);

                default:
                    return new BaseHero(hero, "Unknown", "Icons/icon_gem", "Characters/FacelessOverrideController", null,
                        RoleEnum.DAMAGE, FactionEnum.WATER, 1,
                        50, 50, 50,
                        0, 0, 50, 0, 0,
                        AttackEnum.BASIC_PHYSICAL, AttackEnum.SPECIAL_PHYSICAL, AbilityEnum.NONE,
                        null, null, null,
                        EquipmentType.CLOTH_CHEST, EquipmentType.CLOTH_PANTS, EquipmentType.CLOTH_HAT);
            }
        }

        public static double GetBigStatGain(double baseStat) {
            return ((baseStat - 50.0) / 20.0) + 2.0;
        }
    }

    public class BaseHeroContainer {

        private static Dictionary<HeroEnum, BaseHero> allHeroes;
        public static List<HeroEnum> rarityOne;
        public static List<HeroEnum> rarityTwo;
        public static List<HeroEnum> rarityThree;
        public static List<HeroEnum> rarityFour;
        public static List<HeroEnum> rarityFive;

        public static List<HeroEnum> allTanks;
        public static List<HeroEnum> allDamage;
        public static List<HeroEnum> allSupports;

        public static void Initialize() {
            allHeroes = new Dictionary<HeroEnum, BaseHero>();
            rarityOne = new List<HeroEnum>();
            rarityTwo = new List<HeroEnum>();
            rarityThree = new List<HeroEnum>();
            rarityFour = new List<HeroEnum>();
            rarityFive = new List<HeroEnum>();
            allTanks = new List<HeroEnum>();
            allDamage = new List<HeroEnum>();
            allSupports = new List<HeroEnum>();

            foreach (HeroEnum hero in Enum.GetValues(typeof(HeroEnum))) {
                var b = BaseHero.GetHero(hero);
                allHeroes[hero] = b;
                switch (b.Rarity) {
                    case 1:
                        rarityOne.Add(hero);
                        break;
                    case 2:
                        rarityTwo.Add(hero);
                        break;
                    case 3:
                        rarityThree.Add(hero);
                        break;
                    case 4:
                        rarityFour.Add(hero);
                        break;
                    case 5:
                        rarityFive.Add(hero);
                        break;
                }

                switch (b.Role) {
                    case RoleEnum.PROTECTION:
                        allTanks.Add(hero);
                        break;
                    case RoleEnum.DAMAGE:
                        allDamage.Add(hero);
                        break;
                    case RoleEnum.SUPPORT:
                        allSupports.Add(hero);
                        break;
                }
            }
        }

        public static BaseHero GetBaseHero(HeroEnum hero) {
            if (allHeroes == null || allHeroes.Count == 0) {
                Initialize();
            }
            return allHeroes[hero];
        }

        public static HeroEnum ChooseRandomHero(Random rand) {
            if (allHeroes == null || allHeroes.Count == 0) {
                Initialize();
            }
            double roll = rand.NextDouble();
            if (roll <= 0.3) {
                return ChooseHeroFromList(rand, rarityOne);
            } else if (roll <= 0.6) {
                return ChooseHeroFromList(rand, rarityTwo);
            } else if (roll <= 0.8) {
                return ChooseHeroFromList(rand, rarityThree);
            } else if (roll <= 0.95) {
                return ChooseHeroFromList(rand, rarityFour);
            } else {
                return ChooseHeroFromList(rand, rarityFive);
            }
        }

        public static HeroEnum ChooseHeroFromList(Random rand, List<HeroEnum> choices) {
            int choice = rand.Next(choices.Count);
            return choices[choice];
        }

        public static HeroEnum ChooseRandomHero(RoleEnum role, Random rand) {
            if (allHeroes == null || allHeroes.Count == 0) {
                Initialize();
            }
            switch (role) {
                case RoleEnum.PROTECTION:
                    return ChooseHeroFromList(rand, allTanks);
                case RoleEnum.DAMAGE:
                    return ChooseHeroFromList(rand, allDamage);
                case RoleEnum.SUPPORT:
                    return ChooseHeroFromList(rand, allSupports);
                default:
                    return ChooseHeroFromList(rand, allDamage);
            }
        }
    }
}