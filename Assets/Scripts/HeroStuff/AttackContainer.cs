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
                    damageInstance.AddStatus(burnStatus);
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case AttackEnum.MAGIC_ICE:
                foreach (CombatHero target in enemies) {
                    var damage = CombatMath.Damage(attacker.GetModifiedMagic(), target.GetModifiedReflection()) * 0.6f;
                    target.currentHealth -= damage;
                    step.totalDamage += damage;

                    StatusContainer status;
                    if (target.HasStatus(StatusEnum.CHILLED)) {
                        status = new StatusContainer(StatusEnum.FROZEN, attacker.combatHeroGuid, 0.0, 1);
                    } else {
                        status = new StatusContainer(StatusEnum.CHILLED, attacker.combatHeroGuid, 0.2, 2);
                    }
                    target.AddStatus(status);

                    var damageInstance = new DamageInstance(attacker.baseHero.BasicAttack, null, null, attacker.combatHeroGuid, target.combatHeroGuid);
                    damageInstance.damage = damage;
                    damageInstance.wasCritical = false;
                    damageInstance.AddStatus(status);
                    step.damageInstances.Add(damageInstance);
                }
                attacker.currentEnergy += 25;
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
            case AttackEnum.MAGIC_ICE:
                targets.AddRange(CombatMath.SelectAtRandom(enemies, 2));
                break;
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
        bool isRanged;
        switch (hero.baseHero.BasicAttack) {
            case AttackEnum.MAGIC_BURN:
                isMelee = false;
                isRanged = true;
                break;
            case AttackEnum.BASIC_PHYSICAL:
            case AttackEnum.BASIC_MAGIC:
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

public enum AttackEnum {
    BASIC_PHYSICAL = 1,
    BASIC_MAGIC = 2,
    MAGIC_BURN = 3,
    MAGIC_ICE = 4
}
