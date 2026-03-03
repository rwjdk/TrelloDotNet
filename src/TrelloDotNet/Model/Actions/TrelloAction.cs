using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Represent an Action
    /// </summary>
    [DebuggerDisplay("{Type} (Id: {Id}) @ {Date}")]
    public class TrelloAction
    {
        /// <summary>
        /// Id of a comment action
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// The Id of the Member (User) that did the action
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.IdMemberCreator)]
        [JsonInclude]
        public string MemberCreatorId { get; private set; }

        /// <summary>
        /// Type of the Action (example Update of a Card)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Type)]
        [JsonInclude]
        public string Type { get; private set; }

        /// <summary>
        /// Date of the Action
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Date)]
        [JsonInclude]
        public DateTimeOffset Date { get; private set; }

        /// <summary>
        /// The Id of the Member (User) that did the action
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.MemberCreator)]
        [JsonInclude]
        public Member MemberCreator { get; private set; }

        /// <summary>
        /// Data of the Action
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Data)]
        [JsonInclude]
        public TrelloActionData Data { get; private set; }
    }
}




