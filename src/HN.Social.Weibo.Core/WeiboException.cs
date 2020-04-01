using System;

namespace HN.Social.Weibo
{
    /// <summary>
    /// 微博异常。
    /// </summary>
    public class WeiboException : Exception
    {
        /// <summary>
        /// 初始化 <see cref="WeiboException" /> 类的新实例。
        /// </summary>
        public WeiboException()
        {
        }

        /// <summary>
        /// 初始化 <see cref="WeiboException" /> 类的新实例。
        /// </summary>
        /// <param name="message">描述该异常的错误消息。</param>
        public WeiboException(string message) : base(message)
        {
        }

        /// <summary>
        /// 初始化 <see cref="WeiboException" /> 类的新实例。
        /// </summary>
        /// <param name="message">描述该异常的错误消息。</param>
        /// <param name="innerException">导致该异常的异常。</param>
        public WeiboException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
