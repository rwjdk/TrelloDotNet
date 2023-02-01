using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public enum CustomFieldType
    {
        [JsonPropertyName("checkbox")]
        Checkbox,
        [JsonPropertyName("list")]
        List,
        [JsonPropertyName("number")]
        Number,
        [JsonPropertyName("text")]
        Text,
        [JsonPropertyName("date")]
        Date
    }
}