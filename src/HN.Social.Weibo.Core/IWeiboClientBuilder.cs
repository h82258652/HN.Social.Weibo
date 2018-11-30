using Microsoft.Extensions.DependencyInjection;

namespace HN.Social.Weibo
{
    public interface IWeiboClientBuilder
    {
        IServiceCollection Services { get; }

        IWeiboClient Build();
    }
}
