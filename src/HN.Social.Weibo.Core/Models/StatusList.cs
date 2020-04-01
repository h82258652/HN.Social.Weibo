using System.Text.Json.Serialization;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// 微博列表。
    /// </summary>
    public class StatusList
    {
        /// <summary>
        /// 微博。
        /// </summary>
        [JsonPropertyName("statuses")]
        public Status[] Statuses { get; set; }

        /// <summary>
        /// 微博流内的推广微博ID。
        /// </summary>
        [JsonPropertyName("ad")]
        public object[]? Ad { get; set; }
    }
}
