﻿using System;
using System.Collections.Generic;

public class AttackContainer {
    
    public static CombatStep PerformAttack(CombatHero attacker, List<CombatHero> targets) {
        var step = new CombatStep(attacker, targets, false);
        if (!CanAttack(attacker)) return step;

        switch (attacker.baseHero.BasicAttack) {
            case AttackEnum.BASIC_PHYSICAL:
                foreach (CombatHero target in targets) {
                    var damage = CombatMath.Damage(attacker.currentAttack, target.currentDefense);
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker, target);
                    damageInstance.damage = damage;
                    damageInstance.wasCritical = false;
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.BASIC_MAGIC:
                foreach (CombatHero target in targets) {
                    var damage = CombatMath.Damage(attacker.currentMagic, target.currentReflection);
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker, target);
                    damageInstance.damage = damage;
                    damageInstance.wasCritical = false;
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.MAGIC_BURN:
                foreach (CombatHero target in targets) {
                    var damage = CombatMath.Damage(attacker.currentMagic, target.currentReflection);
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;

                    var burnStatus = new StatusContainer(StatusEnum.BURN, attacker.currentMagic * 0.25f, 5);
                    target.AddStatus(burnStatus);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker, target);
                    damageInstance.damage = damage;
                    damageInstance.wasCritical = false;
                    damageInstance.inflictedStatus.Add(burnStatus);
                    step.damageInstances.Add(damageInstance);
                }
                break;
        }
        return step;
    }

    public static List<CombatHero> DecideTargets(CombatHero attacker, CombatHero[] allies, CombatHero[] enemies) {
        var targets = new List<CombatHero>();
        switch (attacker.baseHero.BasicAttack) {
            case AttackEnum.BASIC_PHYSICAL:
            case AttackEnum.BASIC_MAGIC:
            case AttackEnum.MAGIC_BURN:
            default:
                targets.Add(CombatMath.FirstAlive(enemies));
                break;
        }
        return targets;
    }

    public static bool CanAttack(CombatHero hero) {
        bool isMelee;
        switch (hero.baseHero.BasicAttack) {
            case AttackEnum.BASIC_PHYSICAL:
            case AttackEnum.BASIC_MAGIC:
                isMelee = true;
                break;
            case AttackEnum.MAGIC_BURN:
                isMelee = false;
                break;
            default:
                isMelee = true;
                break;
        }

        if (isMelee) {
            foreach (StatusContainer status in hero.currentStatus) {
                if (StatusContainer.BlocksMelee(status.status)) return false;
            }
            return true;
        } else {
            foreach (StatusContainer status in hero.currentStatus) {
                if (StatusContainer.BlocksRanged(status.status)) return false;
            }
            return true;
        }
    }
}

public enum AttackEnum {
    BASIC_PHYSICAL = 1,
    BASIC_MAGIC = 2,
    MAGIC_BURN
}
