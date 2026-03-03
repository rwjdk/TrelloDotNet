using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The Card Aging type
    /// </summary>
    public enum BoardPreferenceCardAging
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Unknown = -1,

        /// <summary>
        /// Regular
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Regular)]
        Regular,

        /// <summary>
        /// Pirate
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Pirate)]
        Pirate,
    }
}





