using System;
using System.Collections.Generic;
using Android.Runtime;
using Newtonsoft.Json;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class DetailsResponseResult
    {
        #region Properties

        [JsonProperty("address_components")]
        public List<AddressComponent> AddressComponents { get; set; }

        [JsonProperty("adr_address")]
        public string AdrAddress { get; set; }

        [JsonProperty("country-name")]
        public string CountryName { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("formatted_phone_number")]
        public string FormattedPhoneNumber { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("icon")]
        public string IconUrl { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("international_phone_number")]
        public string InternationalPhoneNumber { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photos")]
        public List<PlacesPhoto> Photos { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("postal-code")]
        public string PostalCode { get; set; }

        [JsonProperty("price_level")]
        public int PriceLevel { get; set; }

        [JsonProperty("rating")]
        public decimal Rating { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("reviews")]
        public List<Review> Reviews { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("street-address")]
        public string StreetAddress { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("utcoffset")]
        public short UtcOffset { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }

        [JsonProperty("website")]
        public string WebsiteUrl { get; set; }

        #endregion
    }
}
