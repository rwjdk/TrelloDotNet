using System;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a List (column) on a board
    /// </summary>
    public class List
    {
        /// <summary>
        /// Id of the List
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

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

        //todo - Add Support for position (research in order to understand)
        /*
        [JsonPropertyName("pos")]
        public int Position { get; set; }
        */

        /// <summary>
        /// Indicate if the owner of the Token has subscribed (watching) the list
        /// </summary>
        [JsonPropertyName("subscribed")]
        [QueryParameter]
        public bool Subscribed { get; set; }

        /// <summary>
        /// If there is a Soft Limit to number of cards in the list (Provided by PowerUp 'List Limits' from Trello)
        /// </summary>
        [JsonPropertyName("softLimit")]
        [QueryParameter]
        public int? SoftLimit { get; set; }

        /// <summary>
        /// The Creation date of the list [stored in UTC]
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset Created => IdToCreatedHelper.GetCreatedFromId(Id);

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

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name} (Id: {Id})";
        }
    }
}