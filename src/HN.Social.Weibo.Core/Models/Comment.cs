using System.Text.Json.Serialization;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// 评论。
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// 评论创建时间。
        /// </summary>
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; } = default!;

        /// <summary>
        /// 评论的ID。
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// 评论的内容。
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; } = default!;

        /// <summary>
        /// 评论的来源。
        /// </summary>
        [JsonPropertyName("source")]
        public string? Source { get; set; }

        /// <summary>
        /// 评论作者的用户信息字段。
        /// </summary>
        [JsonPropertyName("user")]
        public User? User { get; set; }

        /// <summary>
        /// 评论的MID。
        /// </summary>
        [JsonPropertyName("mid")]
        public string Mid { get; set; } = default!;

        /// <summary>
        /// 字符串型的评论ID。
        /// </summary>
        [JsonPropertyName("idstr")]
        public string IdStr { get; set; } = default!;

        /// <summary>
        /// 评论的微博信息字段。
        /// </summary>
        [JsonPropertyName("status")]
        public Status? Status { get; set; }

        /// <summary>
        /// 评论来源评论，当本评论属于对另一评论的回复时返回此字段。
        /// </summary>
        [JsonPropertyName("reply_comment")]
        public Comment? ReplyComment { get; set; }
    }
}
