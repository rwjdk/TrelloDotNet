using System.Globalization;
using System;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    public class Label
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("idBoard")]
        public string BoardId { get; set; }

        [JsonIgnore] public DateTimeOffset Created => IdToCreatedHelper.GetCreatedFromId(Id);
        
        public override string ToString()
        {
            return $"{Name} (Id: {Id})";
        }
    }
}