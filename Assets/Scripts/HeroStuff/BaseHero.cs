using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero {

    public HeroEnum Hero { get; }
    public string HeroName { get; }
    public Sprite HeroIcon { get; }
    public AnimatorOverrideController HeroAnimator { get; }

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

    public BaseHero(HeroEnum hero, string heroName, string heroIcon, string animator, RoleEnum role, FactionEnum faction, int rarity,
        double baseHealth, double baseAttack, double baseMagic,
        double baseDefense, double baseReflection, double baseSpeed, double baseCritChance, double baseDeflectionChance,
        AttackEnum basicAttack, AttackEnum specialAttack, AbilityEnum passiveAbility) {

        Hero = hero;
        HeroName = heroName;
        HeroIcon = Resources.Load<Sprite>(heroIcon);
        HeroAnimator = Resources.Load<AnimatorOverrideController>(animator);
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
    }

    public static BaseHero GetHero(HeroEnum hero) {
        switch (hero) {
            // Water heroes.
            case HeroEnum.VAPOR_CLOUD:
                return new BaseHero(hero, "Vapor Cloud", "Icons/Element02_256_04", "Characters/WaterOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.WATER, 1,
                    60, 60, 80, 32, 40, 65, 0.1, 0,
                    AttackEnum.VAPOR_CLOUD, AttackEnum.WATER_SHOT, AbilityEnum.NONE);
            case HeroEnum.RAIN_MAN:
                return new BaseHero(hero, "Rain Man", "Icons/Element02_256_04", "Characters/WaterOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.WATER, 2,
                    75, 60, 70, 38, 42, 60, 0.1, 0,
                    AttackEnum.VAPOR_CLOUD, AttackEnum.DRENCHING_WAVE, AbilityEnum.NONE);
            case HeroEnum.FEESH:
                return new BaseHero(hero, "Feesh", "Icons/Element02_256_04", "Characters/WaterOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.WATER, 3,
                    85, 70, 50, 48, 42, 50, 0.1, 0,
                    AttackEnum.FISH_SLAP, AttackEnum.ENSCALE_TEAM, AbilityEnum.NONE);
            case HeroEnum.MIST_CALLER:
                return new BaseHero(hero, "Mist Caller", "Icons/Element02_256_04", "Characters/WaterOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.WATER, 4,
                    60, 55, 95, 35, 40, 75, 0.15, 0,
                    AttackEnum.VAPOR_CLOUD, AttackEnum.TSUNAMI, AbilityEnum.NONE);
            case HeroEnum.ORACLE:
                return new BaseHero(hero, "Oracle", "Icons/Element02_256_04", "Characters/WaterOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.WATER, 5,
                    75, 60, 90, 38, 42, 75, 0.15, 0,
                    AttackEnum.WATER_RENEW, AttackEnum.HEALING_WAVE, AbilityEnum.NONE);

            // Grass heroes.
            case HeroEnum.HAPPY_FLOWER:
                return new BaseHero(hero, "Happy Flower", "Icons/Element02_256_10", "Characters/GrassOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.GRASS, 1,
                    70, 70, 55, 38, 36, 55, 0.1, 0,
                    AttackEnum.PETAL_SLAP, AttackEnum.HEALING_SUN, AbilityEnum.NONE);
            case HeroEnum.BUSH_WHACKER:
                return new BaseHero(hero, "Bush Whacker", "Icons/Element02_256_10", "Characters/GrassOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.GRASS, 2,
                    75, 80, 60, 36, 34, 80, 0.2, 0,
                    AttackEnum.NEEDLE_STAB, AttackEnum.WEED_WHACKER, AbilityEnum.NONE);
            case HeroEnum.BALL_OF_ROOTS:
                return new BaseHero(hero, "Ball Of Roots", "Icons/Element02_256_10", "Characters/GrassOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.GRASS, 3,
                    90, 65, 60, 36, 34, 80, 0.15, 0,
                    AttackEnum.PETAL_SLAP, AttackEnum.ENTANGLING_ROOTS, AbilityEnum.NONE);
            case HeroEnum.DRYAD:
                return new BaseHero(hero, "Dryad", "Icons/Element02_256_10", "Characters/GrassOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.GRASS, 4,
                    80, 85, 85, 38, 38, 80, 0.15, 0,
                    AttackEnum.SPEAR_THROW, AttackEnum.RITUAL_OF_VENOM, AbilityEnum.NONE);
            case HeroEnum.ANGERY_TREANT:
                return new BaseHero(hero, "Angery Treant", "Icons/Element02_256_10", "Characters/GrassOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.GRASS, 5,
                    100, 75, 60, 46, 42, 50, 0.1, 0,
                    AttackEnum.BRANCH_SLAM, AttackEnum.GIFT_OF_THORNS, AbilityEnum.NONE);

            // Fire heroes.
            case HeroEnum.CANDLE_MAN:
                return new BaseHero(hero, "Candle Man", "Icons/Element02_256_01", "Characters/FireOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.FIRE, 1,
                    55, 60, 85, 31, 40, 85, 0.15, 0,
                    AttackEnum.FIRE_BOLT, AttackEnum.TWIN_FLAME, AbilityEnum.NONE);
            case HeroEnum.EMBER:
                return new BaseHero(hero, "Ember", "Icons/Element02_256_01", "Characters/FireOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.FIRE, 2,
                    70, 55, 90, 34, 38, 80, 0.15, 0,
                    AttackEnum.SCORCH, AttackEnum.TURN_UP_THE_HEAT, AbilityEnum.NONE);
            case HeroEnum.TORCH:
                return new BaseHero(hero, "Torch", "Icons/Element02_256_01", "Characters/FireOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.FIRE, 3,
                    65, 65, 95, 34, 40, 70, 0.2, 0,
                    AttackEnum.FIRE_BOLT, AttackEnum.IMMOLATE, AbilityEnum.NONE);
            case HeroEnum.LAVA_GOLEM:
                return new BaseHero(hero, "Lava Golem", "Icons/Element02_256_01", "Characters/FireOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.FIRE, 4,
                    85, 80, 60, 48, 40, 60, 0.1, 0,
                    AttackEnum.FIRE_PUNCH, AttackEnum.GIFT_OF_LAVA, AbilityEnum.NONE);
            case HeroEnum.INFERNOMANCER:
                return new BaseHero(hero, "Infernomancer", "Icons/Element02_256_01", "Characters/FireOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.FIRE, 5,
                    70, 60, 100, 32, 35, 95, 0.25, 0,
                    AttackEnum.SCORCH, AttackEnum.FIRE_STORM, AbilityEnum.NONE);

            // Ice heroes.
            case HeroEnum.SNOW_MAN:
                return new BaseHero(hero, "Snow Man", "Icons/Element02_256_19", "Characters/IceOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.ICE, 1,
                    70, 70, 65, 36, 40, 60, 0.1, 0,
                    AttackEnum.ICE_PUNCH, AttackEnum.CHILLY_WIND, AbilityEnum.NONE);
            case HeroEnum.ICE_CUBE:
                return new BaseHero(hero, "Ice Cube", "Icons/Element02_256_19", "Characters/IceOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.ICE, 2,
                    85, 75, 55, 40, 45, 55, 0.1, 0,
                    AttackEnum.ICE_PUNCH, AttackEnum.ENCASE_IN_ICE, AbilityEnum.NONE);
            case HeroEnum.ICICLE_FLINGER:
                return new BaseHero(hero, "Icicle Flinger", "Icons/Element02_256_19", "Characters/IceOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.ICE, 3,
                    65, 90, 60, 35, 38, 85, 0.2, 0,
                    AttackEnum.ICICLE_THROW, AttackEnum.FLINGING_SPREE, AbilityEnum.NONE);
            case HeroEnum.BLIZZARD_WIZZARD:
                return new BaseHero(hero, "Blizzard Wizard", "Icons/Element02_256_19", "Characters/IceOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.ICE, 4,
                    75, 60, 95, 32, 38, 90, 0.25, 0,
                    AttackEnum.SNOWY_WIND, AttackEnum.BLIZZARD, AbilityEnum.NONE);
            case HeroEnum.REFLECTOR:
                return new BaseHero(hero, "Reflector", "Icons/Element02_256_19", "Characters/IceOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.ICE, 5,
                    95, 75, 80, 42, 50, 55, 0.15, 0,
                    AttackEnum.ICE_PUNCH, AttackEnum.GIFT_OF_ICE, AbilityEnum.NONE);

            // Earth heroes.
            case HeroEnum.PEBBLE_ELEMENTAL:
                return new BaseHero(hero, "Pebble Elemental", "Icons/Element02_256_22", "Characters/EarthOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.EARTH, 1,
                    80, 75, 50, 45, 35, 55, 0.1, 0,
                    AttackEnum.ROCK_SLAM, AttackEnum.HEAD_CRACK, AbilityEnum.NONE);
            case HeroEnum.BOULDER:
                return new BaseHero(hero, "Boulder", "Icons/Element02_256_22", "Characters/EarthOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.EARTH, 2,
                    90, 70, 66, 46, 38, 60, 0.1, 0,
                    AttackEnum.ROCK_SLAM, AttackEnum.ENCASE_IN_ROCK, AbilityEnum.NONE);
            case HeroEnum.PEBBLE_FLINGER:
                return new BaseHero(hero, "Pebble Flinger", "Icons/Element02_256_22", "Characters/EarthOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.EARTH, 3,
                    70, 85, 60, 38, 32, 75, 0.1, 0,
                    AttackEnum.PEBBLE_TOSS, AttackEnum.PEBBLE_SHOWER, AbilityEnum.NONE);
            case HeroEnum.LIVING_WALL:
                return new BaseHero(hero, "Living Wall", "Icons/Element02_256_22", "Characters/EarthOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.EARTH, 4,
                    95, 75, 60, 48, 38, 50, 0.1, 0,
                    AttackEnum.ROCK_SLAM, AttackEnum.GIFT_OF_EARTH, AbilityEnum.NONE);
            case HeroEnum.EARTHZERKER:
                return new BaseHero(hero, "Earthzerker", "Icons/Element02_256_22", "Characters/EarthOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.EARTH, 5,
                    75, 100, 50, 38, 31, 85, 0.2, 0,
                    AttackEnum.AXE_SLASH, AttackEnum.SPLIT_SKULL, AbilityEnum.STACKING_ATTACK);

            // Electric heroes.
            case HeroEnum.STATIC_CLING:
                return new BaseHero(hero, "Static Cling", "Icons/Element02_256_16", "Characters/ElectricOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.ELECTRIC, 1,
                    55, 60, 80, 32, 38, 85, 0.15, 0,
                    AttackEnum.SPARK, AttackEnum.FLASH_OF_LIGHT, AbilityEnum.NONE);
            case HeroEnum.BATTERY:
                return new BaseHero(hero, "Battery", "Icons/Element02_256_16", "Characters/ElectricOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.ELECTRIC, 2,
                    65, 55, 85, 40, 38, 80, 0.1, 0,
                    AttackEnum.ENERGY_DRAIN, AttackEnum.CHARGE_TEAM, AbilityEnum.NONE);
            case HeroEnum.SPARK_ELEMENTAL:
                return new BaseHero(hero, "Spark Elemental", "Icons/Element02_256_16", "Characters/ElectricOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.ELECTRIC, 3,
                    60, 60, 85, 33, 39, 90, 0.2, 0,
                    AttackEnum.LIGHTNING_BOLT, AttackEnum.OVERCHARGED_BOLT, AbilityEnum.NONE);
            case HeroEnum.LIGHTNING_WRAITH:
                return new BaseHero(hero, "Lightning Wraith", "Icons/Element02_256_16", "Characters/ElectricOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.ELECTRIC, 4,
                    65, 65, 95, 32, 42, 85, 0.2, 0,
                    AttackEnum.LIGHTNING_BOLT, AttackEnum.LIGHTNING_FLASH, AbilityEnum.NONE);
            case HeroEnum.NEUROMANCER:
                return new BaseHero(hero, "Neuromancer", "Icons/Element02_256_16", "Characters/ElectricOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.ELECTRIC, 5,
                    70, 60, 90, 34, 42, 90, 0.25, 0,
                    AttackEnum.FORKED_LIGHTNING, AttackEnum.BRAIN_STORM, AbilityEnum.NONE);

            default:
                return new BaseHero(hero, "Unknown", "Icons/icon_gem", "Characters/FacelessOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.WATER, 1,
                    50, 50, 50,
                    30, 30, 50, 0.10, 0,
                    AttackEnum.BASIC_PHYSICAL, AttackEnum.SPECIAL_PHYSICAL, AbilityEnum.NONE);
        }
    }

    public static int GetBigStatGain(double baseStat) {
        int stat = (int)baseStat;
        return (stat - 40) / 10;
    }

    public static double GetSmallStatGain(double baseStat) {
        return (baseStat - 30.0) / 20.0;
    }
}

