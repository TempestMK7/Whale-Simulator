using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AccountHero : IComparable<AccountHero> {

    [SerializeField] public Guid HeroGuid { get; set; }
    [SerializeField] public HeroEnum HeroType { get; set; }
    [SerializeField] public int AwakeningLevel { get; set; }
    [SerializeField] public int CurrentLevel { get; set; }

    [NonSerialized] private BaseHero baseHero;

    public AccountHero(HeroEnum heroType) {
        HeroGuid = Guid.NewGuid();
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

    public CombatHero GetCombatHeroWithStockEquipment() {
        var fakeEquipment = new List<AccountEquipment>();
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
