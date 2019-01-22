using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class User : WeiboResult
    {
        /// <summary>
        /// 用户UID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// 字符串型的用户UID
        /// </summary>
        [JsonProperty("idstr")]
        public string IdString { get; set; }

        [JsonProperty("class")]
        public int Class { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [JsonProperty("screen_name")]
        public string Nickname { get; set; }

        /// <summary>
        /// 友好显示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户所在省级ID
        /// </summary>
        [JsonProperty("province")]
        public int ProvinceId { get; set; }

        /// <summary>
        /// 用户所在城市ID
        /// </summary>
        [JsonProperty("city")]
        public int CityId { get; set; }

        /// <summary>
        /// 用户所在地
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// 用户个人描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 用户博客地址
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// 用户头像地址（中图），50x50像素
        /// </summary>
        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("cover_image_phone")]
        public string CoverImagePhone { get; set; }

        /// <summary>
        /// 用户的微博统一URL地址
        /// </summary>
        [JsonProperty("profile_url")]
        public string ProfileUrl { get; set; }

        /// <summary>
        /// 用户的个性化域名
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// 用户的微号
        /// </summary>
        [JsonProperty("weihao")]
        public string Weihao { get; set; }

        /// <summary>
        /// 性别，m：男、f：女、n：未知
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// 粉丝数
        /// </summary>
        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        /// <summary>
        /// 关注数
        /// </summary>
        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }

        [JsonProperty("pagefriends_count")]
        public int PageFriendsCount { get; set; }

        /// <summary>
        /// 微博数
        /// </summary>
        [JsonProperty("statuses_count")]
        public int StatusesCount { get; set; }

        [JsonProperty("video_status_count")]
        public int VideoStatusCount { get; set; }

        /// <summary>
        /// 收藏数
        /// </summary>
        [JsonProperty("favourites_count")]
        public int FavouritesCount { get; set; }

        /// <summary>
        /// 用户创建（注册）时间
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// 暂未支持
        /// </summary>
        [JsonProperty("following")]
        public bool Following { get; set; }

        /// <summary>
        /// 是否允许所有人给我发私信，true：是，false：否
        /// </summary>
        [JsonProperty("allow_all_act_msg")]
        public bool AllowAllActMsg { get; set; }

        /// <summary>
        /// 是否允许标识用户的地理位置，true：是，false：否
        /// </summary>
        [JsonProperty("geo_enabled")]
        public bool GeoEnabled { get; set; }

        /// <summary>
        /// 是否是微博认证用户，即加V用户，true：是，false：否
        /// </summary>
        [JsonProperty("verified")]
        public bool Verified { get; set; }

        /// <summary>
        /// 暂未支持
        /// </summary>
        [JsonProperty("verified_type")]
        public int VerifiedType { get; set; }

        /// <summary>
        /// 用户备注信息，只有在查询用户关系时才返回此字段
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("insecurity")]
        public Insecurity Insecurity { get; set; }

        /// <summary>
        /// 用户的最近一条微博信息字段
        /// </summary>
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("ptype")]
        public int PType { get; set; }

        /// <summary>
        /// 是否允许所有人对我的微博进行评论，true：是，false：否
        /// </summary>
        [JsonProperty("allow_all_comment")]
        public bool AllowAllComment { get; set; }

        /// <summary>
        /// 用户头像地址（大图），180×180像素
        /// </summary>
        [JsonProperty("avatar_large")]
        public string AvatarLarge { get; set; }

        /// <summary>
        /// 用户头像地址（高清），高清头像原图
        /// </summary>
        [JsonProperty("avatar_hd")]
        public string AvatarHD { get; set; }

        /// <summary>
        /// 认证原因
        /// </summary>
        [JsonProperty("verified_reason")]
        public string VerifiedReason { get; set; }

        [JsonProperty("verified_trade")]
        public string VerifiedTrade { get; set; }

        [JsonProperty("verified_reason_url")]
        public string VerifiedReasonUrl { get; set; }

        [JsonProperty("verified_source")]
        public string VerifiedSource { get; set; }

        [JsonProperty("verified_source_url")]
        public string VerifiedSourceUrl { get; set; }

        /// <summary>
        /// 该用户是否关注当前登录用户，true：是，false：否
        /// </summary>
        [JsonProperty("follow_me")]
        public bool FollowMe { get; set; }

        [JsonProperty("like")]
        public bool Like { get; set; }

        [JsonProperty("like_me")]
        public bool LikeMe { get; set; }

        /// <summary>
        /// 用户的在线状态，0：不在线、1：在线
        /// </summary>
        [JsonProperty("online_status")]
        public int OnlineStatus { get; set; }

        /// <summary>
        /// 用户的互粉数
        /// </summary>
        [JsonProperty("bi_followers_count")]
        public int BiFollowersCount { get; set; }

        /// <summary>
        /// 用户当前的语言版本，zh-cn：简体中文，zh-tw：繁体中文，en：英语
        /// </summary>
        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("star")]
        public int Star { get; set; }

        [JsonProperty("mbtype")]
        public int Mbtype { get; set; }

        [JsonProperty("mbrank")]
        public int Mbrank { get; set; }

        [JsonProperty("block_word")]
        public int BlockWord { get; set; }

        [JsonProperty("block_app")]
        public int BlockApp { get; set; }

        [JsonProperty("credit_score")]
        public int CreditScore { get; set; }

        [JsonProperty("user_ability")]
        public int UserAbility { get; set; }

        [JsonProperty("urank")]
        public int Urank { get; set; }

        [JsonProperty("story_read_state")]
        public int StoryReadState { get; set; }

        [JsonProperty("vclub_member")]
        public int VclubMember { get; set; }
    }
}
