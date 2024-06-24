using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class UseItemRequest {

        public Guid InventoryId { get; set; }
        public Guid HeroId { get; set; }
        public int Quantity { get; set; }
    }
}
