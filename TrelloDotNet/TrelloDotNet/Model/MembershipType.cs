using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public enum MembershipType
    {
        [JsonPropertyName("admin")]
        Admin,
        [JsonPropertyName("normal")]
        Normal,
        [JsonPropertyName("observer")]
        Observer
    }
}