using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Locations Coordinates
    /// </summary>
    public class Coordinates
    {
        /// <summary>
        /// longitude
        /// </summary>
        [JsonPropertyName("longitude")]
        [JsonInclude]
        public decimal Longitude { get; private set; }
        /// <summary>
        /// latitude
        /// </summary>
        [JsonPropertyName("latitude")]
        [JsonInclude]
        public decimal Latitude { get; private set; }
    }
}