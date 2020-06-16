using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    internal class WeiboClient : IWeiboClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly SignInManager _signInManager;

        public WeiboClient(
            [NotNull] IServiceProvider serviceProvider,
            [NotNull] IHttpClientFactory httpClientFactory,
            [NotNull] SignInManager signInManager,
            [NotNull] IOptions<JsonSerializerOptions> serializerOptionsAccessor)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (httpClientFactory == null)
            {
                throw new ArgumentNullException(nameof(httpClientFactory));
            }

            if (signInManager == null)
            {
                throw new ArgumentNullException(nameof(signInManager));
            }

            if (serializerOptionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(serializerOptionsAccessor));
            }

            Services = serviceProvider;
            _httpClientFactory = httpClientFactory;
            _signInManager = signInManager;
            _serializerOptions = serializerOptionsAccessor.Value;
        }

        public bool IsSignIn => _signInManager.IsSignIn;

        public IServiceProvider Services { get; }

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
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            return SendAsync<T>(new HttpRequestMessage(HttpMethod.Get, uri), cancellationToken);
        }

        public Task<T> PostAsync<T>(string uri, HttpContent? content, CancellationToken cancellationToken = default)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

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
            return JsonSerializer.Deserialize<T>(json, _serializerOptions);
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
