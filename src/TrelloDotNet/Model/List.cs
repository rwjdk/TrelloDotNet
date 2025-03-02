using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a List (column) on a board
    /// </summary>
    [DebuggerDisplay("{Name} (Id: {Id})")]
    public class List
    {
        /// <summary>
        /// Id of the List
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the List
        /// </summary>
        [JsonPropertyName("name")]
        [QueryParameter]
        public string Name { get; set; }

        /// <summary>
        /// If the list is archived (closed)
        /// </summary>
        [JsonPropertyName("closed")]
        [QueryParameter]
        public bool Closed { get; set; }

        /// <summary>
        /// Id of the Board the list belong to
        /// </summary>
        [JsonPropertyName("idBoard")]
        [QueryParameter]
        public string BoardId { get; set; }

        /// <summary>
        /// Id of the Board the list belong to
        /// </summary>
        [JsonPropertyName("color")]
        [QueryParameter]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<ListColor>))]
        public ListColor Color { get; set; }

        /// <summary>
        /// The position of the List on the Board
        /// </summary>
        [JsonPropertyName("pos")]
        [QueryParameter]
        public decimal Position { get; set; }

        /// <summary>
        /// Indicate if the owner of the Token has subscribed (watching) the list
        /// </summary>
        [JsonPropertyName("subscribed")]
        [JsonInclude]
        public bool Subscribed { get; private set; }

        /// <summary>
        /// If there is a Soft Limit to number of cards in the list (Provided by PowerUp 'List Limits' from Trello)
        /// </summary>
        [JsonPropertyName("softLimit")]
        [JsonInclude]
        public int? SoftLimit { get; private set; }

        /// <summary>
        /// The Creation date of the list [stored in UTC]
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Created => IdToCreatedHelper.GetCreatedFromId(Id);

        /// <summary>
        /// The named position of the List: Top (Most Leftmost) or Bottom (Most Rightmost)
        /// </summary>
        [JsonIgnore]
        public NamedPosition? NamedPosition { internal get; set; }

        /// <summary>
        /// The Board the list is on (Only included if IncludeBoard = true in GetListOptions)
        /// </summary>
        [JsonPropertyName("board")]
        [JsonInclude]
        public Board Board { get; internal set; }

        /// <summary>
        /// The Cards on the list is on (Only included if IncludeCards = true in GetListOptions)
        /// </summary>
        [JsonPropertyName("cards")]
        [JsonInclude]
        public List<Card> Cards { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public List()
        {
            //Serialization
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the List</param>
        /// <param name="boardId">Id of the Board the list should be on (NB: Need to be the long version of the BoardId as API do not accept short version in Add/Update/Delete)</param>
        public List(string name, string boardId)
        {
            Name = name;
            BoardId = boardId;
        }
    }
}