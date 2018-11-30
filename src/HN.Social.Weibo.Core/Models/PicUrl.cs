using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class PicUrl
    {
        [JsonProperty("thumbnail_pic")]
        public string ThumbnailPic { get; set; }
    }
}
