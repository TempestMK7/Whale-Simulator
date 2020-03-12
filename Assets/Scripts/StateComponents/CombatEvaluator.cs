using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvaluator {

    public static CombatReport GenerateCombatReport(List<AccountHero> allies, List<AccountHero> enemies) {
        CombatHero[] combatAllies = new CombatHero[allies.Count];
        for (int x = 0; x < combatAllies.Length; x++) {
            combatAllies[x] = allies[x].GetCombatHero();
        }

        CombatHero[] combatEnemies = new CombatHero[enemies.Count];
        for (int x = 0; x < combatEnemies.Length; x++) {
            combatEnemies[x] = enemies[x].GetCombatHero();
        }

        var report = new CombatReport(combatAllies, combatEnemies);
        int turnNumber = 0;
        while (TeamAlive(combatAllies) && TeamAlive(combatEnemies)) {
            turnNumber++;
            var turn = new CombatTurn(combatAllies, combatEnemies);
            turn.steps = PerformTurn(combatAllies, combatAllies);
            report.turns.Add(turn);
        }
        report.alliesWon = TeamAlive(combatAllies);
        return report;
    }

    public static List<CombatStep> PerformTurn(CombatHero[] allies, CombatHero[] enemies) {
        var steps = new List<CombatStep>();
        var haveNotMoved = new List<CombatHero>();
        haveNotMoved.AddRange(allies);
        haveNotMoved.AddRange(enemies);
        haveNotMoved.Sort();
        while (haveNotMoved.Count > 0) {
            var next = haveNotMoved[0];
            if (next.currentEnergy >= 100) {
                var targets = SpecialAttackContainer.DecideTargets(next, allies, enemies);
                var step = SpecialAttackContainer.PerformSpecialAttack(next, targets);
                steps.Add(step);
            } else {
                var targets = AttackContainer.DecideTargets(next, allies, enemies);
                var step = AttackContainer.PerformAttack(next, targets);
                steps.Add(step);
            }
        }
        return steps;
    }

    public static bool TeamAlive(CombatHero[] heroes) {
        foreach (CombatHero hero in heroes) {
            if (hero.health > 0) return true;
        }
        return false;
    }

    public static CombatHero[] SnapShotTeam(CombatHero[] heroes) {
        var output = new CombatHero[heroes.Length];
        for (int x = 0; x < heroes.Length; x++) {
            output[x] = new CombatHero(heroes[x]);
        }
        return output;
    }
}

[Serializable]
public class CombatReport {

    [SerializeField] public CombatHero[] allies;
    [SerializeField] public CombatHero[] enemies;
    [SerializeField] public List<CombatTurn> turns;
    [SerializeField] public bool alliesWon;

    public CombatReport(CombatHero[] allies, CombatHero[] enemies) {
        this.allies = CombatEvaluator.SnapShotTeam(allies);
        this.enemies = CombatEvaluator.SnapShotTeam(enemies);
        turns = new List<CombatTurn>();
    }
}

[Serializable]
public class CombatTurn {

    [SerializeField] public CombatHero[] allies;
    [SerializeField] public CombatHero[] enemies;
    [SerializeField] public List<CombatStep> steps;

    public CombatTurn(CombatHero[] allies, CombatHero[] enemies) {
        this.allies = CombatEvaluator.SnapShotTeam(allies);
        this.enemies = CombatEvaluator.SnapShotTeam(enemies);
        steps = new List<CombatStep>();
    }
}

[Serializable]
public class CombatStep {

    [SerializeField] public CombatHero attacker;
    [SerializeField] public List<CombatHero> targets;
    [SerializeField] public bool wasSpecial;
    [SerializeField] public double totalDamage;
    [SerializeField] public double totalHealing;
    [SerializeField] public double totalEffects;

    public CombatStep(CombatHero attacker, List<CombatHero> targets, bool wasSpecial) {
        this.attacker = attacker;
        this.targets = targets;
        this.wasSpecial = wasSpecial;
    }
}
