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
            step.damageInstances.AddRange(attackInfo.ApplyAttackToEnemy(attacker, target));
        }

        foreach (CombatHero ally in allies) {
            step.damageInstances.Add(attackInfo.ApplyAttackToAlly(attacker, ally));
        }

        attacker.currentEnergy += attackInfo.AttackerEnergyGained;
        step.energyGained = attackInfo.AttackerEnergyGained;

        step.damageInstances.AddRange(CombatMath.EvaluateNegativeSideEffects(attacker, enemies, step));

        foreach (DamageInstance instance in step.damageInstances) {
            step.totalDamage += instance.damage;
            step.totalHealing += instance.healing;
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
    // BASIC ATTACKS.
    BASIC_PHYSICAL = 1,
    BASIC_MAGIC = 2,

    // Water
    VAPOR_CLOUD = 101,
    FISH_SLAP = 102,
    WATER_RENEW = 103,

    // Grass
    PETAL_SLAP = 201,
    NEEDLE_STAB = 202,
    SPEAR_THROW = 203,
    BRANCH_SLAM = 204,

    // Fire
    FIRE_BOLT = 301,
    SCORCH = 302,
    FIRE_PUNCH = 303,

    // Ice
    ICE_PUNCH = 401,
    ICICLE_THROW = 402,
    SNOWY_WIND = 403,

    // Electric
    SPARK = 501,
    ENERGY_DRAIN = 502,
    LIGHTNING_BOLT = 503,
    FORKED_LIGHTNING = 504,

    // Earth
    ROCK_SLAM = 601,
    PEBBLE_TOSS = 602,
    AXE_SLASH = 603,

    // SPECIAL ATTACKS.
    SPECIAL_PHYSICAL = 1000,
    SPECIAL_MAGIC = 1001,

    // Water
    WATER_SHOT = 1101,
    DRENCHING_WAVE = 1102,
    ENSCALE_TEAM = 1103,
    TSUNAMI = 1104,
    HEALING_WAVE = 1105,

    // Grass
    HEALING_SUN = 1201,
    WEED_WHACKER = 1202,
    ENTANGLING_ROOTS = 1203,
    RITUAL_OF_VENOM = 1204,
    GIFT_OF_THORNS = 1205,

    // Fire
    TWIN_FLAME = 1301,
    TURN_UP_THE_HEAT = 1302,
    IMMOLATE = 1303,
    GIFT_OF_LAVA = 1304,
    FIRE_STORM = 1305,

    // Ice
    CHILLY_WIND = 1401,
    ENCASE_IN_ICE = 1402,
    FLINGING_SPREE = 1403,
    BLIZZARD = 1404,
    GIFT_OF_ICE = 1405,
    
    // Earth
    HEAD_CRACK = 1501,
    ENCASE_IN_ROCK = 1502,
    PEBBLE_SHOWER = 1503,
    GIFT_OF_EARTH = 1504,
    SPLIT_SKULL = 1505,

    // Electric
    FLASH_OF_LIGHT = 1601,
    CHARGE_TEAM = 1602,
    OVERCHARGED_BOLT = 1603,
    LIGHTNING_FLASH = 1604,
    BRAIN_STORM = 1605
}

public class AttackInfoContainer {

    private static Dictionary<AttackEnum, AttackInfo> attackDict;

