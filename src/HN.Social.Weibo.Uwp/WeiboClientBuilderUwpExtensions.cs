using System;

namespace HN.Social.Weibo
{
    public static class WeiboClientBuilderUwpExtensions
    {
        public static IWeiboClientBuilder UseDefaultAccessTokenStorage(this IWeiboClientBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.UseAccessTokenStorage<UwpAccessTokenStorageService>();
            return builder;
        }

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
