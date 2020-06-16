using System;

namespace HN.Social.Weibo.Models
{
    /// <summary>
    /// Access token。
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// 到期时间。
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// 用户 Id。
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Access token 的值。
        /// </summary>
        public string Value { get; set; } = default!;
    }
}
