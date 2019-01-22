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
        /// <summary>
        /// 回复一条评论 https://open.weibo.com/wiki/2/comments/reply
        /// </summary>
        /// <param name="client"></param>
        /// <param name="commentId">需要回复的评论 Id。</param>
        /// <param name="statusId">需要评论的微博 Id。</param>
        /// <param name="comment">回复评论内容，内容不超过 140 个汉字。</param>
        /// <param name="withoutMention">回复中是否自动加入“回复@用户名”。</param>
        /// <param name="commentOriginalStatus">当评论转发微博时，是否评论给原微博。</param>
        /// <returns></returns>
        public static Task<Comment> ReplyAsync(this IWeiboClient client, int commentId, int statusId, string comment, bool withoutMention = true, bool commentOriginalStatus = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            var postData = new Dictionary<string, string>
            {
                ["cid"] = commentId.ToString(),
                ["id"] = statusId.ToString(),
                ["comment"] = comment,
                ["without_mention"] = withoutMention ? "0" : "1",
                ["comment_ori"] = commentOriginalStatus ? "1" : "0"
            };
            var postContent = new FormUrlEncodedContent(postData);
            return client.PostAsync<Comment>("/comments/reply.json", postContent, cancellationToken);
        }
    }
}
