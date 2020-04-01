using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace HN.Social.Weibo.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection" /> 扩展类。
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 获取服务是否已添加到服务集合中。
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection" /> 实例。</param>
        /// <param name="type">服务类型。</param>
        /// <returns><see cref="IServiceCollection" /> 实例。</returns>
        public static bool IsAdded(this IServiceCollection services, Type type)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.Any(temp => temp.ServiceType == type);
        }

        /// <summary>
        /// 获取服务是否已添加到服务集合中。
        /// </summary>
        /// <typeparam name="T">服务类型。</typeparam>
        /// <param name="services"><see cref="IServiceCollection" /> 实例。</param>
        /// <returns><see cref="IServiceCollection" /> 实例。</returns>
        public static bool IsAdded<T>(this IServiceCollection services)
        {
            return services.IsAdded(typeof(T));
        }
    }
}
