using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class Geo
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }
    }
}
