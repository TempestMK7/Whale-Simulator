using System;
using System.Collections.Generic;

public class SpecialAttackContainer {

    public static CombatStep PerformSpecialAttack(CombatHero attacker, List<CombatHero> targets) {
        var step = new CombatStep(attacker, targets, true);
        switch (attacker.baseHero.SpecialAttack) {
            case SpecialAttackEnum.BASIC_PHYSICAL:
                foreach (CombatHero target in targets) {
                    var damage = CombatMath.Damage(attacker.currentAttack * 3.0, target.currentDefense);
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy -= 100;

                    var damageInstance = new DamageInstance(null, attacker.baseHero.SpecialAttack, null, attacker, target);
                    damageInstance.damage = damage;
                    damageInstance.wasCritical = false;
                    step.damageInstances.Add(damageInstance);
                }
                break;
            case SpecialAttackEnum.BASIC_MAGIC:
                foreach (CombatHero target in targets) {
                    var damage = CombatMath.Damage(attacker.currentMagic * 3.0, target.currentReflection);
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy -= 100;

                    var damageInstance = new DamageInstance(null, attacker.baseHero.SpecialAttack, null, attacker, target);
                    damageInstance.damage = damage;
                    damageInstance.wasCritical = false;
                    step.damageInstances.Add(damageInstance);
                }
                break;
        }
        return step;
    }

    public static List<CombatHero> DecideTargets(CombatHero attacker, CombatHero[] allies, CombatHero[] enemies) {
        var targets = new List<CombatHero>();
        switch (attacker.baseHero.SpecialAttack) {
            case SpecialAttackEnum.BASIC_PHYSICAL:
                targets.Add(CombatMath.FirstAlive(enemies));
                break;
            case SpecialAttackEnum.BASIC_MAGIC:
                targets.Add(CombatMath.FirstAlive(enemies));
                break;
        }
        return targets;
    }
}

public enum SpecialAttackEnum {
    BASIC_PHYSICAL, BASIC_MAGIC
}
