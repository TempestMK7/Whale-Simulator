using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    class CreateUserResponse {

        public string UserGuid { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
