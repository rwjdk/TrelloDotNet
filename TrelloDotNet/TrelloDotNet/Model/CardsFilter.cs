using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public enum CardsFilter
    {
        [JsonPropertyName("all")]
        All,
        [JsonPropertyName("closed")]
        Closed,
        [JsonPropertyName("open")]
        Open,
        [JsonPropertyName("visible")]
        Visible
    }
}