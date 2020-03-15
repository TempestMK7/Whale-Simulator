using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackContainer {
    
    public static CombatStep PerformAttack(CombatHero attacker, AttackEnum attack, List<CombatHero> allies, List<CombatHero> enemies) {
        var step = new CombatStep(attacker, allies, enemies, attack);
        if (!CanAttack(attacker)) {
            step.skippedTurn = true;
            return step;
        }

        var attackInfo = AttackInfoContainer.GetAttackInfo(attack);

        foreach (CombatHero target in enemies) {
            step.damageInstances.Add(attackInfo.ApplyAttackToEnemy(attacker, target));
        }

        foreach (CombatHero ally in allies) {
            step.damageInstances.Add(attackInfo.ApplyAttackToAlly(attacker, ally));
        }

        step.damageInstances.AddRange(CombatMath.EvaluateNegativeSideEffects(attacker, enemies));

        foreach (DamageInstance instance in step.damageInstances) {
            step.totalDamage += instance.damage;
            step.totalHealing += instance.healing;
            step.energyGained += instance.attackerEnergy;
        }

        return step;
    }

    public static bool CanAttack(CombatHero hero) {
        var attack = hero.currentEnergy >= 100 ? hero.baseHero.SpecialAttack : hero.baseHero.BasicAttack;
        var attackInfo = AttackInfoContainer.GetAttackInfo(attack);

        if (attackInfo.IsMelee) {
            foreach (StatusContainer status in hero.currentStatus) {
                var display = StatusDisplayContainer.GetStatusDisplay(status.status);
                if (display.BlocksMelee) return false;
            }
        } 

        if (attackInfo.IsRanged) {
            foreach (StatusContainer status in hero.currentStatus) {
                var display = StatusDisplayContainer.GetStatusDisplay(status.status);
                if (display.BlocksRanged) return false;
            }
        }

        return true;
    }
}

public enum AttackEnum {
    BASIC_PHYSICAL = 1,
    BASIC_MAGIC = 2,
    VAPOR_CLOUD = 3,
    FISH_SLAP = 4,
    WATER_RENEW = 5,
    PETAL_SLAP = 6,
    NEEDLE_STAB = 7,
    SPEAR_THROW = 8,
    BRANCH_SLAM = 9,
    SCORCH = 10,
    FIRE_PUNCH = 11,
    ICE_PUNCH = 12,
    ICICLE_THROW = 13,
    SNOWY_WIND = 14,
    SPARK = 15,
    ENERGY_DRAIN = 16,
    LIGHTNING_BOLT = 17,
    FORKED_LIGHTNING = 18,
    ROCK_SLAM = 19,
    PEBBLE_TOSS = 20,
    AXE_SLASH = 21,

    SPECIAL_PHYSICAL = 100,
    SPECIAL_MAGIC = 101,
    FROZEN_MIRROR = 200
}

public class AttackInfoContainer {

    private static Dictionary<AttackEnum, AttackInfo> attackDict;

