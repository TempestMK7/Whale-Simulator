using System;
using System.Collections.Generic;

public class AttackContainer {
    
    public static CombatStep PerformAttack(CombatHero attacker, List<CombatHero> targets) {
        var step = new CombatStep(attacker, targets, false);
        return step;
    }

    public static List<CombatHero> DecideTargets(CombatHero attacker, CombatHero[] allies, CombatHero[] enemies) {
        var targets = new List<CombatHero>();
        return targets;
    }
}

public enum AttackEnum {
    BASIC_PHYSICAL, BASIC_MAGIC
}
