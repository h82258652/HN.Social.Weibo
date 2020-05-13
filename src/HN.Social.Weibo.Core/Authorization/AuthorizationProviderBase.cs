using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo.Authorization
{
    /// <summary>
    /// 基础授权提供者。
    /// </summary>
    public abstract class AuthorizationProviderBase : IAuthorizationProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly WeiboOptions _weiboOptions;

        /// <summary>
        /// 初始化 <see cref="AuthorizationProviderBase" /> 类的新实例。
        /// </summary>
        /// <param name="httpClientFactory"><see cref="HttpClient" /> 工厂。</param>
        /// <param name="weiboOptionsAccessor"><see cref="WeiboOptions" /> 实例的访问。</param>
        /// <param name="serializerOptionsAccessor"><see cref="JsonSerializerOptions" /> 实例的访问。</param>
        protected AuthorizationProviderBase(
            [NotNull] IHttpClientFactory httpClientFactory,
            [NotNull] IOptions<WeiboOptions> weiboOptionsAccessor,
            [NotNull] IOptions<JsonSerializerOptions> serializerOptionsAccessor)
        {
            if (httpClientFactory == null)
            {
                throw new ArgumentNullException(nameof(httpClientFactory));
            }

            if (weiboOptionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(weiboOptionsAccessor));
            }

            if (serializerOptionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(serializerOptionsAccessor));
            }

            _httpClientFactory = httpClientFactory;
            _weiboOptions = weiboOptionsAccessor.Value;
            _serializerOptions = serializerOptionsAccessor.Value;
        }

        /// <inheritdoc />
        public async Task<AuthorizeResult> AuthorizeAsync(Uri authorizeUri, Uri callbackUri)
        {
            if (authorizeUri == null)
            {
                throw new ArgumentNullException(nameof(authorizeUri));
            }

            if (callbackUri == null)
            {
                throw new ArgumentNullException(nameof(callbackUri));
            }

            var authorizationCode = await GetAuthorizationCodeAsync(authorizeUri, callbackUri);
            var client = _httpClientFactory.CreateClient();
            var formData = new Dictionary<string, string>
            {
                ["client_id"] = _weiboOptions.AppKey,
                ["client_secret"] = _weiboOptions.AppSecret,
                ["grant_type"] = "authorization_code",
                ["code"] = authorizationCode,
                ["redirect_uri"] = _weiboOptions.RedirectUri
            };

            var postContent = new FormUrlEncodedContent(formData);
            var response = await client.PostAsync(Constants.AccessTokenUrl, postContent);
            var json = await response.Content.ReadAsStringAsync();
            WeiboError? error;
            try
            {
                error = JsonSerializer.Deserialize<WeiboError>(json, _serializerOptions);
            }
            catch
            {
                error = null;
            }

            if (error != null && error.ErrorCode != 0)
            {
                throw new WeiboApiException(error);
            }

            return JsonSerializer.Deserialize<AuthorizeResult>(json, _serializerOptions);
        }

        /// <summary>
        /// 获取授权码。
        /// </summary>
        /// <param name="authorizeUri">授权地址。</param>
        /// <param name="callbackUri">回调地址。</param>
        /// <returns>授权码。</returns>
        protected abstract Task<string> GetAuthorizationCodeAsync(Uri authorizeUri, Uri callbackUri);
    }
}