    public static void Initialize() {
        if (attackDict != null) return;
        attackDict = new Dictionary<AttackEnum, AttackInfo>();
        attackDict[AttackEnum.BASIC_PHYSICAL] = new AttackInfo(
            AttackEnum.BASIC_PHYSICAL, "Basic Physical", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.BASIC_MAGIC] = new AttackInfo(
            AttackEnum.BASIC_MAGIC, "Basic Magic", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            false, true, false,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.VAPOR_CLOUD] = new AttackInfo(
            AttackEnum.VAPOR_CLOUD, "Vapor Cloud", "Icons/RoleDamage", "AttackSounds/VaporCloud",
            false, true, false,
            TargetType.RANDOM, 2, TargetType.NONE, 0,
            0.5, 0, 15, 10, 0,
            StatusEnum.DOWSE, 0, 2, null, 0, 0);
        attackDict[AttackEnum.FISH_SLAP] = new AttackInfo(
            AttackEnum.FISH_SLAP, "Fish Slap", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            StatusEnum.DEFENSE_DOWN, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.WATER_RENEW] = new AttackInfo(
            AttackEnum.WATER_RENEW, "Water Renew", "Icons/RoleDamage", "AttackSounds/WaterRenew",
            false, true, false,
            TargetType.RANDOM, 1, TargetType.LOWEST_HEALTH, 1,
            0.6, 0.2, 15, 10, 0,
            null, 0, 0, StatusEnum.REGENERATION, 0.2, 2);
        attackDict[AttackEnum.PETAL_SLAP] = new AttackInfo(
            AttackEnum.PETAL_SLAP, "Petal Slap", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.NEEDLE_STAB] = new AttackInfo(
            AttackEnum.NEEDLE_STAB, "Needle Stab", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            true, false, true,
            TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            StatusEnum.POISON, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.SPEAR_THROW] = new AttackInfo(
            AttackEnum.SPEAR_THROW, "Spear Throw", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            false, true, true,
            TargetType.RANDOM, 2, TargetType.NONE, 0,
            0.4, 0, 15, 10, 0,
            StatusEnum.POISON, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.BRANCH_SLAM] = new AttackInfo(
            AttackEnum.BRANCH_SLAM, "Branch Slam", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            StatusEnum.DAZE, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.SCORCH] = new AttackInfo(
            AttackEnum.SCORCH, "Scorch", "Icons/RoleDamage", "AttackSounds/Scorch",
            false, true, false,
            TargetType.RANDOM, 2, TargetType.NONE, 0,
            0.4, 0, 15, 10, 0,
            StatusEnum.BURN, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.FIRE_PUNCH] = new AttackInfo(
            AttackEnum.FIRE_PUNCH, "Fire Punch", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            0.8, 0, 25, 10, 0,
            StatusEnum.BURN, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.ICE_PUNCH] = new AttackInfo(
            AttackEnum.ICE_PUNCH, "Ice Slam", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            0.8, 0, 25, 10, 0,
            StatusEnum.CHILL, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.ICICLE_THROW] = new AttackInfo(
            AttackEnum.ICICLE_THROW, "Icicle Throw", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            false, true, true,
            TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.SNOWY_WIND] = new AttackInfo(
            AttackEnum.SNOWY_WIND, "Snowy Wind", "Icons/RoleDamage", "AttackSounds/SnowyWind2",
            false, true, false,
            TargetType.RANDOM, 2, TargetType.NONE, 0,
            0.4, 0, 15, 10, 0,
            StatusEnum.CHILL, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.SPARK] = new AttackInfo(
            AttackEnum.SPARK, "Spark", "Icons/RoleDamage", "AttackSounds/Spark",
            true, false, false,
            TargetType.RANDOM, 1, TargetType.NONE, 0,
            0.8, 0, 25, 10, 0,
            StatusEnum.DAZE, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.ENERGY_DRAIN] = new AttackInfo(
            AttackEnum.ENERGY_DRAIN, "Energy Drain", "Icons/RoleDamage", "AttackSounds/EnergyDrain",
            false, true, false,
            TargetType.RANDOM, 1, TargetType.RANDOM, 1,
            0.6, 0, 35, -10, 10,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.LIGHTNING_BOLT] = new AttackInfo(
            AttackEnum.LIGHTNING_BOLT, "Lightning Bolt", "Icons/RoleDamage", "AttackSounds/LightningBolt",
            false, true, false,
            TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.FORKED_LIGHTNING] = new AttackInfo(
            AttackEnum.FORKED_LIGHTNING, "Forked Lightning", "Icons/RoleDamage", "AttackSounds/LightningBolt",
            false, true, false,
            TargetType.RANDOM, 2, TargetType.NONE, 0,
            0.4, 0, 15, 10, 0,
            StatusEnum.DAZE, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.ROCK_SLAM] = new AttackInfo(
            AttackEnum.ROCK_SLAM, "Rock Slam", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            StatusEnum.ATTACK_DOWN, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.PEBBLE_TOSS] = new AttackInfo(
            AttackEnum.PEBBLE_TOSS, "Pebble Toss", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            false, true, true,
            TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.AXE_SLASH] = new AttackInfo(
            AttackEnum.AXE_SLASH, "Axe Slash", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            StatusEnum.DEFENSE_DOWN, 0.2, 3, null, 0, 0);

        // These are all special attacks.
        attackDict[AttackEnum.SPECIAL_PHYSICAL] = new AttackInfo(
            AttackEnum.SPECIAL_PHYSICAL, "Special Physical", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            3, 0, -100, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.SPECIAL_MAGIC] = new AttackInfo(
            AttackEnum.SPECIAL_MAGIC, "Special Magic", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            false, true, false,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            3, 0, -100, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.FROZEN_MIRROR] = new AttackInfo(
            AttackEnum.FROZEN_MIRROR, "Frozen Mirror", "Icons/RoleDamage", "AttackSounds/FrozenMirror",
            false, false, false,
            TargetType.RANDOM, 0, TargetType.LOWEST_HEALTH, 10,
            0, 0, -100, 0, 0,
            null, 0, 0, StatusEnum.ICE_ARMOR, 0.5, 2);
    }

