using System;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.StateObjects {

    [Serializable]
    public class AccountInventory {

        public List<ServerInventory> ServerListings { get; set; }
        public List<Guid> PurchasedItems { get; set; }

        public AccountInventory(List<ServerInventory> serverListings, List<Guid> purchasedItems) {
            ServerListings = serverListings;
            PurchasedItems = purchasedItems;
        }
    }

    [Serializable]
    public class ServerInventory {

        public Guid ServerID { get; set; }
        public InventoryEnum Inventory { get; set; }
        public bool CostsGems { get; set; }
        public int InventoryRow { get; set; }
        public long InspirationTime { get; set; }
        public long ExpirationTime { get; set; }

        public ServerInventory(Guid serverID, InventoryEnum inventory, bool costsGems, int inventoryRow, long inspirationTime, long expirationTime) {
            ServerID = serverID;
            Inventory = inventory;
            CostsGems = costsGems;
            InventoryRow = inventoryRow;
            InspirationTime = inspirationTime;
            ExpirationTime = expirationTime;
        }
    }
}
