using System.Text.Json.Serialization;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// 地理信息。
    /// </summary>
    public class Geo
    {
        /// <summary>
        /// 类型。
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = default!;

        /// <summary>
        /// 坐标。
        /// </summary>
        [JsonPropertyName("coordinates")]
        public double[] Coordinates { get; set; } = default!;
    }
}