    public static void Initialize() {
        if (attackDict != null) return;
        attackDict = new Dictionary<AttackEnum, AttackInfo>();

        // These are all basic attacks.
        attackDict[AttackEnum.BASIC_PHYSICAL] = new AttackInfo(
            AttackEnum.BASIC_PHYSICAL, "Basic Physical", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.BASIC_MAGIC] = new AttackInfo(
            AttackEnum.BASIC_MAGIC, "Basic Magic", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);

        // Water
        attackDict[AttackEnum.VAPOR_CLOUD] = new AttackInfo(
            AttackEnum.VAPOR_CLOUD, "Vapor Cloud", "Icons/RoleDamage", "AttackSounds/VaporCloud",
            AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.RANDOM, 2, TargetType.NONE, 0,
            0.5, 0, 25, 10, 0,
            StatusEnum.DOWSE, 0, 2, null, 0, 0);
        attackDict[AttackEnum.FISH_SLAP] = new AttackInfo(
            AttackEnum.FISH_SLAP, "Fish Slap", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            StatusEnum.DEFENSE_DOWN, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.WATER_RENEW] = new AttackInfo(
            AttackEnum.WATER_RENEW, "Water Renew", "Icons/RoleDamage", "AttackSounds/WaterRenew",
            AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER,
            false, true, false,
            TargetType.RANDOM, 1, TargetType.LOWEST_HEALTH, 1,
            0.6, 0.2, 25, 10, 0,
            null, 0, 0, StatusEnum.REGENERATION, 0.2, 2);

        // Grass
        attackDict[AttackEnum.PETAL_SLAP] = new AttackInfo(
            AttackEnum.PETAL_SLAP, "Petal Slap", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.NEEDLE_STAB] = new AttackInfo(
            AttackEnum.NEEDLE_STAB, "Needle Stab", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            StatusEnum.POISON, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.SPEAR_THROW] = new AttackInfo(
            AttackEnum.SPEAR_THROW, "Spear Throw", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null,
            false, true, true,
            TargetType.RANDOM, 2, TargetType.NONE, 0,
            0.4, 0, 25, 10, 0,
            StatusEnum.POISON, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.BRANCH_SLAM] = new AttackInfo(
            AttackEnum.BRANCH_SLAM, "Branch Slam", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            StatusEnum.DAZE, 0.2, 2, null, 0, 0);

        // Fire
        attackDict[AttackEnum.FIRE_BOLT] = new AttackInfo(
            AttackEnum.FIRE_BOLT, "Fire Bolt", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            StatusEnum.BURN, 0.2, 1, null, 0, 0);
        attackDict[AttackEnum.SCORCH] = new AttackInfo(
            AttackEnum.SCORCH, "Scorch", "Icons/RoleDamage", "AttackSounds/Scorch",
            AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.RANDOM, 2, TargetType.NONE, 0,
            0.4, 0, 25, 10, 0,
            StatusEnum.BURN, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.FIRE_PUNCH] = new AttackInfo(
            AttackEnum.FIRE_PUNCH, "Fire Punch", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            0.8, 0, 25, 10, 0,
            StatusEnum.BURN, 0.2, 2, null, 0, 0);

        // Ice
        attackDict[AttackEnum.ICE_PUNCH] = new AttackInfo(
            AttackEnum.ICE_PUNCH, "Ice Slam", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            0.8, 0, 25, 10, 0,
            StatusEnum.CHILL, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.ICICLE_THROW] = new AttackInfo(
            AttackEnum.ICICLE_THROW, "Icicle Throw", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER, null, null,
            false, true, true,
            TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.SNOWY_WIND] = new AttackInfo(
            AttackEnum.SNOWY_WIND, "Snowy Wind", "Icons/RoleDamage", "AttackSounds/SnowyWind2",
            AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.RANDOM, 2, TargetType.NONE, 0,
            0.4, 0, 25, 10, 0,
            StatusEnum.CHILL, 0.2, 2, null, 0, 0);

        // Earth
        attackDict[AttackEnum.ROCK_SLAM] = new AttackInfo(
            AttackEnum.ROCK_SLAM, "Rock Slam", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            StatusEnum.ATTACK_DOWN, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.PEBBLE_TOSS] = new AttackInfo(
            AttackEnum.PEBBLE_TOSS, "Pebble Toss", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            AttackParticleEnum.EARTH, ParticleOriginEnum.ATTACKER, null, null,
            false, true, true,
            TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.AXE_SLASH] = new AttackInfo(
            AttackEnum.AXE_SLASH, "Axe Slash", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            StatusEnum.DEFENSE_DOWN, 0.2, 3, null, 0, 0);

        // Electric
        attackDict[AttackEnum.SPARK] = new AttackInfo(
            AttackEnum.SPARK, "Spark", "Icons/RoleDamage", "AttackSounds/Spark",
            null, null, null, null,
            true, false, false,
            TargetType.RANDOM, 1, TargetType.NONE, 0,
            0.8, 0, 25, 10, 0,
            StatusEnum.DAZE, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.ENERGY_DRAIN] = new AttackInfo(
            AttackEnum.ENERGY_DRAIN, "Energy Drain", "Icons/RoleDamage", "AttackSounds/EnergyDrain",
            AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER,
            false, true, false,
            TargetType.RANDOM, 1, TargetType.RANDOM, 1,
            0.6, 0, 35, -10, 10,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.LIGHTNING_BOLT] = new AttackInfo(
            AttackEnum.LIGHTNING_BOLT, "Lightning Bolt", "Icons/RoleDamage", "AttackSounds/LightningBolt",
            AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
            1, 0, 25, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.FORKED_LIGHTNING] = new AttackInfo(
            AttackEnum.FORKED_LIGHTNING, "Forked Lightning", "Icons/RoleDamage", "AttackSounds/LightningBolt",
            AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.RANDOM, 2, TargetType.NONE, 0,
            0.4, 0, 25, 10, 0,
            StatusEnum.DAZE, 0.2, 2, null, 0, 0);

        // These are all special attacks.
        attackDict[AttackEnum.SPECIAL_PHYSICAL] = new AttackInfo(
            AttackEnum.SPECIAL_PHYSICAL, "Special Physical", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            4, 0, -100, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.SPECIAL_MAGIC] = new AttackInfo(
            AttackEnum.SPECIAL_MAGIC, "Special Magic", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            4, 0, -100, 10, 0,
            null, 0, 0, null, 0, 0);

        // Water
        attackDict[AttackEnum.WATER_SHOT] = new AttackInfo(
            AttackEnum.WATER_SHOT, "Water Shot", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.RANDOM, 2, TargetType.NONE, 0,
            1.5, 0, -100, 10, 0,
            StatusEnum.DOWSE, 0, 2, null, 0, 0);
        attackDict[AttackEnum.DRENCHING_WAVE] = new AttackInfo(
            AttackEnum.DRENCHING_WAVE, "Drenching Wave", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            AttackParticleEnum.ELECTRIC, ParticleOriginEnum.OVERHEAD, null, null,
            false, true, false,
            TargetType.RANDOM, 3, TargetType.NONE, 0,
            1, 0, -100, 10, 0,
            StatusEnum.REFLECTION_DOWN, 0.3, 3, null, 0, 0);
        attackDict[AttackEnum.ENSCALE_TEAM] = new AttackInfo(
            AttackEnum.ENSCALE_TEAM, "Enscale Team", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.RANDOM, 5,
            2, 0, -100, 10, 0,
            StatusEnum.DEFENSE_DOWN, 0.2, 2, StatusEnum.DEFENSE_UP, 0.3, 3);
        attackDict[AttackEnum.TSUNAMI] = new AttackInfo(
            AttackEnum.TSUNAMI, "Tsunami", "Icons/RoleDamage", "AttackSounds/VaporCloud",
            AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD, null, null,
            false, true, false,
            TargetType.RANDOM, 5, TargetType.NONE, 0,
            0.7, 0, -100, 10, 0,
            StatusEnum.DOWSE, 0, 2, null, 0, 0);
        attackDict[AttackEnum.HEALING_WAVE] = new AttackInfo(
            AttackEnum.HEALING_WAVE, "Healing Wave", "Icons/RoleSupport", "AttackSounds/WaterRenew",
            AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD, AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD,
            false, false, false,
            TargetType.NONE, 0, TargetType.RANDOM, 5,
            0, 0.4, -100, 0, 0,
            null, 0, 0, StatusEnum.REGENERATION, 0.2, 3);

        // Grass
        attackDict[AttackEnum.HEALING_SUN] = new AttackInfo(
            AttackEnum.HEALING_SUN, "Healing Sun", "Icons/RoleSupport", "AttackSounds/BasicMagic",
            null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.OVERHEAD,
            false, false, false,
            TargetType.NONE, 0, TargetType.RANDOM, 5,
            0, 0.4, -100, 0, 0,
            null, 0, 0, StatusEnum.REGENERATION, 0.2, 3);
        attackDict[AttackEnum.WEED_WHACKER] = new AttackInfo(
             AttackEnum.WEED_WHACKER, "Weed Whacker", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
             null, null, null, null,
             true, false, true,
             TargetType.LOWEST_HEALTH, 2, TargetType.NONE, 0,
             1.5, 0, -100, 10, 0,
             StatusEnum.POISON, 0.5, 2, null, 0, 0);
        attackDict[AttackEnum.ENTANGLING_ROOTS] = new AttackInfo(
             AttackEnum.ENTANGLING_ROOTS, "Entangling Roots", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
             null, null, null, null,
             true, false, true,
             TargetType.RANDOM, 5, TargetType.NONE, 0,
             0.5, 0, -100, 10, 0,
             StatusEnum.ROOT, 0, 2, null, 0, 0);
        attackDict[AttackEnum.RITUAL_OF_VENOM] = new AttackInfo(
             AttackEnum.RITUAL_OF_VENOM, "Ritual of Venom", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
             AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null,
             false, true, false,
             TargetType.RANDOM, 3, TargetType.NONE, 0,
             1, 0, -100, 10, 0,
             StatusEnum.POISON, 0.2, 3, null, 0, 0);
        attackDict[AttackEnum.GIFT_OF_THORNS] = new AttackInfo(
            AttackEnum.GIFT_OF_THORNS, "Gift of Thorns", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER,
            false, false, true,
            TargetType.NONE, 0, TargetType.RANDOM, 5,
            0, 0, -100, 0, 0,
            null, 0, 0, StatusEnum.THORN_ARMOR, 0.2, 3);

        // Fire
        attackDict[AttackEnum.TWIN_FLAME] = new AttackInfo(
            AttackEnum.TWIN_FLAME, "Twin Flame", "Icons/RoleDamage", "AttackSounds/Scorch",
            AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.LOWEST_HEALTH, 2, TargetType.NONE,
            0, 1.5, -100, 10, 0, 0,
            StatusEnum.BURN, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.TURN_UP_THE_HEAT] = new AttackInfo(
            AttackEnum.TURN_UP_THE_HEAT, "Turn Up The Heat", "Icons/RoleSupport", "AttackSounds/BasicMagic",
            AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD, AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD,
            false, false, false,
            TargetType.RANDOM, 10, TargetType.RANDOM, 10,
            0, 0, -100, 0, 0,
            StatusEnum.BURN, 0.2, 2, StatusEnum.SPEED_UP, 0.2, 2);
        attackDict[AttackEnum.IMMOLATE] = new AttackInfo(
            AttackEnum.IMMOLATE, "Immolate", "Icons/RoleDamage", "AttackSounds/Scorch",
            AttackParticleEnum.FIRE, ParticleOriginEnum.TARGET, null, null,
            false, true, false,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            1, 0, -100, 10, 0,
            StatusEnum.BURN, 1, 3, null, 0, 0);
        attackDict[AttackEnum.GIFT_OF_LAVA] = new AttackInfo(
            AttackEnum.GIFT_OF_LAVA, "Gift of Lava", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            null, null, AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD,
            false, false, false,
            TargetType.NONE, 0, TargetType.RANDOM, 10,
            0, 0, -100, 0, 0,
            null, 0, 0, StatusEnum.LAVA_ARMOR, 0.3, 3);
        attackDict[AttackEnum.FIRE_STORM] = new AttackInfo(
            AttackEnum.FIRE_STORM, "Fire Storm", "Icons/RoleDamage", "AttackSounds/Scorch",
            AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD, null, null,
            false, true, false,
            TargetType.RANDOM, 10, TargetType.NONE, 0,
            0.25, 0, -100, 10, 0,
            StatusEnum.BURN, 0.25, 3, null, 0, 0);

        // Ice
        attackDict[AttackEnum.CHILLY_WIND] = new AttackInfo(
            AttackEnum.CHILLY_WIND, "Chilly Wind", "Icons/RoleDamage", "AttackSounds/SnowyWind2",
            AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.RANDOM, 3, TargetType.NONE, 0,
            0, 0, -100, 10, 0,
            StatusEnum.CHILL, 0.4, 2, null, 0, 0);
        attackDict[AttackEnum.ENCASE_IN_ICE] = new AttackInfo(
            AttackEnum.ENCASE_IN_ICE, "Encase in Ice", "Icons/RoleDamage", "AttackSounds/FrozenMirror",
            null, null, AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER,
            false, false, true,
            TargetType.NONE, 0, TargetType.NONE, 0,
            0, 0, -100, 0, 0,
            null, 0, 0, StatusEnum.ICE_ARMOR, 1, 3);
        attackDict[AttackEnum.FLINGING_SPREE] = new AttackInfo(
            AttackEnum.FLINGING_SPREE, "Flinging Spree", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER, null, null,
            false, true, true,
            TargetType.LOWEST_HEALTH, 2, TargetType.NONE, 0,
            2, 0, -100, 10, 0,
            StatusEnum.CHILL, 0.2, 2, null, 0, 0);
        attackDict[AttackEnum.BLIZZARD] = new AttackInfo(
            AttackEnum.BLIZZARD, "Blizzard", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            AttackParticleEnum.ICE, ParticleOriginEnum.OVERHEAD, null, null,
            false, true, false,
            TargetType.RANDOM, 5, TargetType.NONE, 0,
            0.6, 0, -100, 10, 0,
            StatusEnum.CHILL, 0.3, 2, null, 0, 0);
        attackDict[AttackEnum.GIFT_OF_ICE] = new AttackInfo(
            AttackEnum.GIFT_OF_ICE, "Gift of Ice", "Icons/RoleDamage", "AttackSounds/FrozenMirror",
            null, null, AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER,
            false, false, false,
            TargetType.RANDOM, 0, TargetType.LOWEST_HEALTH, 10,
            0, 0, -100, 0, 0,
            null, 0, 0, StatusEnum.ICE_ARMOR, 0.5, 2);

        // Earth
        attackDict[AttackEnum.HEAD_CRACK] = new AttackInfo(
            AttackEnum.HEAD_CRACK, "Head Crack", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
            3, 0, -100, 10, 0,
            StatusEnum.DAZE, 1, 2, null, 0, 0);
        attackDict[AttackEnum.ENCASE_IN_ROCK] = new AttackInfo(
            AttackEnum.ENCASE_IN_ROCK, "Encase in Rock", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            null, null, AttackParticleEnum.EARTH, ParticleOriginEnum.ATTACKER,
            false, false, true,
            TargetType.NONE, 0, TargetType.SELF, 1,
            0, 0, -100, 0, 0,
            null, 0, 0, StatusEnum.EARTH_ARMOR, 1.5, 3);
        attackDict[AttackEnum.PEBBLE_SHOWER] = new AttackInfo(
            AttackEnum.PEBBLE_SHOWER, "Pebble Shower", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            AttackParticleEnum.EARTH, ParticleOriginEnum.OVERHEAD, null, null,
            false, true, true,
            TargetType.RANDOM, 5, TargetType.NONE, 0,
            0.5, 0, -100, 10, 0,
            StatusEnum.BLIND, 0, 1, null, 0, 0);
        attackDict[AttackEnum.GIFT_OF_EARTH] = new AttackInfo(
            AttackEnum.GIFT_OF_EARTH, "Gift of Earth", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            null, null, AttackParticleEnum.EARTH, ParticleOriginEnum.ATTACKER,
            false, false, true,
            TargetType.NONE, 0, TargetType.RANDOM, 5,
            0, 0, -100, 0, 0,
            null, 0, 0, StatusEnum.EARTH_ARMOR, 0.5, 3);
        attackDict[AttackEnum.SPLIT_SKULL] = new AttackInfo(
            AttackEnum.SPLIT_SKULL, "Split Skull", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
            null, null, null, null,
            true, false, true,
            TargetType.LOWEST_HEALTH, 1, TargetType.SELF, 1,
            3, 0, -100, 10, 0,
            StatusEnum.BLEED, 0.5, 2, StatusEnum.ATTACK_UP, 0.4, StatusContainer.INDEFINITE);

        // Electric
        attackDict[AttackEnum.FLASH_OF_LIGHT] = new AttackInfo(
            AttackEnum.FLASH_OF_LIGHT, "Flash of Light", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.RANDOM, 3, TargetType.NONE, 0,
            1, 0, -100, 10, 0,
            StatusEnum.BLIND, 0, 1, null, 0, 0);
        attackDict[AttackEnum.CHARGE_TEAM] = new AttackInfo(
            AttackEnum.CHARGE_TEAM, "Charge Team", "Icons/RoleSupport", "AttackSounds/BasicMagic",
            null, null, AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER,
            false, false, false,
            TargetType.NONE, 0, TargetType.LOWEST_ENERGY, 3,
            0, 0, -100, 0, 40,
            null, 0, 0, StatusEnum.MAGIC_UP, 0.2, 3);
        attackDict[AttackEnum.OVERCHARGED_BOLT] = new AttackInfo(
            AttackEnum.OVERCHARGED_BOLT, "Overcharged Bolt", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
            4, 0, -100, 10, 0,
            null, 0, 0, null, 0, 0);
        attackDict[AttackEnum.LIGHTNING_FLASH] = new AttackInfo(
            AttackEnum.LIGHTNING_FLASH, "Lightning Flash", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            AttackParticleEnum.ELECTRIC, ParticleOriginEnum.OVERHEAD, null, null,
            false, true, false,
            TargetType.RANDOM, 3, TargetType.NONE, 0,
            1.2, 0, -100, 10, 0,
            StatusEnum.BLIND, 0, 1, null, 0, 0);
        attackDict[AttackEnum.BRAIN_STORM] = new AttackInfo(
            AttackEnum.BRAIN_STORM, "Brain Storm", "Icons/RoleDamage", "AttackSounds/BasicMagic",
            AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, null, null,
            false, true, false,
            TargetType.RANDOM, 5, TargetType.NONE, 0,
            0.6, 0, -100, 10, 0,
            StatusEnum.DAZE, 1, 2, null, 0, 0);
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
    public AttackParticleEnum? EnemyParticle { get; }
    public ParticleOriginEnum? EnemyParticleOrigin { get; }
    public AttackParticleEnum? AllyParticle { get; }
    public ParticleOriginEnum? AllyParticleOrigin { get; }
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
        AttackParticleEnum? enemyParticle, ParticleOriginEnum? enemyParticleOrigin, AttackParticleEnum? allyParticle, ParticleOriginEnum? allyParticleOrigin,
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

        EnemyParticle = enemyParticle;
        EnemyParticleOrigin = enemyParticleOrigin;
        AllyParticle = allyParticle;
        AllyParticleOrigin = allyParticleOrigin;

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

    public List<DamageInstance> ApplyAttackToEnemy(CombatHero attacker, CombatHero target) {
        var hitType = CombatMath.RollHitType(attacker, target);
        var hitEffectivity = CombatMath.GetEffectivity(attacker, target);
        var attackValue = IsPhysical ? attacker.GetModifiedAttack() : attacker.GetModifiedMagic();
        var defenseValue = IsPhysical ? attacker.GetModifiedDefense() : attacker.GetModifiedReflection();
        var damage = CombatMath.Damage(attackValue * DamageMultiplier, defenseValue, hitType, hitEffectivity);
        target.currentHealth -= damage;
        target.currentEnergy += TargetEnergyGained;

        var damageInstance = new DamageInstance(Attack, null, attacker.combatHeroGuid, target.combatHeroGuid);
        damageInstance.damage = damage;
        damageInstance.targetEnergy = TargetEnergyGained;
        damageInstance.hitType = hitType;
        var allInstances = new List<DamageInstance>();
        allInstances.Add(damageInstance);

        // If the target died from this attack, bail before applying status.
        if (!target.IsAlive()) {
            target.currentHealth = 0;
            target.currentEnergy = 0;
            target.currentStatus.Clear();

            damageInstance.wasFatal = true;
            return allInstances;
        }

        if (TargetStatus != null) {
            switch (target.baseHero.PassiveAbility) {
                case AbilityEnum.WATER_BODY:
                    if (TargetStatus == StatusEnum.BURN) {
                        return allInstances;
                    }
                    break;
                case AbilityEnum.HOT_BLOODED:
                    if (TargetStatus == StatusEnum.POISON) {
                        return allInstances;
                    }
                    break;
                case AbilityEnum.COLD_BLOODED:
                    if (TargetStatus == StatusEnum.CHILL || TargetStatus == StatusEnum.FREEZE) {
                        return allInstances;
                    }
                    break;
            }

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

            if (target.baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                if (inflictedStatus == StatusEnum.MAGIC_DOWN) {
                    inflictedStatus = StatusEnum.MAGIC_UP;
                } else if (inflictedStatus == StatusEnum.SPEED_DOWN) {
                    inflictedStatus = StatusEnum.SPEED_UP;
                }
            }

            // This is going to break everything.
            if (target.baseHero.PassiveAbility == AbilityEnum.MIRROR_ICE && !IsPhysical) {
                var statusContainer = new StatusContainer(inflictedStatus, target.combatHeroGuid, statusValue, statusDuration);
                attacker.AddStatus(statusContainer);
                var mirrorInstance = new DamageInstance(null, null, target.combatHeroGuid, attacker.combatHeroGuid);
                mirrorInstance.AddStatus(statusContainer);
                allInstances.Add(mirrorInstance);
            } else {
                var statusContainer = new StatusContainer(inflictedStatus, attacker.combatHeroGuid, statusValue, statusDuration);
                target.AddStatus(statusContainer);
                damageInstance.AddStatus(statusContainer);
            }
        }

        return allInstances;
    }

    public DamageInstance ApplyAttackToAlly(CombatHero attacker, CombatHero ally) {
        var attackValue = IsPhysical ? attacker.GetModifiedAttack() : attacker.GetModifiedMagic();
        var healing = attackValue * HealingMultiplier;
        healing = ally.ReceiveHealing(healing);
        ally.currentEnergy += AllyEnergyGained;

        var damageInstance = new DamageInstance(Attack, null, attacker.combatHeroGuid, ally.combatHeroGuid);
        damageInstance.healing = healing;
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
                case StatusEnum.LAVA_ARMOR:
                    statusValue *= attacker.GetModifiedMagic();
                    break;
                case StatusEnum.THORN_ARMOR:
                    statusValue *= attacker.GetModifiedAttack();
                    break;
                default:
                    break;
            }
            var statusContainer = new StatusContainer(bestowedStatus, attacker.combatHeroGuid, statusValue, statusDuration);
            ally.AddStatus(statusContainer);
            damageInstance.AddStatus(statusContainer);
        }

