using System;
using Android.Runtime;
using Newtonsoft.Json;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class Viewport
    {
        #region Properties

        [JsonProperty("northeast")]
        public Location Northeast { get; set; }

        [JsonProperty("southwest")]
        public Location Southwest { get; set; }

        #endregion
    }
}
