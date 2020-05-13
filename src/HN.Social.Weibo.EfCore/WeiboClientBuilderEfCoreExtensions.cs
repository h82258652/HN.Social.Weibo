using System;
using HN.Social.Weibo.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    /// <summary>
    /// 对 <see cref="IWeiboClientBuilder" /> 使用 Entity Framework Core 进行扩展的扩展类。
    /// </summary>
    public static class WeiboClientBuilderEfCoreExtensions
    {
        /// <summary>
        /// 使用 Entity Framework Core 进行 <see cref="AccessToken" /> 存储。
        /// </summary>
        /// <typeparam name="TDbContext">实现 <see cref="IWeiboDbContext" /> 接口的 Entity Framework Core 数据上下文类型。</typeparam>
        /// <param name="builder"><see cref="IWeiboClientBuilder" /> 实例。</param>
        /// <param name="context">Entity Framework Core 数据上下文实例。</param>
        /// <returns></returns>
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
