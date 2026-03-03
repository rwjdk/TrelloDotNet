using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent an item on a Checklist
    /// </summary>
    [DebuggerDisplay("{Name} [{State}] (Id: {Id})")]
    public class ChecklistItem
    {
        /// <summary>
        /// Id of the Item 
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ChecklistFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Item
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ChecklistFields.Name)]
        [QueryParameter]
        public string Name { get; set; }

        /// <summary>
        /// The position of the ChecklistItem on the Checklist
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Pos)]
        [QueryParameter]
        public decimal Position { get; set; }

        /// <summary>
        /// Due Date of the Item [Stored in UTC] [Warning: Not available in free version of Trello]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Due)]
        [QueryParameter]
        public DateTimeOffset? Due { get; set; }

        /// <summary>
        /// Member (user) assigned to the Item [Warning: Not available in free version of Trello]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.IdMember)]
        [QueryParameter]
        public string MemberId { get; set; }

        /// <summary>
        /// Id of the Checklist the item belong to
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ChecklistFields.IdChecklist)]
        [JsonInclude]
        public string ChecklistId { get; private set; }

        /// <summary>
        /// State of the item (incomplete or complete)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ChecklistFields.State)]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<ChecklistItemState>))]
        [QueryParameter]
        public ChecklistItemState State { get; set; }

        /// <summary>
        /// The named position of the Checklist item: Top or Bottom
        /// </summary>
        [JsonIgnore]
        public NamedPosition? NamedPosition { internal get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Item</param>
        /// <param name="due">Due Date of the Item [Stored in UTC]</param>
        /// <param name="memberId">Member (user) assigned to the Item</param>
        public ChecklistItem(string name, DateTimeOffset? due = null, string memberId = null)
        {
            Name = name;
            Due = due;
            MemberId = memberId;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ChecklistItem()
        {
            //Serialization
        }
    }
}





