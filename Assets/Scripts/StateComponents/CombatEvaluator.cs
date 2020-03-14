using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CombatEvaluator {

    public static async Task<CombatReport> GenerateCombatReport(AccountHero[] allies, AccountHero[] enemies) {
        CombatHero[] combatAllies = new CombatHero[allies.Length];
        for (int x = 0; x < combatAllies.Length; x++) {
            if (allies[x] != null) {
                combatAllies[x] = allies[x].GetCombatHero();
            }
        }

        CombatHero[] combatEnemies = new CombatHero[enemies.Length];
        for (int x = 0; x < combatEnemies.Length; x++) {
            if (enemies[x] != null) {
                combatEnemies[x] = enemies[x].GetCombatHero();
            }
        }

        var report = new CombatReport(combatAllies, combatEnemies);
        await Task.Run(() => {
            int turnNumber = 0;
            while (TeamAlive(combatAllies) && TeamAlive(combatEnemies) && turnNumber < 20) {
                turnNumber++;
                var turn = new CombatTurn(turnNumber, combatAllies, combatEnemies);
                turn.steps = PerformTurn(combatAllies, combatEnemies);
                turn.endOfTurn = EndOfTurn(combatAllies, combatEnemies);
                report.turns.Add(turn);
            }
            report.alliesWon = TeamAlive(combatAllies) && !TeamAlive(combatEnemies);
        });
        return report;
    }

    public static List<CombatStep> PerformTurn(CombatHero[] allies, CombatHero[] enemies) {
        var steps = new List<CombatStep>();

        var haveNotMoved = new List<CombatHero>();
        haveNotMoved.AddRange(allies);
        haveNotMoved.AddRange(enemies);
        haveNotMoved.Sort();

        while (haveNotMoved.Count > 0 && TeamAlive(allies) && TeamAlive(enemies)) {
            var next = haveNotMoved[0];
            haveNotMoved.RemoveAt(0);
            if (next == null || !next.IsAlive()) continue;

            CombatHero[] allyTeam;
            CombatHero[] enemyTeam;
            if (ContainsHero(allies, next)) {
                allyTeam = allies;
                enemyTeam = enemies;
            } else {
                allyTeam = enemies;
                enemyTeam = allies;
            }

            if (next.currentEnergy >= 100) {
                if (!SpecialAttackContainer.CanAttack(next)) {
                    next.CountDownStatus(true);
                    continue;
                }
                var enemyTargets = SpecialAttackContainer.DecideTargets(next, enemyTeam);
                var allyTargets = SpecialAttackContainer.DecideAllies(next, allyTeam);
                var step = SpecialAttackContainer.PerformSpecialAttack(next, allyTargets, enemyTargets);
                steps.Add(step);
                next.CountDownStatus(true);
            } else {
                if (!AttackContainer.CanAttack(next)) {
                    next.CountDownStatus(true);
                    continue;
                }
                var enemyTargets = AttackContainer.DecideTargets(next, enemyTeam);
                var allyTargets = AttackContainer.DecideAllies(next, allyTeam);
                var step = AttackContainer.PerformAttack(next, allyTargets, enemyTargets);
                steps.Add(step);
                next.CountDownStatus(true);
            }
            haveNotMoved.Sort();
        }

        return steps;
    }

    public static List<DamageInstance> EndOfTurn(CombatHero[] allies, CombatHero[] enemies) {
        var instances = new List<DamageInstance>();
        foreach (CombatHero hero in allies) {
            if (hero != null) {
                instances.AddRange(AbilityContainer.EvaluatePassives(hero));
                instances.AddRange(StatusContainer.EvaluateStatusEndOfTurn(hero));
            }
        }
        foreach (CombatHero hero in enemies) {
            if (hero != null) {
                instances.AddRange(AbilityContainer.EvaluatePassives(hero));
                instances.AddRange(StatusContainer.EvaluateStatusEndOfTurn(hero));
            }
        }
        return instances;
    }

    public static bool TeamAlive(CombatHero[] heroes) {
        foreach (CombatHero hero in heroes) {
            if (hero != null && hero.currentHealth > 0) return true;
        }
        return false;
    }

    public static CombatHero[] SnapShotTeam(CombatHero[] heroes) {
        var output = new CombatHero[heroes.Length];
        for (int x = 0; x < heroes.Length; x++) {
            if (heroes[x] != null) output[x] = new CombatHero(heroes[x]);
        }
        return output;
    }

    public static bool ContainsHero(CombatHero[] team, CombatHero hero) {
        foreach (CombatHero teamHero in team) {
            if (teamHero == hero) return true;
        }
        return false;
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

    public List<string> ToHumanReadableReport() {
        var heroDict = new Dictionary<Guid, BaseHero>();
        foreach (CombatHero ally in allies) {
            if (ally != null) heroDict[ally.combatHeroGuid] = ally.baseHero;
        }
        foreach (CombatHero enemy in enemies) {
            if (enemy != null) heroDict[enemy.combatHeroGuid] = enemy.baseHero;
        }
        List<string> report = new List<string>();
        foreach (CombatTurn turn in turns) {
            report.AddRange(turn.ToHumanReadableString(heroDict));
        }
        return report;
    }
}

[Serializable]
public class CombatTurn {

    [SerializeField] public int turnNumber;
    [SerializeField] public CombatHero[] allies;
    [SerializeField] public CombatHero[] enemies;
    [SerializeField] public List<CombatStep> steps;
    [SerializeField] public List<DamageInstance> endOfTurn;

    public CombatTurn(int turnNumber, CombatHero[] allies, CombatHero[] enemies) {
        this.turnNumber = turnNumber;
        this.allies = CombatEvaluator.SnapShotTeam(allies);
        this.enemies = CombatEvaluator.SnapShotTeam(enemies);
        steps = new List<CombatStep>();
        endOfTurn = new List<DamageInstance>();
    }

    public List<string> ToHumanReadableString(Dictionary<Guid, BaseHero> heroDict) {
        var output = new List<string>();
        output.Add(string.Format("Turn {0}", turnNumber));
        foreach (CombatStep step in steps) {
            output.AddRange(step.ToHumanReadableString(heroDict));
        }
        foreach (DamageInstance damageInstance in endOfTurn) {
            output.AddRange(damageInstance.ToHumanReadableString(heroDict));
        }
        return output;
    }
}

[Serializable]
public class CombatStep {

    [SerializeField] public CombatHero attacker;
    [SerializeField] public List<CombatHero> allyTargets;
    [SerializeField] public List<CombatHero> enemyTargets;
    [SerializeField] public bool wasSpecial;
    [SerializeField] public double totalDamage;
    [SerializeField] public double totalHealing;
    [SerializeField] public List<DamageInstance> damageInstances;

    public CombatStep(CombatHero attacker, List<CombatHero> allyTargets, List<CombatHero> enemyTargets, bool wasSpecial) {
        this.attacker = new CombatHero(attacker);
        this.allyTargets = new List<CombatHero>();
        foreach (CombatHero target in allyTargets) {
            this.allyTargets.Add(new CombatHero(target));
        }
        this.enemyTargets = new List<CombatHero>();
        foreach (CombatHero target in enemyTargets) {
            this.enemyTargets.Add(new CombatHero(target));
        }
        this.wasSpecial = wasSpecial;
        damageInstances = new List<DamageInstance>();
    }

    public List<string> ToHumanReadableString(Dictionary<Guid, BaseHero> heroDict) {
        List<string> output = new List<string>();
        string attack = wasSpecial ? attacker.baseHero.SpecialAttack.ToString() : attacker.baseHero.BasicAttack.ToString();
        string enemyNames = "";
        foreach (CombatHero enemy in enemyTargets) {
            if (enemyNames.Length > 0) enemyNames += ", ";
            enemyNames += enemy.baseHero.HeroName;
        }
        string allyNames = "";
        foreach (CombatHero ally in allyTargets) {
            if (allyNames.Length > 0) allyNames += ", ";
            allyNames += ally.baseHero.HeroName;
        }
        string formatted = "";
        if (enemyNames.Length > 0) formatted = enemyNames;
        if (allyNames.Length > 0) {
            if (formatted.Length > 0) formatted += " and ";
            formatted += allyNames;
        }
        if (formatted.Length >= 0) formatted = " on " + formatted;
        output.Add(string.Format("{0} used {1}{2}.", attacker.baseHero.HeroName, attack, formatted));

        foreach (DamageInstance damageInstance in damageInstances) {
            output.AddRange(damageInstance.ToHumanReadableString(heroDict));
        }
        return output;
    }
}

[Serializable]
public class DamageInstance {

    [SerializeField] public AttackEnum? attackUsed;
    [SerializeField] public SpecialAttackEnum? specialUsed;
    [SerializeField] public StatusEnum? triggeringStatus;
    [SerializeField] public Guid attackerGuid;
    [SerializeField] public Guid targetGuid;
    [SerializeField] public List<StatusContainer> inflictedStatus;
    [SerializeField] public double damage = 0;
    [SerializeField] public double healing = 0;
    [SerializeField] public HitType hitType = HitType.NORMAL;
    [SerializeField] public bool wasFatal = false;

    public DamageInstance(AttackEnum? attackUsed, SpecialAttackEnum? specialUsed, StatusEnum? triggeringStatus, Guid attackerGuid, Guid targetGuid) {
        this.attackUsed = attackUsed;
        this.specialUsed = specialUsed;
        this.triggeringStatus = triggeringStatus;
        this.attackerGuid = attackerGuid;
        this.targetGuid = targetGuid;
        inflictedStatus = new List<StatusContainer>();
    }

    public void AddStatus(StatusContainer status) {
        inflictedStatus.Add(new StatusContainer(status));
    }

    public List<string> ToHumanReadableString(Dictionary<Guid, BaseHero> heroDict) {
        var output = new List<string>();

        if (wasFatal) {
            output.Add(string.Format("{0} died to attack from {1}.", heroDict[targetGuid].HeroName, heroDict[attackerGuid].HeroName));
            return output;
        }

        if (damage != 0 || healing != 0) {
            string type = healing == 0 ? "damaged" : "healed";
            string value = healing == 0 ? damage.ToString("0") : healing.ToString("0");
            string typeString = "";
            switch (hitType) {
                case HitType.CRITICAL:
                    typeString = " (critical)";
                    break;
                case HitType.DEFLECTION:
                    typeString = " (deflection)";
                    break;
            }
            output.Add(string.Format("{0} {1} {2} for {3}{4}.", heroDict[attackerGuid].HeroName, type, heroDict[targetGuid].HeroName, value, typeString));
        }

        foreach (StatusContainer inflicted in inflictedStatus) {
            output.Add(inflicted.ToHumanReadableString(heroDict));
        }

        return output;
    }
}
