using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class WeiboResult
    {
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }

        [JsonProperty("error")]
        public string ErrorMessage { get; set; }

        [JsonProperty("request")]
        public string Request { get; set; }
    }
}
