using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHeroContainer {

    private static Dictionary<HeroEnum, BaseHero> allHeroes;

    public static void InitializeHeroes() {
        allHeroes = new Dictionary<HeroEnum, BaseHero>();
        foreach (HeroEnum hero in Enum.GetValues(typeof(HeroEnum))) {
            allHeroes[hero] = BaseHero.GetHero(hero);
        }
    }

    public static BaseHero GetBaseHero(HeroEnum hero) {
        if (allHeroes == null || allHeroes.Count == 0) {
            InitializeHeroes();
        }
        return allHeroes[hero];
    }
}
