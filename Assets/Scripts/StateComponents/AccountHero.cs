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
        HeroGuid = new Guid();
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
        return new CombatHero(this);
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

    public BaseHero Base { get; set; }
    public int AwakeningLevel { get; set; }
    public int CurrentLevel { get; set; }
    public double Health { get; set; }
    public double Attack { get; set; }
    public double Magic { get; set; }
    public double Defense { get; set; }
    public double Reflection { get; set; }
    public double Speed { get; set; }

    public double CurrentHealth { get; set; }
    public double CurrentAttack { get; set; }
    public double CurrentMagic { get; set; }
    public double CurrentDefense { get; set; }
    public double CurrentReflection { get; set; }
    public double CurrentSpeed { get; set; }

    public CombatHero(AccountHero accountHero) {
        Base = accountHero.GetBaseHero();
        AwakeningLevel = accountHero.AwakeningLevel;
        CurrentLevel = accountHero.CurrentLevel;

        Health = GetBigStat(Base.BaseHealth);
        Attack = GetBigStat(Base.BaseAttack);
        Magic = GetBigStat(Base.BaseMagic);
        Defense = GetSmallStat(Base.BaseDefense);
        Reflection = GetSmallStat(Base.BaseReflection);
        Speed = GetBigStat(Base.BaseSpeed);

        CurrentHealth = Health;
        CurrentAttack = Attack;
        CurrentMagic = Magic;
        CurrentDefense = Defense;
        CurrentReflection = Reflection;
        CurrentSpeed = Speed;
    }

    private double GetBigStat(double baseStat) {
        return baseStat + (BaseHero.GetBigStatGain(baseStat) * CurrentLevel);
    }

    private double GetSmallStat(double baseStat) {
        return baseStat + (BaseHero.GetSmallStatGain(baseStat) * CurrentLevel);
    }
}
