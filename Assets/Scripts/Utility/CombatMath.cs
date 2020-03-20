using System;
using System.Collections.Generic;

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

    public static double Damage(double attack, double defense, HitType hitType, HitEffectivity hitEffectivity) {
        var modifiedDefense = defense;
        if (hitType == HitType.CRITICAL) {
            modifiedDefense = 0.0;
        } else if (hitType == HitType.DEFLECTION) {
            modifiedDefense *= 2.0;
        }
        var mitigation = Math.Pow(0.5, modifiedDefense / 200);
        var damage = attack * mitigation;
        if (hitEffectivity == HitEffectivity.EMPOWERED) {
            damage *= 1.5;
        } else if (hitEffectivity == HitEffectivity.RESISTED) {
            damage *= 2.0 / 3.0;
        }
        return damage;
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

    public static HitEffectivity GetEffectivity(CombatHero attacker, CombatHero target) {
        var attackerFaction = attacker.baseHero.Faction;
        var targetFaction = target.baseHero.Faction;
        if (IsEmpowered(attackerFaction, targetFaction)) return HitEffectivity.EMPOWERED;
        if (IsEmpowered(targetFaction, attackerFaction)) return HitEffectivity.RESISTED;
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
            double highestHealth = double.MinValue;
            CombatHero selection = null;
            foreach (CombatHero hero in allLiving) {
                if (hero.currentHealth > highestHealth) {
                    highestHealth = hero.currentHealth;
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
        }
        return targets;
    }

    public static List<DamageInstance> EvaluateNegativeSideEffects(CombatHero attacker, List<CombatHero> enemies, CombatStep step) {
        var output = new List<DamageInstance>();
        foreach (CombatHero enemy in enemies) {
            var newEnemyStatus = new List<StatusContainer>();
            var tempAttacker = attacker;
            var tempTarget = enemy;
            bool statusingSelf = attacker.baseHero.PassiveAbility == AbilityEnum.MIRROR_ICE;
            if (statusingSelf) {
                tempAttacker = enemy;
                tempTarget = attacker;
            }

            foreach (StatusContainer status in enemy.currentStatus) {
                switch (status.status) {
                    case StatusEnum.ICE_ARMOR:
                        if (tempAttacker.baseHero.PassiveAbility == AbilityEnum.COLD_BLOODED) break;
                        if (tempAttacker.HasStatus(StatusEnum.CHILL) || tempAttacker.HasStatus(StatusEnum.DOWSE)) {
                            var frozen = new StatusContainer(StatusEnum.FREEZE, status.inflicterGuid, tempTarget.combatHeroGuid, 0, 2);
                            if (statusingSelf) {
                                newEnemyStatus.Add(frozen);
                            } else {
                                attacker.AddStatus(frozen);
                            }

                            var instance = new DamageInstance(null, StatusEnum.ICE_ARMOR, status.inflicterGuid, tempAttacker.combatHeroGuid);
                            instance.AddStatus(frozen);
                            output.Add(instance);
                        } else {
                            var chilled = new StatusContainer(StatusEnum.CHILL, status.inflicterGuid, tempTarget.combatHeroGuid, 0.4, 3);
                            if (statusingSelf) {
                                newEnemyStatus.Add(chilled);
                            } else {
                                attacker.AddStatus(chilled);
                            }

                            var instance = new DamageInstance(null, StatusEnum.ICE_ARMOR, status.inflicterGuid, tempAttacker.combatHeroGuid);
                            instance.AddStatus(chilled);
                            output.Add(instance);
                        }
                        break;
                    case StatusEnum.LAVA_ARMOR:
                        if (tempAttacker.baseHero.PassiveAbility == AbilityEnum.WATER_BODY) break;
                        var burn = new StatusContainer(StatusEnum.BURN, status.inflicterGuid, tempTarget.combatHeroGuid, status.value, 2);
                        if (statusingSelf) {
                            newEnemyStatus.Add(burn);
                        } else {
                            attacker.AddStatus(burn);
                        }

                        var damageInstance = new DamageInstance(null, StatusEnum.LAVA_ARMOR, status.inflicterGuid, tempAttacker.combatHeroGuid);
                        damageInstance.AddStatus(burn);
                        output.Add(damageInstance);
                        break;
                    case StatusEnum.THORN_ARMOR:
                        var damage = status.value;
                        attacker.currentHealth -= damage;

                        damageInstance = new DamageInstance(null, StatusEnum.THORN_ARMOR, status.inflicterGuid, attacker.combatHeroGuid);
                        damageInstance.damage = damage;
                        output.Add(damageInstance);
                        break;
                }
            }
            foreach (StatusContainer newStatus in newEnemyStatus) {
                enemy.AddStatus(newStatus);
            }

            switch (enemy.baseHero.PassiveAbility) {
                case AbilityEnum.VAPORIZE:
                    if (attacker.baseHero.Faction == FactionEnum.FIRE) {
                        var damageInstance = new DamageInstance(null, null, attacker.combatHeroGuid, enemy.combatHeroGuid);
                        var magicUp = new StatusContainer(StatusEnum.MAGIC_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2);
                        var reflectionUp = new StatusContainer(StatusEnum.REFLECTION_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2);
                        enemy.AddStatus(magicUp);
                        enemy.AddStatus(reflectionUp);
                        damageInstance.AddStatus(magicUp);
                        damageInstance.AddStatus(reflectionUp);
                        output.Add(damageInstance);
                    }
                    break;
                case AbilityEnum.BARK_SKIN:
                    var attackInfo = AttackInfoContainer.GetAttackInfo(step.attackUsed);
                    if (attackInfo.IsPhysical) {
                        var damageInstance = new DamageInstance(null, null, attacker.combatHeroGuid, enemy.combatHeroGuid);
                        var attackUp = new StatusContainer(StatusEnum.ATTACK_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2);
                        enemy.AddStatus(attackUp);
                        damageInstance.AddStatus(attackUp);
                        output.Add(damageInstance);
                    }
                    break;
                case AbilityEnum.ABSORB_RAIN:
                    if (attacker.baseHero.Faction == FactionEnum.WATER) {
                        var damageInstance = new DamageInstance(null, null, attacker.combatHeroGuid, enemy.combatHeroGuid);
                        var attackUp = new StatusContainer(StatusEnum.ATTACK_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2);
                        var defenseUp = new StatusContainer(StatusEnum.DEFENSE_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2);
                        enemy.AddStatus(attackUp);
                        enemy.AddStatus(defenseUp);
                        damageInstance.AddStatus(attackUp);
                        damageInstance.AddStatus(defenseUp);
                        output.Add(damageInstance);
                    }
                    break;
                case AbilityEnum.KINDLING:
                    if (attacker.baseHero.Faction == FactionEnum.GRASS) {
                        var damageInstance = new DamageInstance(null, null, attacker.combatHeroGuid, enemy.combatHeroGuid);
                        var magicUp = new StatusContainer(StatusEnum.MAGIC_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2);
                        var speedUp = new StatusContainer(StatusEnum.SPEED_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2);
                        enemy.AddStatus(magicUp);
                        enemy.AddStatus(speedUp);
                        damageInstance.AddStatus(magicUp);
                        damageInstance.AddStatus(speedUp);
                        output.Add(damageInstance);
                    }
                    break;
                case AbilityEnum.JAGGED_SURFACE:
                    var cumulativeInstance = new DamageInstance(null, null, enemy.combatHeroGuid, attacker.combatHeroGuid);
                    foreach (DamageInstance instance in step.damageInstances) {
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
                    attackInfo = AttackInfoContainer.GetAttackInfo(step.attackUsed);
                    if (!attackInfo.IsPhysical) {
                        var damageInstance = new DamageInstance(null, null, attacker.combatHeroGuid, enemy.combatHeroGuid);
                        var magicUp = new StatusContainer(StatusEnum.MAGIC_UP, attacker.combatHeroGuid, enemy.combatHeroGuid, 0.2, 2);
                        enemy.AddStatus(magicUp);
                        damageInstance.AddStatus(magicUp);
                        output.Add(damageInstance);
                    }
                    break;
            }
        }
        return output;
    }
}

public enum HitType {
    NORMAL = 1, CRITICAL = 2, DEFLECTION = 3
}

public enum HitEffectivity {
    NORMAL = 1, EMPOWERED = 2, RESISTED = 3
}

public enum TargetType {
    NONE = 0, SELF = 1, FIRST_ALIVE = 2, RANDOM = 3, LOWEST_HEALTH = 4, HIGHEST_HEALTH = 5, LOWEST_ENERGY = 6, HIGHEST_ENERGY = 7
}
