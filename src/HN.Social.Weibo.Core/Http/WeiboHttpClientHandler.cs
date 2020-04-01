using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HN.Social.Weibo.Models;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo.Http
{
    internal class WeiboHttpClientHandler : DelegatingHandler
    {
        private readonly IAccessTokenStorage _accessTokenStorage;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly SignInManager _signInManager;
        private readonly WeiboOptions _weiboOptions;

        public WeiboHttpClientHandler(
            SignInManager signInManager,
            IAccessTokenStorage accessTokenStorage,
            IOptions<JsonSerializerOptions> serializerOptionsAccesser,
            IOptions<WeiboOptions> weiboOptionsAccesser)
            : base(new HttpClientHandler())
        {
            _signInManager = signInManager;
            _accessTokenStorage = accessTokenStorage;
            _serializerOptions = serializerOptionsAccesser.Value;
            _weiboOptions = weiboOptionsAccesser.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string? accessToken;
            if (_weiboOptions.IsAutoSignInEnabled)
            {
                accessToken = (await _signInManager.SignInAndGetAccessTokenAsync()).Value;
            }
            else
            {
                accessToken = _accessTokenStorage.Load()?.Value;
            }

            if (accessToken != null)
            {
                var uriBuilder = new UriBuilder(request.RequestUri);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["access_token"] = accessToken;
                uriBuilder.Query = query.ToString();
                request.RequestUri = uriBuilder.Uri;
            }
            
            var response = await base.SendAsync(request, cancellationToken);

            if (response.Content is StreamContent streamContent)
            {
                var bytes = await streamContent.ReadAsByteArrayAsync();
                response.Content = new ByteArrayContent(bytes);
            }

            await CheckResponseErrorAsync(response);

            return response;
        }

        private async Task CheckResponseErrorAsync(HttpResponseMessage response)
        {
            WeiboError? error;
            try
            {
                var json = await response.Content.ReadAsStringAsync();
                error = JsonSerializer.Deserialize<WeiboError>(json, _serializerOptions);
                if (error.ErrorCode == Constants.UserRemoveAuthorizationCode)
                {
                    await _signInManager.SignOutAsync();
                }
            }
            catch
            {
                return;
            }

            if (error != null && error.ErrorCode != 0)
            {
                throw new WeiboApiException(error);
            }
        }
    }
}
