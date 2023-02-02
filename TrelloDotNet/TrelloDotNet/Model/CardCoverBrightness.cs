using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public enum CardCoverBrightness
    {
        [JsonPropertyName("dark")]
        Dark,
        [JsonPropertyName("light")]
        Light
    }
}