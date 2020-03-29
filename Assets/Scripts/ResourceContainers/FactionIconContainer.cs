﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.ResourceContainers {

    public class FactionIconContainer {

        private const string waterIcon = "Icons/Element01_256_04";
        private const string grassIcon = "Icons/Element01_256_10";
        private const string fireIcon = "Icons/Element01_256_01";
        private const string iceIcon = "Icons/Element01_256_19";
        private const string earthIcon = "Icons/Element01_256_22";
        private const string electricIcon = "Icons/Element01_256_16";

        private static Dictionary<FactionEnum, Sprite> factionSprites;

        public static void Initialize() {
            factionSprites = new Dictionary<FactionEnum, Sprite>();
            factionSprites.Add(FactionEnum.WATER, Resources.Load<Sprite>(waterIcon));
            factionSprites.Add(FactionEnum.GRASS, Resources.Load<Sprite>(grassIcon));
            factionSprites.Add(FactionEnum.FIRE, Resources.Load<Sprite>(fireIcon));
            factionSprites.Add(FactionEnum.ICE, Resources.Load<Sprite>(iceIcon));
            factionSprites.Add(FactionEnum.EARTH, Resources.Load<Sprite>(earthIcon));
            factionSprites.Add(FactionEnum.ELECTRIC, Resources.Load<Sprite>(electricIcon));
        }

        public static Sprite GetIconForFaction(FactionEnum faction) {
            if (factionSprites == null) {
                Initialize();
            }
            return factionSprites[faction];
        }
    }
}
