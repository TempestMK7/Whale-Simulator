using System;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class LootCaveEncounterResponse {

        public LootCaveEncounter Encounter { get; set; }
    }
}
