using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class Visible
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("list_id")]
        public int ListId { get; set; }
    }
}