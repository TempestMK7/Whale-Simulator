using System;
using System.Collections.Generic;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class UnequipHeroRequest {

        public Guid HeroId { get; set; }
    }
}
