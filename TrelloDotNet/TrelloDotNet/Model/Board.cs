using System;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Representation of a Board
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Id of the Board (This is the long version of the Id)
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the board
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the Board
        /// </summary>
        [JsonPropertyName("desc")]
        public string Description { get; set; }
        
        /// <summary>
        /// URL for the Board
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
        
        /// <summary>
        /// Short Version URL for the Board
        /// </summary>
        [JsonPropertyName("shortUrl")]
        public string ShortUrl { get; set; }

        /// <summary>
        /// Date the Board was created [stored in UTC]
        /// </summary>
        [JsonIgnore] public DateTimeOffset Created => IdToCreatedHelper.GetCreatedFromId(Id);

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name} (Id: {Id})";
        }
    }
}