    public static AttackInfo GetAttackInfo(AttackEnum attack) {
        if (attackDict == null) Initialize();
        return attackDict[attack];
    }
}

public class AttackInfo {

    public AttackEnum Attack { get; }
    public string AttackName { get; }
    public Sprite AttackIcon { get; }
    public AudioClip AttackSound { get; }
    public bool IsMelee { get; }
    public bool IsRanged { get; }
    public bool IsPhysical { get; }
    public TargetType EnemyTargetType { get; }
    public int EnemyTargetCount { get; }
    public TargetType AllyTargetType { get; }
    public int AllyTargetCount { get; }
    public double DamageMultiplier { get; }
    public double HealingMultiplier { get; }
    public int AttackerEnergyGained { get; }
    public int TargetEnergyGained { get; }
    public int AllyEnergyGained { get; }
    public StatusEnum? TargetStatus { get; }
    public double TargetStatusValue { get; }
    public int TargetStatusDuration { get; }
    public StatusEnum? AllyStatus { get; }
    public double AllyStatusValue { get; }
    public int AllyStatusDuration { get; }

    public AttackInfo(AttackEnum attack, string attackName, string attackIcon, string attackSound,
        bool isMelee, bool isRanged, bool isPhysical,
        TargetType enemyTargetType, int enemyTargetCount, TargetType allyTargetType, int allyTargetCount,
        double damageMultiplier, double healingMultiplier,
        int attackerEnergyGained, int targetEnergyGained, int allyEnergyGained,
        StatusEnum? targetStatus, double targetStatusValue, int targetStatusDuration,
        StatusEnum? allyStatus, double allyStatusValue, int allyStatusDuration) {

        Attack = attack;
        AttackName = attackName;
        AttackIcon = Resources.Load<Sprite>(attackIcon);
        AttackSound = Resources.Load<AudioClip>(attackSound);

        IsMelee = isMelee;
        IsRanged = isRanged;
        IsPhysical = isPhysical;

        EnemyTargetType = enemyTargetType;
        EnemyTargetCount = enemyTargetCount;
        AllyTargetType = allyTargetType;
        AllyTargetCount = allyTargetCount;

        DamageMultiplier = damageMultiplier;
        HealingMultiplier = healingMultiplier;

        AttackerEnergyGained = attackerEnergyGained;
        TargetEnergyGained = targetEnergyGained;
        AllyEnergyGained = allyEnergyGained;

        TargetStatus = targetStatus;
        TargetStatusValue = targetStatusValue;
        TargetStatusDuration = targetStatusDuration;

        AllyStatus = allyStatus;
        AllyStatusValue = allyStatusValue;
        AllyStatusDuration = allyStatusDuration;
    }

