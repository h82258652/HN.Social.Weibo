using System;
using System.Linq;
using HN.Social.Weibo.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HN.Social.Weibo
{
    public class EfCoreAccessTokenStorage<TDbContext> : IAccessTokenStorage where TDbContext : DbContext, IWeiboDbContext
    {
        private readonly TDbContext _context;
        private readonly WeiboOptions _weiboOptions;

        public EfCoreAccessTokenStorage(
            [NotNull] TDbContext context, 
            [NotNull] IOptions<WeiboOptions> weiboOptionsAccessor)
        {
            _context = context;
            _weiboOptions = weiboOptionsAccessor.Value;
        }

        public void Clear()
        {
            var existAccessToken = _context
                .WeiboAccessTokens
                .Where(temp => temp.OwnerAppKey == _weiboOptions.AppKey)
                .ToList();
            _context.WeiboAccessTokens.RemoveRange(existAccessToken);
            _context.SaveChanges();
        }

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
