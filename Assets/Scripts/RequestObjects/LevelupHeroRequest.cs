using System;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class LevelupHeroRequest {

        public bool Verified { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid AccountHeroId { get; set; }
    }
}
