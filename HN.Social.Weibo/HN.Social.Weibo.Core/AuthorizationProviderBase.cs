using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HN.Social.Weibo.Http;
using HN.Social.Weibo.Models;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    public abstract class AuthorizationProviderBase : IAuthorizationProvider
    {
        private readonly WeiboOptions _weiboOptions;

        protected AuthorizationProviderBase(IOptions<WeiboOptions> weiboOptions)
        {
            _weiboOptions = weiboOptions.Value;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(Uri authorizationUri, Uri callbackUri)
        {
            var authorizationCode = await GetAuthorizationCodeAsync(authorizationUri, callbackUri);
            using (var client = new HttpClient())
            {
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
                return await response.Content.ReadAsJsonAsync<AuthorizationResult>();
            }
        }

        protected abstract Task<string> GetAuthorizationCodeAsync(Uri authorizationUri, Uri callbackUri);
    }
}