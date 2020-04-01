namespace HN.Social.Weibo.Authorization
{
    /// <summary>
    /// 用户取消授权异常。
    /// </summary>
    public class UserCancelAuthorizationException : AuthorizationException
    {
        /// <summary>
        /// 初始化 <see cref="UserCancelAuthorizationException" /> 类的新实例。
        /// </summary>
        public UserCancelAuthorizationException() : base("用户取消了授权。")
        {
        }
    }
}
