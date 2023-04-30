using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model.Webhook;

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
        [JsonPropertyName("text")]
        [QueryParameter]
        public string Text { get; set; }

        /// <summary>
        /// Simplified Card of the Action
        /// </summary>
        [JsonPropertyName("card")]
        [JsonInclude]
        public TrelloActionDataCard Card { get; private set; }
        
        /// <summary>
        /// Simplified Board of the Action
        /// </summary>
        [JsonPropertyName("board")]
        [JsonInclude]
        public TrelloActionDataBoard Board { get; private set; }

        /// <summary>
        /// BoardTarget of the Action (Only there when event is 'moveCardFromBoard')
        /// </summary>
        [JsonPropertyName("boardTarget")]
        [JsonInclude]
        public TrelloActionDataBoard BoardTarget { get; private set; }

        /// <summary>
        /// BoardSource of the Action (Only there when event is 'moveCardToBoard')
        /// </summary>
        [JsonPropertyName("boardSource")]
        [JsonInclude]
        public TrelloActionDataBoard BoardSource { get; private set; }

        /// <summary>
        /// Plugin of the Action (Only there when event is 'enablePlugin' or 'disablePlugin')
        /// </summary>
        [JsonPropertyName("plugin")]
        [JsonInclude]
        public TrelloActionDataPlugin Plugin { get; private set; }

        /// <summary>
        /// Simplified List of the Action
        /// </summary>
        [JsonPropertyName("list")]
        [JsonInclude]
        public TrelloActionDataList List { get; private set; }
        
        /// <summary>
        /// Simplified ListBefore of the Action (present on card moved to new List)
        /// </summary>
        [JsonPropertyName("listBefore")]
        [JsonInclude]
        public TrelloActionDataList ListBefore { get; private set; }

        /// <summary>
        /// Simplified ListAfter of the Action (present on card moved to new List)
        /// </summary>
        [JsonPropertyName("listAfter")]
        [JsonInclude]
        public TrelloActionDataList ListAfter { get; private set; }

        /// <summary>
        /// Checklist Data Object
        /// </summary>
        [JsonPropertyName("checklist")]
        [JsonInclude]
        public TrelloActionDataChecklist Checklist { get; private set; }

        /// <summary>
        /// CheckItem Data Object
        /// </summary>
        [JsonPropertyName("checkItem")]
        [JsonInclude]
        public TrelloActionDataCheckItem CheckItem { get; private set; }
        
        /// <summary>
        /// Member Data Object
        /// </summary>
        [JsonPropertyName("member")]
        [JsonInclude]
        public TrelloActionDataMember Member { get; private set; }
        
        /// <summary>
        /// Attachment Data Object
        /// </summary>
        [JsonPropertyName("attachment")]
        [JsonInclude]
        public TrelloActionDataAttachment Attachment { get; private set; }

        /// <summary>
        /// Organization
        /// </summary>
        [JsonPropertyName("organization")]
        [JsonInclude]
        public TrelloActionDataOrganization Organization { get; private set; }

        /// <summary>
        /// Old Data
        /// </summary>
        [JsonPropertyName("old")]
        [JsonInclude]
        public TrelloActionDataOld Old { get; private set; }

        /// <summary>
        /// The Type of an added member (Only there when event is 'addMemberToBoard')
        /// </summary>
        [JsonPropertyName("memberType")]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<MembershipType>))]
        public MembershipType MemberType { get; private set; }

        /// <summary>
        /// The Id of an Added Member (Only there when event is 'addMemberToBoard')
        /// </summary>
        [JsonPropertyName("idMemberAdded")]
        [JsonInclude]
        public string MemberIdAdded { get; private set; }
    }
}