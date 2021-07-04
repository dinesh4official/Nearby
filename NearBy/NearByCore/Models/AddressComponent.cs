using System;
using System.Collections.Generic;
using Android.Runtime;
using Newtonsoft.Json;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class AddressComponent
    {
        #region Properties

        [JsonProperty("long_name")]
        public string LongName { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }

        #endregion
    }
}