public enum HeroEnum {
    // 1-3 star heroes.
    VAPOR_CLOUD = 1, RAIN_MAN = 2, FEESH = 3, MIST_CALLER = 4, ORACLE = 5,
    HAPPY_FLOWER = 6, BUSH_WHACKER = 7, BALL_OF_ROOTS = 8, DRYAD = 9, ANGERY_TREANT = 10,
    CANDLE_MAN = 11, EMBER = 12, TORCH = 13, LAVA_GOLEM = 14, INFERNOMANCER = 15,
    SNOW_MAN = 16, ICE_CUBE = 17, ICICLE_FLINGER = 18, BLIZZARD_WIZZARD = 19, REFLECTOR = 20,
    PEBBLE_ELEMENTAL = 21, BOULDER = 22, PEBBLE_FLINGER = 23, LIVING_WALL = 24, EARTHZERKER = 25,
    STATIC_CLING = 26, BATTERY = 27, SPARK_ELEMENTAL = 28, LIGHTNING_WRAITH = 29, NEUROMANCER = 30
}

public enum RoleEnum {
    PROTECTION = 1, DAMAGE = 2, SUPPORT = 3
}

public enum FactionEnum {
    WATER = 1, GRASS = 2, FIRE = 3, ICE = 4, EARTH = 5, ELECTRIC = 6
}
