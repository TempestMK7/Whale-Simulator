using System;

namespace Com.Tempest.Whale.StateObjects {

    [Serializable]
    public class ServerStatus {

        public Guid Id { get; set; }
        public bool ServerOnline { get; set; }
        public double ServerVersion { get; set; }
        public long TimeStamp { get; set; }
    }
}
