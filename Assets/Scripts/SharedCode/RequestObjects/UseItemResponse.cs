using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;
using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class UseItemResponse {

        public bool Success { get; set; }
        public AccountInventory UsedInventory { get; set; }
        public AccountHero TargetHero { get; set; }
        public AccountInventory ResultInventory { get; set; }
        public AttackEnum? NewAttack { get; set; }
    }
}
