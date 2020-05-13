using Microsoft.EntityFrameworkCore;

namespace HN.Social.Weibo
{
    public interface IWeiboDbContext
    {
        DbSet<EfCoreAccessToken> WeiboAccessTokens { get; set; }
    }
}
