using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class Insecurity
    {
        [JsonProperty("sexual_content")]
        public bool SexualContent { get; set; }
    }
}
