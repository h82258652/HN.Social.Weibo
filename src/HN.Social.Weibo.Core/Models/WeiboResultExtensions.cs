namespace HN.Social.Weibo.Models
{
    public static class WeiboResultExtensions
    {
        public static bool Success(this WeiboResult result)
        {
            return result.ErrorCode == 0;
        }
    }
}
