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

public class BaseHero {

    public HeroEnum Hero { get; }
    public long BaseHealth { get; }
    public long BasePhysicalAttack { get; }
    public long BaseMagicAttack { get; }
    public long BasePhysicalDefense { get; }
    public long BaseMagicDefense { get; }
    public long BaseSpeed { get; }
    public double BaseCritChance { get; }
    public double BaseEnergyGeneration { get; }

    public long HealthGain { get; }
    public long PhysicalAttackGain { get; }
    public long MagicAttackGain { get; }
    public long PhysicalDefenseGain { get; }
    public long MagicDefenseGain { get; }
    public long SpeedGain { get; }

    public BaseHero(HeroEnum hero, long baseHealth, long basePhysicalAttack, long baseMagicAttack,
        long basePhysicalDefense, long baseMagicDefense, long baseSpeed,
        double baseCritChance, double baseEnergyGeneration,
        long healthGain, long physicalAttackGain, long magicAttackGain,
        long physicalDefenseGain, long magicDefenseGain, long speedGain) {

        Hero = hero;
        BaseHealth = baseHealth;
        BasePhysicalAttack = basePhysicalAttack;
        BaseMagicAttack = baseMagicAttack;
        BasePhysicalDefense = basePhysicalDefense;
        BaseMagicDefense = baseMagicDefense;
        BaseSpeed = baseSpeed;
        BaseCritChance = baseCritChance;
        BaseEnergyGeneration = baseEnergyGeneration;
        HealthGain = healthGain;
        PhysicalAttackGain = physicalAttackGain;
        MagicAttackGain = magicAttackGain;
        PhysicalDefenseGain = physicalDefenseGain;
        MagicDefenseGain = magicDefenseGain;
        SpeedGain = speedGain;
    }
}
