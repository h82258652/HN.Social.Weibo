using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HN.Social.Weibo.Authorization
{
    public abstract class AuthorizationProviderBase : IAuthorizationProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly WeiboOptions _weiboOptions;

        protected AuthorizationProviderBase(
            IHttpClientFactory httpClientFactory,
            IOptions<WeiboOptions> weiboOptionsAccesser)
        {
            _httpClientFactory = httpClientFactory;
            _weiboOptions = weiboOptionsAccesser.Value;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(Uri authorizationUri, Uri callbackUri)
        {
            var authorizationCode = await GetAuthorizationCodeAsync(authorizationUri, callbackUri);
            var client = _httpClientFactory.CreateClient();
            var postData = new Dictionary<string, string>
            {
                ["client_id"] = _weiboOptions.AppKey,
                ["client_secret"] = _weiboOptions.AppSecret,
                ["grant_type"] = "authorization_code",
                ["code"] = authorizationCode,
                ["redirect_uri"] = _weiboOptions.RedirectUri
            };

            var postContent = new FormUrlEncodedContent(postData);

            var response = await client.PostAsync("https://api.weibo.com/oauth2/access_token", postContent);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthorizationResult>(json);
        }

        protected abstract Task<string> GetAuthorizationCodeAsync(Uri authorizationUri, Uri callbackUri);
    }
}
