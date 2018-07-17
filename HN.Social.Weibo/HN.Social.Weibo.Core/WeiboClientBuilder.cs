using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    public class WeiboClientBuilder : IWeiboClientBuilder
    {
        public WeiboClientBuilder()
        {
            Services = new ServiceCollection();
            Services.AddSingleton<IWeiboClient, WeiboClient>();
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
            if (serviceProvider.GetService<IAccessTokenStorageService>() == null)
            {
                throw new Exception("未配置 AccessToken 存储");
            }

            return serviceProvider.GetRequiredService<IWeiboClient>();
        }
    }
}