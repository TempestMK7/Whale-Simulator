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
    [SerializeField] public double critChance;
    [SerializeField] public double deflectionChance;

    [SerializeField] public double currentHealth;
    [SerializeField] public double currentEnergy;
    [SerializeField] public List<StatusContainer> currentStatus;

    public CombatHero(AccountHero accountHero, List<AccountEquipment> equipped) {
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
        critChance = baseHero.BaseCritChance;
        deflectionChance = baseHero.BaseDeflectionChance;

        currentHealth = health;
        currentEnergy = 50.0;
        currentStatus = new List<StatusContainer>();

        foreach (AccountEquipment equipment in equipped) {
            var baseEquipment = equipment.GetBaseEquipment();
            attack += baseEquipment.BaseAttack * equipment.Level;
            magic += baseEquipment.BaseMagic * equipment.Level;
            defense += baseEquipment.BaseDefense * equipment.Level;
            reflection += baseEquipment.BaseReflection * equipment.Level;
            critChance += baseEquipment.BaseCrit;
            deflectionChance += baseEquipment.BaseDeflect;
        }
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
        critChance = other.critChance;
        deflectionChance = other.deflectionChance;

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

    public void CountDownStatus(bool modifiesAttack) {
        var newStatus = new List<StatusContainer>();
        foreach (StatusContainer status in currentStatus) {
            var statusDisplay = StatusDisplayContainer.GetStatusDisplay(status.status);
            if (status.turnsRemaining == StatusContainer.INDEFINITE) {
                newStatus.Add(status);
            } else if (modifiesAttack == statusDisplay.ModifiesAttack) {
                status.turnsRemaining--;
                if (status.turnsRemaining > 0) {
                    newStatus.Add(status);
                }
            } else {
                newStatus.Add(status);
            }
        }
        currentStatus = newStatus;
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
        var flatAmount = 0.0;
        foreach (StatusContainer status in currentStatus) {
            if (status.status == StatusEnum.DEFENSE_UP) {
                multiplier += status.value;
            } else if (status.status == StatusEnum.DEFENSE_DOWN) {
                multiplier -= status.value;
            } else if (status.status == StatusEnum.ICE_ARMOR) {
                multiplier += status.value / 2.0;
            } else if (status.status == StatusEnum.LAVA_ARMOR) {
                flatAmount += status.value;
            } else if (status.status == StatusEnum.THORN_ARMOR) {
                flatAmount += status.value;
            } else if (status.status == StatusEnum.EARTH_ARMOR) {
                multiplier += status.value;
            }
        }
        return (defense * multiplier) + flatAmount;
    }

    public double GetModifiedReflection() {
        var multiplier = 1.0;
        var flatAmount = 0.0;
        foreach (StatusContainer status in currentStatus) {
            if (status.status == StatusEnum.REFLECTION_UP) {
                multiplier += status.value;
            } else if (status.status == StatusEnum.REFLECTION_DOWN) {
                multiplier -= status.value;
            } else if (status.status == StatusEnum.ICE_ARMOR) {
                multiplier += status.value;
            } else if (status.status == StatusEnum.LAVA_ARMOR) {
                flatAmount += status.value;
            } else if (status.status == StatusEnum.THORN_ARMOR) {
                flatAmount += status.value / 2.0;
            } else if (status.status == StatusEnum.EARTH_ARMOR) {
                multiplier += status.value / 2.0;
            }
        }
        return (reflection * multiplier) + flatAmount;
    }

    public double GetModifiedSpeed() {
        var multiplier = 1.0;
        foreach (StatusContainer status in currentStatus) {
            if (status.status == StatusEnum.SPEED_UP) {
                multiplier += status.value;
            } else if (status.status == StatusEnum.SPEED_DOWN) {
                multiplier -= status.value;
            } else if (status.status == StatusEnum.CHILL) {
                if (baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                    multiplier += status.value;
                } else {
                    multiplier -= status.value;
                }
            }
        }
        return multiplier * speed;
    }

    public double GetModifiedCrit() {
        var modified = critChance;
        foreach (StatusContainer status in currentStatus) {
            if (status.status == StatusEnum.DAZE) {
                modified -= status.value;
            }
        }
        return modified;
    }

    public double GetModifiedDeflection() {
        var modified = deflectionChance;
        foreach (StatusContainer status in currentStatus) {
            if (status.status == StatusEnum.DAZE) {
                modified -= status.value;
            }
        }
        return modified;
    }

    public double ReceiveHealing(double healing) {
        double missingHealth = health - currentHealth;
        if (missingHealth >= healing) {
            currentHealth += healing;
            return healing;
        } else {
            currentHealth = health;
            return missingHealth;
        }
    }
}
