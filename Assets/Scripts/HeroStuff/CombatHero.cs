using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CombatHero : IComparable<CombatHero> {

    [NonSerialized] public BaseHero baseHero;

    [SerializeField] public Guid combatHeroGuid;
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
    [SerializeField] public double currentEnergy;
    [SerializeField] public List<StatusContainer> currentStatus;

    public CombatHero(AccountHero accountHero) {
        baseHero = accountHero.GetBaseHero();
        combatHeroGuid = Guid.NewGuid();
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
        currentEnergy = 50.0;
        currentStatus = new List<StatusContainer>();
    }

    public CombatHero(CombatHero other) {
        baseHero = other.baseHero;
        combatHeroGuid = other.combatHeroGuid;
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
        int currentSpeedComp = other.GetModifiedSpeed().CompareTo(GetModifiedSpeed());
        if (currentSpeedComp != 0) return currentSpeedComp;
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

    public double GetModifiedAttack() {
        var multiplier = 1.0;
        foreach (StatusContainer status in currentStatus) {
            if (status.status == StatusEnum.ATTACK_UP) {
                multiplier += status.value;
            } else if (status.status == StatusEnum.ATTACK_DOWN) {
                multiplier -= status.value;
            }
        }
        return multiplier * attack;
    }

    public double GetModifiedMagic() {
        var multiplier = 1.0;
        foreach (StatusContainer status in currentStatus) {
            if (status.status == StatusEnum.MAGIC_UP) {
                multiplier += status.value;
            } else if (status.status == StatusEnum.MAGIC_DOWN) {
                multiplier -= status.value;
            }
        }
        return multiplier * magic;
    }

    public double GetModifiedDefense() {
        var multiplier = 1.0;
        foreach (StatusContainer status in currentStatus) {
            if (status.status == StatusEnum.DEFENSE_UP) {
                multiplier += status.value;
            } else if (status.status == StatusEnum.DEFENSE_DOWN) {
                multiplier -= status.value;
            } else if (status.status == StatusEnum.ICE_ARMOR) {
                multiplier += status.value / 2.0;
            }
        }
        return defense * multiplier;
    }

    public double GetModifiedReflection() {
        var multiplier = 1.0;
        foreach (StatusContainer status in currentStatus) {
            if (status.status == StatusEnum.REFLECTION_UP) {
                multiplier += status.value;
            } else if (status.status == StatusEnum.REFLECTION_DOWN) {
                multiplier -= status.value;
            } else if (status.status == StatusEnum.ICE_ARMOR) {
                multiplier += status.value;
            }
        }
        return reflection * multiplier;
    }

    public double GetModifiedSpeed() {
        var multiplier = 1.0;
        foreach (StatusContainer status in currentStatus) {
            if (status.status == StatusEnum.SPEED_UP) {
                multiplier += status.value;
            } else if (status.status == StatusEnum.SPEED_DOWN) {
                multiplier -= status.value;
            } else if (status.status == StatusEnum.CHILLED) {
                multiplier -= status.value;
            }
        }
        return multiplier * speed;
    }
}
