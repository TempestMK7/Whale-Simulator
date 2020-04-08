using System;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class EquipRequest {

        public Guid EquipmentId { get; set; }
        public Guid? HeroId { get; set; }
        public EquipmentSlot? Slot { get; set; }
    }
}
