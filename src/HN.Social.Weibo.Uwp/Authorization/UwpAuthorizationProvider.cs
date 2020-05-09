using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Windows.Foundation;
using Windows.Security.Authentication.Web;
using JetBrains.Annotations;

namespace HN.Social.Weibo.Authorization
{
    /// <summary>
    /// Windows 应用商店端授权提供者。
    /// </summary>
    public class UwpAuthorizationProvider : AuthorizationProviderBase
    {
        /// <summary>
        /// 初始化 <see cref="UwpAuthorizationProvider" /> 类的新实例。
        /// </summary>
        /// <param name="httpClientFactory"><see cref="HttpClient" /> 工厂。</param>
        /// <param name="weiboOptionsAccessor"><see cref="WeiboOptions" /> 实例的访问。</param>
        /// <param name="serializerOptionsAccessor"><see cref="JsonSerializerOptions" /> 实例的访问。</param>
        public UwpAuthorizationProvider(
            [NotNull] IHttpClientFactory httpClientFactory, 
            [NotNull] IOptions<WeiboOptions> weiboOptionsAccessor,
            [NotNull] IOptions<JsonSerializerOptions> serializerOptionsAccessor) 
            : base(httpClientFactory, weiboOptionsAccessor, serializerOptionsAccessor)
        {
        }

        /// <inheritdoc />
        protected override async Task<string> GetAuthorizationCodeAsync(Uri authorizationUri, Uri callbackUri)
        {
            var authenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, authorizationUri, callbackUri);
            switch (authenticationResult.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    var responseUri = new Uri(authenticationResult.ResponseData, UriKind.Absolute);
                    return new WwwFormUrlDecoder(responseUri.Query).GetFirstValueByName("code");

                case WebAuthenticationStatus.UserCancel:
                    throw new UserCancelAuthorizationException();

                case WebAuthenticationStatus.ErrorHttp:
                    throw new HttpErrorAuthorizationException();

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
