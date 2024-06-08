using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    class DownloadStateRequest {

        public string AccountGuid { get; set; }
        public bool Verified { get; set; }
    }
}
