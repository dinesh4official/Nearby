using System;
using Android.Runtime;
using Newtonsoft.Json;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class AltId
    {
        #region Properties

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        #endregion
    }
}
