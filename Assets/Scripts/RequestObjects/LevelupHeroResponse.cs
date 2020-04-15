using System;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class LevelupHeroResponse {

        public bool LevelupSuccessful { get; set; }
        public int HeroLevel { get; set; }
        public double CurrentGold { get; set; }
        public double CurrentSouls { get; set; }
    }
}
