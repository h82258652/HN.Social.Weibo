using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class Place
    {
        [JsonProperty("poiid")]
        public string PoiId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
