using System;
using HN.Social.Weibo.Authorization;
using HN.Social.Weibo.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace HN.Social.Weibo
{
    /// <summary>
    /// <see cref="IWeiboClientBuilder" /> 扩展类。
    /// </summary>
    public static class WeiboClientBuilderExtensions
    {
        /// <summary>
        /// 使用 <typeparamref name="T" /> 类型的 <see cref="AccessToken" /> 存储。
        /// </summary>
        /// <typeparam name="T">实现了 <see cref="IAccessTokenStorage" /> 的类。</typeparam>
        /// <param name="builder"><see cref="IWeiboClientBuilder" /> 实例。</param>
        /// <returns><see cref="IWeiboClientBuilder" /> 实例。</returns>
        public static IWeiboClientBuilder UseAccessTokenStorage<T>([NotNull] this IWeiboClientBuilder builder) where T : class, IAccessTokenStorage
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddTransient<IAccessTokenStorage, T>();
            return builder;
        }

        /// <summary>
        /// 使用 <typeparamref name="T" /> 类型的授权提供者。
        /// </summary>
        /// <typeparam name="T">实现了 <see cref="IAuthorizationProvider" /> 的类。</typeparam>
        /// <param name="builder"><see cref="IWeiboClientBuilder" /> 实例。</param>
        /// <returns><see cref="IWeiboClientBuilder" /> 实例。</returns>
        public static IWeiboClientBuilder UseAuthorizationProvider<T>([NotNull] this IWeiboClientBuilder builder) where T : class, IAuthorizationProvider
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddTransient<IAuthorizationProvider, T>();
            return builder;
        }

        /// <summary>
        /// 使用内存 <see cref="AccessToken" /> 存储。
        /// </summary>
        /// <param name="builder"><see cref="IWeiboClientBuilder" /> 实例。</param>
        /// <returns><see cref="IWeiboClientBuilder" /> 实例。</returns>
        public static IWeiboClientBuilder UseMemoryAccessTokenStorage([NotNull] this IWeiboClientBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseAccessTokenStorage<MemoryAccessTokenStorage>();
        }

        /// <summary>
        /// 对 <see cref="IWeiboClientBuilder" /> 进行配置。
        /// </summary>
        /// <param name="builder"><see cref="IWeiboClientBuilder" /> 实例。</param>
        /// <param name="configureOptions">执行配置的委托。</param>
        /// <returns><see cref="IWeiboClientBuilder" /> 实例。</returns>
        public static IWeiboClientBuilder WithConfig([NotNull] this IWeiboClientBuilder builder, [NotNull] Action<WeiboOptions> configureOptions)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            builder.Services.Configure(configureOptions);
            return builder;
        }

        /// <summary>
        /// 对 <see cref="IWeiboClientBuilder" /> 进行配置。
        /// </summary>
        /// <param name="builder"><see cref="IWeiboClientBuilder" /> 实例。</param>
        /// <param name="appKey">申请应用时分配的AppKey。</param>
        /// <param name="appSecret">申请应用时分配的AppSecret。</param>
        /// <param name="redirectUri">授权回调地址，站外应用需与设置的回调地址一致，站内应用需填写canvas page的地址。</param>
        /// <param name="scope">申请scope权限所需参数，可一次申请多个scope权限，用逗号分隔。</param>
        /// <param name="isAutoSignInEnabled">是否在调用微博 API 时自动执行授权操作。默认 <see langword="true" />。</param>
        /// <returns><see cref="IWeiboClientBuilder" /> 实例。</returns>
        public static IWeiboClientBuilder WithConfig(
            [NotNull] this IWeiboClientBuilder builder,
            [NotNull] string appKey,
            [NotNull] string appSecret,
            [NotNull] string redirectUri,
            [CanBeNull] string? scope = null,
            bool isAutoSignInEnabled = true)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (appKey == null)
            {
                throw new ArgumentNullException(nameof(appKey));
            }

            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }

            if (redirectUri == null)
            {
                throw new ArgumentNullException(nameof(redirectUri));
            }

            return builder.WithConfig(options =>
            {
                options.AppKey = appKey;
                options.AppSecret = appSecret;
                options.RedirectUri = redirectUri;
                options.Scope = scope;
                options.IsAutoSignInEnabled = isAutoSignInEnabled;
            });
        }
    }
}
