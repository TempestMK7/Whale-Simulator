using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class ClaimResourcesResponse {

        public long LastClaimTimeStamp { get; set; }
        public double CurrentGold { get; set; }
        public double CurrentSouls { get; set; }
        public double CurrentExperience { get; set; }
        public int CurrentLevel { get; set; }
    }
}
