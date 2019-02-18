using System;
using HN.Social.Weibo.Authorization;

namespace HN.Social.Weibo
{
    public static class WeiboClientBuilderUwpExtensions
    {
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
