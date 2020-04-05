using System;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    class UploadStateRequest {

        public bool Verified { get; set; }
        public AccountState UploadedState { get; set; }
    }
}
