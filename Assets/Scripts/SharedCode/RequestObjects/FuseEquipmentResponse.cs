using System;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class FuseEquipmentResponse {

        public bool FusionSuccessful { get; set; }
        public AccountEquipment FusedEquipment { get; set; }
    }
}
