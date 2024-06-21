using System;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.StateObjects {

    [Serializable]
    public class AccountInventory : IComparable<AccountInventory> {

        public Guid Id { get; set; }
        public ItemEnum ItemType { get; set; }
        public long Quantity { get; set; }

        public int CompareTo(AccountInventory other) {
            return (int)ItemType - (int)other.ItemType;
        }
    }
}
