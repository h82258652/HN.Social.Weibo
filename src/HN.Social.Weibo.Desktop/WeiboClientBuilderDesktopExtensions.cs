using System;

namespace HN.Social.Weibo
{
    public static class WeiboClientBuilderDesktopExtensions
    {
        public static IWeiboClientBuilder UseDefaultAccessTokenStorage(this IWeiboClientBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.UseAccessTokenStorage<DesktopAccessTokenStorageService>();
            return builder;
        }

        public static IWeiboClientBuilder UseDefaultAuthorizationProvider(this IWeiboClientBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.UseAuthorizationProvider<DesktopAuthorizationProvider>();
            return builder;
        }
    }
}
