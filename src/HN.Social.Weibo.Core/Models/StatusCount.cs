using System.Text.Json.Serialization;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// 该微博的相关数量属性。
    /// </summary>
    public class StatusCount
    {
        /// <summary>
        /// 微博ID。
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// 评论数。
        /// </summary>
        [JsonPropertyName("comments")]
        public int Comments { get; set; }

        /// <summary>
        /// 转发数。
        /// </summary>
        [JsonPropertyName("reposts")]
        public int Reposts { get; set; }

        /// <summary>
        /// 暂未支持。
        /// </summary>
        [JsonPropertyName("attitudes")]
        public int Attitudes { get; set; }
    }
}
