using System;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class WhaleRequest<T> {

        public bool Verified { get; set; }
        public Guid AccountGuid { get; set; }
        public T Request { get; set; }

        public WhaleRequest(bool verified, Guid accountGuid, T request) {
            Verified = verified;
            AccountGuid = accountGuid;
            Request = request;
        }
    }
}
