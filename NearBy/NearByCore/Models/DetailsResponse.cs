using System;
using Android.Runtime;
using Newtonsoft.Json;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class DetailsResponse
    {
        #region Properties

        [JsonProperty("result")]
        public DetailsResponseResult Result { get; set; }

        [JsonIgnore]
        public DateTime RetrievedDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        #endregion
    }
}
