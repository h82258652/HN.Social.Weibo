using System;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;
using JetBrains.Annotations;

namespace HN.Social.Weibo.Authorization
{
    /// <summary>
    /// 授权提供者。
    /// </summary>
    public interface IAuthorizationProvider
    {
        /// <summary>
        /// 执行授权操作。
        /// </summary>
        /// <param name="authorizeUri">授权地址。</param>
        /// <param name="callbackUri">回调地址。</param>
        /// <returns>授权结果。</returns>
        Task<AuthorizeResult> AuthorizeAsync([NotNull] Uri authorizeUri, [NotNull] Uri callbackUri);
    }
}
