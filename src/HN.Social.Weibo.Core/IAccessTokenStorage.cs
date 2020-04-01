using HN.Social.Weibo.Models;
using JetBrains.Annotations;

namespace HN.Social.Weibo
{
    /// <summary>
    /// <see cref="AccessToken" /> 存储。
    /// </summary>
    public interface IAccessTokenStorage
    {
        /// <summary>
        /// 清空存储的 <see cref="AccessToken" />。
        /// </summary>
        void Clear();

        /// <summary>
        /// 读取存储的 <see cref="AccessToken" />。
        /// </summary>
        /// <returns>存储的 <see cref="AccessToken" />。</returns>
        [CanBeNull]
        AccessToken? Load();

        /// <summary>
        /// 保存 <see cref="AccessToken" />。
        /// </summary>
        /// <param name="accessToken"><see cref="AccessToken" /> 对象。</param>
        void Save([NotNull] AccessToken accessToken);
    }
}
