using System;

namespace HN.Social.Weibo.Models
{
    public class AccessToken
    {
        public DateTime ExpiresAt { get; set; }

        public long UserId { get; set; }

        public string Value { get; set; }
    }
}
