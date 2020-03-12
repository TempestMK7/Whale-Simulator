using System;
using System.Collections.Generic;

public class CombatMath {

    public static double Damage(double attack, double defense) {
        var mitigation = Math.Pow(0.5, defense / 100);
        return attack * mitigation;
    }

    public static CombatHero FirstAlive(CombatHero[] heroes) {
        foreach (CombatHero hero in heroes) {
            if (hero.currentHealth > 0) return hero;
        }
        return heroes[0];
    }
}
