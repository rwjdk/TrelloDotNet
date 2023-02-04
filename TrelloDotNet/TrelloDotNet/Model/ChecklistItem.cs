using System;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent an item on a Checklist
    /// </summary>
    public class ChecklistItem
    {
        /// <summary>
        /// Id of the Item 
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Item
        /// </summary>
        [JsonPropertyName("name")]
        [QueryParameter]
        public string Name { get; set; }

        //todo - support Name-data field?

        //todo - Add Support for position (research in order to understand)
        /*
        [JsonPropertyName("pos")]
        public int Position { get; set; }
        */

        /// <summary>
        /// Due Date of the Item [Stored in UTC]
        /// </summary>
        [JsonPropertyName("due")]
        [QueryParameter]
        public DateTimeOffset? Due { get; set; }

        //todo - support dueReminder??

        /// <summary>
        /// Member (user) assigned to the Item
        /// </summary>
        [JsonPropertyName("idMember")]
        [QueryParameter]
        public string MemberId { get; set; }

        /// <summary>
        /// Id of the Checklist the item belong to
        /// </summary>
        [JsonPropertyName("idChecklist")]
        [JsonInclude]
        public string ChecklistId { get; private set; }

        /// <summary>
        /// State of the item (incomplete or complete)
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; } //todo - enum?

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

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name} [{State}] (Id: {Id})";
        }
    }
}