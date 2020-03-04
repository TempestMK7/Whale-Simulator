using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AccountHero {

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
}
