using System;
using System.Collections.Generic;

public class AttackContainer {
    
    public static CombatStep PerformAttack(CombatHero attacker, List<CombatHero> allies, List<CombatHero> enemies) {
        var step = new CombatStep(attacker, allies, enemies, false);
        if (!CanAttack(attacker)) return step;

        switch (attacker.baseHero.BasicAttack) {
            case AttackEnum.BASIC_PHYSICAL:
                foreach (CombatHero target in enemies) {
                    var damage = CombatMath.Damage(attacker.GetModifiedAttack(), target.GetModifiedDefense());
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.wasCritical = false;
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.BASIC_MAGIC:
                foreach (CombatHero target in enemies) {
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic(), target.GetModifiedReflection());
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.wasCritical = false;
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.MAGIC_BURN:
                foreach (CombatHero target in enemies) {
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic(), target.GetModifiedReflection());
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;

                    var burnStatus = new StatusContainer(StatusEnum.BURN, attacker.combatHeroGuid, attacker.GetModifiedMagic() * 0.25f, 5);
                    target.AddStatus(burnStatus);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.wasCritical = false;
                    damageInstance.inflictedStatus.Add(burnStatus);
                    step.damageInstances.Add(damageInstance);
                }
                break;
        }

        step.damageInstances.AddRange(CombatMath.EvaluateNegativeSideEffects(attacker, enemies));
        return step;
    }

    public static List<CombatHero> DecideTargets(CombatHero attacker, CombatHero[] enemies) {
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

    public static List<CombatHero> DecideAllies(CombatHero attacker, CombatHero[] allies) {
        var targets = new List<CombatHero>();
        switch (attacker.baseHero.BasicAttack) {
            default:
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
