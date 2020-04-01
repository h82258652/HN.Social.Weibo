using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;
using Windows.Storage;

namespace HN.Social.Weibo
{
    /// <summary>
    /// <see cref="IWeiboClient" /> 扩展类。
    /// </summary>
    public static class WeiboClientUwpExtensions
    {
        /// <summary>
        /// 第三方分享一条链接到微博。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="status">用户分享到微博的文本内容，内容不超过140个汉字，文本中不能包含“#话题词#”，同时文本中必须包含至少一个第三方分享到微博的网页URL，且该URL只能是该第三方（调用方）绑定域下的URL链接，绑定域在“我的应用 － 应用信息 － 基本应用信息编辑 － 安全域名”里设置。</param>
        /// <param name="pic">用户想要分享到微博的图片，仅支持JPEG、GIF、PNG图片，上传图片大小限制为&lt;5M。</param>
        /// <param name="rip">开发者上报的操作用户真实IP，形如：211.156.0.1。</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Status> ShareAsync(this IWeiboClient client, string status, StorageFile pic, string? rip = null, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }
            if (pic == null)
            {
                throw new ArgumentNullException(nameof(pic));
            }

            var image = (await FileIO.ReadBufferAsync(pic)).ToArray();
            return await client.ShareAsync(status, image, rip, cancellationToken);
        }
    }
}
