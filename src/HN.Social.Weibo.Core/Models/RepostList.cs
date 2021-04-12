using System.Text.Json.Serialization;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// 转发微博列表。
    /// </summary>
    public class RepostList
    {
        /// <summary>
        /// 微博。
        /// </summary>
        [JsonPropertyName("reposts")]
        public Status[] Reposts { get; set; } = default!;
    }
}
