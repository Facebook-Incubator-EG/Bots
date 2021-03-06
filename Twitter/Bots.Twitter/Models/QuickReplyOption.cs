﻿using Newtonsoft.Json;

namespace Bots.Twitter
{
    public class QuickReplyOption
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }
    }
}
