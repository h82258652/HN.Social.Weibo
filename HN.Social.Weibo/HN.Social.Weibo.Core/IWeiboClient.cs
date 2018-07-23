using System.Net.Http;
using System.Threading.Tasks;

namespace HN.Social.Weibo
{
    public interface IWeiboClient
    {
        Task<T> GetAsync<T>(string uri);

        Task<long?> GetCurrentUserId();

        Task<bool> IsSignIn();

        Task<T> PostAsync<T>(string uri, HttpContent content);

        Task<T> SendAsync<T>(HttpRequestMessage request);

        Task SignInAsync();

        Task SignOutAsync();
    }
}