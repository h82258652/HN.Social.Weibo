using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HN.Social.Weibo.Models;
using Newtonsoft.Json;

namespace HN.Social.Weibo.Http
{
    internal class WeiboHttpClientHandler : DelegatingHandler
    {
        private readonly SignInManager _signInManager;

        public WeiboHttpClientHandler(SignInManager signInManager) : base(new HttpClientHandler())
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

            await CheckResponseErrorCodeAsync(response);

            return response;
        }
        
        [DebuggerStepThrough]
        private async Task CheckResponseErrorCodeAsync(HttpResponseMessage response)
        {
            try
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<WeiboResult>(json);
                if (result.ErrorCode == (int)WeiboErrorCode.UserRemoveAuthorization)
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