    public DamageInstance ApplyAttackToEnemy(CombatHero attacker, CombatHero target) {
        var hitType = CombatMath.RollHitType(attacker, target);
        var hitEffectivity = CombatMath.GetEffectivity(attacker, target);
        var attackValue = IsPhysical ? attacker.GetModifiedAttack() : attacker.GetModifiedMagic();
        var defenseValue = IsPhysical ? attacker.GetModifiedDefense() : attacker.GetModifiedReflection();
        var damage = CombatMath.Damage(attackValue * DamageMultiplier, defenseValue, hitType, hitEffectivity);
        target.currentHealth -= damage;
        target.currentEnergy += TargetEnergyGained;
        attacker.currentEnergy += AttackerEnergyGained;

        var damageInstance = new DamageInstance(Attack, null, attacker.combatHeroGuid, target.combatHeroGuid);
        damageInstance.damage = damage;
        damageInstance.attackerEnergy = AttackerEnergyGained;
        damageInstance.targetEnergy = TargetEnergyGained;
        damageInstance.hitType = hitType;

        // If the target died from this attack, bail before applying status.
        if (!target.IsAlive()) {
            target.currentHealth = 0;
            target.currentEnergy = 0;
            target.currentStatus.Clear();

            damageInstance.wasFatal = true;
            return damageInstance;
        }

        if (TargetStatus != null) {
            var inflictedStatus = TargetStatus.GetValueOrDefault();
            var statusValue = TargetStatusValue;
            var statusDuration = TargetStatusDuration;
            switch (inflictedStatus) {
                case StatusEnum.BURN:
                    statusValue *= attacker.GetModifiedMagic();
                    break;
                case StatusEnum.BLEED:
                    statusValue *= attacker.GetModifiedAttack();
                    break;
                case StatusEnum.POISON:
                    statusValue *= attacker.GetModifiedMagic();
                    break;
                case StatusEnum.CHILL:
                    if (target.HasStatus(StatusEnum.CHILL) || target.HasStatus(StatusEnum.DOWSE)) {
                        inflictedStatus = StatusEnum.FREEZE;
                        statusValue = 0;
                        statusDuration = 1;
                    }
                    break;
                case StatusEnum.DAZE:
                    if (target.HasStatus(StatusEnum.DAZE) || (target.HasStatus(StatusEnum.DOWSE) && attacker.baseHero.Faction == FactionEnum.ELECTRIC)) {
                        inflictedStatus = StatusEnum.STUN;
                        statusValue = 0;
                        statusDuration = 1;
                    }
                    break;
                default:
                    break;
            }
            var statusContainer = new StatusContainer(inflictedStatus, attacker.combatHeroGuid, statusValue, statusDuration);
            target.AddStatus(statusContainer);
            damageInstance.AddStatus(statusContainer);
        }

        return damageInstance;
    }

    public DamageInstance ApplyAttackToAlly(CombatHero attacker, CombatHero ally) {
        var attackValue = IsPhysical ? attacker.GetModifiedAttack() : attacker.GetModifiedMagic();
        var healing = attackValue * HealingMultiplier;
        healing = ally.ReceiveHealing(healing);
        ally.currentEnergy += AllyEnergyGained;
        attacker.currentEnergy += AttackerEnergyGained;

        var damageInstance = new DamageInstance(Attack, null, attacker.combatHeroGuid, ally.combatHeroGuid);
        damageInstance.healing = healing;
        damageInstance.attackerEnergy = AttackerEnergyGained;
        damageInstance.targetEnergy = AllyEnergyGained;

        // If the ally died from this attack somehow, bail before applying status.
        if (!ally.IsAlive()) {
            ally.currentHealth = 0;
            ally.currentEnergy = 0;
            ally.currentStatus.Clear();

            damageInstance.wasFatal = true;
            return damageInstance;
        }

        if (AllyStatus != null) {
            var bestowedStatus = AllyStatus.GetValueOrDefault();
            var statusValue = AllyStatusValue;
            var statusDuration = AllyStatusDuration;
            switch (bestowedStatus) {
                case StatusEnum.REGENERATION:
                    statusValue *= attacker.GetModifiedMagic();
                    break;
                default:
                    break;
            }
            var statusContainer = new StatusContainer(bestowedStatus, attacker.combatHeroGuid, statusValue, statusDuration);
            ally.AddStatus(statusContainer);
            damageInstance.AddStatus(statusContainer);
        }

        return damageInstance;
    }
}
