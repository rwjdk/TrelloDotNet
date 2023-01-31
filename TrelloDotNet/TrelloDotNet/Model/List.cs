using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public class List //todo - should it be renamed to not confuse with generic lists?
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} (Id: {Id})";
        }
    }
}