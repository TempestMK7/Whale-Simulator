using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class SummonRequest {

        public bool Verified { get; set; }
        public Guid AccountGuid { get; set; }
        public int SummonCount { get; set; }
    }
}
