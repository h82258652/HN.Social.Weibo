using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HN.Social.Weibo.Models;
using JetBrains.Annotations;

namespace HN.Social.Weibo
{
    public static partial class WeiboClientExtensions
    {
        /// <summary>
        /// 根据用户ID获取用户信息。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uid">需要查询的用户ID。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<User> GetUserAsync([NotNull] this IWeiboClient client, long uid, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/users/show.json?uid={uid}";
            return client.GetAsync<User>(url, cancellationToken);
        }

        /// <summary>
        /// 根据用户ID获取用户信息。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="screenName">需要查询的用户昵称。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<User> GetUserAsync([NotNull] this IWeiboClient client, string screenName, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (screenName == null)
            {
                throw new ArgumentNullException(nameof(screenName));
            }

            var url = $"/users/show.json?screen_name={HttpUtility.UrlEncode(screenName)}";
            return client.GetAsync<User>(url, cancellationToken);
        }

        /// <summary>
        /// 通过个性化域名获取用户资料以及用户最新的一条微博。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="domain">需要查询的个性化域名。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<User> GetUserByDomainAsync([NotNull] this IWeiboClient client, [NotNull] string domain, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (domain == null)
            {
                throw new ArgumentNullException(nameof(domain));
            }

            var url = $"/users/domain_show.json?domain={HttpUtility.UrlEncode(domain)}";
            return client.GetAsync<User>(url, cancellationToken);
        }

        /// <summary>
        /// 批量获取用户的粉丝数、关注数、微博数。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uids">需要获取数据的用户UID，最多不超过100个。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<UserCount[]> GetUserCountsAsync([NotNull] this IWeiboClient client, long[] uids, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (uids == null)
            {
                throw new ArgumentNullException(nameof(uids));
            }

            var url = $"/users/counts.json?uids={HttpUtility.UrlEncode(string.Join(",", uids))}";
            return client.GetAsync<UserCount[]>(url, cancellationToken);
        }
    }
}
