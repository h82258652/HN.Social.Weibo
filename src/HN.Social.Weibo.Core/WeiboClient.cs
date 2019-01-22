using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HN.Social.Weibo.Authorization;
using HN.Social.Weibo.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HN.Social.Weibo
{
    internal class WeiboClient : IWeiboClient
    {
        private readonly SignInManager _signInManager;

        public WeiboClient(IOptions<WeiboOptions> weiboOptions, IAuthorizationProvider authorizationProvider, IAccessTokenStorage accessTokenStorage)
        {
            _signInManager = new SignInManager(weiboOptions, authorizationProvider, accessTokenStorage);
        }

        public bool IsSignIn => _signInManager.IsSignIn;

        /// <summary>
        /// 获取当前已登录的用户 Id。
        /// </summary>
        /// <exception cref="InvalidOperationException">用户未登录。</exception>
        public long UserId
        {
            get
            {
                var accessToken = _signInManager.GetAccessToken();
                if (accessToken == null)
                {
                    throw new InvalidOperationException("用户未登录");
                }
                return accessToken.UserId;
            }
        }

        public Task<T> GetAsync<T>(string uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendAsync<T>(new HttpRequestMessage(HttpMethod.Get, uri), cancellationToken);
        }

        public Task<T> PostAsync<T>(string uri, HttpContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendAsync<T>(new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = content
            }, cancellationToken);
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            using (var client = new WeiboHttpClient(_signInManager))
            {
                var response = await client.SendAsync(request, cancellationToken);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
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
    }
}
