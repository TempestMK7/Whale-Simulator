using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusContainer {

    [NonSerialized] public const int INDEFINITE = -1;

    [SerializeField] public StatusEnum status;
    [SerializeField] public Guid inflicterGuid;
    [SerializeField] public double value;
    [SerializeField] public int turnsRemaining;

    public StatusContainer(StatusEnum status, Guid inflicterGuid, double value, int turnsRemaining) {
        this.status = status;
        this.inflicterGuid = inflicterGuid;
        this.value = value;
        this.turnsRemaining = turnsRemaining;
    }

    public StatusContainer(StatusContainer other) {
        status = other.status;
        inflicterGuid = other.inflicterGuid;
        value = other.value;
        turnsRemaining = other.turnsRemaining;
    }

    public string ToHumanReadableString(Dictionary<Guid, BaseHero> heroDict) {
        var inflicterName = heroDict[inflicterGuid].HeroName;
        var valueSuffix = value == 0 ? "." : string.Format(" with a value of {0}", value.ToString("0"));
        return string.Format("{0} inflicted {1} for {2} turns{3}", inflicterName, status, turnsRemaining, valueSuffix);
    }

    public static List<DamageInstance> EvaluateStatus(CombatHero hero) {
        List<DamageInstance> instances = new List<DamageInstance>();
        foreach (StatusContainer status in hero.currentStatus) {
            switch (status.status) {
                case StatusEnum.BURN:
                case StatusEnum.BLEED:
                case StatusEnum.POISON:
                    var damage = status.value;
                    hero.currentHealth -= damage;

                    var damageInstance = new DamageInstance(null, null, status.status, status.inflicterGuid, hero.combatHeroGuid);
                    damageInstance.damage = damage;
                    instances.Add(damageInstance);
                    break;
                case StatusEnum.HEALING:
                    var healing = status.value;
                    hero.currentHealth += healing;

                    damageInstance = new DamageInstance(null, null, status.status, status.inflicterGuid, hero.combatHeroGuid);
                    damageInstance.healing = healing;
                    instances.Add(damageInstance);
                    break;
            }

            if (!hero.IsAlive()) {
                hero.currentHealth = 0;
                hero.currentEnergy = 0;
                hero.currentStatus.Clear();

                var damageInstance = new DamageInstance(null, null, null, hero.combatHeroGuid, status.inflicterGuid);
                damageInstance.wasFatal = true;
                instances.Add(damageInstance);
                return instances;
            }
        }
        hero.CountDownStatus();
        return instances;
    }

    public static bool BlocksMelee(StatusEnum status) {
        switch (status) {
            case StatusEnum.STUN:
            case StatusEnum.ROOT:
            case StatusEnum.FROZEN:
                return true;
            default: return false;
        }
    }

    public static bool BlocksRanged(StatusEnum status) {
        switch (status) {
            case StatusEnum.STUN:
            case StatusEnum.BLIND:
            case StatusEnum.FROZEN:
                return true;
            default: return false;
        }
    }
}

public enum StatusEnum {
    STUN =              1,
    BLIND =             2,
    ROOT =              3,

    BLEED =             4,
    BURN =              5,
    POISON =            6,

    HEALING =           7,

    THORN_ARMOR =       8,
    LAVA_SHIELD =       9,
    ICE_ARMOR =         10,
    EARTH_ARMOR =       11,

    DOWSE =             12,
    CHILLED =           13,
    FROZEN =            14,

    ATTACK_UP =         15,
    MAGIC_UP =          16,
    DEFENSE_UP =         17,
    REFLECTION_UP =     18,
    SPEED_UP =          19,

    ATTACK_DOWN =       20,
    MAGIC_DOWN =        21,
    DEFENSE_DOWN =      22,
    REFLECTION_DOWN =   23,
    SPEED_DOWN =        24
}
