using System;
using Android.Runtime;
using Newtonsoft.Json;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class PlacesPhoto
    {
        #region Properties

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("photo_reference")]
        public string PhotoReferenceId { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        #endregion
    }
}
