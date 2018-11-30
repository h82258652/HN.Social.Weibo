using System;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;

namespace HN.Social.Weibo
{
    public interface IAuthorizationProvider
    {
        Task<AuthorizationResult> AuthorizeAsync(Uri authorizationUri, Uri callbackUri);
    }
}
