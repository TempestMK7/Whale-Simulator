using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class LoginRequest {

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
