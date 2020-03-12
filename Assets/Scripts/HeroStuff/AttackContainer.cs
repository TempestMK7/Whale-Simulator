using System;
using System.Collections.Generic;

public class AttackContainer {
    
    public static CombatStep PerformAttack(CombatHero attacker, List<CombatHero> targets) {
        var step = new CombatStep(attacker, targets, false);
        switch (attacker.baseHero.BasicAttack) {
            case AttackEnum.BASIC_PHYSICAL:
                foreach (CombatHero target in targets) {
                    var damage = CombatMath.Damage(attacker.currentAttack, target.currentDefense);
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;
                }
                break;
            case AttackEnum.BASIC_MAGIC:
                foreach (CombatHero target in targets) {
                    var damage = CombatMath.Damage(attacker.currentMagic, target.currentReflection);
                    target.currentHealth -= damage;
                    step.totalDamage += damage;
                    attacker.currentEnergy += 25;
                }
                break;
        }
        return step;
    }

    public static List<CombatHero> DecideTargets(CombatHero attacker, CombatHero[] allies, CombatHero[] enemies) {
        var targets = new List<CombatHero>();
        switch (attacker.baseHero.BasicAttack) {
            case AttackEnum.BASIC_PHYSICAL:
                targets.Add(CombatMath.FirstAlive(enemies));
                break;
            case AttackEnum.BASIC_MAGIC:
                targets.Add(CombatMath.FirstAlive(enemies));
                break;
        }
        return targets;
    }
}

public enum AttackEnum {
    BASIC_PHYSICAL, BASIC_MAGIC
}
