using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Custom Field (NB: Feature not available in Free Edition of Trello)
    /// </summary>
    public class CustomField
    {
        /// <summary>
        /// Id of Custom Field
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of Custom Field
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}