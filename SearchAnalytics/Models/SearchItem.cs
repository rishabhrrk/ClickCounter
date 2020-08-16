using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchAnalytics.Models
{
    // Class to store individual result
    // Each object stores TrackId, Kind, CollectionId, TrackName, CollectionName, TrackViewURL, ArtworkURL100, Clicks
    public class SearchItem
    {
        // Unique Id of each content
        [JsonProperty("trackId")]
        public int Id { get; set; }

        // Type of media
        [JsonProperty("kind")]
        public string Kind { get; set; }

        // Collection to which media belongs
        [JsonProperty("collectionId")]
        public int CollectionId { get; set; }

        // Name of artist
        [JsonProperty("artistName")]
        public string ArtistName { get; set; }

        // Name of Track
        [JsonProperty("trackName")]
        public string TrackName { get; set; }

        // Name of collection to which it belongs
        [JsonProperty("collectionName")]
        public string CollectionName { get; set; }

        // Media link
        [JsonProperty("trackViewUrl")]
        public string TrackUri { get; set; }

        // Image of the content, album cover or image in resolution 100
        [JsonProperty("artworkUrl100")]
        public string ArtworkUrl100 { get; set; }

        // Counter for maintaining number of clicks
        public int Clicks { get; set; }
    }
}