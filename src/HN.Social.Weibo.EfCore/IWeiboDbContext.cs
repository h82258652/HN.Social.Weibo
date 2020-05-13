using HN.Social.Weibo.Models;
using Microsoft.EntityFrameworkCore;

namespace HN.Social.Weibo
{
    /// <summary>
    /// <see cref="AccessToken" /> 存储数据上下文。
    /// </summary>
    public interface IWeiboDbContext
    {
        /// <summary>
        /// <see cref="EfCoreAccessToken" /> 数据集。
        /// </summary>
        DbSet<EfCoreAccessToken> WeiboAccessTokens { get; set; }
    }
}
