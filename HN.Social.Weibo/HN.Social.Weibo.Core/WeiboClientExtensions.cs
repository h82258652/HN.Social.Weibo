﻿using System;
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

        public static Task<Timeline> GetCurrentUserTimelineAsync(this IWeiboClient client, long sinceId = 0, long maxId = 0, long count = 20, int page = 1, bool onlyCurrentApp = false)
        {
            // TODO 剩余参数
            // feature
            // trim_user

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<Timeline>($"/statuses/user_timeline.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&base_app={(onlyCurrentApp ? "1" : "0")}");
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

        public static Task<Timeline> GetHomeTimelineAsync(this IWeiboClient client, long sinceId = 0, long maxId = 0, int count = 20, int page = 1, bool onlyCurrentApp = false)
        {
            // TODO 剩余参数
            // feature
            // trim_user

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<Timeline>($"/statuses/home_timeline.json?since_id={sinceId}&max_id={maxId}&count={count}&page={page}&base_app={(onlyCurrentApp ? "1" : "0")}");
        }

        public static Task<RepostTimeline> GetRepostTimelineAsync(this IWeiboClient client, long statusId, long sinceId = 0, long maxId = 0, int count = 20, int page = 1)
        {
            // TODO filter_by_author

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<RepostTimeline>($"/statuses/repost_timeline.json?id={statusId}&since_id={sinceId}&max_id={maxId}&count={count}&page={page}");
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

        public static Task<Timeline> GetUserTimelineAsync(this IWeiboClient client, long userId, long sinceId = 0, long maxId = 0, int count = 20, int page = 1, bool onlyCurrentApp = false)
        {
            // TODO 剩余参数
            // feature
            // trim_user

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<Timeline>($"/statuses/user_timeline.json?uid={userId}&since_id={sinceId}&max_id={maxId}&count={count}&page={page}&base_app={(onlyCurrentApp ? "1" : "0")}");
        }

        public static Task<Timeline> GetUserTimelineAsync(this IWeiboClient client, string nickname, long sinceId = 0, long maxId = 0, int count = 20, int page = 1, bool onlyCurrentApp = false)
        {
            // TODO 剩余参数
            // feature
            // trim_user

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (nickname == null)
            {
                throw new ArgumentNullException(nameof(nickname));
            }

            return client.GetAsync<Timeline>($"/statuses/user_timeline.json?screen_name={WebUtility.UrlEncode(nickname)}&since_id={sinceId}&max_id={maxId}&count={count}&page={page}&base_app={(onlyCurrentApp ? "1" : "0")}");
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