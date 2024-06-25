using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class UseItemRequest {

        public Guid UsedInventoryId { get; set; }
        public Guid TargetHeroId { get; set; }
        public int Quantity { get; set; }
    }
}
