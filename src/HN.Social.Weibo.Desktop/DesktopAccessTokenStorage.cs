using System;
using System.Security.Cryptography;
using System.Text;
using HN.Social.Weibo.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace HN.Social.Weibo
{
    public class DesktopAccessTokenStorage : IAccessTokenStorage
    {
        private const string AccessTokenKey = "AccessToken";
        private const string ExpiresAtKey = "ExpiresAt";
        private const string UserIdKey = "UserId";
        private const string WeiboFileName = "weibo";
        private static readonly ISettings Settings = CrossSettings.Current;

        public void Clear()
        {
            Settings.Remove(ExpiresAtKey, WeiboFileName);
            Settings.Remove(UserIdKey, WeiboFileName);
            Settings.Remove(AccessTokenKey, WeiboFileName);
        }

        public AccessToken Load()
        {
            try
            {
                var protectedExpiresAt = Settings.GetValueOrDefault(ExpiresAtKey, null, WeiboFileName);
                var protectedUserId = Settings.GetValueOrDefault(UserIdKey, null, WeiboFileName);
                var protectedValue = Settings.GetValueOrDefault(AccessTokenKey, null, WeiboFileName);

                var expiresAt = DateTime.Parse(Unprotect(protectedExpiresAt));
                var userId = long.Parse(Unprotect(protectedUserId));
                var value = Unprotect(protectedValue);

                return new AccessToken
                {
                    ExpiresAt = expiresAt,
                    UserId = userId,
                    Value = value
                };
            }
            catch
            {
                return null;
            }
        }

        public void Save(AccessToken accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            var protectedValue = Protect(accessToken.Value);
            var protectedExpiresAt = Protect(accessToken.ExpiresAt.ToString("O"));
            var protectedUserId = Protect(accessToken.UserId.ToString());

            Settings.AddOrUpdateValue(ExpiresAtKey, protectedExpiresAt, WeiboFileName);
            Settings.AddOrUpdateValue(UserIdKey, protectedUserId, WeiboFileName);
            Settings.AddOrUpdateValue(AccessTokenKey, protectedValue, WeiboFileName);
        }

        private static string Protect(string plaintext)
        {
            var bytes = Encoding.UTF8.GetBytes(plaintext);
            var protectedData = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(protectedData);
        }

        private static string Unprotect(string protectedData)
        {
            var bytes = Convert.FromBase64String(protectedData);
            var unprotectedData = ProtectedData.Unprotect(bytes, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(unprotectedData);
        }
    }
}
