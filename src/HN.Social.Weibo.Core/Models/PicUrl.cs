using System.Text.Json.Serialization;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// 微博配图地址。
    /// </summary>
    public class PicUrl
    {
        /// <summary>
        /// 图片链接。
        /// </summary>
        [JsonPropertyName("thumbnail_pic")]
        public string ThumbnailPic { get; set; } = default!;
    }
}
