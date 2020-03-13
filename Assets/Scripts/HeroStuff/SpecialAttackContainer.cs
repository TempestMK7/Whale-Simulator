using System;
using System.Collections.Generic;

public class SpecialAttackContainer {

    public static CombatStep PerformSpecialAttack(CombatHero attacker, List<CombatHero> allies, List<CombatHero> enemies) {
        var step = new CombatStep(attacker, allies, enemies, true);
        if (!CanAttack(attacker)) return step;

        switch (attacker.baseHero.SpecialAttack) {
            case SpecialAttackEnum.BASIC_PHYSICAL:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedAttack() * 3.0, target.GetModifiedDefense(), hitType);
                    target.currentHealth -= damage;
                    step.totalDamage += damage;

                    var damageInstance = new DamageInstance(null, attacker.baseHero.SpecialAttack, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.hitType = hitType;
                    step.damageInstances.Add(damageInstance);
                }
                attacker.currentEnergy -= 100;
                break;
            case SpecialAttackEnum.BASIC_MAGIC:
                foreach (CombatHero target in enemies) {
                    var hitType = CombatMath.RollHitType(attacker, target);
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic() * 3.0, target.GetModifiedReflection(), hitType);
                    target.currentHealth -= damage;
                    step.totalDamage += damage;

                    var damageInstance = new DamageInstance(null, attacker.baseHero.SpecialAttack, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.hitType = hitType;
                    step.damageInstances.Add(damageInstance);
                }
                attacker.currentEnergy -= 100;
                break;
            case SpecialAttackEnum.FROZEN_MIRROR:
                foreach (CombatHero ally in allies) {
                    var iceArmor = new StatusContainer(StatusEnum.ICE_ARMOR, attacker.combatHeroGuid, 0.5, 2);
                    ally.currentStatus.Add(iceArmor);

                    var damageInstance = new DamageInstance(null, SpecialAttackEnum.FROZEN_MIRROR, null, attacker.combatHeroGuid, ally.combatHeroGuid);
                    damageInstance.AddStatus(iceArmor);
                    step.damageInstances.Add(damageInstance);
                }
                attacker.currentEnergy -= 100;
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
        switch (attacker.baseHero.SpecialAttack) {
            case SpecialAttackEnum.FROZEN_MIRROR:
                break;
            case SpecialAttackEnum.BASIC_PHYSICAL:
            case SpecialAttackEnum.BASIC_MAGIC:
            default:
                targets.Add(CombatMath.FirstAlive(enemies));
                break;
        }
        return targets;
    }

    public static List<CombatHero> DecideAllies(CombatHero attacker, CombatHero[] allies) {
        var targets = new List<CombatHero>();
        switch (attacker.baseHero.SpecialAttack) {
            case SpecialAttackEnum.FROZEN_MIRROR:
                targets.AddRange(CombatMath.AllLiving(allies));
                break;
            default:
                break;
        }
        return targets;
    }

    public static bool CanAttack(CombatHero hero) {
        bool isMelee;
        bool isRanged;
        switch (hero.baseHero.SpecialAttack) {
            case SpecialAttackEnum.BASIC_PHYSICAL:
            case SpecialAttackEnum.BASIC_MAGIC:
                isMelee = true;
                isRanged = false;
                break;
            case SpecialAttackEnum.FROZEN_MIRROR:
                isMelee = false;
                isRanged = false;
                break;
            default:
                isMelee = true;
                isRanged = false;
                break;
        }

        if (isMelee) {
            foreach (StatusContainer status in hero.currentStatus) {
                if (StatusContainer.BlocksMelee(status.status)) return false;
            }
        }

        if (isRanged) {
            foreach (StatusContainer status in hero.currentStatus) {
                if (StatusContainer.BlocksRanged(status.status)) return false;
            }
        }

        return true;
    }
}

public enum SpecialAttackEnum {
    BASIC_PHYSICAL = 1,
    BASIC_MAGIC = 2,
    FROZEN_MIRROR = 3
}
