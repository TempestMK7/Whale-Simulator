using System;
using System.Collections.Generic;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class FuseEquipmentRequest {

        public Guid FusedEquipmentId { get; set; }
        public List<Guid> DestroyedEquipmentIds { get; set; }
    }
}
