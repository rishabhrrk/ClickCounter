using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SearchAnalytics.Models
{
    // Class to store the JSON response from the iTunes search API
    public class JsonResults
    {
        [JsonProperty("resultCount")]
        public int ResultCount { get; set; }
        
        [JsonProperty("results")]
        public List<SearchItem> Results { get; set; }
    }
}