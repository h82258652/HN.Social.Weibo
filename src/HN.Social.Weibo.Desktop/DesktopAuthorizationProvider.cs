using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using HN.Social.Weibo.Authorization;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    public class DesktopAuthorizationProvider : AuthorizationProviderBase
    {
        public DesktopAuthorizationProvider(IOptions<WeiboOptions> weiboOptions) : base(weiboOptions)
        {
        }

        protected override Task<string> GetAuthorizationCodeAsync(Uri authorizationUri, Uri callbackUri)
        {
            var authorizationDialog = new AuthorizationDialog(authorizationUri);
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
