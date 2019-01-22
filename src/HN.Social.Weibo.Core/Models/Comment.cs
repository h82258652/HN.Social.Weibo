using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class Comment : WeiboResult
    {
        /// <summary>
        /// 评论创建时间
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// 评论的ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("rootid")]
        public long RootId { get; set; }

        [JsonProperty("floor_number")]
        public int FloorNumber { get; set; }

        /// <summary>
        /// 评论的内容
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("disable_reply")]
        public int DisableReply { get; set; }

        /// <summary>
        /// 评论作者的用户信息字段
        /// </summary>
        [JsonProperty("user")]
        public User User { get; set; }

        /// <summary>
        /// 评论的MID
        /// </summary>
        [JsonProperty("mid")]
        public string Mid { get; set; }

        /// <summary>
        /// 字符串型的评论ID
        /// </summary>
        [JsonProperty("idstr")]
        public string IdString { get; set; }

        /// <summary>
        /// 评论的微博信息字段
        /// </summary>
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("reply_comment")]
        public Comment ReplyComment { get; set; }

        [JsonProperty("appKey")]
        public string AppKey { get; set; }
    }
}
