using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HN.Social.Weibo
{
    internal class WeiboClient : IWeiboClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SignInManager _signInManager;

        public WeiboClient(
            IHttpClientFactory httpClientFactory, 
            SignInManager signInManager)
        {
            _httpClientFactory = httpClientFactory;
            _signInManager = signInManager;
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

        public Task<T> GetAsync<T>(string uri, CancellationToken cancellationToken = default)
        {
            return SendAsync<T>(new HttpRequestMessage(HttpMethod.Get, uri), cancellationToken);
        }

        public Task<T> PostAsync<T>(string uri, HttpContent content, CancellationToken cancellationToken = default)
        {
            return SendAsync<T>(new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = content
            }, cancellationToken);
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var client = _httpClientFactory.CreateClient(Constants.HttpClientName);
            var response = await client.SendAsync(request, cancellationToken);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
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
