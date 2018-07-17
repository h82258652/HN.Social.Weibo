using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;

namespace HN.Social.Weibo
{
    public static class WeiboClientExtensions
    {
        public static async Task<UserInfo> GetCurrentUserInfoAsync(this IWeiboClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var userId = await client.GetCurrentUserId();
            if (!userId.HasValue)
            {
                throw new InvalidOperationException("用户未登录");
            }
            return await GetUserInfoAsync(client, userId.Value);
        }

        public static Task<UserInfo> GetUserInfoAsync(this IWeiboClient client, long userId)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<UserInfo>($"/users/show.json?uid={userId}");
        }

        public static Task<UserInfo> GetUserInfoAsync(this IWeiboClient client, string nickname)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (nickname == null)
            {
                throw new ArgumentNullException(nameof(nickname));
            }

            return client.GetAsync<UserInfo>($"/users/show.json?screen_name={WebUtility.UrlEncode(nickname)}");
        }

        public static Task<Comment> ReplyCommentAsync(this IWeiboClient client, long commentId, long statusId, string comment, bool withoutMention = true, bool commentOriginStatus = false, string ip = null)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            var postData = new Dictionary<string, string>();
            postData["cid"] = commentId.ToString();
            postData["id"] = statusId.ToString();
            postData["comment"] = comment;
            postData["without_mention"] = withoutMention ? "0" : "1";
            postData["comment_ori"] = commentOriginStatus ? "1" : "0";
            var postContent = new FormUrlEncodedContent(postData);
            return client.PostAsync<Comment>("/comments/reply.json", postContent);
        }

        public static Task<Comment> SendCommentAsync(this IWeiboClient client, long statusId, string content, bool commentOriginStatus = false, string ip = null)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var postData = new Dictionary<string, string>
            {
                ["comment"] = content,
                ["id"] = statusId.ToString(),
                ["comment_ori"] = commentOriginStatus ? "1" : "0",
            };
            if (ip != null)
            {
                postData["rip"] = ip;
            }
            var postContent = new FormUrlEncodedContent(postData);
            return client.PostAsync<Comment>("/comments/create.json", postContent);
        }

        public static Task<Status> ShareAsync(this IWeiboClient client, string status, string ip = null)
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
            if (ip != null)
            {
                postData["rip"] = ip;
            }
            var postContent = new FormUrlEncodedContent(postData);
            return client.PostAsync<Status>("/statuses/share.json", postContent);
        }

        public static Task<Status> ShareAsync(this IWeiboClient client, string status, byte[] image, string ip = null)
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
            if (ip != null)
            {
                postContent.Add(new StringContent(ip), "rip");
            }
            return client.PostAsync<Status>("/statuses/share.json", postContent);
        }
    }
}