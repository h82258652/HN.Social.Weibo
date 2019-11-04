using HN.Social.Weibo.Models;

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

        AccessToken Load();

        void Save(AccessToken accessToken);
    }
}
