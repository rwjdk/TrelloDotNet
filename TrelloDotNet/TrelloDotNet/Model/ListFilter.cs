using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public enum ListFilter
    {
        [JsonPropertyName("all")]
        All,
        [JsonPropertyName("closed")]
        Closed,
        [JsonPropertyName("open")]
        Open
    }
}