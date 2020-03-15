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
        var display = StatusDisplayContainer.GetStatusDisplay(status);
        var inflicterName = heroDict[inflicterGuid].HeroName;
        var verb = display.IsBeneficial ? "bestowed" : "inflicted";
        var valueSuffix = value == 0 ? "." : string.Format(" with a value of {0}", value.ToString("0.0"));
        return string.Format("{0} {1} {2} for {3} turns{4}", inflicterName, verb, display.StatusName, turnsRemaining, valueSuffix);
    }

    public static List<DamageInstance> EvaluateStatusEndOfTurn(CombatHero hero) {
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
                case StatusEnum.REGENERATION:
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

                var damageInstance = new DamageInstance(null, null, null, status.inflicterGuid, hero.combatHeroGuid);
                damageInstance.wasFatal = true;
                instances.Add(damageInstance);
                return instances;
            }
        }
        hero.CountDownStatus(false);
        return instances;
    }
}

public enum StatusEnum {
    STUN =              1,
    BLIND =             2,
    ROOT =              3,

    BLEED =             4,
    BURN =              5,
    POISON =            6,

    REGENERATION =      7,

    THORN_ARMOR =       8,
    LAVA_ARMOR =        9,
    ICE_ARMOR =         10,
    EARTH_ARMOR =       11,

    DOWSE =             12,
    CHILL =             13,
    FREEZE =            14,

    DAZE =              15,

    ATTACK_UP =         16,
    MAGIC_UP =          17,
    DEFENSE_UP =        18,
    REFLECTION_UP =     19,
    SPEED_UP =          20,

    ATTACK_DOWN =       21,
    MAGIC_DOWN =        22,
    DEFENSE_DOWN =      23,
    REFLECTION_DOWN =   24,
    SPEED_DOWN =        25
}

public class StatusDisplay {

    public StatusEnum Status { get; }
    public string StatusName { get; }
    public FactionEnum AssociatedFaction { get; }
    public bool IsBeneficial { get; }
    public bool ModifiesAttack { get; }
    public bool BlocksMelee { get; }
    public bool BlocksRanged { get; }

    public StatusDisplay(StatusEnum status, string statusName, FactionEnum associatedFaction,
        bool isBeneficial, bool modifiesAttack, bool blocksMelee, bool blocksRanged) {
        Status = status;
        StatusName = statusName;
        AssociatedFaction = associatedFaction;
        IsBeneficial = isBeneficial;
        ModifiesAttack = modifiesAttack;
        BlocksMelee = blocksMelee;
        BlocksRanged = blocksRanged;
    }
}

public class StatusDisplayContainer {

    private static Dictionary<StatusEnum, StatusDisplay> statusDict;

