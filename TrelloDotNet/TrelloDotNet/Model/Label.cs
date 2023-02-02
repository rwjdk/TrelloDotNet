using System;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Label defined on a Board and Assigned to Cards
    /// </summary>
    public class Label
    {
        /// <summary>
        /// Id of the Label
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the Label
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Color of the Label
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; }

        /// <summary>
        /// Id of the Board the Label belong to
        /// </summary>
        [JsonPropertyName("idBoard")]
        public string BoardId { get; set; }

        /// <summary>
        /// When the Label was created [stored in UTC]
        /// </summary>
        [JsonIgnore] public DateTimeOffset Created => IdToCreatedHelper.GetCreatedFromId(Id);

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name} (Id: {Id})";
        }
    }
}