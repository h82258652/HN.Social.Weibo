using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HN.Social.Weibo
{
    public interface IWeiboClient
    {
        bool IsSignIn { get; }

        long UserId { get; }

        Task<T> GetAsync<T>(string uri, CancellationToken cancellationToken = default(CancellationToken));

        Task<T> PostAsync<T>(string uri, HttpContent content, CancellationToken cancellationToken = default(CancellationToken));

        Task<T> SendAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken));

        Task SignInAsync();

        Task SignOutAsync();
    }
}
