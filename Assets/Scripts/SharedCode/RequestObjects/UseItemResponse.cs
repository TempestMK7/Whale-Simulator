using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;
using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class UseItemResponse {

        public bool Success { get; set; }
        public AccountInventory NewInventory { get; set; }
        public AccountHero NewHero { get; set; }
        public AttackEnum? NewAttack { get; set; }
    }
}
