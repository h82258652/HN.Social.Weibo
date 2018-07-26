using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class Attitude
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}