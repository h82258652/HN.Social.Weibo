using System.Text.Json;

namespace HN.Social.Weibo
{
    /// <summary>
    /// 常量。
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// <see cref="JsonSerializerOptions" /> 的配置名字。
        /// </summary>
        public const string JsonSerializerOptionsConfigureName = "Weibo";

        internal const string AccessTokenUrl = "https://api.weibo.com/oauth2/access_token";

        internal const string AuthorizeUrl = "https://api.weibo.com/oauth2/authorize";

        internal const string HttpClientName = "Weibo";

        internal const int UserRemoveAuthorizationCode = 21332;

        internal const string WeiboApiUrlBase = "https://api.weibo.com/2";
    }
}
