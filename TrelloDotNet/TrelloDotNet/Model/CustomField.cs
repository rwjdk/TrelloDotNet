using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public class CustomField : RawJsonObject
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}