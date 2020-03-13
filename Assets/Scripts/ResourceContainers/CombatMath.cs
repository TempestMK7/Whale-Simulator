using System;
using System.Collections.Generic;

public class CombatMath {

    public static double Damage(double attack, double defense, HitType hitType) {
        var modifiedDefense = defense;
        if (hitType == HitType.CRITICAL) {
            modifiedDefense = 0.0;
        } else if (hitType == HitType.DEFLECTION) {
            modifiedDefense *= 2.0;
        }
        var mitigation = Math.Pow(0.5, modifiedDefense / 200);
        return attack * mitigation;
    }

    public static HitType RollHitType(CombatHero attacker, CombatHero defender) {
        double critChance = attacker.GetModifiedCrit() - defender.GetModifiedDeflection();
        if (critChance == 0) return HitType.NORMAL;
        else if (critChance > 0) {
            var random = new Random((int)EpochTime.CurrentTimeMillis());
            var roll = random.NextDouble();
            return roll < critChance ? HitType.CRITICAL : HitType.NORMAL;
        } else {
            var deflectionChance = Math.Abs(critChance);
            var random = new Random((int)EpochTime.CurrentTimeMillis());
            var roll = random.NextDouble();
            return roll < deflectionChance ? HitType.DEFLECTION : HitType.NORMAL;
        }
    }

    public static HitType RollHitType(CombatHero attacker) {
        double critChance = attacker.GetModifiedCrit();
        if (critChance == 0) return HitType.NORMAL;
        else if (critChance > 0) {
            var random = new Random((int)EpochTime.CurrentTimeMillis());
            var roll = random.NextDouble();
            return roll < critChance ? HitType.CRITICAL : HitType.NORMAL;
        } else {
            var deflectionChance = Math.Abs(critChance);
            var random = new Random((int)EpochTime.CurrentTimeMillis());
            var roll = random.NextDouble();
            return roll < deflectionChance ? HitType.DEFLECTION : HitType.NORMAL;
        }
    }

    public static CombatHero FirstAlive(CombatHero[] heroes) {
        foreach (CombatHero hero in heroes) {
            if (hero.currentHealth > 0) return hero;
        }
        return heroes[0];
    }

    public static CombatHero LowestHealth(CombatHero[] heroes) {
        double lowestHealth = double.MaxValue;
        CombatHero selection = null;
        foreach (CombatHero hero in heroes) {
            if (hero.IsAlive() && hero.currentHealth < lowestHealth) {
                lowestHealth = hero.currentHealth;
                selection = hero;
            }
        }
        return selection;
    }

    public static CombatHero HighestHealth(CombatHero[] heroes) {
        double highestHealth = double.MinValue;
        CombatHero selection = null;
        foreach (CombatHero hero in heroes) {
            if (hero.IsAlive() && hero.currentHealth > highestHealth) {
                highestHealth = hero.currentHealth;
                selection = hero;
            }
        }
        return selection;
    }

    public static CombatHero HighestEnergy(CombatHero[] heroes) {
        double highestEnergy = double.MinValue;
        CombatHero selection = null;
        foreach (CombatHero hero in heroes) {
            if (hero.IsAlive() && hero.currentEnergy > highestEnergy) {
                highestEnergy = hero.currentEnergy;
                selection = hero;
            }
        }
        return selection;
    }

    public static List<CombatHero> AllLiving(CombatHero[] heroes) {
        var newList = new List<CombatHero>();
        foreach (CombatHero hero in heroes) {
            if (hero.IsAlive()) newList.Add(hero);
        }
        return newList;
    }

    public static List<CombatHero> SelectAtRandom(CombatHero[] heroes, int number) {
        var alive = AllLiving(heroes);
        if (alive.Count <= number) return alive;
        var random = new Random((int)EpochTime.CurrentTimeMillis());
        var output = new List<CombatHero>();
        for (int x = 0; x < number; x++) {
            int selected = random.Next(alive.Count);
            output.Add(alive[selected]);
            alive.RemoveAt(selected);
        }
        return output;
    }

    public static List<DamageInstance> EvaluateNegativeSideEffects(CombatHero attacker, List<CombatHero> enemies) {
        var output = new List<DamageInstance>();
        foreach (CombatHero enemy in enemies) {
            foreach (StatusContainer status in enemy.currentStatus) {
                switch (status.status) {
                    case StatusEnum.ICE_ARMOR:
                        if (attacker.HasStatus(StatusEnum.CHILLED) || attacker.HasStatus(StatusEnum.DOWSE)) {
                            var frozen = new StatusContainer(StatusEnum.FROZEN, status.inflicterGuid, 0, 1);
                            attacker.AddStatus(frozen);

                            var instance = new DamageInstance(null, null, StatusEnum.ICE_ARMOR, status.inflicterGuid, attacker.combatHeroGuid);
                            instance.AddStatus(frozen);
                            output.Add(instance);
                        } else {
                            var chilled = new StatusContainer(StatusEnum.CHILLED, status.inflicterGuid, 0.4, 3);
                            attacker.AddStatus(chilled);

                            var instance = new DamageInstance(null, null, StatusEnum.ICE_ARMOR, status.inflicterGuid, attacker.combatHeroGuid);
                            instance.AddStatus(chilled);
                            output.Add(instance);
                        }
                        break;
                    case StatusEnum.LAVA_SHIELD:
                        var burn = new StatusContainer(StatusEnum.BURN, status.inflicterGuid, status.value, 3);
                        attacker.AddStatus(burn);

                        var damageInstance = new DamageInstance(null, null, StatusEnum.LAVA_SHIELD, status.inflicterGuid, attacker.combatHeroGuid);
                        damageInstance.AddStatus(burn);
                        output.Add(new DamageInstance(null, null, StatusEnum.LAVA_SHIELD, status.inflicterGuid, attacker.combatHeroGuid));
                        break;
                    case StatusEnum.THORN_ARMOR:
                        var damage = status.value;
                        attacker.currentHealth -= damage;
                        output.Add(new DamageInstance(null, null, StatusEnum.THORN_ARMOR, status.inflicterGuid, attacker.combatHeroGuid));
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
