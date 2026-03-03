using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent an Emoji
    /// </summary>
    [DebuggerDisplay("Name = {Name}, ShortName = {ShortName}")]
    public class Emoji
    {
        /// <summary>
        /// Unified Id of Emoji
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Unified)]
        [JsonInclude]
        public string UnifiedId { get; set; }

        /// <summary>
        /// The Native value (aka the visual emoji)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Native)]
        [JsonInclude]
        public string Native { get; set; }

        /// <summary>
        /// Name of the Emoji
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Name)]
        [JsonInclude]
        public string Name { get; set; }

        /// <summary>
        /// The Skin-variation of the Emoji
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.LabelFields.SkinVariation)]
        [JsonInclude]
        public string SkinVariation { get; set; }

        /// <summary>
        /// ShortName of the Emoji
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.ShortName)]
        [JsonInclude]
        public string ShortName { get; set; }
    }
}





