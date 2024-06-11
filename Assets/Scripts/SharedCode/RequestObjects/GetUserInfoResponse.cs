using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class GetUserInfoResponse {

        public bool AccountCreated { get; set; }
        public bool EmailVerified { get; set; }
    }
}
