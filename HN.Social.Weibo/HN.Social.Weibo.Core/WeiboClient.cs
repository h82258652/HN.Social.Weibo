using System;
using System.Net.Http;
using System.Threading.Tasks;
using HN.Social.Weibo.Http;
using HN.Social.Weibo.Models;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    internal class WeiboClient : IWeiboClient
    {
        private readonly SignInManager _signInManager;

        public WeiboClient(IOptions<WeiboOptions> weiboOptions, IAuthorizationProvider authorizationProvider, IAccessTokenStorageService accessTokenStorageService)
        {
            _signInManager = new SignInManager(weiboOptions, authorizationProvider, accessTokenStorageService);
        }

        public async Task<T> GetAsync<T>(string uri) where T : WeiboResult
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync(uri);
                return await response.Content.ReadAsJsonAsync<T>();
            }
        }

        public async Task<long?> GetCurrentUserId()
        {
            var accessToken = await _signInManager.GetAccessToken();
            return accessToken?.UserId;
        }

        public Task<bool> IsSignIn()
        {
            return _signInManager.IsSignIn();
        }

        public async Task<T> PostAsync<T>(string uri, HttpContent content) where T : WeiboResult
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            using (var client = CreateHttpClient())
            {
                var response = await client.PostAsync(uri, content);
                return await response.Content.ReadAsJsonAsync<T>();
            }
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage request) where T : WeiboResult
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            using (var client = CreateHttpClient())
            {
                var response = await client.SendAsync(request);
                return await response.Content.ReadAsJsonAsync<T>();
            }
        }

        public Task SignInAsync()
        {
            return _signInManager.SignInAsync();
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        private WeiboHttpClient CreateHttpClient()
        {
            return new WeiboHttpClient(_signInManager);
        }
    }
}