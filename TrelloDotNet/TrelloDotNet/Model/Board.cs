using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Representation of a Board
    /// </summary>
    [DebuggerDisplay("{Name} (Id: {Id})")]
    public class Board
    {
        /// <summary>
        /// Id of the Board (This is the long version of the Id)
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the board
        /// </summary>
        [JsonPropertyName("name")]
        [QueryParameter]
        public string Name { get; set; }

        /// <summary>
        /// Description of the Board
        /// </summary>
        [JsonPropertyName("desc")]
        [QueryParameter]
        public string Description { get; set; }
        
        /// <summary>
        /// URL for the Board
        /// </summary>
        [JsonPropertyName("url")]
        [JsonInclude]
        public string Url { get; private set; }
        
        /// <summary>
        /// Short Version URL for the Board
        /// </summary>
        [JsonPropertyName("shortUrl")]
        [JsonInclude]
        public string ShortUrl { get; private set; }

        /// <summary>
        /// If the Board is Archived (closed)
        /// </summary>
        [JsonPropertyName("closed")]
        [JsonInclude]
        public bool Closed { get; private set; }

        /// <summary>
        /// Id of Organization
        /// </summary>
        [JsonPropertyName("idOrganization")]
        [JsonInclude]
        public string OrganizationId { get; private set; }

        /// <summary>
        /// Id of Enterprise
        /// </summary>
        [JsonPropertyName("idEnterprise")]
        [JsonInclude]
        public string EnterpriseId { get; private set; }

        /// <summary>
        /// If the Board is pinned or not
        /// </summary>
        [JsonPropertyName("pinned")]
        [JsonInclude]
        public bool Pinned { get; private set; }

        /// <summary>
        /// Date the Board was created [stored in UTC]
        /// </summary>
        [JsonIgnore] public DateTimeOffset? Created => IdToCreatedHelper.GetCreatedFromId(Id);

        /// <summary>
        /// Constructor
        /// </summary>
        public Board()
        {
            //Serialization
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of Board</param>
        /// <param name="description">Description of board</param>
        public Board(string name, string description = null)
        {
            Name = name;
            Description = description;
        }
    }
}