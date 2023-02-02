using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public enum CardCoverSize //todo - use
    {
        [JsonPropertyName("normal")]
        Normal,
        [JsonPropertyName("full")]
        Full
    }
}