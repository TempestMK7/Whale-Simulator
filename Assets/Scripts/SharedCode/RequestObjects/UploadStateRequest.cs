using System;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    class UploadStateRequest {

        public AccountState UploadedState { get; set; }
    }
}
