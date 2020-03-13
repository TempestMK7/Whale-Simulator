using System;
using System.Collections.Generic;

public class SpecialAttackContainer {

    public static CombatStep PerformSpecialAttack(CombatHero attacker, List<CombatHero> allies, List<CombatHero> enemies) {
        var step = new CombatStep(attacker, allies, enemies, true);
        if (!CanAttack(attacker)) return step;

        switch (attacker.baseHero.SpecialAttack) {
            case SpecialAttackEnum.BASIC_PHYSICAL:
                foreach (CombatHero target in enemies) {
                    var damage = CombatMath.Damage(attacker.GetModifiedAttack() * 3.0, target.GetModifiedDefense());
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy -= 100;

                    var damageInstance = new DamageInstance(null, attacker.baseHero.SpecialAttack, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.wasCritical = false;
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case SpecialAttackEnum.BASIC_MAGIC:
                foreach (CombatHero target in enemies) {
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic() * 3.0, target.GetModifiedReflection());
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy -= 100;

                    var damageInstance = new DamageInstance(null, attacker.baseHero.SpecialAttack, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.wasCritical = false;
                    step.damageInstances.Add(damageInstance);
                }
                break;
        }

        step.damageInstances.AddRange(CombatMath.EvaluateNegativeSideEffects(attacker, enemies));
        return step;
    }

    public static List<CombatHero> DecideTargets(CombatHero attacker, CombatHero[] enemies) {
        var targets = new List<CombatHero>();
        switch (attacker.baseHero.SpecialAttack) {
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
            default:
                break;
        }
        return targets;
    }

    public static bool CanAttack(CombatHero hero) {
        bool isMelee;
        switch (hero.baseHero.SpecialAttack) {
            case SpecialAttackEnum.BASIC_PHYSICAL:
            case SpecialAttackEnum.BASIC_MAGIC:
                isMelee = true;
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

public enum SpecialAttackEnum {
    BASIC_PHYSICAL = 1,
    BASIC_MAGIC = 2
}
