using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public enum CustomFieldPosition //todo- should this stay
    {
        [JsonPropertyName("top")]
        Top,
        [JsonPropertyName("1024")]
        Position1,
        [JsonPropertyName("2048")]
        Position2,
        //todo - Add more
        [JsonPropertyName("bottom")]
        Bottom,
    }
}