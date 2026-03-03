using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a permission of a token
    /// </summary>
    [DebuggerDisplay("ModelType = {ModelType}, ModelId = {ModelId}, Read = {Read}, Write = {Write}")]
    public class TokenInformationPermission
    {
        /// <summary>
        /// The Id of the Trello Object indicated by the ModelType Field (Aka id of a Member, Board or Organization)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.TokenFields.IdModel)]
        [JsonInclude]
        public string ModelId { get; private set; }

        /// <summary>
        /// The Type of Model the ModelId represent (Member, Board or Organization)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.TokenFields.ModelType)]
        [JsonInclude]
        public string ModelType { get; private set; }

        /// <summary>
        /// If the Token have Read rights or not
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.TokenFields.Read)]
        [JsonInclude]
        public bool Read { get; private set; }

        /// <summary>
        /// If the Token have Write rights or not
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.TokenFields.Write)]
        [JsonInclude]
        public bool Write { get; private set; }
    }
}





