using System;
using HN.Social.Weibo.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace HN.Social.Weibo
{
    public class DesktopAccessTokenStorage : IAccessTokenStorage
    {
        private const string WeiboFileName = "weibo";
        private static readonly ISettings Settings = CrossSettings.Current;

        public void Clear()
        {
            Settings.Remove("ExpiresAt", WeiboFileName);
            Settings.Remove("UserId", WeiboFileName);
            Settings.Remove("AccessToken", WeiboFileName);
        }

        public AccessToken Load()
        {
            var expiresAt = Settings.GetValueOrDefault("ExpiresAt", DateTime.MinValue, WeiboFileName);
            var userId = Settings.GetValueOrDefault("UserId", 0L, WeiboFileName);
            var value = Settings.GetValueOrDefault("AccessToken", null, WeiboFileName);

            return new AccessToken
            {
                ExpiresAt = expiresAt,
                UserId = userId,
                Value = value
            };
        }

        public void Save(AccessToken accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            Settings.AddOrUpdateValue("ExpiresAt", accessToken.ExpiresAt, WeiboFileName);
            Settings.AddOrUpdateValue("UserId", accessToken.UserId, WeiboFileName);
            Settings.AddOrUpdateValue("AccessToken", accessToken.Value, WeiboFileName);
        }
    }
}
