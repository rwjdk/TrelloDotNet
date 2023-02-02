using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public enum CardCoverColor //todo - use
    {
        [JsonPropertyName("pink")]
        Pink,
        [JsonPropertyName("yellow")]
        Yellow,
        [JsonPropertyName("lime")]
        Lime,
        [JsonPropertyName("blue")]
        Blue,
        [JsonPropertyName("black")]
        Black,
        [JsonPropertyName("orange")]
        Orange,
        [JsonPropertyName("red")]
        Red,
        [JsonPropertyName("purple")]
        Purple,
        [JsonPropertyName("sky")]
        Sky,
        [JsonPropertyName("green")]
        Green
    }

}