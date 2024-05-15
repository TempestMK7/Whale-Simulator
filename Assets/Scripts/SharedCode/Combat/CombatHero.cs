using System;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.Combat {

    [Serializable]
    public class CombatHero : IComparable<CombatHero> {

        [NonSerialized] public BaseHero baseHero;

        public Guid combatHeroGuid;
        public HeroEnum heroEnum;
        public int awakeningLevel;
        public int currentLevel;

        public double health;
        public double attack;
        public double magic;
        public double defense;
        public double reflection;
        public double speed;
        public double critChance;
        public double deflectionChance;

        public double currentHealth;
        public double currentEnergy;
        public List<CombatStatus> currentStatus;

        public CombatHero() {
            // Empty constructor required by NewtonSoft.
        }

        public CombatHero(AccountHero accountHero, List<AccountEquipment> equipped) {
            baseHero = accountHero.GetBaseHero();
            combatHeroGuid = Guid.NewGuid();
            heroEnum = baseHero.Hero;
            awakeningLevel = accountHero.AwakeningLevel;
            currentLevel = accountHero.CurrentLevel;

            health = GetBigStat(baseHero.BaseHealth) * 10.0;
            attack = GetBigStat(baseHero.BaseAttack);
            magic = GetBigStat(baseHero.BaseMagic);
            defense = baseHero.BaseDefense;
            reflection = baseHero.BaseReflection;
            speed = GetBigStat(baseHero.BaseSpeed);
            critChance = baseHero.BaseCritChance;
            deflectionChance = baseHero.BaseDeflectionChance;

            currentHealth = health;
            currentEnergy = 50.0;
            currentStatus = new List<CombatStatus>();

            foreach (AccountEquipment equipment in equipped) {
                var baseEquipment = equipment.GetBaseEquipment();
                attack += baseEquipment.BaseAttack * equipment.Level;
                magic += baseEquipment.BaseMagic * equipment.Level;
                defense += equipment.GetDefense();
                reflection += equipment.GetReflection();
                critChance += baseEquipment.BaseCrit;
                deflectionChance += baseEquipment.BaseDeflect;
            }
        }

        public CombatHero(CombatHero other) {
            baseHero = other.baseHero;
            combatHeroGuid = other.combatHeroGuid;
            heroEnum = other.heroEnum;
            awakeningLevel = other.awakeningLevel;
            currentLevel = other.currentLevel;

            health = other.health;
            attack = other.attack;
            magic = other.magic;
            defense = other.defense;
            reflection = other.reflection;
            speed = other.speed;
            critChance = other.critChance;
            deflectionChance = other.deflectionChance;

            currentHealth = other.currentHealth;
            currentEnergy = other.currentEnergy;
            currentStatus = new List<CombatStatus>();
            foreach (CombatStatus container in other.currentStatus) {
                currentStatus.Add(new CombatStatus(container));
            }
        }

        public void RestoreUnserializedData() {
            baseHero = BaseHeroContainer.GetBaseHero(heroEnum);
        }

        private double GetBigStat(double baseStat) {
            return (baseStat + (BaseHero.GetBigStatGain(baseStat) * currentLevel)) * Math.Pow(1.1, awakeningLevel - 1);
        }

        public int CompareTo(CombatHero other) {
            if (other == null) return -1;
            int currentSpeedComp = other.GetModifiedSpeed().CompareTo(GetModifiedSpeed());
            if (currentSpeedComp != 0) return currentSpeedComp;
            int energyComp = other.currentEnergy.CompareTo(currentEnergy);
            if (energyComp != 0) return energyComp;
            int levelComp = other.currentLevel.CompareTo(currentLevel);
            if (levelComp != 0) return levelComp;
            int awakeningComp = other.awakeningLevel.CompareTo(awakeningLevel);
            if (awakeningComp != 0) return awakeningComp;
            return other.baseHero.HeroName.CompareTo(baseHero.HeroName);
        }

        public bool IsAlive() {
            return currentHealth > 0;
        }

        public bool HasAbility(AbilityEnum ability) {
            return baseHero.PassiveAbility == ability;
        }

        public bool HasStatus(StatusEnum status) {
            foreach (CombatStatus container in currentStatus) {
                if (container.status == status) return true;
            }
            return false;
        }

        public void AddStatus(CombatStatus status) {
            currentStatus.Add(status);
        }

        public void ClearControlEffects() {
            var newStatus = new List<CombatStatus>();
            foreach (CombatStatus status in currentStatus) {
                var statusDisplay = StatusInfoContainer.GetStatusInfo(status.status);
                if (!statusDisplay.ModifiesAttack) {
                    newStatus.Add(status);
                }
            }
            currentStatus = newStatus;
        }

        public void CountDownStatus(bool modifiesAttack) {
            var newStatus = new List<CombatStatus>();
            foreach (CombatStatus status in currentStatus) {
                var statusDisplay = StatusInfoContainer.GetStatusInfo(status.status);
                if (status.turnsRemaining == CombatStatus.INDEFINITE) {
                    newStatus.Add(status);
                } else if (modifiesAttack == statusDisplay.ModifiesAttack) {
                    status.turnsRemaining--;
                    if (status.turnsRemaining > 0) {
                        newStatus.Add(status);
                    }
                } else {
                    newStatus.Add(status);
                }
            }
            currentStatus = newStatus;
        }

        public double GetModifiedAttack() {
            var multiplier = 1.0;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.ATTACK_UP) {
                    multiplier += status.value;
                } else if (status.status == StatusEnum.ATTACK_DOWN) {
                    multiplier -= status.value;
                } else if (status.status == StatusEnum.CHILL) {
                    multiplier -= status.value;
                } else if (status.status == StatusEnum.ROOT) {
                    multiplier -= status.value;
                }
            }
            return multiplier * attack;
        }

        public double GetAttackModifier() {
            var multiplier = 1.0;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.ATTACK_UP) {
                    multiplier += status.value;
                } else if (status.status == StatusEnum.ATTACK_DOWN) {
                    multiplier -= status.value;
                } else if (status.status == StatusEnum.CHILL) {
                    multiplier -= status.value;
                } else if (status.status == StatusEnum.ROOT) {
                    multiplier -= status.value;
                }
            }
            return multiplier;
        }

        public double GetModifiedMagic() {
            var multiplier = 1.0;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.MAGIC_UP) {
                    multiplier += status.value;
                } else if (status.status == StatusEnum.MAGIC_DOWN) {
                    multiplier -= status.value;
                } else if (status.status == StatusEnum.DAZE) {
                    if (baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                        multiplier += status.value;
                    } else {
                        multiplier -= status.value;
                    }
                } else if (status.status == StatusEnum.BLIND) {
                    if (baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                        multiplier += status.value;
                    } else {
                        multiplier -= status.value;
                    }
                }
            }
            return multiplier * magic;
        }

        public double GetMagicModifier() {
            var multiplier = 1.0;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.MAGIC_UP) {
                    multiplier += status.value;
                } else if (status.status == StatusEnum.MAGIC_DOWN) {
                    multiplier -= status.value;
                } else if (status.status == StatusEnum.DAZE) {
                    if (baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                        multiplier += status.value;
                    } else {
                        multiplier -= status.value;
                    }
                } else if (status.status == StatusEnum.BLIND) {
                    if (baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                        multiplier += status.value;
                    } else {
                        multiplier -= status.value;
                    }
                }
            }
            return multiplier;
        }

        public double GetModifiedDefense() {
            var modifier = 0.0;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.DEFENSE_UP) {
                    modifier += status.value;
                } else if (status.status == StatusEnum.DEFENSE_DOWN) {
                    modifier -= status.value;
                } else if (status.status == StatusEnum.ICE_ARMOR) {
                    modifier += 0.1;
                } else if (status.status == StatusEnum.LAVA_ARMOR) {
                    modifier += 0.2;
                } else if (status.status == StatusEnum.THORN_ARMOR) {
                    modifier += 0.2;
                } else if (status.status == StatusEnum.EARTH_ARMOR) {
                    modifier += 0.3;
                }
            }
            return defense + modifier;
        }

        public double GetModifiedReflection() {
            var modifier = 0.0;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.REFLECTION_UP) {
                    modifier += status.value;
                } else if (status.status == StatusEnum.REFLECTION_DOWN) {
                    modifier -= status.value;
                } else if (status.status == StatusEnum.ICE_ARMOR) {
                    modifier += 0.3;
                } else if (status.status == StatusEnum.LAVA_ARMOR) {
                    modifier += 0.2;
                } else if (status.status == StatusEnum.THORN_ARMOR) {
                    modifier += 0.2;
                } else if (status.status == StatusEnum.EARTH_ARMOR) {
                    modifier += 0.1;
                }
            }
            return reflection + modifier;
        }

        public double GetModifiedSpeed() {
            var multiplier = 1.0;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.SPEED_UP) {
                    multiplier += status.value;
                } else if (status.status == StatusEnum.SPEED_DOWN) {
                    multiplier -= status.value;
                } else if (status.status == StatusEnum.CHILL) {
                    if (baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                        multiplier += status.value;
                    } else {
                        multiplier -= status.value;
                    }
                }
            }
            return multiplier * speed;
        }

        public double GetModifiedCrit() {
            var modified = critChance;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.DAZE) {
                    modified -= status.value;
                }
            }
            return modified;
        }

        public double GetModifiedDeflection() {
            var modified = deflectionChance;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.DAZE) {
                    modified -= status.value;
                }
            }
            return modified;
        }

        public double ReceiveHealing(double healing) {
            double missingHealth = health - currentHealth;
            if (missingHealth >= healing) {
                currentHealth += healing;
                return healing;
            } else {
                currentHealth = health;
                return missingHealth;
            }
        }
    }
}
