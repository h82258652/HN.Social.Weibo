using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HN.Social.Weibo.Http
{
    internal static class HttpContentExtensions
    {
        internal static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}