using System;
using System.Collections.Generic;
using HN.Social.Weibo.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    /// <summary>
    /// 内存 <see cref="AccessToken" /> 存储。
    /// </summary>
    public class MemoryAccessTokenStorage : IAccessTokenStorage
    {
        private static readonly IDictionary<string, AccessToken> MemoryAccessTokens = new Dictionary<string, AccessToken>();
        private readonly WeiboOptions _weiboOptions;

        /// <summary>
        /// 初始化 <see cref="MemoryAccessTokenStorage" /> 类的新实例。
        /// </summary>
        /// <param name="weiboOptionsAccessor"><see cref="WeiboOptions" /> 实例的访问。</param>
        public MemoryAccessTokenStorage([NotNull] IOptions<WeiboOptions> weiboOptionsAccessor)
        {
            if (weiboOptionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(weiboOptionsAccessor));
            }

            _weiboOptions = weiboOptionsAccessor.Value;
        }

        /// <inheritdoc />
        public void Clear()
        {
            MemoryAccessTokens.Remove(_weiboOptions.AppKey);
        }

        /// <inheritdoc />
        public AccessToken? Load()
        {
            if (MemoryAccessTokens.TryGetValue(_weiboOptions.AppKey, out var accessToken))
            {
                return accessToken;
            }

            return null;
        }

        /// <inheritdoc />
        public void Save(AccessToken accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            MemoryAccessTokens[_weiboOptions.AppKey] = accessToken;
        }
    }
}
