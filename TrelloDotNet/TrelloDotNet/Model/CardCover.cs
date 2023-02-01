using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public class CardCover
    {
        [JsonPropertyName("size")]
        public string Size { get; set; } //todo - enum?
        
        [JsonPropertyName("color")]
        public string Color { get; set; } //todo - enum

        [JsonPropertyName("idUploadedBackground")]
        public string BackgroundImageId { get; set; }
    }
}