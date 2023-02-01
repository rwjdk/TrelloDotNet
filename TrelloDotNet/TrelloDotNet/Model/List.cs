using System;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    public class List
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("closed")]
        public bool Closed { get; set; }

        [JsonPropertyName("idBoard")]
        public string BoardId { get; set; }

        [JsonPropertyName("pos")]
        public int Position { get; set; }

        [JsonPropertyName("subscribed")]
        public bool Subscribed { get; set; }
        
        /// <summary>
        /// If there is a Soft Limit to number of cards in the list (Provided by PowerUp 'List Limits' from Trello)
        /// </summary>
        [JsonPropertyName("softLimit")]
        public int? SoftLimit { get; set; }

        [JsonIgnore] public DateTimeOffset Created => IdToCreatedHelper.GetCreatedFromId(Id);

        public override string ToString()
        {
            return $"{Name} (Id: {Id})";
        }
    }
}