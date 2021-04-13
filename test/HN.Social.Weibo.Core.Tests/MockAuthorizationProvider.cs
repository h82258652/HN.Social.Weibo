using System;
using System.Threading.Tasks;
using HN.Social.Weibo.Authorization;
using HN.Social.Weibo.Models;

namespace HN.Social.Weibo.Core.Tests
{
    public class MockAuthorizationProvider : IAuthorizationProvider
    {
        public Task<AuthorizeResult> AuthorizeAsync(Uri authorizeUri, Uri callbackUri)
        {
            return Task.FromResult(new AuthorizeResult());
        }
    }
}
