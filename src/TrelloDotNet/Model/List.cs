using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represents a List (column) on a Board
    /// </summary>
    [DebuggerDisplay("{Name} (Id: {Id})")]
    public class List
    {
        /// <summary>
        /// ID of the List
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
        /// Indicates if the List is archived (closed)
        /// </summary>
        [JsonPropertyName("closed")]
        [QueryParameter]
        public bool Closed { get; set; }

        /// <summary>
        /// ID of the Board the List belongs to
        /// </summary>
        [JsonPropertyName("idBoard")]
        [QueryParameter]
        public string BoardId { get; set; }

        /// <summary>
        /// Color of the List
        /// </summary>
        [JsonPropertyName("color")]
        [QueryParameter]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<ListColor>))]
        public ListColor Color { get; set; }

        /// <summary>
        /// Position of the List on the Board
        /// </summary>
        [JsonPropertyName("pos")]
        [QueryParameter]
        public decimal Position { get; set; }

        /// <summary>
        /// Indicates if the owner of the Token is subscribed (watching) the List
        /// </summary>
        [JsonPropertyName("subscribed")]
        [JsonInclude]
        public bool Subscribed { get; private set; }

        /// <summary>
        /// Soft limit for the number of Cards in the List (provided by PowerUp 'List Limits' from Trello)
        /// </summary>
        [JsonPropertyName("softLimit")]
        [JsonInclude]
        public int? SoftLimit { get; private set; }

        /// <summary>
        /// Creation date of the List (stored in UTC)
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Created => IdToCreatedHelper.GetCreatedFromId(Id);

        /// <summary>
        /// Named position of the List: Top (leftmost) or Bottom (rightmost)
        /// </summary>
        [JsonIgnore]
        public NamedPosition? NamedPosition { internal get; set; }

        /// <summary>
        /// Board the List is on (only included if IncludeBoard = true in GetListOptions)
        /// </summary>
        [JsonPropertyName("board")]
        [JsonInclude]
        public Board Board { get; internal set; }

        /// <summary>
        /// Cards on the List (only included if IncludeCards = true in GetListOptions)
        /// </summary>
        [JsonPropertyName("cards")]
        [JsonInclude]
        public List<Card> Cards { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="List"/> class for serialization
        /// </summary>
        public List()
        {
            // Serialization
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="List"/> class
        /// </summary>
        /// <param name="name">Name of the List</param>
        /// <param name="boardId">ID of the Board the List should be on</param>
        public List(string name, string boardId)
        {
            Name = name;
            BoardId = boardId;
        }
    }
}