using System;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class LevelupHeroRequest {

        public Guid AccountHeroId { get; set; }
    }
}
