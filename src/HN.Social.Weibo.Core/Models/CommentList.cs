using System.Text.Json.Serialization;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// 评论列表。
    /// </summary>
    public class CommentList
    {
        /// <summary>
        /// 评论。
        /// </summary>
        [JsonPropertyName("comments")]
        public Comment[] Comments { get; set; }

        /// <summary>
        /// 上一条游标。
        /// </summary>
        [JsonPropertyName("previous_cursor")]
        public long PreviousCursor { get; set; }

        /// <summary>
        /// 下一条游标。
        /// </summary>
        [JsonPropertyName("next_cursor")]
        public long NextCursor { get; set; }

        /// <summary>
        /// 总数。
        /// </summary>
        [JsonPropertyName("total_number")]
        public int TotalNumber { get; set; }
    }
}
