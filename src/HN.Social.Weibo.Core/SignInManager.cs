using System;
using System.Threading.Tasks;
using HN.Social.Weibo.Authorization;
using HN.Social.Weibo.Models;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    internal class SignInManager
    {
        private readonly IAccessTokenStorage _accessTokenStorage;
        private readonly IAuthorizationProvider _authorizationProvider;
        private readonly WeiboOptions _weiboOptions;

        internal SignInManager(IOptions<WeiboOptions> weiboOptions, IAuthorizationProvider authorizationProvider, IAccessTokenStorage accessTokenStorage)
        {
            _weiboOptions = weiboOptions.Value;
            _authorizationProvider = authorizationProvider;
            _accessTokenStorage = accessTokenStorage;
        }

        internal bool IsSignIn => GetAccessToken() != null;

        internal AccessToken GetAccessToken()
        {
            var accessToken = _accessTokenStorage.Load();
            if (accessToken == null)
            {
                return null;
            }

            if (accessToken.ExpiresAt <= DateTime.Now)
            {
                _accessTokenStorage.Clear();
                return null;
            }

            return accessToken;
        }

        internal async Task<AccessToken> SignInAndGetAccessTokenAsync()
        {
            var accessToken = GetAccessToken();
            if (accessToken != null)
            {
                return accessToken;
            }

            var requestTime = DateTime.Now;
            var authorizationUri = new Uri($"https://api.weibo.com/oauth2/authorize?client_id={_weiboOptions.AppKey}&redirect_uri={_weiboOptions.RedirectUri}");
            var authorizationResult = await _authorizationProvider.AuthorizeAsync(authorizationUri, new Uri(_weiboOptions.RedirectUri));

            accessToken = new AccessToken()
            {
                ExpiresAt = requestTime.AddSeconds(authorizationResult.ExpiresIn).AddMinutes(-5),// 5 分钟用作缓冲
                UserId = authorizationResult.UserId,
                Value = authorizationResult.AccessToken
            };
            _accessTokenStorage.Save(accessToken);
            return accessToken;
        }

        internal Task SignInAsync()
        {
            return SignInAndGetAccessTokenAsync();
        }

        internal Task SignOutAsync()
        {
            _accessTokenStorage.Clear();
            return Task.CompletedTask;
        }
    }
}
