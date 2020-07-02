using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HN.Social.Weibo
{
    /// <summary>
    /// 提供用于调用微博 OpenAPI 的类。
    /// </summary>
    public interface IWeiboClient
    {
        /// <summary>
        /// 获取用户是否已登入。
        /// </summary>
        bool IsSignIn { get; }

        /// <summary>
        /// 获取已注入的服务。
        /// </summary>
        [NotNull]
        IServiceProvider Services { get; }

        /// <summary>
        /// 获取已登入用户的用户 Id。
        /// </summary>
        long UserId { get; }

        /// <summary>
        /// 发送 GET 请求。
        /// </summary>
        /// <typeparam name="T">结果类型。</typeparam>
        /// <param name="uri">请求地址。</param>
        /// <param name="cancellationToken">要监视取消请求的标记。</param>
        /// <returns>结果。</returns>
        Task<T> GetAsync<T>([NotNull] string uri, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送 POST 请求。
        /// </summary>
        /// <typeparam name="T">结果类型。</typeparam>
        /// <param name="uri">请求地址。</param>
        /// <param name="content">请求内容。</param>
        /// <param name="cancellationToken">要监视取消请求的标记。</param>
        /// <returns>结果。</returns>
        Task<T> PostAsync<T>([NotNull] string uri, [CanBeNull] HttpContent? content, CancellationToken cancellationToken = default);

        /// <summary>
        /// 发送请求。
        /// </summary>
        /// <typeparam name="T">结果类型。</typeparam>
        /// <param name="request">请求消息。</param>
        /// <param name="cancellationToken">要监视取消请求的标记。</param>
        /// <returns>结果。</returns>
        Task<T> SendAsync<T>([NotNull] HttpRequestMessage request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 登入。
        /// </summary>
        /// <returns>表示异步登入操作的任务。</returns>
        Task SignInAsync();

        /// <summary>
        /// 登出。
        /// </summary>
        /// <returns>表示异步登出操作的任务。</returns>
        Task SignOutAsync();
    }
}
