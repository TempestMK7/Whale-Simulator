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

        public AttackEnum basicAttack;
        public AttackEnum chargeAttack;

        public double health;
        public double strength;
        public double power;
        public double toughness;
        public double resistance;
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

            basicAttack = accountHero.GetBasicAttackEnum();
            chargeAttack = accountHero.GetChargeAttackEnum();

            health = GetBigStat(baseHero.BaseHealth) * 10.0;
            strength = GetBigStat(baseHero.BaseStrength);
            power = GetBigStat(baseHero.BasePower);
            toughness = GetBigStat(baseHero.BaseToughness);
            resistance = GetBigStat(baseHero.BaseResistance);
            speed = GetBigStat(baseHero.BaseSpeed);
            critChance = baseHero.BaseCritChance;
            deflectionChance = baseHero.BaseDeflectionChance;

            currentHealth = health;
            currentEnergy = 50.0;
            currentStatus = new List<CombatStatus>();

            foreach (AccountEquipment equipment in equipped) {
                var baseEquipment = equipment.GetBaseEquipment();
                strength += baseEquipment.BaseStrength * equipment.Level;
                power += baseEquipment.BasePower * equipment.Level;
                toughness += baseEquipment.BaseToughness * equipment.Level;
                resistance += baseEquipment.BaseResistance * equipment.Level;
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
            strength = other.strength;
            power = other.power;
            toughness = other.toughness;
            resistance = other.resistance;
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

        public CombatStatus GetStatus(StatusEnum status) {
            foreach (CombatStatus container in currentStatus) {
                if (container.status == status) return container;
            }
            return null;
        }

        public void AddStatus(CombatStatus status) {
            currentStatus.Add(status);
        }

        public void ClearControlEffects() {
            var newStatus = new List<CombatStatus>();
            foreach (CombatStatus status in currentStatus) {
                var statusDisplay = StatusInfoContainer.GetStatusInfo(status.status);
                if (!statusDisplay.ModifiesOffense) {
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
                } else if (modifiesAttack == statusDisplay.ModifiesOffense) {
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

        public double GetModifiedStrength() {
            var multiplier = 1.0;
            foreach (CombatStatus status in currentStatus) {
                switch (status.status) {
                    case StatusEnum.STRENGTH_DOWN:
                    case StatusEnum.CHILL:
                    case StatusEnum.ROOT:
                    case StatusEnum.BLIND:
                    case StatusEnum.DAZE:
                        if (baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                            multiplier += status.value;
                        } else {
                            multiplier -= status.value;
                        }
                        break;
                    case StatusEnum.STRENGTH_UP:
                    case StatusEnum.HIGH_GROUND:
                        multiplier += status.value;
                        break;
                }
            }
            var baseValue = HasStatus(StatusEnum.REVERSE_POLARITY) ? power : strength;
            return multiplier * baseValue;
        }

        public double GetModifiedPower() {
            var multiplier = 1.0;
            foreach (CombatStatus status in currentStatus) {
                switch (status.status) {
                    case StatusEnum.POWER_DOWN:
                    case StatusEnum.CHILL:
                    case StatusEnum.ROOT:
                    case StatusEnum.BLIND:
                    case StatusEnum.DAZE:
                        if (baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                            multiplier += status.value;
                        } else {
                            multiplier -= status.value;
                        }
                        break;
                    case StatusEnum.POWER_UP:
                    case StatusEnum.HIGH_GROUND:
                        multiplier += status.value;
                        break;
                }
            }
            var baseValue = HasStatus(StatusEnum.REVERSE_POLARITY) ? strength : power;
            return multiplier * baseValue;
        }

        public double GetModifiedToughness() {
            var modifier = 1.0;
            var flatAmount = 0.0;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.TOUGHNESS_UP) {
                    modifier += status.value;
                } else if (status.status == StatusEnum.TOUGHNESS_DOWN) {
                    modifier -= status.value;
                } else if (status.status == StatusEnum.EARTH_ARMOR) {
                    flatAmount += status.value;
                } else if (status.status == StatusEnum.HIGH_GROUND) {
                    modifier += status.value;
                }
            }
            return (toughness * modifier) + flatAmount;
        }

        public double GetModifiedResistance() {
            var modifier = 1.0;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.RESISTANCE_UP) {
                    modifier += status.value;
                } else if (status.status == StatusEnum.RESISTANCE_DOWN) {
                    modifier -= status.value;
                } else if (status.status == StatusEnum.HIGH_GROUND) {
                    modifier += status.value;
                }
            }
            return resistance * modifier;
        }

        public double GetModifiedSpeed() {
            var multiplier = 1.0;
            foreach (CombatStatus status in currentStatus) {
                switch (status.status) {
                    case StatusEnum.SPEED_DOWN:
                    case StatusEnum.CHILL:
                    case StatusEnum.ROOT:
                        if (baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                            multiplier += status.value;
                        } else {
                            multiplier -= status.value;
                        }
                        break;
                    case StatusEnum.SPEED_UP:
                        multiplier += status.value;
                        break;
                }
            }
            return multiplier * speed;
        }

        public double GetModifiedCrit() {
            var modified = critChance;
            foreach (CombatStatus status in currentStatus) {
                switch (status.status) {
                    case StatusEnum.CRITICAL_DOWN:
                    case StatusEnum.DAZE:
                    case StatusEnum.BLIND:
                        if (baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                            modified += status.value;
                        } else {
                            modified -= status.value;
                        }
                        break;
                    case StatusEnum.CRITICAL_UP:
                        modified += status.value;
                        break;
                }
            }
            return modified;
        }

        public double GetModifiedDeflection() {
            var modified = deflectionChance;
            foreach (CombatStatus status in currentStatus) {
                if (status.status == StatusEnum.DAZE || status.status == StatusEnum.BLIND) {
                    modified -= status.value;
                } else if (status.status == StatusEnum.DEFLECTION_UP) {
                    modified += status.value;
                } else if (status.status == StatusEnum.DEFLECTION_DOWN) {
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
