using System.Threading.Tasks;
using HN.Social.Weibo.Models;

namespace HN.Social.Weibo
{
    public interface IAccessTokenStorageService
    {
        Task ClearAsync();

        Task<AccessToken> LoadAsync();

        Task SaveAsync(AccessToken accessToken);
    }
}
