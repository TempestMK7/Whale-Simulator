using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Tempest.Whale.RequestObjects {

    [Serializable]
    public class ClaimResourcesRequest {

        public bool Verified { get; set; }
        public Guid AccountGuid { get; set; }
    }
}

