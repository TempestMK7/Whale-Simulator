using System;
using System.Collections.Generic;

public class AttackContainer {
    
    public static CombatStep PerformAttack(CombatHero attacker, List<CombatHero> allies, List<CombatHero> enemies) {
        var step = new CombatStep(attacker, allies, enemies, false);
        if (!CanAttack(attacker)) return step;

        switch (attacker.baseHero.BasicAttack) {
            case AttackEnum.BASIC_PHYSICAL:
            case AttackEnum.PETAL_SLAP:
            case AttackEnum.ICICLE_THROW:
            case AttackEnum.ICE_PUNCH:
            case AttackEnum.ROCK_SLAM:
            case AttackEnum.PEBBLE_TOSS:
            case AttackEnum.AXE_SLASH:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedAttack(), target.GetModifiedDefense(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;
                    step.energyGained += 25;
                    target.currentEnergy += energy;

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.BASIC_MAGIC:
            case AttackEnum.LIGHTNING_BOLT:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic(), target.GetModifiedReflection(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;
                    step.energyGained += 25;
                    target.currentEnergy += energy;

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.VAPOR_CLOUD:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic() * 0.5, target.GetModifiedReflection(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 15;
                    step.energyGained += 15;
                    target.currentEnergy += energy;

                    var dowseStatus = new StatusContainer(StatusEnum.DOWSE, attacker.combatHeroGuid, 0.0, 3);
                    target.AddStatus(dowseStatus);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    damageInstance.AddStatus(dowseStatus);
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.FISH_SLAP:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedAttack(), target.GetModifiedDefense(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;
                    step.energyGained += 25;
                    target.currentEnergy += energy;

                    var defenseDownStatus = new StatusContainer(StatusEnum.DEFENSE_DOWN, attacker.combatHeroGuid, 0.2, 2);
                    target.AddStatus(defenseDownStatus);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    damageInstance.AddStatus(defenseDownStatus);
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.WATER_RENEW:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic() * 0.75, target.GetModifiedReflection(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 15;
                    step.energyGained += 15;
                    target.currentEnergy += energy;

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    step.damageInstances.Add(damageInstance);
                }
                foreach (CombatHero ally in allies) {
                    var healing = attacker.GetModifiedMagic() * 0.25;
                    var actualHealing = ally.ReceiveHealing(healing);
                    step.totalHealing += actualHealing;
                    attacker.currentEnergy += 15;

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, ally.combatHeroGuid);
                    damageInstance.healing = actualHealing;
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.NEEDLE_STAB:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedAttack(), target.GetModifiedDefense(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;
                    step.energyGained += 25;
                    target.currentEnergy += energy;

                    var poisonStatus = new StatusContainer(StatusEnum.POISON, attacker.combatHeroGuid, attacker.GetModifiedAttack() * 0.2, 2);
                    target.AddStatus(poisonStatus);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    damageInstance.AddStatus(poisonStatus);
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.SPEAR_THROW:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedAttack() * 0.5, target.GetModifiedDefense(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;
                    step.energyGained += 25;
                    target.currentEnergy += energy;

                    var poisonStatus = new StatusContainer(StatusEnum.POISON, attacker.combatHeroGuid, attacker.GetModifiedAttack() * 0.1, 2);
                    target.AddStatus(poisonStatus);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    damageInstance.AddStatus(poisonStatus);
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.BRANCH_SLAM:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedAttack(), target.GetModifiedDefense(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;
                    step.energyGained += 25;
                    target.currentEnergy += energy;

                    StatusContainer status;
                    // Note that branch slam only checks for daze and not dowse when applying stun.
                    if (target.HasStatus(StatusEnum.DAZE)) {
                        status = new StatusContainer(StatusEnum.STUN, attacker.combatHeroGuid, 0.0, 1);
                    } else {
                        status = new StatusContainer(StatusEnum.DAZE, attacker.combatHeroGuid, 0.2, 2);
                    }
                    target.AddStatus(status);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    damageInstance.AddStatus(status);
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.SCORCH:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic() * 0.4, target.GetModifiedReflection(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 15;
                    step.energyGained += 15;
                    target.currentEnergy += energy;

                    var burnStatus = new StatusContainer(StatusEnum.BURN, attacker.combatHeroGuid, attacker.GetModifiedMagic() * 0.2f, 3);
                    target.AddStatus(burnStatus);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    damageInstance.AddStatus(burnStatus);
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.FIRE_PUNCH:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedAttack(), target.GetModifiedDefense(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;
                    step.energyGained += 25;
                    target.currentEnergy += energy;

                    var burnStatus = new StatusContainer(StatusEnum.BURN, attacker.combatHeroGuid, attacker.GetModifiedMagic() * 0.25f, 5);
                    target.AddStatus(burnStatus);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    damageInstance.AddStatus(burnStatus);
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.SNOWY_WIND:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic() * 0.4, target.GetModifiedReflection(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 15;
                    step.energyGained += 15;
                    target.currentEnergy += energy;

                    StatusContainer status;
                    if (target.HasStatus(StatusEnum.CHILL) || target.HasStatus(StatusEnum.DOWSE)) {
                        status = new StatusContainer(StatusEnum.FREEZE, attacker.combatHeroGuid, 0.0, 1);
                    } else {
                        status = new StatusContainer(StatusEnum.CHILL, attacker.combatHeroGuid, 0.2, 2);
                    }
                    target.AddStatus(status);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    damageInstance.AddStatus(status);
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.SPARK:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic(), target.GetModifiedReflection(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;
                    step.energyGained += 25;
                    target.currentEnergy += energy;

                    StatusContainer status;
                    if (target.HasStatus(StatusEnum.DAZE) || target.HasStatus(StatusEnum.DOWSE)) {
                        status = new StatusContainer(StatusEnum.STUN, attacker.combatHeroGuid, 0.0, 1);
                    } else {
                        status = new StatusContainer(StatusEnum.DAZE, attacker.combatHeroGuid, 0.2, 2);
                    }
                    target.AddStatus(status);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    damageInstance.AddStatus(status);
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.ENERGY_DRAIN:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic(), target.GetModifiedReflection(), hitType);
                    var energy = -5;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 35;
                    step.energyGained += 35;
                    target.currentEnergy += energy;

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.FORKED_LIGHTNING:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic() * 0.4, target.GetModifiedReflection(), hitType);
                    var energy = 10;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;
                    step.energyGained += 25;
                    target.currentEnergy += energy;

                    StatusContainer status;
                    if (target.HasStatus(StatusEnum.DAZE) || target.HasStatus(StatusEnum.DOWSE)) {
                        status = new StatusContainer(StatusEnum.STUN, attacker.combatHeroGuid, 0.0, 1);
                    } else {
                        status = new StatusContainer(StatusEnum.DAZE, attacker.combatHeroGuid, 0.2, 2);
                    }
                    target.AddStatus(status);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.energy = energy;
                    damageInstance.hitType = hitType;
                    damageInstance.AddStatus(status);
                    step.damageInstances.Add(damageInstance);
                }
                break;
        }

        foreach (CombatHero target in enemies) {
            if (!target.IsAlive()) {
                target.currentHealth = 0;
                target.currentEnergy = 0;
                target.currentStatus.Clear();

                var damageInstance = new DamageInstance(null, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                damageInstance.wasFatal = true;
                step.damageInstances.Add(damageInstance);
            }
        }

        step.damageInstances.AddRange(CombatMath.EvaluateNegativeSideEffects(attacker, enemies));
        return step;
    }

    public static List<CombatHero> DecideTargets(CombatHero attacker, CombatHero[] enemies) {
        var targets = new List<CombatHero>();
        switch (attacker.baseHero.BasicAttack) {
            case AttackEnum.VAPOR_CLOUD:
            case AttackEnum.SPEAR_THROW:
            case AttackEnum.SCORCH:
            case AttackEnum.SNOWY_WIND:
            case AttackEnum.FORKED_LIGHTNING:
                targets.AddRange(CombatMath.SelectAtRandom(enemies, 2));
                break;
            case AttackEnum.WATER_RENEW:
                targets.AddRange(CombatMath.SelectAtRandom(enemies, 1));
                break;
            case AttackEnum.NEEDLE_STAB:
            case AttackEnum.ICICLE_THROW:
                targets.Add(CombatMath.LowestHealth(enemies));
                break;
            case AttackEnum.ENERGY_DRAIN:
                targets.Add(CombatMath.HighestEnergy(enemies));
                break;
            case AttackEnum.BASIC_PHYSICAL:
            case AttackEnum.BASIC_MAGIC:
            case AttackEnum.FISH_SLAP:
            case AttackEnum.PETAL_SLAP:
            case AttackEnum.BRANCH_SLAM:
            case AttackEnum.FIRE_PUNCH:
            case AttackEnum.ICE_PUNCH:
            case AttackEnum.SPARK:
            case AttackEnum.LIGHTNING_BOLT:
            case AttackEnum.ROCK_SLAM:
            case AttackEnum.PEBBLE_TOSS:
            case AttackEnum.AXE_SLASH:
            default:
                targets.Add(CombatMath.FirstAlive(enemies));
                break;
        }
        return targets;
    }

    public static List<CombatHero> DecideAllies(CombatHero attacker, CombatHero[] allies) {
        var targets = new List<CombatHero>();
        switch (attacker.baseHero.BasicAttack) {
            case AttackEnum.WATER_RENEW:
                targets.Add(CombatMath.LowestHealth(allies));
                break;
            default:
                break;
        }
        return targets;
    }

    public static bool CanAttack(CombatHero hero) {
        bool isMelee;
        bool isRanged;
        switch (hero.baseHero.BasicAttack) {
            case AttackEnum.BASIC_PHYSICAL:
            case AttackEnum.FISH_SLAP:
            case AttackEnum.FIRE_PUNCH:
            case AttackEnum.PETAL_SLAP:
            case AttackEnum.NEEDLE_STAB:
            case AttackEnum.BRANCH_SLAM:
            case AttackEnum.ICE_PUNCH:
            case AttackEnum.SPARK:
            case AttackEnum.ENERGY_DRAIN:
            case AttackEnum.ROCK_SLAM:
            case AttackEnum.AXE_SLASH:
                isMelee = true;
                isRanged = false;
                break;
            case AttackEnum.BASIC_MAGIC:
            case AttackEnum.VAPOR_CLOUD:
            case AttackEnum.WATER_RENEW:
            case AttackEnum.SCORCH:
            case AttackEnum.SPEAR_THROW:
            case AttackEnum.ICICLE_THROW:
            case AttackEnum.SNOWY_WIND:
            case AttackEnum.LIGHTNING_BOLT:
            case AttackEnum.PEBBLE_TOSS:
                isMelee = false;
                isRanged = true;
                break;
            default:
                isMelee = true;
                isRanged = false;
                break;
        }

        if (isMelee) {
            foreach (StatusContainer status in hero.currentStatus) {
                var display = StatusDisplayContainer.GetStatusDisplay(status.status);
                if (display.BlocksMelee) return false;
            }
        } 

        if (isRanged) {
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
    AXE_SLASH = 21
}
