using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class CreateLoginRequest {

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
