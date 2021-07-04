using System;
using System.Collections.Generic;
using Android.Runtime;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class PlaceDetails
    {
        #region Properties

        public string AddressDigest { get; set; }

        public string FirstPhotoUrl { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public long NumberOfRatings { get; set; }

        public List<string> PhotoUrls { get; set; }

        public string PlaceId { get; set; }

        public List<string> PlaceTypes { get; set; }

        public int PriceLevel { get; set; }

        public decimal Rating { get; set; }

        #endregion
    }
}
