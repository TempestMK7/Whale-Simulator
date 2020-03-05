using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHeroContainer {

    private static Dictionary<HeroEnum, BaseHero> allHeroes;
    public static List<HeroEnum> rarityOne;
    public static List<HeroEnum> rarityTwo;
    public static List<HeroEnum> rarityThree;
    public static List<HeroEnum> rarityFour;
    public static List<HeroEnum> rarityFive;

    public static void InitializeHeroes() {
        allHeroes = new Dictionary<HeroEnum, BaseHero>();
        rarityOne = new List<HeroEnum>();
        rarityTwo = new List<HeroEnum>();
        rarityThree = new List<HeroEnum>();
        rarityFour = new List<HeroEnum>();
        rarityFive = new List<HeroEnum>();

        foreach (HeroEnum hero in Enum.GetValues(typeof(HeroEnum))) {
            var b = BaseHero.GetHero(hero);
            allHeroes[hero] = b;
            switch (b.Rarity) {
                case 1: 
                    rarityOne.Add(hero);
                    break;
                case 2:
                    rarityTwo.Add(hero);
                    break;
                case 3:
                    rarityThree.Add(hero);
                    break;
                case 4:
                    rarityFour.Add(hero);
                    break;
                case 5:
                    rarityFive.Add(hero);
                    break;
            }
        }
    }

    public static BaseHero GetBaseHero(HeroEnum hero) {
        if (allHeroes == null || allHeroes.Count == 0) {
            InitializeHeroes();
        }
        return allHeroes[hero];
    }
}