        if (attacker.baseHero.PassiveAbility == AbilityEnum.CLEANSING_RAIN) {
            var newStatus = new List<StatusContainer>();
            foreach (StatusContainer status in ally.currentStatus) {
                var statusDisplay = StatusDisplayContainer.GetStatusDisplay(status.status);
                if (status.turnsRemaining == StatusContainer.INDEFINITE) {
                    newStatus.Add(status);
                } else if (statusDisplay.IsBeneficial) {
                    newStatus.Add(status);
                } else {
                    status.turnsRemaining -= 1;
                    if (status.turnsRemaining > 0) {
                        newStatus.Add(status);
                    }
                }
            }
            ally.currentStatus = newStatus;
        }

        return damageInstance;
    }

    public string GetTooltip() {
        string output = "";

        if (EnemyTargetType != TargetType.NONE) {
            string damageMultiplier = (DamageMultiplier * 100).ToString("0");
            string damageType = IsPhysical ? "attack" : "magic";
            string targetting = GetTargettingTooltip(EnemyTargetType, EnemyTargetCount, false);
            string firstPart = DamageMultiplier == 0 ? "Targets" : string.Format("Damages for {0} of {1} to", damageMultiplier, damageType);
            output = string.Format("{0} {1}.{2}", firstPart, targetting,
                GetStatusTooltip(TargetStatus.GetValueOrDefault(), TargetStatusDuration, TargetStatusValue, false));
        }

        if (AllyTargetType != TargetType.NONE) {
            string healMultiplier = (HealingMultiplier * 100).ToString("0");
            string damageType = IsPhysical ? "attack" : "magic";
            string targetting = GetTargettingTooltip(AllyTargetType, AllyTargetCount, true);
            string firstPart = HealingMultiplier == 0 ? "Targets" : string.Format("Heals for {0} of {1} to", healMultiplier, damageType);
            string allyTip = string.Format("{0} {1}.{2}", firstPart, targetting,
                GetStatusTooltip(AllyStatus.GetValueOrDefault(), AllyStatusDuration, AllyStatusValue, true));

            if (output.Length == 0) output = allyTip;
            else output += "  " + allyTip;
        }

        return output;
    }

    private string GetTargettingTooltip(TargetType targetType, int targetCount, bool ally) {
        string singular = ally ? "ally" : "enemy";
        string plural = ally ? "allies" : "enemies";
        string targetting;
        switch (targetType) {
            case TargetType.FIRST_ALIVE:
                string targettingSuffix = targetCount != 1 ? string.Format("{0} {1}", targetCount, plural) : singular;
                targetting = string.Format("the first {0}", targettingSuffix);
                break;
            case TargetType.LOWEST_HEALTH:
                string count = targetCount != 1 ? string.Format("{0} {1}", targetCount, plural) : singular;
                targetting = string.Format("the {0} with the lowest health", count);
                break;
            case TargetType.HIGHEST_HEALTH:
                count = targetCount != 1 ? string.Format("{0} {1}", targetCount, plural) : singular;
                targetting = string.Format("the {0} with the highest health", count);
                break;
            case TargetType.LOWEST_ENERGY:
                count = targetCount != 1 ? string.Format("{0} {1}", targetCount, plural) : singular;
                targetting = string.Format("the {0} with the lowest energy", count);
                break;
            case TargetType.HIGHEST_ENERGY:
                count = targetCount != 1 ? string.Format("{0} {1}", targetCount, plural) : singular;
                targetting = string.Format("the {0} with the highest energy", count);
                break;
            case TargetType.SELF:
                return "self";
            case TargetType.RANDOM:
            default:
                count = targetCount != 1 ? plural : singular;
                targetting = string.Format("{0} {1} at random", targetCount, count);
                break;
        }
        return targetting;
    }

    private string GetStatusTooltip(StatusEnum statusType, int statusDuration, double value, bool ally) {
        string turnPlural = statusDuration != 1 ? "turns" : "turn";
        string statusValue = (value * 100).ToString("0");
        switch (statusType) {
            case StatusEnum.BURN:
            case StatusEnum.BLEED:
            case StatusEnum.POISON:
                return GetEnemyDamageStatusTooltip(statusType, statusDuration, value);

            case StatusEnum.ATTACK_UP:
            case StatusEnum.ATTACK_DOWN:
            case StatusEnum.MAGIC_UP:
            case StatusEnum.MAGIC_DOWN:
            case StatusEnum.DEFENSE_UP:
            case StatusEnum.DEFENSE_DOWN:
            case StatusEnum.REFLECTION_UP:
            case StatusEnum.REFLECTION_DOWN:
            case StatusEnum.SPEED_UP:
            case StatusEnum.SPEED_DOWN:
                return GetStatModStatusTooltip(statusType, statusDuration, value, ally);

            case StatusEnum.CHILL:
                return string.Format("Chills for {0} {1} reducing speed by {2}%.  If the target is already chilled, freeze the target for 1 turn instead preventing all attacks.",
                    statusDuration, turnPlural, statusValue);
            case StatusEnum.DAZE:
                return string.Format("Dazes for {0} {1} reducing critical and deflection chances by {2}%.  If the target is already dazed, stun the target for 1 turn instead preventing all attacks.",
                    statusDuration, turnPlural, statusValue);
            case StatusEnum.FREEZE:
                return string.Format("Freezes for {0} {1} preventing all attacks.", statusDuration, turnPlural);
            case StatusEnum.STUN:
                return string.Format("Stuns for {0} {1} preventing all attacks.", statusDuration, turnPlural);
            case StatusEnum.BLIND:
                return string.Format("Stuns for {0} {1} preventing ranged attacks.", statusDuration, turnPlural);
            case StatusEnum.ROOT:
                return string.Format("Entangles for {0} {1} preventing melee attacks.", statusDuration, turnPlural);
            case StatusEnum.DOWSE:
                return string.Format("Dowses for {0} {1}.  Dowsed targets are stunned when dazed and frozen when chilled.", statusDuration, turnPlural);

            case StatusEnum.REGENERATION:
                return string.Format("Bestows Regeneration for {0} {1}, healing for {2} of magic each turn.",
                    statusDuration, turnPlural, statusValue);
            case StatusEnum.THORN_ARMOR:
                return string.Format("Bestows Thorn Armor for {0} {1}.  Whenever a hero with thorn armor is attacked, the attacker takes damage equal to {2} of attack.",
                    statusDuration, turnPlural, statusValue);
            case StatusEnum.LAVA_ARMOR:
                return string.Format("Bestows Lava Armor for {0} {1}.  Whenever a hero with lava armor is attacked, the attacker is burned for 2 turns, taking {2} of magic each turn.",
                    statusDuration, turnPlural, statusValue);
            case StatusEnum.ICE_ARMOR:
                return string.Format("Bestows Ice Armor for {0} {1}.  Whenever a hero with ice armor is attacked, the attacker is chilled for 2 turns, reducing speed by {2} (or frozen if already chilled or dowsed).",
                    statusDuration, turnPlural, statusValue);
            case StatusEnum.EARTH_ARMOR:
                return string.Format("Bestows Earth Armor for {0} {1}, raising defense by {2}% and reflection by {3}%.", statusDuration, turnPlural, statusValue, (value * 100 / 2).ToString("0"));
            default:
                return "";
        }
    }

    private string GetEnemyDamageStatusTooltip(StatusEnum statusType, int duration, double value) {
        string statusName = StatusDisplayContainer.GetStatusDisplay(statusType).StatusName;
        string turnPlural = duration != 1 ? "turns" : "turn";
        string damageType = statusType == StatusEnum.BLEED ? "attack" : "magic";
        string damageAmount = (value * 100).ToString("0");
        return string.Format(" Inflicts the {0} status for {1} {2}, dealing {3} of {4} per turn.", statusName, duration, turnPlural, damageAmount, damageType);
    }

    private string GetStatModStatusTooltip(StatusEnum statusType, int duration, double value, bool ally) {
        string inflictWord = ally ? "Bestows" : "Inflicts";
        string statusAmount = (value * 100).ToString("0");
        string statusName = StatusDisplayContainer.GetStatusDisplay(statusType).StatusName;
        string turnPlural = duration != 1 ? "turns" : "turn";
        return string.Format(" {0} {1} {2} for {3} {4}.", inflictWord, statusAmount, statusName, duration, turnPlural);
    }
}
