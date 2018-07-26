using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class Status : WeiboResult
    {
        /// <summary>
        /// 微博创建时间
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// 微博ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// 字符串型的微博ID
        /// </summary>
        [JsonProperty("idstr")]
        public string IdString { get; set; }

        /// <summary>
        /// 微博MID
        /// </summary>
        [JsonProperty("mid")]
        public long Mid { get; set; }

        [JsonProperty("can_edit")]
        public bool CanEdit { get; set; }

        /// <summary>
        /// 微博信息内容
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("textLength")]
        public int? TextLength { get; set; }

        [JsonProperty("source_allowclick")]
        public int SourceAllowClick { get; set; }

        [JsonProperty("source_type")]
        public int SourceType { get; set; }

        /// <summary>
        /// 微博来源
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// 是否已收藏，true：是，false：否
        /// </summary>
        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        /// <summary>
        /// 是否被截断，true：是，false：否
        /// </summary>
        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        /// <summary>
        /// （暂未支持）回复ID
        /// </summary>
        [JsonProperty("in_reply_to_status_id")]
        public string InReplyToStatusId { get; set; }

        /// <summary>
        /// （暂未支持）回复人ID
        /// </summary>
        [JsonProperty("in_reply_to_user_id")]
        public string InReplyToUserId { get; set; }

        /// <summary>
        /// （暂未支持）回复人昵称
        /// </summary>
        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        [JsonProperty("pic_urls")]
        public PicUrl[] PicUrls { get; set; }

        /// <summary>
        /// 地理信息字段
        /// </summary>
        [JsonProperty("geo")]
        public Geo Geo { get; set; }

        [JsonProperty("is_paid")]
        public bool IsPaid { get; set; }

        [JsonProperty("mblog_vip_type")]
        public int MblogVipType { get; set; }

        [JsonProperty("annotations")]
        public Annotation[] Annotations { get; set; }

        /// <summary>
        /// 微博作者的用户信息字段
        /// </summary>
        [JsonProperty("user")]
        public UserInfo User { get; set; }

        /// <summary>
        /// 转发数
        /// </summary>
        [JsonProperty("reposts_count")]
        public int RepostsCount { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        [JsonProperty("comments_count")]
        public int CommentsCount { get; set; }

        /// <summary>
        /// 表态数
        /// </summary>
        [JsonProperty("attitudes_count")]
        public int AttitudesCount { get; set; }

        [JsonProperty("pending_approval_count")]
        public int PendingApprovalCount { get; set; }

        [JsonProperty("isLongText")]
        public bool IsLongText { get; set; }

        [JsonProperty("multi_attitude")]
        public Attitude[] MultiAttitude { get; set; }

        [JsonProperty("hide_flag")]
        public int HideFlag { get; set; }

        /// <summary>
        /// 暂未支持
        /// </summary>
        [JsonProperty("mlevel")]
        public int Mlevel { get; set; }

        /// <summary>
        /// 微博的可见性及指定可见分组信息。该object中type取值，0：普通微博，1：私密微博，3：指定分组微博，4：密友微博；list_id为分组的组号
        /// </summary>
        [JsonProperty("visible")]
        public Visible Visible { get; set; }

        [JsonProperty("biz_ids")]
        public int[] BizIds { get; set; }

        [JsonProperty("biz_feature")]
        public long BizFeature { get; set; }

        [JsonProperty("hasActionTypeCard")]
        public int HasActionTypeCard { get; set; }

        [JsonProperty("darwin_tags")]
        public object[] DarwinTags { get; set; }

        [JsonProperty("hot_weibo_tags")]
        public object[] HotWeiboTags { get; set; }

        [JsonProperty("text_tag_tips")]
        public object[] TextTagTips { get; set; }

        [JsonProperty("mblogtype")]
        public int? MblogType { get; set; }

        [JsonProperty("rid")]
        public string Rid { get; set; }

        [JsonProperty("userType")]
        public int UserType { get; set; }

        [JsonProperty("more_info_type")]
        public int MoreInfoType { get; set; }

        [JsonProperty("positive_recom_flag")]
        public int PositiveRecomFlag { get; set; }

        [JsonProperty("content_auth")]
        public int ContentAuth { get; set; }

        [JsonProperty("gif_ids")]
        public string GifIds { get; set; }

        [JsonProperty("is_show_bulletin")]
        public int IsShowBulletin { get; set; }

        [JsonProperty("comment_manage_info")]
        public CommentManageInfo CommentManageInfo { get; set; }
    }
}