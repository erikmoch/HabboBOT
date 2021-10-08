using Newtonsoft.Json;

namespace HabboBOT.Core.Messages
{
    public class Incoming
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}