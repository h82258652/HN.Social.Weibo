using System;
using System.Net.Http;

namespace HN.Social.Weibo.Http
{
    internal class WeiboHttpClient : HttpClient
    {
        internal WeiboHttpClient(SignInManager signInManager) : base(new WeiboHttpClientHandler(signInManager))
        {
            BaseAddress = new Uri("https://api.weibo.com/2");
        }
    }
}