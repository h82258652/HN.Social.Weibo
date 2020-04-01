using System.Text.Json.Serialization;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// 微博错误。
    /// </summary>
    public class WeiboError
    {
        /// <summary>
        /// 错误消息。
        /// </summary>
        [JsonPropertyName("error")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 错误代码。
        /// </summary>
        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// 请求地址。
        /// </summary>
        [JsonPropertyName("request")]
        public string Request { get; set; }
    }
}
