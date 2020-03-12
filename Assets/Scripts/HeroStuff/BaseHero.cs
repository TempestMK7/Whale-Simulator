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

    public AttackEnum BasicAttack { get; }
    public SpecialAttackEnum SpecialAttack { get; }

    public BaseHero(HeroEnum hero, string heroName, string heroIcon, string animator, RoleEnum role, FactionEnum faction, int rarity,
        double baseHealth, double baseAttack, double baseMagic,
        double baseDefense, double baseReflection, double baseSpeed, double baseCritChance,
        AttackEnum basicAttack, SpecialAttackEnum specialAttack) {

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
        BasicAttack = basicAttack;
        SpecialAttack = specialAttack;
    }

    public static BaseHero GetHero(HeroEnum hero) {
        switch (hero) {
            case HeroEnum.VAPOR_CLOUD:
                return new BaseHero(hero, "Vapor Cloud", "Icons/Element02_256_04", "Characters/WaterOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.WATER, 1,
                    60, 60, 80, 32, 40, 65, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.RAIN_MAN:
                return new BaseHero(hero, "Rain Man", "Icons/Element02_256_04", "Characters/WaterOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.WATER, 2,
                    75, 60, 70, 38, 42, 60, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.FEESH:
                return new BaseHero(hero, "Feesh", "Icons/Element02_256_04", "Characters/WaterOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.WATER, 3,
                    85, 70, 50, 48, 42, 50, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.MIST_CALLER:
                return new BaseHero(hero, "Mist Caller", "Icons/Element02_256_04", "Characters/WaterOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.WATER, 4,
                    60, 55, 95, 35, 40, 75, 0.15,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.ORACLE:
                return new BaseHero(hero, "Oracle", "Icons/Element02_256_04", "Characters/WaterOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.WATER, 5,
                    75, 60, 90, 38, 42, 75, 0.15,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.CANDLE_MAN:
                return new BaseHero(hero, "Candle Man", "Icons/Element02_256_01", "Characters/FireOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.FIRE, 1,
                    55, 60, 85, 31, 40, 85, 0.15,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.EMBER:
                return new BaseHero(hero, "Ember", "Icons/Element02_256_01", "Characters/FireOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.FIRE, 2,
                    70, 55, 90, 34, 38, 80, 0.15,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.TORCH:
                return new BaseHero(hero, "Torch", "Icons/Element02_256_01", "Characters/FireOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.FIRE, 3,
                    65, 65, 95, 34, 40, 70, 0.2,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.LAVA_GOLEM:
                return new BaseHero(hero, "Lava Golem", "Icons/Element02_256_01", "Characters/FireOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.FIRE, 4,
                    85, 80, 60, 48, 40, 60, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.INFERNOMANCER:
                return new BaseHero(hero, "Infernomancer", "Icons/Element02_256_01", "Characters/FireOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.FIRE, 5,
                    70, 60, 100, 32, 35, 95, 0.25,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.HAPPY_FLOWER:
                return new BaseHero(hero, "Happy Flower", "Icons/Element02_256_10", "Characters/GrassOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.GRASS, 1,
                    70, 70, 55, 38, 36, 55, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.BUSH_WHACKER:
                return new BaseHero(hero, "Bush Whacker", "Icons/Element02_256_10", "Characters/GrassOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.GRASS, 2,
                    75, 80, 60, 36, 34, 80, 0.2,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.BALL_OF_ROOTS:
                return new BaseHero(hero, "Ball Of Roots", "Icons/Element02_256_10", "Characters/GrassOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.GRASS, 3,
                    90, 65, 60, 36, 34, 80, 0.15,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.DRYAD:
                return new BaseHero(hero, "Dryad", "Icons/Element02_256_10", "Characters/GrassOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.GRASS, 4,
                    80, 85, 85, 38, 38, 80, 0.15,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.ANGERY_TREANT:
                return new BaseHero(hero, "Angery Treant", "Icons/Element02_256_10", "Characters/GrassOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.GRASS, 5,
                    100, 75, 60, 46, 42, 50, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.SNOW_MAN:
                return new BaseHero(hero, "Snow Man", "Icons/Element02_256_19", "Characters/IceOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.ICE, 1,
                    70, 70, 65, 36, 40, 60, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.ICE_CUBE:
                return new BaseHero(hero, "Ice Cube", "Icons/Element02_256_19", "Characters/IceOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.ICE, 2,
                    85, 75, 55, 40, 45, 55, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.ICICLE_FLINGER:
                return new BaseHero(hero, "Icicle Flinger", "Icons/Element02_256_19", "Characters/IceOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.ICE, 3,
                    65, 90, 60, 35, 38, 85, 0.2,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.BLIZZARD_WIZZARD:
                return new BaseHero(hero, "Blizzard Wizard", "Icons/Element02_256_19", "Characters/IceOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.ICE, 4,
                    75, 60, 95, 32, 38, 90, 0.25,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.REFLECTOR:
                return new BaseHero(hero, "Reflector", "Icons/Element02_256_19", "Characters/IceOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.ICE, 5,
                    95, 75, 80, 42, 50, 55, 0.15,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.STATIC_CLING:
                return new BaseHero(hero, "Static Cling", "Icons/Element02_256_16", "Characters/ElectricOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.ELECTRIC, 1,
                    55, 60, 80, 32, 38, 85, 0.15,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.BATTERY:
                return new BaseHero(hero, "Battery", "Icons/Element02_256_16", "Characters/ElectricOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.ELECTRIC, 2,
                    65, 55, 85, 40, 38, 80, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.SPARK_ELEMENTAL:
                return new BaseHero(hero, "Spark Elemental", "Icons/Element02_256_16", "Characters/ElectricOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.ELECTRIC, 3,
                    60, 60, 85, 33, 39, 90, 0.2,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.LIGHTNING_WRAITH:
                return new BaseHero(hero, "Lightning Wraith", "Icons/Element02_256_16", "Characters/ElectricOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.ELECTRIC, 4,
                    65, 65, 95, 32, 42, 85, 0.2,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.NEUROMANCER:
                return new BaseHero(hero, "Neuromancer", "Icons/Element02_256_16", "Characters/ElectricOverrideController",
                    RoleEnum.SUPPORT, FactionEnum.ELECTRIC, 5,
                    70, 60, 90, 34, 42, 90, 0.25,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.PEBBLE_ELEMENTAL:
                return new BaseHero(hero, "Pebble Elemental", "Icons/Element02_256_22", "Characters/EarthOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.EARTH, 1,
                    80, 75, 50, 45, 35, 55, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.BOULDER:
                return new BaseHero(hero, "Boulder", "Icons/Element02_256_22", "Characters/EarthOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.EARTH, 2,
                    90, 70, 66, 46, 38, 60, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.PEBBLE_FLINGER:
                return new BaseHero(hero, "Pebble Flinger", "Icons/Element02_256_22", "Characters/EarthOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.EARTH, 3,
                    70, 85, 60, 38, 32, 75, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.LIVING_WALL:
                return new BaseHero(hero, "Living Wall", "Icons/Element02_256_22", "Characters/EarthOverrideController",
                    RoleEnum.PROTECTION, FactionEnum.EARTH, 4,
                    95, 75, 60, 48, 38, 50, 0.1,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            case HeroEnum.EARTHZERKER:
                return new BaseHero(hero, "Earthzerker", "Icons/Element02_256_22", "Characters/EarthOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.EARTH, 5,
                    75, 100, 50, 38, 31, 85, 0.2,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
            default:
                return new BaseHero(hero, "Unknown", "Icons/icon_gem", "Characters/FacelessOverrideController",
                    RoleEnum.DAMAGE, FactionEnum.WATER, 1,
                    50, 50, 50,
                    30, 30, 50, 0.10,
                    AttackEnum.BASIC_MAGIC, SpecialAttackEnum.BASIC_MAGIC);
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
    VAPOR_CLOUD, RAIN_MAN, FEESH, MIST_CALLER, ORACLE,
    CANDLE_MAN, EMBER, TORCH, LAVA_GOLEM, INFERNOMANCER,
    HAPPY_FLOWER, BUSH_WHACKER, BALL_OF_ROOTS, DRYAD, ANGERY_TREANT,
    SNOW_MAN, ICE_CUBE, ICICLE_FLINGER, BLIZZARD_WIZZARD, REFLECTOR,
    STATIC_CLING, BATTERY, SPARK_ELEMENTAL, LIGHTNING_WRAITH, NEUROMANCER,
    PEBBLE_ELEMENTAL, BOULDER, PEBBLE_FLINGER, LIVING_WALL, EARTHZERKER
}

public enum RoleEnum {
    PROTECTION, DAMAGE, SUPPORT
}

public enum FactionEnum {
    WATER, GRASS, FIRE, ICE, EARTH, ELECTRIC
}
