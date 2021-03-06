﻿using System;

namespace HN.Social.Weibo.Authorization
{
    /// <summary>
    /// 授权异常。
    /// </summary>
    public class AuthorizationException : WeiboException
    {
        /// <summary>
        /// 初始化 <see cref="AuthorizationException" /> 类的新实例。
        /// </summary>
        public AuthorizationException()
        {
        }

        /// <summary>
        /// 初始化 <see cref="WeiboException" /> 类的新实例。
        /// </summary>
        /// <param name="message">描述该异常的错误消息。</param>
        public AuthorizationException(string message) : base(message)
        {
        }

        /// <summary>
        /// 初始化 <see cref="WeiboException" /> 类的新实例。
        /// </summary>
        /// <param name="message">描述该异常的错误消息。</param>
        /// <param name="innerException">导致该异常的异常。</param>
        public AuthorizationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
