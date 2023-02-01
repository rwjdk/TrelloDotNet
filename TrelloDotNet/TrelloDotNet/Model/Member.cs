using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public class Member
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}