    public static void Intialize() {
        statusDict = new Dictionary<StatusEnum, StatusDisplay>();
        statusDict[StatusEnum.STUN] = new StatusDisplay(StatusEnum.STUN, "Stun", FactionEnum.ELECTRIC,
            false, true, true, true);
        statusDict[StatusEnum.BLIND] = new StatusDisplay(StatusEnum.BLIND, "Blind", FactionEnum.ELECTRIC,
            false, true, false, true);
        statusDict[StatusEnum.ROOT] = new StatusDisplay(StatusEnum.ROOT, "Entangle", FactionEnum.GRASS,
            false, true, true, false);
        statusDict[StatusEnum.BLEED] = new StatusDisplay(StatusEnum.BLEED, "Bleed", FactionEnum.GRASS,
            false, false, false, false);
        statusDict[StatusEnum.BURN] = new StatusDisplay(StatusEnum.BURN, "Burn", FactionEnum.FIRE,
            false, false, false, false);
        statusDict[StatusEnum.POISON] = new StatusDisplay(StatusEnum.POISON, "Poison", FactionEnum.GRASS,
            false, false, false, false);
        statusDict[StatusEnum.REGENERATION] = new StatusDisplay(StatusEnum.REGENERATION, "Regeneration", FactionEnum.WATER,
            true, false, false, false);
        statusDict[StatusEnum.THORN_ARMOR] = new StatusDisplay(StatusEnum.THORN_ARMOR, "Thorn Armor", FactionEnum.GRASS,
            true, false, false, false);
        statusDict[StatusEnum.LAVA_ARMOR] = new StatusDisplay(StatusEnum.LAVA_ARMOR, "Lava Armor", FactionEnum.FIRE,
            true, false, false, false);
        statusDict[StatusEnum.ICE_ARMOR] = new StatusDisplay(StatusEnum.ICE_ARMOR, "Ice Armor", FactionEnum.ICE,
            true, false, false, false);
        statusDict[StatusEnum.EARTH_ARMOR] = new StatusDisplay(StatusEnum.EARTH_ARMOR, "Earth Armor", FactionEnum.EARTH,
            true, false, false, false);
        statusDict[StatusEnum.DOWSE] = new StatusDisplay(StatusEnum.DOWSE, "Dowse", FactionEnum.WATER,
            false, true, false, false);
        statusDict[StatusEnum.CHILL] = new StatusDisplay(StatusEnum.CHILL, "Chill", FactionEnum.ICE,
            false, true, false, false);
        statusDict[StatusEnum.FREEZE] = new StatusDisplay(StatusEnum.FREEZE, "Freeze", FactionEnum.ICE,
            false, true, true, true);
        statusDict[StatusEnum.DAZE] = new StatusDisplay(StatusEnum.DAZE, "Daze", FactionEnum.ELECTRIC,
            false, true, false, false);
        statusDict[StatusEnum.ATTACK_UP] = new StatusDisplay(StatusEnum.ATTACK_UP, "Attack Up", FactionEnum.GRASS,
            true, true, false, false);
        statusDict[StatusEnum.MAGIC_UP] = new StatusDisplay(StatusEnum.MAGIC_UP, "Magic Up", FactionEnum.FIRE,
            true, true, false, false);
        statusDict[StatusEnum.DEFENSE_UP] = new StatusDisplay(StatusEnum.DEFENSE_UP, "Defense Up", FactionEnum.EARTH,
            true, false, false, false);
        statusDict[StatusEnum.REFLECTION_UP] = new StatusDisplay(StatusEnum.REFLECTION_UP, "Reflection Up", FactionEnum.ICE,
            true, false, false, false);
        statusDict[StatusEnum.SPEED_UP] = new StatusDisplay(StatusEnum.SPEED_UP, "Speed Up", FactionEnum.ELECTRIC,
            true, true, false, false);
        statusDict[StatusEnum.ATTACK_DOWN] = new StatusDisplay(StatusEnum.ATTACK_DOWN, "Attack Down", FactionEnum.GRASS,
            false, true, false, false);
        statusDict[StatusEnum.MAGIC_DOWN] = new StatusDisplay(StatusEnum.MAGIC_DOWN, "Magic Down", FactionEnum.FIRE,
            false, true, false, false);
        statusDict[StatusEnum.DEFENSE_DOWN] = new StatusDisplay(StatusEnum.DEFENSE_DOWN, "Defense Down", FactionEnum.EARTH,
            false, false, false, false);
        statusDict[StatusEnum.REFLECTION_DOWN] = new StatusDisplay(StatusEnum.REFLECTION_DOWN, "Reflection Down", FactionEnum.ICE,
            false, false, false, false);
        statusDict[StatusEnum.SPEED_DOWN] = new StatusDisplay(StatusEnum.SPEED_DOWN, "Speed Down", FactionEnum.ELECTRIC,
            false, true, false, false);
    }

    public static StatusDisplay GetStatusDisplay(StatusEnum status) {
        if (statusDict == null) Intialize();
        return statusDict[status];
    }
}
