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
    }
}
