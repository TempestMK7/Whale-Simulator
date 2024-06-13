using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class UpdateTutorialsResponse {

        public bool HasEnteredHub { get; set; }
        public bool HasEnteredSanctum { get; set; }
        public bool HasEnteredPortal { get; set; }
        public bool HasEnteredCampaign { get; set; }
    }
}
