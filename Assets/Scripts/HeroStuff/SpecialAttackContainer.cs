using System;
using System.Collections.Generic;

public class SpecialAttackContainer {

    public static CombatStep PerformSpecialAttack(CombatHero attacker, List<CombatHero> targets) {
        var step = new CombatStep(attacker, targets, true);
        return step;
    }

    public static List<CombatHero> DecideTargets(CombatHero attacker, CombatHero[] allies, CombatHero[] enemies) {
        var targets = new List<CombatHero>();
        return targets;
    }
}

public enum SpecialAttackEnum {
    STANDARD_PHYSICAL, STANDARD_MAGIC
}
