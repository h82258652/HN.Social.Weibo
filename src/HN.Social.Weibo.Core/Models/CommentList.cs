﻿using Newtonsoft.Json;

namespace HN.Social.Weibo.Models
{
    public class CommentList : WeiboResult
    {
        [JsonProperty("comments")]
        public Comment[] Comments { get; set; }

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
    }
}
