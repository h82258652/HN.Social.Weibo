using System.Text.Json.Serialization;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// 微博。
    /// </summary>
    public class Status
    {
        /// <summary>
        /// 微博创建时间。
        /// </summary>
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// 微博ID。
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// 微博MID。
        /// </summary>
        [JsonPropertyName("mid")]
        public string Mid { get; set; }

        /// <summary>
        /// 字符串类型的微博ID。
        /// </summary>
        [JsonPropertyName("idstr")]
        public string IdStr { get; set; }

        /// <summary>
        /// 微博信息内容。
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// 微博来源。
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; }

        /// <summary>
        /// 是否已收藏，true：是，false：否。
        /// </summary>
        [JsonPropertyName("favorited")]
        public bool Favorited { get; set; }

        /// <summary>
        /// 是否被截断，true：是，false：否。
        /// </summary>
        [JsonPropertyName("truncated")]
        public bool Truncated { get; set; }

        /// <summary>
        /// （暂未支持）回复ID。
        /// </summary>
        [JsonPropertyName("in_reply_to_status_id")]
        public string InReplyToStatusId { get; set; }

        /// <summary>
        /// （暂未支持）回复人UID。
        /// </summary>
        [JsonPropertyName("in_reply_to_user_id")]
        public string InReplyToUserId { get; set; }

        /// <summary>
        /// （暂未支持）回复人昵称。
        /// </summary>
        [JsonPropertyName("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        /// <summary>
        /// 缩略图片地址（小图），没有时不返回此字段。
        /// </summary>
        [JsonPropertyName("thumbnail_pic")]
        public string? ThumbnailPic { get; set; }

        /// <summary>
        /// 中等尺寸图片地址（中图），没有时不返回此字段。
        /// </summary>
        [JsonPropertyName("bmiddle_pic")]
        public string? BmiddlePic { get; set; }

        /// <summary>
        /// 原始图片地址（原图），没有时不返回此字段。
        /// </summary>
        [JsonPropertyName("original_pic")]
        public string? OriginalPic { get; set; }

        /// <summary>
        /// 地理信息字段。
        /// </summary>
        [JsonPropertyName("geo")]
        public Geo? Geo { get; set; }

        /// <summary>
        /// 微博作者的用户信息字段。
        /// </summary>
        [JsonPropertyName("user")]
        public User? User { get; set; }

        /// <summary>
        /// 被转发的原微博信息字段，当该微博为转发微博时返回。
        /// </summary>
        [JsonPropertyName("retweeted_status")]
        public Status? RetweetedStatus { get; set; }

        /// <summary>
        /// 转发数。
        /// </summary>
        [JsonPropertyName("reposts_count")]
        public int RepostsCount { get; set; }

        /// <summary>
        /// 评论数。
        /// </summary>
        [JsonPropertyName("comments_count")]
        public int CommentsCount { get; set; }

        /// <summary>
        /// 表态数。
        /// </summary>
        [JsonPropertyName("attitudes_count")]
        public int AttitudesCount { get; set; }

        /// <summary>
        /// 暂未支持。
        /// </summary>
        [JsonPropertyName("mlevel")]
        public int Mlevel { get; set; }

        /// <summary>
        /// 微博的可见性及指定可见分组信息。该object中type取值，0：普通微博，1：私密微博，3：指定分组微博，4：密友微博；list_id为分组的组号。
        /// </summary>
        [JsonPropertyName("visible")]
        public Visible Visible { get; set; }

        /// <summary>
        /// 微博配图地址。多图时返回多图链接。无配图返回"[]"。
        /// </summary>
        [JsonPropertyName("pic_urls")]
        public PicUrl[] PicUrls { get; set; }

        /// <summary>
        /// 微博流内的推广微博ID。
        /// </summary>
        [JsonPropertyName("ad")]
        public object[] Ad { get; set; }
    }
}
