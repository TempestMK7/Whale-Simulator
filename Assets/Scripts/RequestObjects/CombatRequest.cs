using System;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class CombatRequest {

        public BattleEnum EncounterType { get; set; }
        public Guid?[] SelectedHeroes { get; set; }
    }
}
