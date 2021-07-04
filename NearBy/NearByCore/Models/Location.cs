using System;
using Android.Runtime;
using Newtonsoft.Json;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class Location
    {
        #region Properties

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lng")]
        public double Longitude { get; set; }

        #endregion
    }
}
