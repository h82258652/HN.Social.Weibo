using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using HN.Social.Weibo.Models;
using Windows.Storage;

namespace HN.Social.Weibo
{
    public static class WeiboClientUwpExtensions
    {
        public static async Task<Status> ShareAsync(this IWeiboClient client, string status, StorageFile imageFile, string ip = null)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }
            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }

            var image = (await FileIO.ReadBufferAsync(imageFile)).ToArray();
            return await client.ShareAsync(status, image, ip);
        }
    }
}