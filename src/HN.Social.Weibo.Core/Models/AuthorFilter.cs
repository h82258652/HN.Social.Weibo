namespace HN.Social.Weibo.Models
{
    public enum AuthorFilter
    { 
        None = 0,

        /// <summary>
        /// 我关注的人
        /// </summary>
        Following = 1,

        /// <summary>
        /// 陌生人
        /// </summary>
        Stranger = 2,
    }
}