using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class LoginResponse {

        public bool EmailVerified { get; set; }
        public string Token { get; set; }
    }
}
