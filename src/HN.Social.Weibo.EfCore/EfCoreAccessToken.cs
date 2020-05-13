using System;
using HN.Social.Weibo.Models;

namespace HN.Social.Weibo
{
    /// <summary>
    /// <see cref="AccessToken" /> 的数据库实体。
    /// </summary>
    public class EfCoreAccessToken
    {
        /// <inheritdoc cref="AccessToken.ExpiresAt" />
        public DateTime ExpiresAt { get; set; }
        
        /// <summary>
        /// Id。
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 拥有该 <see cref="AccessToken" /> 的 AppKey。
        /// </summary>
        public string OwnerAppKey { get; set; }

        /// <inheritdoc cref="AccessToken.UserId" />
        public long UserId { get; set; }

        /// <inheritdoc cref="AccessToken.Value" />
        public string Value { get; set; }
    }
}
