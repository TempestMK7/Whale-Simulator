using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class VerifyEmailRequest {

        public int VerificationCode { get; set; }
    }
}
