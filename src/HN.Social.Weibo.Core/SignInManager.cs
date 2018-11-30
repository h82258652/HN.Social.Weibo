using System;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    internal class SignInManager
    {
        private readonly IAccessTokenStorageService _accessTokenStorageService;
        private readonly IAuthorizationProvider _authorizationProvider;
        private readonly WeiboOptions _weiboOptions;
        private AccessToken _memoryAccessToken;

        internal SignInManager(IOptions<WeiboOptions> weiboOptions, IAuthorizationProvider authorizationProvider, IAccessTokenStorageService accessTokenStorageService)
        {
            _weiboOptions = weiboOptions.Value;
            _authorizationProvider = authorizationProvider;
            _accessTokenStorageService = accessTokenStorageService;
        }

        internal async Task<AccessToken> GetAccessToken()
        {
            if (_memoryAccessToken == null)
            {
                _memoryAccessToken = await _accessTokenStorageService.LoadAsync();
            }
            if (_memoryAccessToken == null)
            {
                return null;
            }
            if (_memoryAccessToken.ExpiressAt <= DateTime.Now)
            {
                await _accessTokenStorageService.ClearAsync();
                _memoryAccessToken = null;
                return null;
            }
            return _memoryAccessToken;
        }

        internal async Task<bool> IsSignIn()
        {
            return await GetAccessToken() != null;
        }

        internal async Task<AccessToken> SignInAndGetAccessTokenAsync()
        {
            var accessToken = await GetAccessToken();
            if (accessToken != null)
            {
                return accessToken;
            }

            var requestTime = DateTime.Now;
            var authorizationUri = new Uri($"https://api.weibo.com/oauth2/authorize?client_id={_weiboOptions.AppKey}&redirect_uri={_weiboOptions.RedirectUri}");
            var authorizationResult = await _authorizationProvider.AuthorizeAsync(authorizationUri, new Uri(_weiboOptions.RedirectUri));

            accessToken = new AccessToken()
            {
                Value = authorizationResult.AccessToken,
                ExpiressAt = requestTime.AddSeconds(authorizationResult.ExpiresIn).AddMinutes(-5),// 5 分钟用作缓冲
                UserId = authorizationResult.UserId
            };
            await _accessTokenStorageService.SaveAsync(accessToken);
            _memoryAccessToken = accessToken;
            return accessToken;
        }

        internal Task SignInAsync()
        {
            return SignInAndGetAccessTokenAsync();
        }

        internal Task SignOutAsync()
        {
            _memoryAccessToken = null;
            return _accessTokenStorageService.ClearAsync();
        }
    }
}
