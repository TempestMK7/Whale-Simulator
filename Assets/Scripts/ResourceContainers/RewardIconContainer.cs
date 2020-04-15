using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Tempest.Whale.ResourceContainers {

    public class RewardIconContainer {

        private static Dictionary<RewardType, Sprite> iconDict;

        public static void Initialize() {
            if (iconDict != null) return;
            iconDict = new Dictionary<RewardType, Sprite>();
            iconDict[RewardType.NONE] = Resources.Load<Sprite>("Icons/icon_gift");
            iconDict[RewardType.GOLD] = Resources.Load<Sprite>("Icons/icon_gold_smaller");
            iconDict[RewardType.SOULS] = Resources.Load<Sprite>("Icons/icon_soul_small");
            iconDict[RewardType.PLAYER_EXPERIENCE] = Resources.Load<Sprite>("Icons/icon_gift");
            iconDict[RewardType.GEMS] = Resources.Load<Sprite>("Icons/icon_gem");
            iconDict[RewardType.STANDARD_SUMMON] = Resources.Load<Sprite>("Icons/icon_summon");
            iconDict[RewardType.BRONZE_SUMMON] = Resources.Load<Sprite>("Icons/icon_bronze_summon");
            iconDict[RewardType.SILVER_SUMMON] = Resources.Load<Sprite>("Icons/icon_silver_summon");
            iconDict[RewardType.GOLD_SUMMON] = Resources.Load<Sprite>("Icons/icon_gold_summon");
        }

        public static Sprite GetIconForReward(RewardType reward) {
            if (iconDict == null) Initialize();
            if (iconDict.ContainsKey(reward)) {
                return iconDict[reward];
            }
            return iconDict[RewardType.NONE];
        }
    }

    public enum RewardType {
        NONE, GOLD, SOULS, PLAYER_EXPERIENCE, GEMS, STANDARD_SUMMON, BRONZE_SUMMON, SILVER_SUMMON, GOLD_SUMMON, EQUIPMENT
    }
}
