using System;
using HN.Social.Weibo.Authorization;
using HN.Social.Weibo.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    public class WeiboClientBuilder : IWeiboClientBuilder
    {
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
        }

        public IServiceCollection Services { get; }

        public IWeiboClient Build()
        {
            var serviceProvider = Services.BuildServiceProvider();
            if (serviceProvider.GetService<IOptions<WeiboOptions>>() == null)
            {
                throw new Exception("未进行配置，请调用 WithConfig 方法");
            }
            if (serviceProvider.GetService<IAuthorizationProvider>() == null)
            {
                throw new Exception("未配置 AuthorizationProvider");
            }
            if (serviceProvider.GetService<IAccessTokenStorage>() == null)
            {
                throw new Exception("未配置 AccessToken 存储");
            }

            return ActivatorUtilities.CreateInstance<WeiboClient>(serviceProvider);
        }
    }
}
