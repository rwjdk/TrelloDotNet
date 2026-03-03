using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Action Data about CheckItems
    /// </summary>
    public class TrelloActionDataCheckItem
    {
        /// <summary>
        /// Id of the Item 
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Item
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// State of the item (incomplete or complete)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ChecklistFields.State)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<ChecklistItemState>))]
        public ChecklistItemState State { get; private set; }
    }
}




