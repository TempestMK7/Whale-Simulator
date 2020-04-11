using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.Combat {

    public class CombatEvaluator {

        public static CombatReport GenerateCombatReport(AccountHero[] allies, AccountHero[] enemies, 
            List<AccountEquipment> allyEquipment, List<AccountEquipment> enemyEquipment, bool usePreferredGear = false, bool nerfPreferredGear = false) {
            CombatHero[] combatAllies = new CombatHero[allies.Length];
            for (int x = 0; x < combatAllies.Length; x++) {
                if (allies[x] != null) {
                    combatAllies[x] = allies[x].GetCombatHeroFromAllEquipment(allyEquipment);
                }
            }

            CombatHero[] combatEnemies = new CombatHero[enemies.Length];
            for (int x = 0; x < combatEnemies.Length; x++) {
                if (enemies[x] != null) {
                    if (usePreferredGear) combatEnemies[x] = enemies[x].GetCombatHero(MissionContainer.GetStockEquipmentLoadout(enemies[x], nerfPreferredGear));
                    else combatEnemies[x] = enemies[x].GetCombatHeroFromAllEquipment(enemyEquipment);
                }
            }

            var report = new CombatReport(combatAllies, combatEnemies);
            int turnNumber = 0;
            while (TeamAlive(combatAllies) && TeamAlive(combatEnemies) && turnNumber < 20) {
                turnNumber++;
                var round = new CombatRound(turnNumber, combatAllies, combatEnemies);
                round.turns = PerformRound(combatAllies, combatEnemies);
                round.endOfTurn = EndOfTurn(combatAllies, combatEnemies);
                // This ability is so well designed that it deserves its own evaluation step.
                EvaluateFeedTheInferno(round);
                report.rounds.Add(round);
            }
            report.alliesWon = TeamAlive(combatAllies) && !TeamAlive(combatEnemies);
            report.alliesEnd = SnapShotTeam(combatAllies);
            report.enemiesEnd = SnapShotTeam(combatEnemies);
            return report;
        }

        public static List<CombatTurn> PerformRound(CombatHero[] allies, CombatHero[] enemies) {
            var steps = new List<CombatTurn>();

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

                var attack = next.currentEnergy >= 100 ? next.baseHero.SpecialAttack : next.baseHero.BasicAttack;
                var attackInfo = AttackInfoContainer.GetAttackInfo(attack);
                var enemyTargets = CombatMath.DecideTargets(next, attackInfo.EnemyTargetType, attackInfo.EnemyTargetCount, enemyTeam);
                var allyTargets = CombatMath.DecideTargets(next, attackInfo.AllyTargetType, attackInfo.AllyTargetCount, allyTeam);
                var step = CombatMath.PerformAttack(next, attack, allyTargets, enemyTargets);
                steps.Add(step);

                if (step.skippedTurn) {
                    next.ClearControlEffects();
                }
                next.CountDownStatus(true);
                haveNotMoved.Sort();
            }

            return steps;
        }

        public static List<CombatStep> EndOfTurn(CombatHero[] allies, CombatHero[] enemies) {
            var steps = new List<CombatStep>();
            foreach (CombatHero hero in allies) {
                if (hero != null && hero.IsAlive()) {
                    steps.AddRange(CombatMath.EvaluatePassives(hero));
                    steps.AddRange(CombatStatus.EvaluateStatusEndOfTurn(hero));
                }
            }
            foreach (CombatHero hero in enemies) {
                if (hero != null && hero.IsAlive()) {
                    steps.AddRange(CombatMath.EvaluatePassives(hero));
                    steps.AddRange(CombatStatus.EvaluateStatusEndOfTurn(hero));
                }
            }
            return steps;
        }

        public static void EvaluateFeedTheInferno(CombatRound round) {
            var allInferno = new List<CombatHero>();
            foreach (CombatHero hero in round.allies) {
                if (hero != null && hero.IsAlive() && hero.baseHero.PassiveAbility == AbilityEnum.FEED_THE_INFERNO) {
                    allInferno.Add(hero);
                }
            }
            foreach (CombatHero hero in round.enemies) {
                if (hero != null && hero.IsAlive() && hero.baseHero.PassiveAbility == AbilityEnum.FEED_THE_INFERNO) {
                    allInferno.Add(hero);
                }
            }
            if (allInferno.Count == 0) return;

            int burnCount = 0;
            foreach (CombatTurn turn in round.turns) {
                foreach (CombatStep stepInstance in turn.steps) {
                    foreach (CombatStatus status in stepInstance.inflictedStatus) {
                        if (status.status == StatusEnum.BURN) burnCount++;
                    }
                }
            }
            foreach (CombatStep endOfTurnStep in round.endOfTurn) {
                foreach (CombatStatus status in endOfTurnStep.inflictedStatus) {
                    if (status.status == StatusEnum.BURN) burnCount++;
                }
            }

            List<CombatStep> infernoEndOfTurn = new List<CombatStep>();
            foreach (CombatHero inferno in allInferno) {
                var magicUp = new CombatStatus(StatusEnum.MAGIC_UP, inferno.combatHeroGuid, inferno.combatHeroGuid, 0.05 * burnCount, CombatStatus.INDEFINITE);
                inferno.AddStatus(magicUp);

                var infernoInstance = new CombatStep(null, null, inferno.combatHeroGuid, inferno.combatHeroGuid);
                infernoInstance.AddStatus(magicUp);
                infernoEndOfTurn.Add(infernoInstance);
            }
            round.endOfTurn.AddRange(infernoEndOfTurn);
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

        public CombatHero[] allies;
        public CombatHero[] enemies;
        public List<CombatRound> rounds;
        public bool alliesWon;
        public CombatHero[] alliesEnd;
        public CombatHero[] enemiesEnd;

        public CombatReport() {
            // Empty constructor required by NewtonSoft.
        }

        public CombatReport(CombatHero[] allies, CombatHero[] enemies) {
            this.allies = CombatEvaluator.SnapShotTeam(allies);
            this.enemies = CombatEvaluator.SnapShotTeam(enemies);
            rounds = new List<CombatRound>();
        }

        public void RestoreUnserializedData() {
            foreach (CombatHero hero in allies) {
                if (hero != null) hero.RestoreUnserializedData();
            }
            foreach (CombatHero hero in enemies) {
                if (hero != null) hero.RestoreUnserializedData();
            }
            foreach (CombatHero hero in alliesEnd) {
                if (hero != null) hero.RestoreUnserializedData();
            }
            foreach (CombatHero hero in enemiesEnd) {
                if (hero != null) hero.RestoreUnserializedData();
            }
            foreach (CombatRound round in rounds) {
                round.RestoreUnserializedData();
            }
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
            foreach (CombatRound round in rounds) {
                report.AddRange(round.ToHumanReadableString(heroDict));
            }
            return report;
        }
    }

    [Serializable]
    public class CombatRound {

        public int turnNumber;
        public CombatHero[] allies;
        public CombatHero[] enemies;
        public List<CombatTurn> turns;
        public List<CombatStep> endOfTurn;

        public CombatRound() {
            // Empty constructor required by NewtonSoft.
        }

        public CombatRound(int turnNumber, CombatHero[] allies, CombatHero[] enemies) {
            this.turnNumber = turnNumber;
            this.allies = CombatEvaluator.SnapShotTeam(allies);
            this.enemies = CombatEvaluator.SnapShotTeam(enemies);
            turns = new List<CombatTurn>();
            endOfTurn = new List<CombatStep>();
        }

        public void RestoreUnserializedData() {
            foreach (CombatHero hero in allies) {
                if (hero != null) hero.RestoreUnserializedData();
            }
            foreach (CombatHero hero in enemies) {
                if (hero != null) hero.RestoreUnserializedData();
            }
            foreach (CombatTurn turn in turns) {
                turn.RestoreUnserializedData();
            }
        }

        public List<string> ToHumanReadableString(Dictionary<Guid, BaseHero> heroDict) {
            var output = new List<string>();
            output.Add(string.Format("Turn {0}", turnNumber));
            foreach (CombatTurn turn in turns) {
                output.AddRange(turn.ToHumanReadableString(heroDict));
            }
            foreach (CombatStep step in endOfTurn) {
                output.AddRange(step.ToHumanReadableString(heroDict));
            }
            return output;
        }
    }

    [Serializable]
    public class CombatTurn {

        public CombatHero attacker;
        public List<CombatHero> allyTargets;
        public List<CombatHero> enemyTargets;
        public AttackEnum attackUsed;

        public bool skippedTurn;
        public double totalDamage;
        public double totalHealing;
        public double energyGained;
        public List<CombatStep> steps;

        public CombatTurn() {
            // Empty constructor required by NewtonSoft.
        }

        public CombatTurn(CombatHero attacker, List<CombatHero> allyTargets, List<CombatHero> enemyTargets, AttackEnum attackUsed) {
            this.attacker = new CombatHero(attacker);
            this.allyTargets = new List<CombatHero>();
            foreach (CombatHero target in allyTargets) {
                this.allyTargets.Add(new CombatHero(target));
            }
            this.enemyTargets = new List<CombatHero>();
            foreach (CombatHero target in enemyTargets) {
                this.enemyTargets.Add(new CombatHero(target));
            }
            this.attackUsed = attackUsed;
            steps = new List<CombatStep>();
        }

        public void RestoreUnserializedData() {
            if (attacker != null) attacker.RestoreUnserializedData();
            foreach (CombatHero hero in allyTargets) {
                if (hero != null) hero.RestoreUnserializedData();
            }
            foreach (CombatHero hero in enemyTargets) {
                if (hero != null) hero.RestoreUnserializedData();
            }
        }

        public List<string> ToHumanReadableString(Dictionary<Guid, BaseHero> heroDict) {
            List<string> output = new List<string>();
            string attack = AttackInfoContainer.GetAttackInfo(attackUsed).AttackName;

            if (skippedTurn) {
                output.Add(string.Format("{0} attempted to use {1}, but was unable.", attacker.baseHero.HeroName, attack));
                return output;
            }

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

            foreach (CombatStep step in steps) {
                output.AddRange(step.ToHumanReadableString(heroDict));
            }
            return output;
        }
    }

    [Serializable]
    public class CombatStep {

        public AttackEnum? attackUsed;
        public StatusEnum? triggeringStatus;
        public Guid attackerGuid;
        public Guid targetGuid;
        public List<CombatStatus> inflictedStatus;
        public double damage = 0;
        public double healing = 0;
        public double targetEnergy = 0;
        public HitType hitType = HitType.NORMAL;
        public bool wasFatal = false;

        public CombatStep() {
            // Empty constructor required by NewtonSoft.
        }

        public CombatStep(AttackEnum? attackUsed, StatusEnum? triggeringStatus, Guid attackerGuid, Guid targetGuid) {
            this.attackUsed = attackUsed;
            this.triggeringStatus = triggeringStatus;
            this.attackerGuid = attackerGuid;
            this.targetGuid = targetGuid;
            inflictedStatus = new List<CombatStatus>();
        }

        public void AddStatus(CombatStatus status) {
            inflictedStatus.Add(new CombatStatus(status));
        }

        public List<string> ToHumanReadableString(Dictionary<Guid, BaseHero> heroDict) {
            var output = new List<string>();

            if (triggeringStatus != null) {
                var statusDisplay = StatusInfoContainer.GetStatusInfo(triggeringStatus.GetValueOrDefault());
                string type = damage == 0 ? "healing" : "damage";
                string value = damage == 0 ? healing.ToString("0") : damage.ToString("0");
                string inflicter = attackerGuid == null ? "" : string.Format(" ({0})", heroDict[attackerGuid].HeroName);
                output.Add(string.Format("{0} received {1} {2} from {3}{4} at end of turn.", heroDict[targetGuid].HeroName, value, type, statusDisplay.StatusName, inflicter));
            } else if (damage != 0 || healing != 0) {
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

            if (wasFatal) {
                string cause = triggeringStatus == null ? "attack" : StatusInfoContainer.GetStatusInfo(triggeringStatus.GetValueOrDefault()).StatusName;
                output.Add(string.Format("{0} died to {1} from {2}.", heroDict[targetGuid].HeroName, cause, heroDict[attackerGuid].HeroName));
            } else {
                foreach (CombatStatus inflicted in inflictedStatus) {
                    output.Add(inflicted.ToHumanReadableString(heroDict));
                }
            }

            return output;
        }
    }

    [Serializable]
    public class CombatStatus {

        [NonSerialized] public const int INDEFINITE = -1;

        public StatusEnum status;
        public Guid inflicterGuid;
        public Guid targetGuid;
        public double value;
        public int turnsRemaining;

        public CombatStatus() {
            // Empty constructor required by NewtonSoft.
        }

        public CombatStatus(StatusEnum status, Guid inflicterGuid, Guid targetGuid, double value, int turnsRemaining) {
            this.status = status;
            this.inflicterGuid = inflicterGuid;
            this.targetGuid = targetGuid;
            this.value = value;
            this.turnsRemaining = turnsRemaining;
        }

        public CombatStatus(CombatStatus other) {
            status = other.status;
            inflicterGuid = other.inflicterGuid;
            targetGuid = other.targetGuid;
            value = other.value;
            turnsRemaining = other.turnsRemaining;
        }

        public string ToHumanReadableString(Dictionary<Guid, BaseHero> heroDict) {
            var display = StatusInfoContainer.GetStatusInfo(status);

            var inflicterName = heroDict[inflicterGuid].HeroName;
            var targetName = heroDict[targetGuid].HeroName;
            var verb = display.IsBeneficial ? "bestowed" : "inflicted";
            var statusPrefix = inflicterGuid.Equals(targetGuid) ?
                string.Format("{0} gained {1}", heroDict[targetGuid].HeroName, display.StatusName) :
                string.Format("{0} {1} {2} on {3}", inflicterName, verb, display.StatusName, targetName);

            var turnSuffix = turnsRemaining == INDEFINITE ? "" : string.Format(" for {0} turns", turnsRemaining);

            string printableValue;
            switch (status) {
                case StatusEnum.BURN:
                case StatusEnum.BLEED:
                case StatusEnum.POISON:
                case StatusEnum.REGENERATION:
                case StatusEnum.LAVA_ARMOR:
                case StatusEnum.THORN_ARMOR:
                    printableValue = value.ToString("0");
                    break;
                default:
                    printableValue = string.Format("{0}%", value * 100);
                    break;
            }
            var valueSuffix = value == 0 ? "" : string.Format(" with a value of {0}", printableValue);

            return string.Format("{0}{1}{2}.", statusPrefix, turnSuffix, valueSuffix);
        }

        public static List<CombatStep> EvaluateStatusEndOfTurn(CombatHero hero) {
            List<CombatStep> steps = new List<CombatStep>();
            foreach (CombatStatus status in hero.currentStatus) {
                switch (status.status) {
                    case StatusEnum.BURN:
                    case StatusEnum.BLEED:
                    case StatusEnum.POISON:
                        var damage = status.value;
                        hero.currentHealth -= damage;
                        var step = new CombatStep(null, status.status, status.inflicterGuid, hero.combatHeroGuid);
                        step.damage = damage;

                        // We bail immediately if a status kills a hero to prevent future stati from bringing them back somehow.
                        if (!hero.IsAlive()) {
                            hero.currentHealth = 0;
                            hero.currentEnergy = 0;
                            step.wasFatal = true;
                            steps.Add(step);
                            hero.currentStatus.Clear();
                            return steps;
                        }

                        steps.Add(step);
                        break;
                    case StatusEnum.REGENERATION:
                        var healing = status.value;
                        healing = hero.ReceiveHealing(healing);

                        step = new CombatStep(null, status.status, status.inflicterGuid, hero.combatHeroGuid);
                        step.healing = healing;
                        steps.Add(step);
                        break;
                }
            }
            hero.CountDownStatus(false);
            return steps;
        }
    }
}
