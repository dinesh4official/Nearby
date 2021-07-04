using System;
using System.Collections.Generic;
using Android.Runtime;
using Newtonsoft.Json;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class PlacesResponse
    {
        #region Properties

        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }

        [JsonProperty("results")]
        public List<PlaceResult> Results { get; set; }

        [JsonIgnore]
        public DateTime RetrievedDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        #endregion
    }
}
