using System;
using HN.Social.Weibo.Models;
using JetBrains.Annotations;

namespace HN.Social.Weibo
{
    /// <summary>
    /// 微博 API 异常。
    /// </summary>
    public class WeiboApiException : WeiboException
    {
        /// <summary>
        /// 初始化 <see cref="WeiboApiException" /> 类的新实例。
        /// </summary>
        /// <param name="error">微博错误。</param>
        public WeiboApiException([NotNull] WeiboError error) : base(error.ErrorMessage)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            Error = error;
        }

        /// <summary>
        /// 获取微博错误。
        /// </summary>
        [NotNull]
        public WeiboError Error { get; }
    }
}
