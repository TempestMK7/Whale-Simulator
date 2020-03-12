using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusContainer {

    [NonSerialized] public const int INDEFINITE = -1;

    [SerializeField] public StatusEnum status;
    [SerializeField] public double value;
    [SerializeField] public int turnsRemaining;

    public StatusContainer(StatusEnum status, double value, int turnsRemaining) {
        this.status = status;
        this.value = value;
        this.turnsRemaining = turnsRemaining;
    }

    public StatusContainer(StatusContainer other) {
        status = other.status;
        value = other.value;
        turnsRemaining = other.turnsRemaining;
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

                    var damageInstance = new DamageInstance(null, null, status.status, null, null);
                    damageInstance.damage = damage;
                    instances.Add(damageInstance);
                    break;
                case StatusEnum.HEALING:
                    var healing = status.value;
                    hero.currentHealth += healing;

                    damageInstance = new DamageInstance(null, null, status.status, null, null);
                    damageInstance.healing = healing;
                    instances.Add(damageInstance);
                    break;
            }
        }
        hero.CountDownStatus();
        return instances;
    }

    public static bool BlocksMelee(StatusEnum status) {
        switch (status) {
            case StatusEnum.STUN:
            case StatusEnum.BLIND:
                return true;
            default: return false;
        }
    }

    public static bool BlocksRanged(StatusEnum status) {
        switch (status) {
            case StatusEnum.STUN:
            case StatusEnum.BLIND:
                return true;
            default: return false;
        }
    }
}

public enum StatusEnum {
    STUN = 1,
    BLIND = 2,
    ROOT = 3,

    BLEED = 4,
    BURN = 5,
    POISON = 6,

    HEALING = 7,

    THORN_ARMOR,
    LAVA_SHIELD,
    ICE_ARMOR = 8,
    EARTH_ARMOR = 9,

    DOWSE,
    CHILLED,
    FROZEN,

    ATTACK_UP = 10,
    MAGIC_UP = 11,
    DEFNSE_UP = 12,
    REFLECTION_UP = 13,
    SPEED_UP = 14,

    ATTACK_DOWN = 15,
    MAGIC_DOWN = 16,
    DEFENSE_DOWN = 17,
    REFLECTION_DOWN = 18,
    SPEED_DOWN = 19
}
