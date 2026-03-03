using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Locations Coordinates
    /// </summary>
    [DebuggerDisplay("Long: {Longitude} Lat: {Latitude}")]
    public class Coordinates
    {
        /// <summary>
        /// longitude
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Longitude)]
        [JsonInclude]
        public decimal Longitude { get; private set; }

        /// <summary>
        /// latitude
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Latitude)]
        [JsonInclude]
        public decimal Latitude { get; private set; }

        internal static Coordinates CreateDummy()
        {
            return new Coordinates()
            {
                Latitude = 1,
                Longitude = 2,
            };
        }
    }
}





