using System;
using Android.Runtime;
using Newtonsoft.Json;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class OpeningHours
    {
        #region Properties

        [JsonProperty("open_now")]
        public bool OpenNow { get; set; }

        #endregion
    }
}
