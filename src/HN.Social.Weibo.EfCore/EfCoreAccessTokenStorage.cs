using System;
using System.Linq;
using HN.Social.Weibo.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    /// <inheritdoc />
    public class EfCoreAccessTokenStorage<TDbContext> : IAccessTokenStorage where TDbContext : DbContext, IWeiboDbContext
    {
        private readonly TDbContext _context;
        private readonly WeiboOptions _weiboOptions;

        /// <summary>
        /// 初始化 <see cref="EfCoreAccessTokenStorage{TDbContext}" /> 类的新实例。
        /// </summary>
        /// <param name="context">Entity Framework Core 数据上下文。</param>
        /// <param name="weiboOptionsAccessor"><see cref="WeiboOptions" /> 实例的访问。</param>
        public EfCoreAccessTokenStorage(
            [NotNull] TDbContext context, 
            [NotNull] IOptions<WeiboOptions> weiboOptionsAccessor)
        {
            _context = context;
            _weiboOptions = weiboOptionsAccessor.Value;
        }

        /// <inheritdoc />
        public void Clear()
        {
            var existAccessToken = _context
                .WeiboAccessTokens
                .Where(temp => temp.OwnerAppKey == _weiboOptions.AppKey)
                .ToList();
            _context.WeiboAccessTokens.RemoveRange(existAccessToken);
            _context.SaveChanges();
        }

        /// <inheritdoc />
        public AccessToken? Load()
        {
            var accessToken = _context
                .WeiboAccessTokens
                .Where(temp => temp.OwnerAppKey == _weiboOptions.AppKey)
                .OrderByDescending(temp => temp.ExpiresAt)
                .FirstOrDefault();
            if (accessToken == null)
            {
                return null;
            }

            return new AccessToken
            {
                ExpiresAt = accessToken.ExpiresAt,
                UserId = accessToken.UserId,
                Value = accessToken.Value
            };
        }

        /// <inheritdoc />
        public void Save(AccessToken accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            var existAccessToken = _context
                .WeiboAccessTokens
                .Where(temp => temp.OwnerAppKey == _weiboOptions.AppKey)
                .OrderByDescending(temp => temp.ExpiresAt)
                .FirstOrDefault();
            if (existAccessToken == null)
            {
                existAccessToken = new EfCoreAccessToken
                {
                    ExpiresAt = accessToken.ExpiresAt,
                    UserId = accessToken.UserId,
                    Value = accessToken.Value
                };
                _context.WeiboAccessTokens.Add(existAccessToken);
            }
            else
            {
                existAccessToken.ExpiresAt = accessToken.ExpiresAt;
                existAccessToken.UserId = accessToken.UserId;
                existAccessToken.Value = accessToken.Value;
            }

            _context.SaveChanges();
        }
    }
}
