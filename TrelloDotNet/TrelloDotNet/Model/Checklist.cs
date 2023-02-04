using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Card Checklist
    /// </summary>
    public class Checklist
    {
        /// <summary>
        /// Id of the checklist
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Checklist
        /// </summary>
        [JsonPropertyName("name")]
        [QueryParameter]
        public string Name { get; set; }

        /// <summary>
        /// Id of the Card the checklist is on
        /// </summary>
        [JsonPropertyName("idCard")]
        [JsonInclude]
        public string CardId { get; private set; }

        //todo - Add Support for position (research in order to understand)
        /*
        [JsonPropertyName("pos")]
        [QueryParameter]
        public int Position { get; set; }
        */

        /// <summary>
        /// The items of the Checklist
        /// </summary>
        [JsonPropertyName("checkItems")]
        public List<ChecklistItem> Items { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Checklist</param>
        /// <param name="items">The items you wish on a new checklist</param>
        public Checklist(string name, List<ChecklistItem> items = null)
        {
            Name = name;
            Items = items;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Checklist()
        {
            //Serialization
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name} (Id: {Id})";
        }
    }
}