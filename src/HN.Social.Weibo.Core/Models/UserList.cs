using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class UserList : WeiboResult
    {
        [JsonProperty("users")]
        public UserInfo Users { get; set; }

        [JsonProperty("next_cursor")]
        public int NextCursor { get; set; }

        [JsonProperty("previous_cursor")]
        public int PreviousCursor { get; set; }

        [JsonProperty("total_number")]
        public int TotalNumber { get; set; }
    }
}
