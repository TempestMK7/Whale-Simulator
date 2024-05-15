using System;
using System.Collections.Generic;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class EquipResponse {

        public List<Guid> UnequippedIds { get; set; }
    }
}
