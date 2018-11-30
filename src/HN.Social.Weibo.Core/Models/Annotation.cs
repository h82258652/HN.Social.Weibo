using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class Annotation
    {
        [JsonProperty("shooting")]
        public int? Shooting { get; set; }

        [JsonProperty("place")]
        public Place Place { get; set; }

        [JsonProperty("client_mblogid")]
        public string ClientMblogId { get; set; }

        [JsonProperty("mapi_request")]
        public bool? MapiRequest { get; set; }
    }
}
