using System;

namespace HN.Social.Weibo
{
    public class EfCoreAccessToken
    {
        public DateTime ExpiresAt { get; set; }
        
        public Guid Id { get; set; }
        
        public string OwnerAppKey { get; set; }

        public long UserId { get; set; }

        public string Value { get; set; }
    }
}
