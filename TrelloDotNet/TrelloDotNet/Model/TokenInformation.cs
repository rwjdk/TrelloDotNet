using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent information about a Token
    /// </summary>
    [DebuggerDisplay("{Identifier} (Id: {Id})")]
    public class TokenInformation
    {
        /// <summary>
        /// Id of the Token use in the TrelloClient
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// The Identifier of the Trello Application the token belong to
        /// </summary>
        [JsonPropertyName("identifier")]
        [JsonInclude]
        public string Identifier { get; private set; }

        /// <summary>
        /// The Date the Token was created
        /// </summary>
        [JsonPropertyName("dateCreated")]
        [JsonInclude]
        public DateTimeOffset? Created { get; private set; }

        /// <summary>
        /// The Date the Token will expire
        /// </summary>
        [JsonPropertyName("dateExpires")]
        [JsonInclude]
        public DateTimeOffset? Expires { get; private set; }

        /// <summary>
        /// The Id of the Member (User) the Token belong to
        /// </summary>
        [JsonPropertyName("idMember")]
        [JsonInclude]
        public string MemberId { get; private set; }

        /// <summary>
        /// The Id of the Member (User) the Token belong to
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("permissions")]
        public List<TokenInformationPermission> Permissions { get; private set; }
    }
}