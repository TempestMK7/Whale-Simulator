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
                    var damage = status.value;
                    hero.currentHealth -= damage;

                    var damageInstance = new DamageInstance(null, null, status.status, null, null);
                    damageInstance.damage = damage;
                    instances.Add(damageInstance);
                    break;
            }
        }
        hero.CountDownStatus();
        return instances;
    }
}

public enum StatusEnum {
    STUN, BLIND, BLEED, POISON, BURN, HEALING, ATTACK_DOWN, MAGIC_DOWN, ATTACK_UP, MAGIC_UP
}
