using System;
using System.Collections.Generic;
using System.Net.Http;
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
        /// 根据微博ID返回某条微博的评论列表。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="id">需要查询的微博ID。</param>
        /// <param name="sinceId">若指定此参数，则返回ID比since_id大的评论（即比since_id时间晚的评论），默认为0。</param>
        /// <param name="maxId">若指定此参数，则返回ID小于或等于max_id的评论，默认为0。</param>
        /// <param name="count">单页返回的记录条数，默认为50。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="filterByAuthor">作者筛选类型，0：全部、1：我关注的人、2：陌生人，默认为0。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<CommentList> GetCommentsAsync([NotNull] this IWeiboClient client, long id, long sinceId = 0, long maxId = 0, int count = 50, int page = 1, int filterByAuthor = 0, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/comments/show.json?id={id}&since_id={sinceId}&max_id={maxId}&count={count}&page={page}&filter_by_author={filterByAuthor}";
            return client.GetAsync<CommentList>(url, cancellationToken);
        }

        /// <summary>
        /// 获取当前登录用户所发出的评论列表。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sinceId">若指定此参数，则返回ID比since_id大的评论（即比since_id时间晚的评论），默认为0。</param>
        /// <param name="maxId">若指定此参数，则返回ID小于或等于max_id的评论，默认为0。</param>
        /// <param name="count">单页返回的记录条数，默认为50。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="filterBySource">来源筛选类型，0：全部、1：来自微博的评论、2：来自微群的评论，默认为0。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<CommentList> GetCommentsByMeAsync([NotNull] this IWeiboClient client, long sinceId = 0, long maxId = 0, int count = 50, int page = 1, int filterBySource = 0, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/comments/by_me.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&filter_by_source={filterBySource}";
            return client.GetAsync<CommentList>(url, cancellationToken);
        }

        /// <summary>
        /// 获取当前登录用户所接收到的评论列表。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sinceId">若指定此参数，则返回ID比since_id大的评论（即比since_id时间晚的评论），默认为0。</param>
        /// <param name="maxId">若指定此参数，则返回ID小于或等于max_id的评论，默认为0。</param>
        /// <param name="count">单页返回的记录条数，默认为50。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="filterByAuthor">作者筛选类型，0：全部、1：我关注的人、2：陌生人，默认为0。</param>
        /// <param name="filterBySource">来源筛选类型，0：全部、1：来自微博的评论、2：来自微群的评论，默认为0。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<CommentList> GetCommentsToMeAsync([NotNull] this IWeiboClient client, long sinceId = 0, long maxId = 0, int count = 50, int page = 1, int filterByAuthor = 0, int filterBySource = 0, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/comments/to_me.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&filter_by_author={filterByAuthor}&filter_by_source={filterBySource}";
            return client.GetAsync<CommentList>(url, cancellationToken);
        }

        /// <summary>
        /// 获取当前登录用户的最新评论包括接收到的与发出的。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sinceId">若指定此参数，则返回ID比since_id大的评论（即比since_id时间晚的评论），默认为0。</param>
        /// <param name="maxId">若指定此参数，则返回ID小于或等于max_id的评论，默认为0。</param>
        /// <param name="count">单页返回的记录条数，默认为50。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="trimUser">返回值中user字段开关，0：返回完整user字段、1：user字段仅返回user_id，默认为0。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<CommentList> GetCommentsTimelineAsync([NotNull] this IWeiboClient client, long sinceId = 0, long maxId = 0, int count = 50, int page = 1, int trimUser = 0, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/comments/timeline.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&trim_user={trimUser}";
            return client.GetAsync<CommentList>(url, cancellationToken);
        }

        /// <summary>
        /// 获取最新的提到当前登录用户的评论，即@我的评论。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sinceId">若指定此参数，则返回ID比since_id大的评论（即比since_id时间晚的评论），默认为0。</param>
        /// <param name="maxId">若指定此参数，则返回ID小于或等于max_id的评论，默认为0。</param>
        /// <param name="count">单页返回的记录条数，默认为50。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="filterByAuthor">作者筛选类型，0：全部、1：我关注的人、2：陌生人，默认为0。</param>
        /// <param name="filterBySource">来源筛选类型，0：全部、1：来自微博的评论、2：来自微群的评论，默认为0。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<CommentList> GetCommentsMentionsAsync([NotNull] this IWeiboClient client, long sinceId = 0, long maxId = 0, int count = 50, int page = 1, int filterByAuthor = 0, int filterBySource = 0, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/comments/mentions.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&filter_by_author={filterByAuthor}&filter_by_source={filterBySource}";
            return client.GetAsync<CommentList>(url, cancellationToken);
        }

        /// <summary>
        /// 根据评论ID批量返回评论信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cids">需要查询的批量评论ID，最大50。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Comment[]> GetCommentsBatchAsync([NotNull] this IWeiboClient client, long[] cids, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (cids == null)
            {
                throw new ArgumentNullException(nameof(cids));
            }

            var url = $"/comments/show_batch.json?cids={HttpUtility.UrlEncode(string.Join(",", cids))}";
            return client.GetAsync<Comment[]>(url, cancellationToken);
        }

        /// <summary>
        /// 对一条微博进行评论。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="comment">评论内容，内容不超过140个汉字。</param>
        /// <param name="id">需要评论的微博ID。</param>
        /// <param name="commentOri">当评论转发微博时，是否评论给原微博，0：否、1：是，默认为0。</param>
        /// <param name="rip">开发者上报的操作用户真实IP，形如：211.156.0.1。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Comment> CreateCommentAsync([NotNull] this IWeiboClient client, string comment, long id, int commentOri = 0, string? rip = null, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var formData = new Dictionary<string, string>
            {
                ["comment"] = comment,
                ["id"] = id.ToString(),
                ["comment_ori"] = commentOri.ToString()
            };
            if (rip != null)
            {
                formData["rip"] = rip;
            }
            var postContent = new FormUrlEncodedContent(formData);
            const string url = "/comments/create.json";
            return client.PostAsync<Comment>(url, postContent, cancellationToken);
        }

        /// <summary>
        /// 删除一条评论。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cid">要删除的评论ID，只能删除登录用户自己发布的评论。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Comment> DestroyCommentAsync([NotNull] this IWeiboClient client, long cid, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var formData = new Dictionary<string, string>
            {
                ["cid"] = cid.ToString()
            };
            var postContent = new FormUrlEncodedContent(formData);
            const string url = "/comments/destroy.json";
            return client.PostAsync<Comment>(url, postContent, cancellationToken);
        }

        /// <summary>
        /// 根据评论ID批量删除评论。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cids">需要删除的评论ID，最多20个。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Comment[]> DestroyBatchCommentAsync([NotNull] this IWeiboClient client, long[] cids, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var formData = new Dictionary<string, string>
            {
                ["cids"] = string.Join(",", cids)
            };
            var postContent = new FormUrlEncodedContent(formData);
            const string url = "/comments/destroy_batch.json";
            return client.PostAsync<Comment[]>(url, postContent, cancellationToken);
        }

        /// <summary>
        /// 回复一条评论。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cid">需要回复的评论ID。</param>
        /// <param name="id">需要评论的微博ID。</param>
        /// <param name="comment">回复评论内容，内容不超过140个汉字。</param>
        /// <param name="withoutMention">回复中是否自动加入“回复@用户名”，0：是、1：否，默认为0。</param>
        /// <param name="commentOri">当评论转发微博时，是否评论给原微博，0：否、1：是，默认为0。</param>
        /// <param name="rip">开发者上报的操作用户真实IP，形如：211.156.0.1。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Comment> ReplyCommentAsync(this IWeiboClient client, long cid, long id, string comment, int withoutMention = 0, int commentOri = 0, string? rip = null, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var formData = new Dictionary<string, string>
            {
                ["cid"] = cid.ToString(),
                ["id"] = id.ToString(),
                ["comment"] = comment,
                ["without_mention"] = withoutMention.ToString(),
                ["comment_ori"] = commentOri.ToString()
            };
            if (rip != null)
            {
                formData["rip"] = rip;
            }
            var postContent = new FormUrlEncodedContent(formData);
            const string url = "/comments/reply.json";
            return client.PostAsync<Comment>(url, postContent, cancellationToken);
        }
    }
}
