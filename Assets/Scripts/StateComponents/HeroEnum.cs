using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroEnum {
    // 1-3 star heroes.
    VAPOR_CLOUD, RAIN_MAN, FEESH,
    CANDLE_MAN, EMBER, TORCH,
    HAPPY_FLOWER, BUSH_WHACKER, BALL_OF_ROOTS,
    SNOW_MAN, ICE_CUBE, ICICLE_FLINGER,
    STATIC_CLING, BATTERY, SPARK_ELEMENTAL,
    PEBBLE_ELEMENTAL, BOULDER, PEBBLE_FLINGER
}

public enum RoleEnum {
    PROTECTION, DAMAGE, SUPPORT
}

public enum FactionEnum {
    WATER, FIRE, GRASS, ICE, ELECTRIC, EARTH
}

public class BaseHero {

    public HeroEnum Hero { get; }
    public string HeroName { get; }
    public Sprite HeroIcon { get; }
    public RoleEnum Role { get; }
    public FactionEnum Faction { get; }
    public int Rarity { get; }
    public double BaseHealth { get; }
    public double BasePhysicalAttack { get; }
    public double BaseMagicAttack { get; }
    public double BasePhysicalDefense { get; }
    public double BaseMagicDefense { get; }
    public double BaseSpeed { get; }
    public double BaseCritChance { get; }

    public double HealthGain { get; }
    public double PhysicalAttackGain { get; }
    public double MagicAttackGain { get; }
    public double PhysicalDefenseGain { get; }
    public double MagicDefenseGain { get; }
    public double SpeedGain { get; }

    public BaseHero(HeroEnum hero, string heroName, string heroIcon, RoleEnum role, FactionEnum faction, int rarity,
        double baseHealth, double basePhysicalAttack, double baseMagicAttack,
        double basePhysicalDefense, double baseMagicDefense, double baseSpeed, double baseCritChance,
        double healthGain, double physicalAttackGain, double magicAttackGain,
        double physicalDefenseGain, double magicDefenseGain, double speedGain) {

        Hero = hero;
        HeroName = heroName;
        HeroIcon = Resources.Load<Sprite>(heroIcon);
        if (HeroIcon == null) Debug.Log("Sprite is null: " + heroIcon);
        Role = role;
        Faction = faction;
        Rarity = rarity;
        BaseHealth = baseHealth;
        BasePhysicalAttack = basePhysicalAttack;
        BaseMagicAttack = baseMagicAttack;
        BasePhysicalDefense = basePhysicalDefense;
        BaseMagicDefense = baseMagicDefense;
        BaseSpeed = baseSpeed;
        BaseCritChance = baseCritChance;
        HealthGain = healthGain;
        PhysicalAttackGain = physicalAttackGain;
        MagicAttackGain = magicAttackGain;
        PhysicalDefenseGain = physicalDefenseGain;
        MagicDefenseGain = magicDefenseGain;
        SpeedGain = speedGain;
    }

    public static BaseHero GetHero(HeroEnum hero) {
        switch (hero) {
            case HeroEnum.VAPOR_CLOUD:
                return new BaseHero(
                    hero, "Vapor Cloud", "Icons/icon_gift", RoleEnum.DAMAGE, FactionEnum.WATER, 1,
                    60, 60, 80,
                    32, 40, 65, 0.1,
                    2, 1.5, 3.5,
                    .7, 1.2, 0.8);
            case HeroEnum.RAIN_MAN:
                return new BaseHero(
                    hero, "Rain Man", "Icons/icon_gift_blue", RoleEnum.SUPPORT, FactionEnum.WATER, 2,
                    75, 60, 70,
                    38, 42, 60, 0.1,
                    3, 1.5, 3,
                    .8, 1.5, .75);
            default:
                return new BaseHero(hero, "Unknown", "Icons/icon_gem", RoleEnum.DAMAGE, FactionEnum.WATER, 1,
                    50, 50, 50,
                    30, 30, 50, 0.10,
                    1, 1, 1,
                    0.5, 0.5, 1);
        }
    }
}
