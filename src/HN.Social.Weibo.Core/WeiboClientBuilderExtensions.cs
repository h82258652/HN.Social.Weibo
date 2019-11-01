using System;
using HN.Social.Weibo.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace HN.Social.Weibo
{
    public static class WeiboClientBuilderExtensions
    {
        public static IWeiboClientBuilder UseAccessTokenStorage<T>(this IWeiboClientBuilder builder) where T : class, IAccessTokenStorage
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddTransient<IAccessTokenStorage, T>();
            return builder;
        }

        public static IWeiboClientBuilder UseAuthorizationProvider<T>(this IWeiboClientBuilder builder) where T : class, IAuthorizationProvider
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddTransient<IAuthorizationProvider, T>();
            return builder;
        }

        public static IWeiboClientBuilder WithConfig(this IWeiboClientBuilder builder, string appKey, string appSecret, string redirectUri)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(appKey))
            {
                throw new ArgumentException("AppKey 为空");
            }
            if (string.IsNullOrEmpty(appSecret))
            {
                throw new ArgumentException("AppSecret 为空");
            }
            if (string.IsNullOrEmpty(redirectUri))
            {
                throw new ArgumentException("RedirectUri 为空");
            }

            builder.Services.Configure<WeiboOptions>(options =>
            {
                options.AppKey = appKey;
                options.AppSecret = appSecret;
                options.RedirectUri = redirectUri;
            });
            return builder;
        }
    }
}
