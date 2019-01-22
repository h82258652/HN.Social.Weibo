namespace HN.Social.Weibo
{
    public class WeiboOptions
    {
        /// <summary>
        /// 申请应用时分配的AppKey。
        /// </summary>
        public string AppKey { get; set; }
        
        /// <summary>
        /// 申请应用时分配的AppSecret。
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 授权回调地址，站外应用需与设置的回调地址一致，站内应用需填写canvas page的地址。
        /// </summary>
        public string RedirectUri { get; set; }
    }
}
