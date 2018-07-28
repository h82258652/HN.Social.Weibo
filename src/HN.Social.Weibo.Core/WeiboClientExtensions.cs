using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace HN.Social.Weibo
{
    public static class WeiboClientExtensions
    {
        public static async Task<IReadOnlyList<District>> GetCityListAsync(this IWeiboClient client, string provinceCode, char? firstLetter = null, DistrictLanguage language = DistrictLanguage.SimplifiedChinese)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (provinceCode == null)
            {
                throw new ArgumentNullException(nameof(provinceCode));
            }

            var url = $"/common/get_city.json?province={provinceCode}";
            if (firstLetter.HasValue)
            {
                url += $"&capital={firstLetter.Value.ToString()}";
            }
            switch (language)
            {
                case DistrictLanguage.SimplifiedChinese:
                    url += "&language=zh-cn";
                    break;

                case DistrictLanguage.TraditionalChinese:
                    url += "&language=zh-tw";
                    break;

                case DistrictLanguage.English:
                    url += "&language=english";
                    break;
            }

            var result = await client.GetAsync<IEnumerable<JObject>>(url);
            return result
                .Select(item => (JProperty)item.First)
                .Select(property => new District()
                {
                    Code = property.Name,
                    Name = property.Value.ToObject<string>()
                })
                .ToList();
        }

        public static async Task<IReadOnlyList<District>> GetCountryListAsync(this IWeiboClient client, char? firstLetter = null, DistrictLanguage language = DistrictLanguage.SimplifiedChinese)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var queryString = new QueryString();
            if (firstLetter.HasValue)
            {
                queryString = queryString.Add("capital", firstLetter.Value.ToString());
            }
            switch (language)
            {
                case DistrictLanguage.SimplifiedChinese:
                    queryString = queryString.Add("language", "zh-cn");
                    break;

                case DistrictLanguage.TraditionalChinese:
                    queryString = queryString.Add("language", "zh-tw");
                    break;

                case DistrictLanguage.English:
                    queryString = queryString.Add("language", "english");
                    break;
            }
            var url = "/common/get_country.json" + queryString.ToUriComponent();

            var result = await client.GetAsync<IEnumerable<JObject>>(url);
            return result
                .Select(item => (JProperty)item.First)
                .Select(property => new District
                {
                    Code = property.Name,
                    Name = property.Value.ToObject<string>()
                })
                .ToList();
        }

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

        public static Task<Timeline> GetCurrentUserTimelineAsync(this IWeiboClient client, long sinceId = 0, long maxId = 0, long count = 20, int page = 1, bool onlyCurrentApp = false, bool includeUser = true)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<Timeline>($"/statuses/user_timeline.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&base_app={(onlyCurrentApp ? "1" : "0")}&trim_user={(includeUser ? "0" : "1")}");
        }

        /// <summary>
        /// 获取微博官方表情的详细信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="type">表情类别，默认为普通表情</param>
        /// <param name="language">语言类别，默认为简体</param>
        /// <returns></returns>
        public static Task<IReadOnlyList<Emotion>> GetEmotionsAsync(this IWeiboClient client, EmotionType type = EmotionType.Face, EmotionLanguage language = EmotionLanguage.SimplifiedChinese)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (!Enum.IsDefined(typeof(EmotionType), type))
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }
            if (!Enum.IsDefined(typeof(EmotionLanguage), language))
            {
                throw new ArgumentOutOfRangeException(nameof(language));
            }

            var url = "/emotions.json";
            switch (type)
            {
                case EmotionType.Face:
                    url += "?type=face";
                    break;

                case EmotionType.Animation:
                    url += "?type=ani";
                    break;

                case EmotionType.Cartoon:
                    url += "?type=cartoon";
                    break;
            }
            switch (language)
            {
                case EmotionLanguage.SimplifiedChinese:
                    url += "&language=cnname";
                    break;

                case EmotionLanguage.TraditionalChinese:
                    url += "&language=twname";
                    break;
            }
            return client.GetAsync<IReadOnlyList<Emotion>>(url);
        }

        public static Task<Timeline> GetHomeTimelineAsync(this IWeiboClient client, long sinceId = 0, long maxId = 0, int count = 20, int page = 1, bool onlyCurrentApp = false, bool includeUser = true)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<Timeline>($"/statuses/home_timeline.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&base_app={(onlyCurrentApp ? "1" : "0")}&trim_user={(includeUser ? "0" : "1")}");
        }

        public static async Task<IReadOnlyList<District>> GetProvinceListAsync(this IWeiboClient client, string countryCode, char? firstLetter = null, DistrictLanguage language = DistrictLanguage.SimplifiedChinese)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (countryCode == null)
            {
                throw new ArgumentNullException(nameof(countryCode));
            }

            var url = $"/common/get_province.json?country={countryCode}";
            if (firstLetter.HasValue)
            {
                url += $"&capital={firstLetter.Value.ToString()}";
            }
            switch (language)
            {
                case DistrictLanguage.SimplifiedChinese:
                    url += "&language=zh-cn";
                    break;

                case DistrictLanguage.TraditionalChinese:
                    url += "&language=zh-tw";
                    break;

                case DistrictLanguage.English:
                    url += "&language=english";
                    break;
            }

            var result = await client.GetAsync<IEnumerable<JObject>>(url);
            return result
                .Select(item => (JProperty)item.First)
                .Select(property => new District
                {
                    Code = property.Name,
                    Name = property.Value.ToObject<string>()
                })
                .ToList();
        }

        public static Task<RepostTimeline> GetRepostTimelineAsync(this IWeiboClient client, long statusId, long sinceId = 0, long maxId = 0, int count = 20, int page = 1, AuthorFilter authorFilter = AuthorFilter.None)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<RepostTimeline>($"/statuses/repost_timeline.json?id={statusId}&since_id={sinceId}&max_id={maxId}&count={count}&page={page}&filter_by_author={(int)authorFilter}");
        }

        /// <summary>
        /// 获取当前登录用户所发出的评论列表
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sinceId"></param>
        /// <param name="maxId"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="sourceFilter"></param>
        /// <returns></returns>
        public static Task<CommentList> GetCurrentUserSentCommentsAsync(this IWeiboClient client, long sinceId = 0, long maxId = 0, int count = 50, int page = 1, SourceFilter sourceFilter = SourceFilter.None)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/comments/by_me.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&filter_by_source={(int)sourceFilter}";
            return client.GetAsync<CommentList>(url);
        }

        /// <summary>
        /// 获取当前登录用户所接收到的评论列表
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sinceId"></param>
        /// <param name="maxId"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="authorFilter"></param>
        /// <param name="sourceFilter"></param>
        /// <returns></returns>
        public static Task<CommentList> GetCurrentUserReceivedCommentsAsync(this IWeiboClient client, long sinceId = 0, long maxId = 0, int count = 50, int page = 1, AuthorFilter authorFilter = AuthorFilter.None, SourceFilter sourceFilter = SourceFilter.None)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/comments/to_me.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&filter_by_author={(int)authorFilter}&filter_by_source={(int)sourceFilter}";
            return client.GetAsync<CommentList>(url);
        }

        public static Task<Status> GetStatusAsync(this IWeiboClient client, long id)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<Status>($"/statuses/show.json?id={id}");
        }

        public static async Task<IReadOnlyList<string>> GetTimezoneListAsync(this IWeiboClient client, DistrictLanguage language = DistrictLanguage.SimplifiedChinese)
        {
            var url = "/common/get_timezone.json";
            switch (language)
            {
                case DistrictLanguage.SimplifiedChinese:
                    url += "?language=zh-cn";
                    break;

                case DistrictLanguage.TraditionalChinese:
                    url += "?language=zh-tw";
                    break;

                case DistrictLanguage.English:
                    url += "?language=english";
                    break;
            }

            var result = await client.GetAsync<IDictionary<string, string>>(url);
            return result.Values.ToList();
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

        public static Task<Timeline> GetUserTimelineAsync(this IWeiboClient client, long userId, long sinceId = 0, long maxId = 0, int count = 20, int page = 1, bool onlyCurrentApp = false, bool includeUser = true)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<Timeline>($"/statuses/user_timeline.json?uid={userId}&since_id={sinceId}&max_id={maxId}&count={count}&page={page}&base_app={(onlyCurrentApp ? "1" : "0")}&trim_user={(includeUser ? "0" : "1")}");
        }

        public static Task<Timeline> GetUserTimelineAsync(this IWeiboClient client, string nickname, long sinceId = 0, long maxId = 0, int count = 20, int page = 1, bool onlyCurrentApp = false, bool includeUser = true)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (nickname == null)
            {
                throw new ArgumentNullException(nameof(nickname));
            }

            return client.GetAsync<Timeline>($"/statuses/user_timeline.json?screen_name={WebUtility.UrlEncode(nickname)}&since_id={sinceId}&max_id={maxId}&count={count}&page={page}&base_app={(onlyCurrentApp ? "1" : "0")}&trim_user={(includeUser ? "0" : "1")}");
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

            var postData = new Dictionary<string, string>
            {
                ["cid"] = commentId.ToString(),
                ["id"] = statusId.ToString(),
                ["comment"] = comment,
                ["without_mention"] = withoutMention ? "0" : "1",
                ["comment_ori"] = commentOriginStatus ? "1" : "0"
            };
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