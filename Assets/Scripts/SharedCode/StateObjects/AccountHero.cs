using System;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.Combat;

namespace Com.Tempest.Whale.StateObjects {

    [Serializable]
    public class AccountHero : IComparable<AccountHero> {

        public Guid Id { get; set; }
        public HeroEnum HeroType { get; set; }
        public int AwakeningLevel { get; set; }
        public int CurrentLevel { get; set; }
        public double CurrentExperience { get; set; }

        public AttackEnum? CurrentBasicAttack { get; set; }
        public AttackEnum? CurrentChargeAttack { get; set; }
        public List<AttackEnum> UnlockedAttacks { get; set; }

        [NonSerialized] private BaseHero baseHero;

        public AccountHero() {

        }

        public AccountHero(HeroEnum heroType) {
            Id = Guid.NewGuid();
            HeroType = heroType;
            baseHero = BaseHeroContainer.GetBaseHero(HeroType);
            AwakeningLevel = baseHero.Rarity;
            CurrentLevel = 1;
            CurrentExperience = 0;
            UnlockedAttacks = new List<AttackEnum>();
        }

        public void LoadBaseHero() {
            baseHero = BaseHeroContainer.GetBaseHero(HeroType);
        }

        public BaseHero GetBaseHero() {
            return baseHero;
        }

        public CombatHero GetCombatHeroFromAllEquipment(List<AccountEquipment> accountEquipment) {
            var equipped = accountEquipment.FindAll((AccountEquipment equipment) => {
                return Id.Equals(equipment.EquippedHeroId);
            });
            return new CombatHero(this, equipped);
        }

        public CombatHero GetCombatHero(List<AccountEquipment> fakeEquipment) {
            return new CombatHero(this, fakeEquipment);
        }

        public AttackEnum GetBasicAttackEnum() {
            return CurrentBasicAttack ?? (AwakeningLevel < 6 ? baseHero.SimpleBasic : baseHero.IntermediateBasic);
        }

        public AttackEnum GetChargeAttackEnum() {
            return CurrentChargeAttack ?? (AwakeningLevel < 6 ? baseHero.SimpleCharge : baseHero.IntermediateCharge);
        }

        public int CompareTo(AccountHero other) {
            var level = other.CurrentLevel - CurrentLevel;
            if (level != 0) return level;
            var awakening = other.AwakeningLevel - AwakeningLevel;
            if (awakening != 0) return awakening;
            var myHero = baseHero;
            var otherHero = other.GetBaseHero();
            var faction = myHero.Faction.CompareTo(otherHero.Faction);
            if (faction != 0) return faction;
            var rarity = otherHero.Rarity - myHero.Rarity;
            if (rarity != 0) return rarity;
            return otherHero.HeroName.CompareTo(myHero.HeroName);
        }

        public int GetNextExperienceRequirement() {
            // Experience requirement doubles every 10 levels.
            // return (int)(Math.Pow(1.071774, CurrentLevel - 1.0) * 1000);
            double multiplier = Math.Pow(1.1042, CurrentLevel - 1.0);
            var rounding = multiplier > 10 ? 0 : 1;
            return (int)(Math.Round(multiplier, rounding) * 100);
        }

        public void AwardExperience(long experience) {
            CurrentExperience += experience;
            if (CurrentLevel >= 100) {
                CurrentLevel = 100;
                CurrentExperience = 0;
                return;
            }
            while (CurrentExperience >= GetNextExperienceRequirement() && CurrentLevel < 100) {
                CurrentExperience -= GetNextExperienceRequirement();
                CurrentLevel++;
            }
        }

        public int ExperienceReward(int enemyLevel) {
            var baseExperience = (int)(10 * Math.Pow(1.05316, enemyLevel - 1));
            if (enemyLevel >= CurrentLevel) {
                var difference = enemyLevel - CurrentLevel;
                var multiplier = 1.0 + (difference * 0.05);
                return (int)(baseExperience * multiplier);
            } else {
                return (int)(baseExperience * Math.Pow(0.98, CurrentLevel - enemyLevel));
            }
        }

        public List<AttackEnum> GetUnknownAttacks(MoveComplexity complexity) {
            if (baseHero == null) GetBaseHero();
            var unknownAttacks = new List<AttackEnum>();
            foreach (AttackEnum basic in baseHero.TeachableBasics) {
                if (!UnlockedAttacks.Contains(basic) && AttackInfoContainer.GetAttackInfo(basic).Complexity == complexity) unknownAttacks.Add(basic);
            }
            foreach (AttackEnum charge in baseHero.TeachableCharges) {
                if (!UnlockedAttacks.Contains(charge) && AttackInfoContainer.GetAttackInfo(charge).Complexity == complexity) unknownAttacks.Add(charge);
            }
            return unknownAttacks;
        }
    }
}
