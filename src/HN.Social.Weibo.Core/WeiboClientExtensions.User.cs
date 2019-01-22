using System;
using System.Threading;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;

namespace HN.Social.Weibo
{
    public static partial class WeiboClientExtensions
    {
        public static Task<User> GetCurrentUserAsync(this IWeiboClient client, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetUserAsync(client.UserId, cancellationToken);
        }

        public static Task<User> GetUserAsync(this IWeiboClient client, long userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<User>($"/users/show.json?uid={userId}", cancellationToken);
        }
    }
}
