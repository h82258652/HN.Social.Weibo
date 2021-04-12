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
        /// 获取当前登录用户及其所关注（授权）用户的最新微博。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sinceId">若指定此参数，则返回ID比since_id大的微博（即比since_id时间晚的微博），默认为0。</param>
        /// <param name="maxId">若指定此参数，则返回ID小于或等于max_id的微博，默认为0。</param>
        /// <param name="count">单页返回的记录条数，最大不超过100，默认为20。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="baseApp">是否只获取当前应用的数据。0为否（所有数据），1为是（仅当前应用），默认为0。</param>
        /// <param name="feature">过滤类型ID，0：全部、1：原创、2：图片、3：视频、4：音乐，默认为0。</param>
        /// <param name="trimUser">返回值中user字段开关，0：返回完整user字段、1：user字段仅返回user_id，默认为0。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<StatusList> GetHomeTimelineAsync(this IWeiboClient client, long sinceId = 0, long maxId = 0, int count = 20, int page = 1, int baseApp = 0, int feature = 0, int trimUser = 0, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/statuses/home_timeline.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&base_app={baseApp}&feature={feature}&trim_user={trimUser}";
            return client.GetAsync<StatusList>(url, cancellationToken);
        }

        /// <summary>
        /// 获取某个用户最新发表的微博列表。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uid">需要查询的用户ID。</param>
        /// <param name="sinceId">若指定此参数，则返回ID比since_id大的微博（即比since_id时间晚的微博），默认为0。</param>
        /// <param name="maxId">若指定此参数，则返回ID小于或等于max_id的微博，默认为0。</param>
        /// <param name="count">单页返回的记录条数，最大不超过100，超过100以100处理，默认为20。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="baseApp">是否只获取当前应用的数据。0为否（所有数据），1为是（仅当前应用），默认为0。</param>
        /// <param name="feature">过滤类型ID，0：全部、1：原创、2：图片、3：视频、4：音乐，默认为0。</param>
        /// <param name="trimUser">返回值中user字段开关，0：返回完整user字段、1：user字段仅返回user_id，默认为0。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<StatusList> GetUserTimelineAsync(this IWeiboClient client, long uid, long sinceId = 0, long maxId = 0, int count = 20, int page = 1, int baseApp = 0, int feature = 0, int trimUser = 0, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/statuses/user_timeline.json?uid={uid}&since_id={sinceId}&max_id={maxId}&count={count}&page={page}&base_app={baseApp}&feature={feature}&trim_user={trimUser}";
            return client.GetAsync<StatusList>(url, cancellationToken);
        }

        /// <summary>
        /// 获取指定微博的转发微博列表。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="id">需要查询的微博ID。</param>
        /// <param name="sinceId">若指定此参数，则返回ID比since_id大的微博（即比since_id时间晚的微博），默认为0。</param>
        /// <param name="maxId">若指定此参数，则返回ID小于或等于max_id的微博，默认为0。</param>
        /// <param name="count">单页返回的记录条数，最大不超过200，默认为20。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="filterByAuthor">作者筛选类型，0：全部、1：我关注的人、2：陌生人，默认为0。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<RepostList> GetRepostTimelineAsync(this IWeiboClient client, long id, long sinceId = 0, long maxId = 0, int count = 20, int page = 1, int filterByAuthor = 0, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/statuses/repost_timeline.json?id={id}&since_id={sinceId}&max_id={maxId}&count={count}&page={page}&filter_by_author={filterByAuthor}";
            return client.GetAsync<RepostList>(url, cancellationToken);
        }

        /// <summary>
        /// 获取最新的提到登录用户的微博列表，即@我的微博。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sinceId">若指定此参数，则返回ID比since_id大的微博（即比since_id时间晚的微博），默认为0。</param>
        /// <param name="maxId">若指定此参数，则返回ID小于或等于max_id的微博，默认为0。</param>
        /// <param name="count">单页返回的记录条数，最大不超过200，默认为20。</param>
        /// <param name="page">返回结果的页码，默认为1。</param>
        /// <param name="filterByAuthor">作者筛选类型，0：全部、1：我关注的人、2：陌生人，默认为0。</param>
        /// <param name="filterBySource">来源筛选类型，0：全部、1：来自微博、2：来自微群，默认为0。</param>
        /// <param name="filterByType">原创筛选类型，0：全部微博、1：原创的微博，默认为0。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<StatusList> GetStatusesMentionsAsync(this IWeiboClient client, long sinceId = 0, long maxId = 0, int count = 20, int page = 1, int filterByAuthor = 0, int filterBySource = 0, int filterByType = 0, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/statuses/mentions.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&filter_by_author={filterByAuthor}&filter_by_source={filterBySource}&filter_by_type={filterByType}";
            return client.GetAsync<StatusList>(url, cancellationToken);
        }

        /// <summary>
        /// 根据微博ID获取单条微博内容。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="id">需要获取的微博ID。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Status> GetStatusAsync(this IWeiboClient client, long id, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/statuses/show.json?id={id}";
            return client.GetAsync<Status>(url, cancellationToken);
        }

        /// <summary>
        /// 批量获取指定微博的转发数评论数。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="ids">需要获取数据的微博ID，最多不超过100个。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<StatusCount[]> GetStatusCountAsync(this IWeiboClient client, long[] ids, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/statuses/count.json?ids={HttpUtility.UrlEncode(string.Join(",", ids))}";
            return client.GetAsync<StatusCount[]>(url, cancellationToken);
        }

        /// <summary>
        /// 第三方分享一条链接到微博。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="status">用户分享到微博的文本内容，内容不超过140个汉字，文本中不能包含“#话题词#”，同时文本中必须包含至少一个第三方分享到微博的网页URL，且该URL只能是该第三方（调用方）绑定域下的URL链接，绑定域在“我的应用 － 应用信息 － 基本应用信息编辑 － 安全域名”里设置。</param>
        /// <param name="rip">开发者上报的操作用户真实IP，形如：211.156.0.1。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Status> ShareAsync([NotNull] this IWeiboClient client, string status, string? rip = null, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var formData = new List<KeyValuePair<string?, string?>>
            {
                new KeyValuePair<string?, string?>("status", status)
            };
            if (rip != null)
            {
                formData.Add(new KeyValuePair<string?, string?>("rip", rip));
            }

            var postContent = new FormUrlEncodedContent(formData);
            const string url = "/statuses/share.json";
            return client.PostAsync<Status>(url, postContent, cancellationToken);
        }

        /// <summary>
        /// 第三方分享一条链接到微博。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="status">用户分享到微博的文本内容，内容不超过140个汉字，文本中不能包含“#话题词#”，同时文本中必须包含至少一个第三方分享到微博的网页URL，且该URL只能是该第三方（调用方）绑定域下的URL链接，绑定域在“我的应用 － 应用信息 － 基本应用信息编辑 － 安全域名”里设置。</param>
        /// <param name="pic">用户想要分享到微博的图片，仅支持JPEG、GIF、PNG图片，上传图片大小限制为&lt;5M。</param>
        /// <param name="rip">开发者上报的操作用户真实IP，形如：211.156.0.1。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Status> ShareAsync([NotNull] this IWeiboClient client, string status, byte[] pic, string? rip = null, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var postContent = new MultipartFormDataContent
            {
                {
                    new StringContent(status), "status"
                },
                {
                    new ByteArrayContent(pic), "pic"
                }
            };
            if (rip != null)
            {
                postContent.Add(new StringContent(rip), "rip");
            }

            const string url = "/statuses/share.json";
            return client.PostAsync<Status>(url, postContent, cancellationToken);
        }
    }
}
