using System.Net.Http;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;

namespace HN.Social.Weibo
{
    public interface IWeiboClient
    {
        Task<T> GetAsync<T>(string uri) where T : WeiboResult;

        Task<long?> GetCurrentUserId();

        Task<bool> IsSignIn();

        Task<T> PostAsync<T>(string uri, HttpContent content) where T : WeiboResult;

        Task<T> SendAsync<T>(HttpRequestMessage request) where T : WeiboResult;

        Task SignInAsync();

        Task SignOutAsync();
    }
}