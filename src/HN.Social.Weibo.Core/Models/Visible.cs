using System.Text.Json.Serialization;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// 微博可见性。
    /// </summary>
    public class Visible
    {
        /// <summary>
        /// 0：普通微博，1：私密微博，3：指定分组微博，4：密友微博。
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }

        /// <summary>
        /// 分组的组号。
        /// </summary>
        [JsonPropertyName("list_id")]
        public int ListId { get; set; }
    }
}
