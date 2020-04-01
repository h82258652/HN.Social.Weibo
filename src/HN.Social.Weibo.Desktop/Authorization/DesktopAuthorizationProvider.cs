using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo.Authorization
{
    /// <summary>
    /// Windows 桌面端授权提供者。
    /// </summary>
    public class DesktopAuthorizationProvider : AuthorizationProviderBase
    {
        /// <summary>
        /// 初始化 <see cref="DesktopAuthorizationProvider" /> 类的新实例。
        /// </summary>
        /// <param name="httpClientFactory"><see cref="HttpClient" /> 工厂。</param>
        /// <param name="weiboOptionsAccesser"><see cref="WeiboOptions" /> 实例的访问。</param>
        /// <param name="serializerOptionsAccesser"><see cref="JsonSerializerOptions" /> 实例的访问。</param>
        public DesktopAuthorizationProvider(
            [NotNull] IHttpClientFactory httpClientFactory, 
            [NotNull] IOptions<WeiboOptions> weiboOptionsAccesser,
            [NotNull] IOptions<JsonSerializerOptions> serializerOptionsAccesser) 
            : base(httpClientFactory, weiboOptionsAccesser, serializerOptionsAccesser)
        {
        }

        /// <inheritdoc />
        protected override Task<string> GetAuthorizationCodeAsync(Uri authorizeUri, Uri callbackUri)
        {
            using var authorizationDialog = new AuthorizationDialog(authorizeUri);
            if (authorizationDialog.ShowDialog() == DialogResult.OK)
            {
                return Task.FromResult(authorizationDialog.AuthorizationCode);
            }
            else
            {
                if (authorizationDialog.IsHttpError)
                {
                    throw new HttpErrorAuthorizationException();
                }
                else
                {
                    throw new UserCancelAuthorizationException();
                }
            }
        }
    }
}
