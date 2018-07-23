using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class Timeline : WeiboResult
    {
        [JsonProperty("statuses")]
        public Status[] Statuses { get; set; }

        [JsonProperty("advertises")]
        public object[] Advertises { get; set; }

        [JsonProperty("ad")]
        public object[] Ad { get; set; }

        [JsonProperty("hasvisible")]
        public bool HasVisible { get; set; }

        [JsonProperty("previous_cursor")]
        public long PreviousCursor { get; set; }

        [JsonProperty("next_cursor")]
        public long NextCursor { get; set; }

        [JsonProperty("total_number")]
        public int TotalNumber { get; set; }

        [JsonProperty("interval")]
        public int Interval { get; set; }

        [JsonProperty("uve_blank")]
        public int UveBlank { get; set; }

        [JsonProperty("since_id")]
        public long SinceId { get; set; }

        [JsonProperty("max_id")]
        public long MaxId { get; set; }

        [JsonProperty("has_unread")]
        public int HasUnread { get; set; }
    }
}