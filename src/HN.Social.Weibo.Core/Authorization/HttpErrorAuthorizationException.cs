using System.Net.Http;

namespace HN.Social.Weibo.Authorization
{
    /// <summary>
    /// 授权期间网络异常。
    /// </summary>
    public class HttpErrorAuthorizationException : AuthorizationException
    {
        /// <summary>
        /// 初始化 <see cref="HttpErrorAuthorizationException" /> 类的新实例。
        /// </summary>
        public HttpErrorAuthorizationException() : base("授权期间网络异常。")
        {
        }

        /// <summary>
        /// 初始化 <see cref="HttpErrorAuthorizationException" /> 类的新实例。
        /// </summary>
        /// <param name="httpRequestException">Http 请求异常。</param>
        public HttpErrorAuthorizationException(HttpRequestException httpRequestException) : base(httpRequestException.Message, httpRequestException)
        {
        }
    }
}
