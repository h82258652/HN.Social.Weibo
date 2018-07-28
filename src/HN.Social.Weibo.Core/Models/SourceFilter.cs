namespace HN.Social.Weibo.Models
{
    public enum SourceFilter
    {
        None = 0,

        /// <summary>
        /// 来自微博
        /// </summary>
        FromWeibo = 1,

        /// <summary>
        /// 来自微群
        /// </summary>
        FromWeiqun = 2
    }
}