using System.Collections.Generic;

using Newtonsoft.Json;

namespace HabboBOT.Core.Messages
{
    public class HeaderModel
    {
        [JsonProperty("Incoming")]
        public List<Incoming> Incoming { get; set; }

        [JsonProperty("Outgoing")]
        public List<Outgoing> Outgoing { get; set; }
    }
}
