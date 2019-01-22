using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;

namespace HN.Social.Weibo
{
    public static partial class WeiboClientExtensions
    {
        public static Task GetHomeTimelineAsync(this IWeiboClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            throw new NotImplementedException();
        }

        public static Task GetUserTimelineAsync(this IWeiboClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            throw new NotImplementedException();
        }

        public static Task<Status> ShareAsync(this IWeiboClient client, string status, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }

            var postData = new Dictionary<string, string>
            {
                ["status"] = status
            };
            var postContent = new FormUrlEncodedContent(postData);
            return client.PostAsync<Status>("/statuses/share.json", postContent, cancellationToken);
        }

        public static Task<Status> ShareAsync(this IWeiboClient client, string status, byte[] image, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            var postContent = new MultipartFormDataContent
            {
                {
                    new StringContent(status), "status"
                },
                {
                    new ByteArrayContent(image), "pic"
                }
            };
            return client.PostAsync<Status>("/statuses/share.json", postContent, cancellationToken);
        }
    }
}
