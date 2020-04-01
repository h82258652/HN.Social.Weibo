using System.Text.Json.Serialization;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// 该用户的相关数量属性。
    /// </summary>
    public class UserCount
    {
        /// <summary>
        /// 微博ID。
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// 粉丝数。
        /// </summary>
        [JsonPropertyName("followers_count")]
        public int FollowersCount { get; set; }

        /// <summary>
        /// 关注数。
        /// </summary>
        [JsonPropertyName("friends_count")]
        public int FriendsCount { get; set; }

        /// <summary>
        /// 微博数。
        /// </summary>
        [JsonPropertyName("statuses_count")]
        public int StatusesCount { get; set; }

        /// <summary>
        /// 暂未支持。
        /// </summary>
        [JsonPropertyName("private_friends_count")]
        public int PrivateFriendsCount { get; set; }
    }
}
