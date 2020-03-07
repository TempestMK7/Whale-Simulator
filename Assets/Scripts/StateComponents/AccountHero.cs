using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AccountHero : IComparable<AccountHero> {

    [SerializeField] public HeroEnum HeroType { get; }
    [SerializeField] public int AwakeningLevel { get; }
    [SerializeField] public int CurrentLevel { get; }

    [NonSerialized] private BaseHero baseHero;

    public AccountHero(HeroEnum heroType) {
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

    public int CompareTo(AccountHero other) {
        var awakening = other.AwakeningLevel - AwakeningLevel;
        if (awakening != 0) return awakening;
        var myHero = baseHero;
        var otherHero = other.GetBaseHero();
        var rarity = otherHero.Rarity - myHero.Rarity;
        if (rarity != 0) return rarity;
        var faction = myHero.Faction.CompareTo(otherHero.Faction);
        if (faction != 0) return faction;
        var name = otherHero.HeroName.CompareTo(myHero.HeroName);
        if (name != 0) return name;
        return other.CurrentLevel - CurrentLevel;
    }
}

public class CombatHero {

}
