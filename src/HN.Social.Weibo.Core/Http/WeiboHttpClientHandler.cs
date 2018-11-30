using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HN.Social.Weibo.Models;

namespace HN.Social.Weibo.Http
{
    internal class WeiboHttpClientHandler : DelegatingHandler
    {
        private readonly SignInManager _signInManager;

        internal WeiboHttpClientHandler(SignInManager signInManager) : base(new HttpClientHandler())
        {
            _signInManager = signInManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _signInManager.SignInAndGetAccessTokenAsync();

            var uriBuilder = new UriBuilder(request.RequestUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["access_token"] = accessToken.Value;
            uriBuilder.Query = query.ToString();
            request.RequestUri = uriBuilder.Uri;

            var response = await base.SendAsync(request, cancellationToken);

            if (response.Content is StreamContent)
            {
                var bytes = await response.Content.ReadAsByteArrayAsync();
                response.Content = new ByteArrayContent(bytes);
            }

            CheckResponseErrorCode(response);

            return response;
        }

        [DebuggerStepThrough]
        private async void CheckResponseErrorCode(HttpResponseMessage response)
        {
            try
            {
                var result = await response.Content.ReadAsJsonAsync<WeiboResult>();
                if (result.ErrorCode == Constants.UserRemoveAuthorizationErrorCode)
                {
                    await _signInManager.SignOutAsync();
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
