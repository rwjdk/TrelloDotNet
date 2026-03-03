using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Represent comment Action Data
    /// </summary>
    public class TrelloActionData
    {
        /// <summary>
        /// Text of the comment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Text)]
        [QueryParameter]
        public string Text { get; set; }

        /// <summary>
        /// Simplified Card of the Action
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Card)]
        [JsonInclude]
        public TrelloActionDataCard Card { get; private set; }

        /// <summary>
        /// Simplified Board of the Action
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Board)]
        [JsonInclude]
        public TrelloActionDataBoard Board { get; private set; }

        /// <summary>
        /// BoardTarget of the Action (Only there when event is 'moveCardFromBoard')
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.BoardTarget)]
        [JsonInclude]
        public TrelloActionDataBoard BoardTarget { get; private set; }

        /// <summary>
        /// BoardSource of the Action (Only there when event is 'moveCardToBoard')
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.BoardSource)]
        [JsonInclude]
        public TrelloActionDataBoard BoardSource { get; private set; }

        /// <summary>
        /// Plugin of the Action (Only there when event is 'enablePlugin' or 'disablePlugin')
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.PluginFields.Plugin)]
        [JsonInclude]
        public TrelloActionDataPlugin Plugin { get; private set; }

        /// <summary>
        /// Simplified List of the Action
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.List)]
        [JsonInclude]
        public TrelloActionDataList List { get; private set; }

        /// <summary>
        /// Simplified ListBefore of the Action (present on card moved to new List)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.ListBefore)]
        [JsonInclude]
        public TrelloActionDataList ListBefore { get; private set; }

        /// <summary>
        /// Simplified ListAfter of the Action (present on card moved to new List)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.ListAfter)]
        [JsonInclude]
        public TrelloActionDataList ListAfter { get; private set; }

        /// <summary>
        /// Checklist Data Object
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ChecklistFields.Checklist)]
        [JsonInclude]
        public TrelloActionDataChecklist Checklist { get; private set; }

        /// <summary>
        /// CheckItem Data Object
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ChecklistFields.CheckItem)]
        [JsonInclude]
        public TrelloActionDataCheckItem CheckItem { get; private set; }

        /// <summary>
        /// Member Data Object
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Member)]
        [JsonInclude]
        public TrelloActionDataMember Member { get; private set; }

        /// <summary>
        /// Attachment Data Object
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.Attachment)]
        [JsonInclude]
        public TrelloActionDataAttachment Attachment { get; private set; }

        /// <summary>
        /// Organization
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Organization)]
        [JsonInclude]
        public TrelloActionDataOrganization Organization { get; private set; }

        /// <summary>
        /// Old Data
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Old)]
        [JsonInclude]
        public TrelloActionDataOld Old { get; private set; }

        /// <summary>
        /// The Type of added member (Only there when event is 'addMemberToBoard')
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.MemberType)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<MembershipType>))]
        public MembershipType MemberType { get; private set; }

        /// <summary>
        /// The Id of an Added Member (Only there when event is 'addMemberToBoard')
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.IdMemberAdded)]
        [JsonInclude]
        public string MemberIdAdded { get; private set; }
    }
}



