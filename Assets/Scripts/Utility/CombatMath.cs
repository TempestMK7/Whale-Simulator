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

    public static CombatHero FirstAlive(CombatHero[] heroes) {
        foreach (CombatHero hero in heroes) {
            if (hero != null && hero.currentHealth > 0) return hero;
        }
        return heroes[0];
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

    public static List<CombatHero> DecideTargets(TargetType targetType, int targetCount, CombatHero[] potentialTargets) {
        var targets = new List<CombatHero>();
        switch (targetType) {
            case TargetType.FIRST_ALIVE:
                targets.Add(FirstAlive(potentialTargets));
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

    public static List<DamageInstance> EvaluateNegativeSideEffects(CombatHero attacker, List<CombatHero> enemies) {
        var output = new List<DamageInstance>();
        foreach (CombatHero enemy in enemies) {
            foreach (StatusContainer status in enemy.currentStatus) {
                switch (status.status) {
                    case StatusEnum.ICE_ARMOR:
                        if (attacker.HasStatus(StatusEnum.CHILL) || attacker.HasStatus(StatusEnum.DOWSE)) {
                            var frozen = new StatusContainer(StatusEnum.FREEZE, status.inflicterGuid, 0, 2);
                            attacker.AddStatus(frozen);

                            var instance = new DamageInstance(null, StatusEnum.ICE_ARMOR, status.inflicterGuid, attacker.combatHeroGuid);
                            instance.AddStatus(frozen);
                            output.Add(instance);
                        } else {
                            var chilled = new StatusContainer(StatusEnum.CHILL, status.inflicterGuid, 0.4, 3);
                            attacker.AddStatus(chilled);

                            var instance = new DamageInstance(null, StatusEnum.ICE_ARMOR, status.inflicterGuid, attacker.combatHeroGuid);
                            instance.AddStatus(chilled);
                            output.Add(instance);
                        }
                        break;
                    case StatusEnum.LAVA_ARMOR:
                        var burn = new StatusContainer(StatusEnum.BURN, status.inflicterGuid, status.value, 3);
                        attacker.AddStatus(burn);

                        var damageInstance = new DamageInstance(null, StatusEnum.LAVA_ARMOR, status.inflicterGuid, attacker.combatHeroGuid);
                        damageInstance.AddStatus(burn);
                        output.Add(new DamageInstance(null, StatusEnum.LAVA_ARMOR, status.inflicterGuid, attacker.combatHeroGuid));
                        break;
                    case StatusEnum.THORN_ARMOR:
                        var damage = status.value;
                        attacker.currentHealth -= damage;
                        output.Add(new DamageInstance(null, StatusEnum.THORN_ARMOR, status.inflicterGuid, attacker.combatHeroGuid));
                        break;
                }
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
    NONE = 0, FIRST_ALIVE = 1, RANDOM = 2, LOWEST_HEALTH = 3, HIGHEST_HEALTH = 4, LOWEST_ENERGY = 5, HIGHEST_ENERGY = 6
}
