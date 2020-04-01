using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace HN.Social.Weibo
{
    /// <summary>
    /// <see cref="IWeiboClient" /> 构建器。
    /// </summary>
    public interface IWeiboClientBuilder
    {
        /// <summary>
        /// 服务集合。
        /// </summary>
        [NotNull]
        IServiceCollection Services { get; }

        /// <summary>
        /// 构建 <see cref="IWeiboClient" /> 实例。
        /// </summary>
        /// <returns><see cref="IWeiboClient" /> 实例。</returns>
        [NotNull]
        IWeiboClient Build();
    }
}
