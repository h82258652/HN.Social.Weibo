using System;
using HN.Social.Weibo.Authorization;
using HN.Social.Weibo.Models;

namespace HN.Social.Weibo
{
    /// <summary>
    /// <see cref="IWeiboClientBuilder" /> 扩展类。
    /// </summary>
    public static class WeiboClientBuilderUwpExtensions
    {
        /// <summary>
        /// 使用默认的 <see cref="AccessToken" /> 存储。
        /// </summary>
        /// <param name="builder"><see cref="IWeiboClientBuilder" /> 实例。</param>
        /// <returns><see cref="IWeiboClientBuilder" /> 实例。</returns>
        public static IWeiboClientBuilder UseDefaultAccessTokenStorage(this IWeiboClientBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseAccessTokenStorage<UwpAccessTokenStorage>();
        }

        /// <summary>
        /// 使用默认的授权提供者。
        /// </summary>
        /// <param name="builder"><see cref="IWeiboClientBuilder" /> 实例。</param>
        /// <returns><see cref="IWeiboClientBuilder" /> 实例。</returns>
        public static IWeiboClientBuilder UseDefaultAuthorizationProvider(this IWeiboClientBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.UseAuthorizationProvider<UwpAuthorizationProvider>();
            return builder;
        }
    }
}
