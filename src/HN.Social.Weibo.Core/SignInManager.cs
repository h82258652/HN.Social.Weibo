﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HN.Social.Weibo.Authorization;
using HN.Social.Weibo.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Nito.AsyncEx;

namespace HN.Social.Weibo
{
    internal class SignInManager
    {
        private static readonly AsyncLock AsyncLock = new AsyncLock();
        private readonly IAccessTokenStorage _accessTokenStorage;
        private readonly IAuthorizationProvider _authorizationProvider;
        private readonly WeiboOptions _weiboOptions;

        /// <summary>
        /// 初始化 <see cref="SignInManager" /> 类的新实例。
        /// </summary>
        /// <param name="accessTokenStorage"><see cref="AccessToken" /> 存储。</param>
        /// <param name="authorizationProvider">授权提供者。</param>
        /// <param name="weiboOptionsAccessor"><see cref="WeiboOptions" /> 实例的访问。</param>
        public SignInManager(
            [NotNull] IAccessTokenStorage accessTokenStorage,
            [NotNull] IAuthorizationProvider authorizationProvider,
            [NotNull] IOptions<WeiboOptions> weiboOptionsAccessor)
        {
            if (accessTokenStorage == null)
            {
                throw new ArgumentNullException(nameof(accessTokenStorage));
            }

            if (authorizationProvider == null)
            {
                throw new ArgumentNullException(nameof(authorizationProvider));
            }

            if (weiboOptionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(weiboOptionsAccessor));
            }

            _accessTokenStorage = accessTokenStorage;
            _authorizationProvider = authorizationProvider;
            _weiboOptions = weiboOptionsAccessor.Value;
        }

        internal bool IsSignIn => GetAccessToken() != null;

        [CanBeNull]
        internal AccessToken? GetAccessToken()
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
            using (await AsyncLock.LockAsync())
            {
                var accessToken = GetAccessToken();
                if (accessToken != null)
                {
                    return accessToken;
                }

                var requestTime = DateTime.Now;

                var authorizeUrl = $"{Constants.AuthorizeUrl}?client_id={_weiboOptions.AppKey}&redirect_uri={HttpUtility.UrlEncode(_weiboOptions.RedirectUri)}";
                if (_weiboOptions.Scope != null)
                {
                    authorizeUrl += $"&scope={HttpUtility.UrlEncode(_weiboOptions.Scope)}";
                }

                var authorizeUri = new Uri(authorizeUrl);
                AuthorizeResult authorizeResult;
                try
                {
                    authorizeResult = await _authorizationProvider.AuthorizeAsync(authorizeUri, new Uri(_weiboOptions.RedirectUri));
                }
                catch (HttpRequestException ex)
                {
                    throw new HttpErrorAuthorizationException(ex);
                }

                accessToken = new AccessToken
                {
                    ExpiresAt = requestTime.AddSeconds(authorizeResult.ExpiresIn).AddMinutes(-5),// 5 分钟用作缓冲
                    UserId = long.Parse(authorizeResult.UserId),
                    Value = authorizeResult.AccessToken
                };
                _accessTokenStorage.Save(accessToken);
                return accessToken;
            }
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
