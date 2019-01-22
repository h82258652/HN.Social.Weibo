using HN.Social.Weibo.Models;

namespace HN.Social.Weibo
{
    public interface IAccessTokenStorage
    {
        void Clear();

        AccessToken Load();

        void Save(AccessToken accessToken);
    }
}
