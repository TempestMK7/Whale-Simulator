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

        [NonSerialized] private BaseHero baseHero;

        public AccountHero(HeroEnum heroType) {
            Id = Guid.NewGuid();
            HeroType = heroType;
            baseHero = BaseHeroContainer.GetBaseHero(HeroType);
            AwakeningLevel = baseHero.Rarity;
            CurrentLevel = 1;
        }

        public void LoadBaseHero() {
            baseHero = BaseHeroContainer.GetBaseHero(HeroType);
        }

        public BaseHero GetBaseHero() {
            return baseHero;
        }

        public CombatHero GetCombatHero() {
            var state = StateManager.GetCurrentState();
            return new CombatHero(this, state.GetEquipmentForHero(this));
        }

        public CombatHero GetCombatHero(List<AccountEquipment> fakeEquipment) {
            return new CombatHero(this, fakeEquipment);
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
