using System;
using System.Text.Json;
using HN.Social.Weibo.Authorization;
using HN.Social.Weibo.Extensions;
using HN.Social.Weibo.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    /// <inheritdoc />
    public class WeiboClientBuilder : IWeiboClientBuilder
    {
        /// <summary>
        /// 初始化 <see cref="WeiboClientBuilder" /> 类的新实例。
        /// </summary>
        public WeiboClientBuilder()
        {
            Services = new ServiceCollection();
            Services.AddTransient<WeiboHttpClientHandler>();
            Services
                .AddHttpClient(Constants.HttpClientName, client =>
                {
                    client.BaseAddress = new Uri(Constants.WeiboApiUrlBase);
                })
                .ConfigurePrimaryHttpMessageHandler(serviceProvider => serviceProvider.GetRequiredService<WeiboHttpClientHandler>());
            Services.AddTransient<SignInManager>();
            Services.Configure<JsonSerializerOptions>(options =>
            {
                options.PropertyNameCaseInsensitive = true;
            });
        }

        /// <inheritdoc />
        public IServiceCollection Services { get; }

        /// <inheritdoc />
        public IWeiboClient Build()
        {
            if (!Services.IsAdded<IAuthorizationProvider>())
            {
                throw new WeiboException("未配置 AuthorizationProvider");
            }

            if (!Services.IsAdded<IAccessTokenStorage>())
            {
                throw new WeiboException("未配置 AccessToken 存储");
            }

            var serviceProvider = Services.BuildServiceProvider();
            if (serviceProvider.GetService<IOptions<WeiboOptions>>() == null)
            {
                throw new WeiboException("未进行配置，请调用 WithConfig 方法");
            }

            return ActivatorUtilities.CreateInstance<WeiboClient>(serviceProvider);
        }
    }
}
