using System;
using System.Collections.Generic;
using Android.Runtime;
using Newtonsoft.Json;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class PlaceResult
    {
        #region Properties

        [JsonProperty("alt_ids")]
        public List<AltId> AltIds { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("icon")]
        public string IconUrl { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("opening_hours")]
        public OpeningHours OpeningHours { get; set; }

        [JsonProperty("photos")]
        public List<PlacesPhoto> Photos { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("types")]
        public List<string> PlaceTypes { get; set; }

        [JsonProperty("rating")]
        public decimal Rating { get; set; }

        [JsonProperty("referece")]
        public string ReferenceId { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }

        #endregion
    }
}