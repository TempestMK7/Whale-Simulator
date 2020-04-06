using System;
using System.Collections.Generic;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class FuseHeroRequest {

        public bool Verified { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid FusedHeroId { get; set; }
        public List<Guid> DestroyedHeroIds { get; set; }
    }
}
