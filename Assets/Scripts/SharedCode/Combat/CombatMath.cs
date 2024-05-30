using System;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.Combat {

    public class CombatMath {

        private static Random randomGen;

        public static double RandomDouble() {
            if (randomGen == null) randomGen = new Random((int)EpochTime.CurrentTimeMillis());
            return randomGen.NextDouble();
        }

        public static int RandomInt(int lower, int upper) {
            if (randomGen == null) randomGen = new Random((int)EpochTime.CurrentTimeMillis());
            return randomGen.Next(lower, upper);
        }

        #region Hit calculation and modifiers.

        public static double Damage(double modifiedAttack, double modifiedDefense, double baseDamage, bool hasStab, HitType hitType, HitEffectivity hitEffectivity) {
            var damage = baseDamage * (modifiedAttack / modifiedDefense);

            if (hasStab) {
                damage *= 1.333;
            }

            if (hitType == HitType.CRITICAL) {
                damage *= 1.5;
            } else if (hitType == HitType.DEFLECTION) {
                damage *= 0.67;
            }

            if (hitEffectivity == HitEffectivity.EMPOWERED) {
                damage *= 1.333;
            } else if (hitEffectivity == HitEffectivity.RESISTED) {
                damage *= 0.75;
            }

            return damage;
        }

        public static double Healing(double modifiedAttack, double attackerLevel, double targetAwakening, double baseHealing, bool hasStab, HitType hitType) {
            // This will likely need work later.  Because I do not have a defense value to divide attack by,
            // I take the average defense a target would have at the attacker's level and defender's awakening.
            var effectiveDefense = 75.0 + (2.75 * attackerLevel);
            effectiveDefense *= Math.Pow(1.1, targetAwakening);
            var healing = baseHealing * (modifiedAttack / effectiveDefense);

            if (hasStab) {
                healing *= 1.333;
            }

            if (hitType == HitType.CRITICAL) {
                healing *= 1.5;
            }

            return healing;
        }

        public static HitType RollHitType(CombatHero attacker, CombatHero defender) {
            double critChance = attacker.GetModifiedCrit() - defender.GetModifiedDeflection();
            if (critChance == 0) return HitType.NORMAL;
            else if (critChance > 0) {
                var roll = RandomDouble();
                return roll < critChance ? HitType.CRITICAL : HitType.NORMAL;
            } else {
                var deflectionChance = Math.Abs(critChance);
                var roll = RandomDouble();
                return roll < deflectionChance ? HitType.DEFLECTION : HitType.NORMAL;
            }
        }

        public static HitType RollHitType(CombatHero attacker) {
            double critChance = attacker.GetModifiedCrit();
            if (critChance == 0) return HitType.NORMAL;
            else if (critChance > 0) {
                var roll = RandomDouble();
                return roll < critChance ? HitType.CRITICAL : HitType.NORMAL;
            } else {
                var deflectionChance = Math.Abs(critChance);
                var roll = RandomDouble();
                return roll < deflectionChance ? HitType.DEFLECTION : HitType.NORMAL;
            }
        }

        public static HitEffectivity GetEffectivity(FactionEnum moveFaction, FactionEnum targetFaction) {
            if (IsEmpowered(moveFaction, targetFaction)) return HitEffectivity.EMPOWERED;
            if (IsEmpowered(targetFaction, moveFaction)) return HitEffectivity.RESISTED;
            return HitEffectivity.NORMAL;
        }

        public static bool IsEmpowered(FactionEnum attacking, FactionEnum defending) {
            switch (attacking) {
                case FactionEnum.WATER:
                    return defending == FactionEnum.FIRE || defending == FactionEnum.EARTH;
                case FactionEnum.GRASS:
                    return defending == FactionEnum.WATER || defending == FactionEnum.ELECTRIC;
                case FactionEnum.FIRE:
                    return defending == FactionEnum.GRASS || defending == FactionEnum.ICE;
                case FactionEnum.ICE:
                    return defending == FactionEnum.GRASS || defending == FactionEnum.EARTH;
                case FactionEnum.EARTH:
                    return defending == FactionEnum.FIRE || defending == FactionEnum.ELECTRIC;
                case FactionEnum.ELECTRIC:
                    return defending == FactionEnum.WATER || defending == FactionEnum.ICE;
                default:
                    return false;
            }
        }

        #endregion

        #region Targetting.

        public static List<CombatHero> FirstAlive(CombatHero[] heroes, int targetCount) {
            var allLiving = AllLiving(heroes);
            while (allLiving.Count > targetCount && allLiving.Count > 0) {
                allLiving.RemoveAt(allLiving.Count - 1);
            }
            return allLiving;
        }

        public static List<CombatHero> LowestHealth(CombatHero[] heroes, int targetCount) {
            var allLiving = AllLiving(heroes);
            while (allLiving.Count > targetCount) {
                double highestHealth = 0;
                CombatHero selection = null;
                foreach (CombatHero hero in allLiving) {
                    var healthPercent = hero.currentHealth / hero.health;
                    if (healthPercent > highestHealth) {
                        highestHealth = healthPercent;
                        selection = hero;
                    }
                }
                allLiving.Remove(selection);
            }
            return allLiving;
        }

        public static List<CombatHero> HighestHealth(CombatHero[] heroes, int targetCount) {
            var allLiving = AllLiving(heroes);
            while (allLiving.Count > targetCount) {
                double lowestHealth = double.MaxValue;
                CombatHero selection = null;
                foreach (CombatHero hero in allLiving) {
                    if (hero.currentHealth < lowestHealth) {
                        lowestHealth = hero.currentHealth;
                        selection = hero;
                    }
                }
                allLiving.Remove(selection);
            }
            return allLiving;
        }

        public static List<CombatHero> LowestEnergy(CombatHero[] heroes, int targetCount) {
            var allLiving = AllLiving(heroes);
            while (allLiving.Count > targetCount) {
                double highestEnergy = double.MinValue;
                CombatHero selection = null;
                foreach (CombatHero hero in allLiving) {
                    if (hero.currentEnergy > highestEnergy) {
                        highestEnergy = hero.currentEnergy;
                        selection = hero;
                    }
                }
                allLiving.Remove(selection);
            }
            return allLiving;
        }

        public static List<CombatHero> HighestEnergy(CombatHero[] heroes, int targetCount) {
            var allLiving = AllLiving(heroes);
            while (allLiving.Count > targetCount) {
                double lowestEnergy = double.MaxValue;
                CombatHero selection = null;
                foreach (CombatHero hero in allLiving) {
                    if (hero.currentEnergy < lowestEnergy) {
                        lowestEnergy = hero.currentEnergy;
                        selection = hero;
                    }
                }
                allLiving.Remove(selection);
            }
            return allLiving;
        }

        public static List<CombatHero> HighestStrength(CombatHero[] heroes, int targetCount) {
            var allLiving = AllLiving(heroes);
            while (allLiving.Count > targetCount) {
                double lowestStrength = double.MaxValue;
                CombatHero selection = null;
                foreach (CombatHero hero in allLiving) {
                    if (hero.GetModifiedStrength() < lowestStrength) {
                        lowestStrength = hero.GetModifiedStrength();
                        selection = hero;
                    }
                }
                allLiving.Remove(selection);
            }
            return allLiving;
        }

        public static List<CombatHero> HighestPower(CombatHero[] heroes, int targetCount) {
            var allLiving = AllLiving(heroes);
            while (allLiving.Count > targetCount) {
                double lowestPower = double.MaxValue;
                CombatHero selection = null;
                foreach (CombatHero hero in allLiving) {
                    if (hero.GetModifiedPower() < lowestPower) {
                        lowestPower = hero.GetModifiedPower();
                        selection = hero;
                    }
                }
                allLiving.Remove(selection);
            }
            return allLiving;
        }

        public static List<CombatHero> HighestToughness(CombatHero[] heroes, int targetCount) {
            var allLiving = AllLiving(heroes);
            while (allLiving.Count > targetCount) {
                double lowestToughness = double.MaxValue;
                CombatHero selection = null;
                foreach (CombatHero hero in allLiving) {
                    if (hero.GetModifiedToughness() < lowestToughness) {
                        lowestToughness = hero.GetModifiedToughness();
                        selection = hero;
                    }
                }
                allLiving.Remove(selection);
            }
            return allLiving;
        }

        public static List<CombatHero> HighestResistance(CombatHero[] heroes, int targetCount) {
            var allLiving = AllLiving(heroes);
            while (allLiving.Count > targetCount) {
                double lowestResistance = double.MaxValue;
                CombatHero selection = null;
                foreach (CombatHero hero in allLiving) {
                    if (hero.GetModifiedResistance() < lowestResistance) {
                        lowestResistance = hero.GetModifiedResistance();
                        selection = hero;
                    }
                }
                allLiving.Remove(selection);
            }
            return allLiving;
        }

        public static List<CombatHero> FrontRowFirst(CombatHero[] heroes, int targetCount) {
            var output = new List<CombatHero>();
            if (heroes[0].IsAlive() || heroes[1].IsAlive()) {
                if (heroes.Length > 0 && heroes[0].IsAlive()) output.Add(heroes[0]);
                if (heroes.Length > 1 && heroes[1].IsAlive()) output.Add(heroes[1]);
            } else {
                if (heroes.Length > 2 && heroes[2].IsAlive()) output.Add(heroes[2]);
                if (heroes.Length > 3 && heroes[3].IsAlive()) output.Add(heroes[3]);
                if (heroes.Length > 4 && heroes[4].IsAlive()) output.Add(heroes[4]);
            }
            return output;
        }

        public static List<CombatHero> BackRowFirst(CombatHero[] heroes, int targetCount) {
            var output = new List<CombatHero>();
            if (heroes[2].IsAlive() || heroes[3].IsAlive() || heroes[4].IsAlive()) {
                if (heroes.Length > 2 && heroes[2].IsAlive()) output.Add(heroes[2]);
                if (heroes.Length > 3 && heroes[3].IsAlive()) output.Add(heroes[3]);
                if (heroes.Length > 4 && heroes[4].IsAlive()) output.Add(heroes[4]);
            } else {
                if (heroes.Length > 0 && heroes[0].IsAlive()) output.Add(heroes[0]);
                if (heroes.Length > 1 && heroes[1].IsAlive()) output.Add(heroes[1]);
            }
            return output;
        }

        public static List<CombatHero> AllLiving(CombatHero[] heroes) {
            var newList = new List<CombatHero>();
            foreach (CombatHero hero in heroes) {
                if (hero != null && hero.IsAlive()) newList.Add(hero);
            }
            return newList;
        }

        public static List<CombatHero> SelectAtRandom(CombatHero[] heroes, int number) {
            var alive = AllLiving(heroes);
            if (alive.Count <= number) return alive;
            var output = new List<CombatHero>();
            for (int x = 0; x < number; x++) {
                int selected = RandomInt(0, alive.Count);
                output.Add(alive[selected]);
                alive.RemoveAt(selected);
            }
            return output;
        }

        public static List<CombatHero> DecideTargets(CombatHero targetter, TargetType targetType, int targetCount, CombatHero[] potentialTargets) {
            var targets = new List<CombatHero>();
            switch (targetType) {
                case TargetType.SELF:
                    targets.Add(targetter);
                    break;
                case TargetType.FIRST_ALIVE:
                    targets.AddRange(FirstAlive(potentialTargets, targetCount));
                    break;
                case TargetType.RANDOM:
                    targets.AddRange(SelectAtRandom(potentialTargets, targetCount));
                    break;
                case TargetType.LOWEST_HEALTH:
                    targets.AddRange(LowestHealth(potentialTargets, targetCount));
                    break;
                case TargetType.HIGHEST_HEALTH:
                    targets.AddRange(HighestHealth(potentialTargets, targetCount));
                    break;
                case TargetType.LOWEST_ENERGY:
                    targets.AddRange(LowestEnergy(potentialTargets, targetCount));
                    break;
                case TargetType.HIGHEST_ENERGY:
                    targets.AddRange(HighestEnergy(potentialTargets, targetCount));
                    break;
                case TargetType.FRONT_ROW_FIRST:
                    targets.AddRange(FrontRowFirst(potentialTargets, targetCount));
                    break;
                case TargetType.BACK_ROW_FIRST:
                    targets.AddRange(BackRowFirst(potentialTargets, targetCount));
                    break;
                case TargetType.HIGHEST_STRENGTH:
                    targets.AddRange(HighestStrength(potentialTargets, targetCount));
                    break;
                case TargetType.HIGHEST_POWER:
                    targets.AddRange(HighestPower(potentialTargets, targetCount));
                    break;
                case TargetType.HIGHEST_TOUGHNESS:
                    targets.AddRange(HighestToughness(potentialTargets, targetCount));
                    break;
                case TargetType.HIGHEST_RESISTANCE:
                    targets.AddRange(HighestResistance(potentialTargets, targetCount));
                    break;
            }
            return targets;
        }

        #endregion

        #region Attack appliers.

        public static CombatTurn PerformAttack(CombatHero attacker, AttackEnum attack, List<CombatHero> allies, List<CombatHero> enemies) {
            var turn = new CombatTurn(attacker, allies, enemies, attack);
            if (!CanAttack(attacker, attack)) {
                turn.skippedTurn = true;
                return turn;
            }

            var attackInfo = AttackInfoContainer.GetAttackInfo(attack);

            foreach (CombatHero target in enemies) {
                turn.steps.AddRange(attackInfo.ApplyAttackToEnemy(attacker, target));
            }

            foreach (CombatHero ally in allies) {
                turn.steps.Add(attackInfo.ApplyAttackToAlly(attacker, ally));
            }

            attacker.currentEnergy += attackInfo.AttackerEnergyGained;
            turn.energyGained = attackInfo.AttackerEnergyGained;

            turn.steps.AddRange(EvaluateNegativeSideEffects(attacker, enemies, turn));

            foreach (CombatStep step in turn.steps) {
                turn.totalDamage += step.damage;
                turn.totalHealing += step.healing;
            }

            return turn;
        }

        public static bool CanAttack(CombatHero hero, AttackEnum attack) {
            var attackInfo = AttackInfoContainer.GetAttackInfo(attack);
            if (hero.GetModifiedSpeed() <= 0) return false;

            if (attackInfo.IsPhysical) {
                if (hero.GetModifiedStrength() <= 0) return false;
            } else {
                if (hero.GetModifiedPower() <= 0) return false;
            }
            return true;
        }

        public static List<CombatStep> EvaluateNegativeSideEffects(CombatHero attacker, List<CombatHero> enemies, CombatTurn turn) {
            var output = new List<CombatStep>();
            foreach (CombatHero enemy in enemies) {
                var newEnemyStatus = new List<CombatStatus>();
                var tempAttacker = attacker;
                var tempTarget = enemy;
                bool statusingSelf = attacker.baseHero.PassiveAbility == AbilityEnum.MIRROR_ICE;
                if (statusingSelf) {
                    tempAttacker = enemy;
                    tempTarget = attacker;
                }

                foreach (CombatStatus status in enemy.currentStatus) {
                    switch (status.status) {
                        case StatusEnum.ICE_ARMOR:
                            if (tempAttacker.baseHero.PassiveAbility == AbilityEnum.COLD_BLOODED) break;
                            double value = status.value;
                            if (tempAttacker.HasStatus(StatusEnum.DOWSE)) {
                                value *= 2;
                            }
                            var chilled = new CombatStatus(StatusEnum.CHILL, status.inflicterGuid, tempTarget.combatHeroGuid, value, 2, status.associatedFaction);
                            var step = new CombatStep(null, StatusEnum.ICE_ARMOR, status.inflicterGuid, tempAttacker.combatHeroGuid);
                            step.AddStatus(chilled);
                            output.Add(step);
                            break;
                        case StatusEnum.LAVA_ARMOR:
                            if (tempAttacker.baseHero.PassiveAbility == AbilityEnum.WATER_BODY) break;
                            var burn = new CombatStatus(StatusEnum.BURN, status.inflicterGuid, tempTarget.combatHeroGuid, status.value / attacker.GetModifiedResistance(), 2, status.associatedFaction);
                            if (statusingSelf) {
                                newEnemyStatus.Add(burn);
                            } else {
                                attacker.AddStatus(burn);
                            }

                            step = new CombatStep(null, StatusEnum.LAVA_ARMOR, status.inflicterGuid, tempAttacker.combatHeroGuid);
                            step.AddStatus(burn);
                            output.Add(step);
                            break;
                        case StatusEnum.THORN_ARMOR:
                            var damage = status.value / attacker.GetModifiedToughness();
                            attacker.currentHealth -= damage;

                            step = new CombatStep(null, StatusEnum.THORN_ARMOR, status.inflicterGuid, attacker.combatHeroGuid);
                            step.damage = damage;
                            output.Add(step);
                            break;
                    }
                }
                foreach (CombatStatus newStatus in newEnemyStatus) {
                    enemy.AddStatus(newStatus);
                }

                switch (enemy.baseHero.PassiveAbility) {
                    case AbilityEnum.VAPORIZE:
                        if (attacker.baseHero.Faction == FactionEnum.FIRE) {
                            var damageInstance = new CombatStep(null, null, attacker.combatHeroGuid, enemy.combatHeroGuid);
                            var magicUp = new CombatStatus(StatusEnum.POWER_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2, enemy.baseHero.Faction);
                            var reflectionUp = new CombatStatus(StatusEnum.RESISTANCE_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2, enemy.baseHero.Faction);
                            enemy.AddStatus(magicUp);
                            enemy.AddStatus(reflectionUp);
                            damageInstance.AddStatus(magicUp);
                            damageInstance.AddStatus(reflectionUp);
                            output.Add(damageInstance);
                        }
                        break;
                    case AbilityEnum.BARK_SKIN:
                        var attackInfo = AttackInfoContainer.GetAttackInfo(turn.attackUsed);
                        if (attackInfo.IsPhysical) {
                            var damageInstance = new CombatStep(null, null, attacker.combatHeroGuid, enemy.combatHeroGuid);
                            var attackUp = new CombatStatus(StatusEnum.STRENGTH_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2, enemy.baseHero.Faction);
                            enemy.AddStatus(attackUp);
                            damageInstance.AddStatus(attackUp);
                            output.Add(damageInstance);
                        }
                        break;
                    case AbilityEnum.ABSORB_RAIN:
                        if (attacker.baseHero.Faction == FactionEnum.WATER) {
                            var damageInstance = new CombatStep(null, null, attacker.combatHeroGuid, enemy.combatHeroGuid);
                            var attackUp = new CombatStatus(StatusEnum.STRENGTH_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2, enemy.baseHero.Faction);
                            var defenseUp = new CombatStatus(StatusEnum.TOUGHNESS_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2, enemy.baseHero.Faction);
                            enemy.AddStatus(attackUp);
                            enemy.AddStatus(defenseUp);
                            damageInstance.AddStatus(attackUp);
                            damageInstance.AddStatus(defenseUp);
                            output.Add(damageInstance);
                        }
                        break;
                    case AbilityEnum.KINDLING:
                        if (attacker.baseHero.Faction == FactionEnum.GRASS) {
                            var damageInstance = new CombatStep(null, null, attacker.combatHeroGuid, enemy.combatHeroGuid);
                            var magicUp = new CombatStatus(StatusEnum.POWER_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2, enemy.baseHero.Faction);
                            var speedUp = new CombatStatus(StatusEnum.SPEED_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2, enemy.baseHero.Faction);
                            enemy.AddStatus(magicUp);
                            enemy.AddStatus(speedUp);
                            damageInstance.AddStatus(magicUp);
                            damageInstance.AddStatus(speedUp);
                            output.Add(damageInstance);
                        }
                        break;
                    case AbilityEnum.JAGGED_SURFACE:
                        var cumulativeInstance = new CombatStep(null, null, enemy.combatHeroGuid, attacker.combatHeroGuid);
                        foreach (CombatStep instance in turn.steps) {
                            if (instance.attackerGuid.Equals(attacker.combatHeroGuid) && instance.targetGuid.Equals(enemy.combatHeroGuid) && instance.attackUsed != null) {
                                attackInfo = AttackInfoContainer.GetAttackInfo(instance.attackUsed.GetValueOrDefault());
                                if (attackInfo.IsMelee) {
                                    cumulativeInstance.damage += instance.damage * 0.2;
                                }
                            }
                        }
                        if (cumulativeInstance.damage > 0) {
                            attacker.currentHealth -= cumulativeInstance.damage;
                            output.Add(cumulativeInstance);
                        }
                        break;
                    case AbilityEnum.CONDUCTIVITY:
                        attackInfo = AttackInfoContainer.GetAttackInfo(turn.attackUsed);
                        if (!attackInfo.IsPhysical) {
                            var damageInstance = new CombatStep(null, null, attacker.combatHeroGuid, enemy.combatHeroGuid);
                            var magicUp = new CombatStatus(StatusEnum.POWER_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2, enemy.baseHero.Faction);
                            enemy.AddStatus(magicUp);
                            damageInstance.AddStatus(magicUp);
                            output.Add(damageInstance);
                        }
                        break;
                }
            }
            return output;
        }

        public static List<CombatStep> EvaluatePassives(CombatHero hero) {
            var output = new List<CombatStep>();
            switch (hero.baseHero.PassiveAbility) {
                case AbilityEnum.MOUNTING_RAGE:
                    var status = new CombatStatus(StatusEnum.STRENGTH_UP, hero.combatHeroGuid, hero.combatHeroGuid, 0.2, CombatStatus.INDEFINITE, hero.baseHero.Faction);
                    hero.AddStatus(status);
                    var damageInstance = new CombatStep(null, null, hero.combatHeroGuid, hero.combatHeroGuid);
                    damageInstance.AddStatus(status);
                    output.Add(damageInstance);
                    break;
                case AbilityEnum.DEEP_ROOTS:
                    damageInstance = new CombatStep(null, null, hero.combatHeroGuid, hero.combatHeroGuid);
                    var missingHealth = hero.health - hero.currentHealth;
                    var healingAmount = missingHealth * 0.05;
                    hero.currentHealth += healingAmount;
                    damageInstance.healing = healingAmount;
                    output.Add(damageInstance);
                    break;
            }
            return output;
        }
    }

    #endregion
}
