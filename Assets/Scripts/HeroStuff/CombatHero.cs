using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CombatHero : IComparable<CombatHero> {

    [NonSerialized] public BaseHero baseHero;

    [SerializeField] public HeroEnum heroEnum;
    [SerializeField] public int awakeningLevel;
    [SerializeField] public int currentLevel;

    [SerializeField] public double health;
    [SerializeField] public double attack;
    [SerializeField] public double magic;
    [SerializeField] public double defense;
    [SerializeField] public double reflection;
    [SerializeField] public double speed;

    [SerializeField] public double currentHealth;
    [SerializeField] public double currentAttack;
    [SerializeField] public double currentMagic;
    [SerializeField] public double currentDefense;
    [SerializeField] public double currentReflection;
    [SerializeField] public double currentSpeed;
    [SerializeField] public double currentEnergy;
    [SerializeField] public List<StatusContainer> currentStatus;

    public CombatHero(AccountHero accountHero) {
        baseHero = accountHero.GetBaseHero();
        heroEnum = baseHero.Hero;
        awakeningLevel = accountHero.AwakeningLevel;
        currentLevel = accountHero.CurrentLevel;

        health = GetBigStat(baseHero.BaseHealth) * 10.0;
        attack = GetBigStat(baseHero.BaseAttack);
        magic = GetBigStat(baseHero.BaseMagic);
        defense = GetSmallStat(baseHero.BaseDefense);
        reflection = GetSmallStat(baseHero.BaseReflection);
        speed = GetBigStat(baseHero.BaseSpeed);

        currentHealth = health;
        currentAttack = attack;
        currentMagic = magic;
        currentDefense = defense;
        currentReflection = reflection;
        currentSpeed = speed;
        currentEnergy = 50.0;
        currentStatus = new List<StatusContainer>();
    }

    public CombatHero(CombatHero other) {
        baseHero = other.baseHero;
        heroEnum = other.heroEnum;
        awakeningLevel = other.awakeningLevel;
        currentLevel = other.currentLevel;

        health = other.health;
        attack = other.attack;
        magic = other.magic;
        defense = other.defense;
        reflection = other.reflection;
        speed = other.speed;

        currentHealth = other.currentHealth;
        currentAttack = other.currentAttack;
        currentMagic = other.currentMagic;
        currentDefense = other.currentDefense;
        currentReflection = other.currentReflection;
        currentSpeed = other.currentSpeed;
        currentEnergy = other.currentEnergy;
        currentStatus = new List<StatusContainer>();
        foreach (StatusContainer container in other.currentStatus) {
            currentStatus.Add(new StatusContainer(container));
        }
    }

    private double GetBigStat(double baseStat) {
        return (baseStat + (BaseHero.GetBigStatGain(baseStat) * currentLevel)) * Mathf.Pow(1.1f, awakeningLevel - 1);
    }

    private double GetSmallStat(double baseStat) {
        return baseStat + (BaseHero.GetSmallStatGain(baseStat) * currentLevel) * Mathf.Pow(1.1f, awakeningLevel - 1);
    }

    public int CompareTo(CombatHero other) {
        int currentSpeedComp = other.currentSpeed.CompareTo(currentSpeed);
        if (currentSpeedComp != 0) return currentSpeedComp;
        int speedComp = other.speed.CompareTo(speed);
        if (speedComp != 0) return speedComp;
        int energyComp = other.currentEnergy.CompareTo(currentEnergy);
        if (energyComp != 0) return energyComp;
        int levelComp = other.currentLevel.CompareTo(currentLevel);
        if (levelComp != 0) return levelComp;
        int awakeningComp = other.awakeningLevel.CompareTo(awakeningLevel);
        if (awakeningComp != 0) return awakeningComp;
        return other.baseHero.HeroName.CompareTo(baseHero.HeroName);
    }

    public bool IsAlive() {
        return currentHealth > 0;
    }

    public bool HasAbility(AbilityEnum ability) {
        return baseHero.PassiveAbility == ability;
    }

    public bool HasStatus(StatusEnum status) {
        foreach (StatusContainer container in currentStatus) {
            if (container.status == status) return true;
        }
        return false;
    }

    public void AddStatus(StatusContainer status) {
        currentStatus.Add(status);
    }

    public void CountDownStatus() {
        var newStatus = new List<StatusContainer>();
        foreach (StatusContainer status in currentStatus) {
            if (status.turnsRemaining == StatusContainer.INDEFINITE) {
                newStatus.Add(status);
            } else {
                status.turnsRemaining--;
                if (status.turnsRemaining > 0) {
                    newStatus.Add(status);
                }
            }
        }
    }
}
