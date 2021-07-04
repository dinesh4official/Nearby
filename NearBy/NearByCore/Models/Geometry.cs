using System;
using Android.Runtime;
using Newtonsoft.Json;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class Geometry
    {
        #region Properties

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("viewport")]
        public Viewport Viewport { get; set; }

        #endregion
    }
}
