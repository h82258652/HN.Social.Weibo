using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    public static class WeiboClientBuilderEfCoreExtensions
    {
        public static IWeiboClientBuilder UseEfCoreAccessTokenStorage<TDbContext>([NotNull] this IWeiboClientBuilder builder, [NotNull] TDbContext context) where TDbContext : DbContext, IWeiboDbContext
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            builder.Services.AddTransient<IAccessTokenStorage>(services =>
            {
                var weiboOptionsAccessor = services.GetRequiredService<IOptions<WeiboOptions>>();
                return new EfCoreAccessTokenStorage<TDbContext>(context, weiboOptionsAccessor);
            });
            return builder;
        }
    }
}
