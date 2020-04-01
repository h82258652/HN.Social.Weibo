namespace HN.Social.Weibo
{
    /// <summary>
    /// 微博配置。
    /// </summary>
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
        /// 是否在调用微博 API 时自动执行授权操作。默认 <see langword="true" />。
        /// </summary>
        public bool IsAutoSignInEnabled { get; set; } = true;

        /// <summary>
        /// 授权回调地址，站外应用需与设置的回调地址一致，站内应用需填写canvas page的地址。
        /// </summary>
        public string RedirectUri { get; set; }
        
        /// <summary>
        /// 申请scope权限所需参数，可一次申请多个scope权限，用逗号分隔。
        /// </summary>
        public string? Scope { get; set; }
    }
}
