using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;
using TrelloDotNet.Model.Actions;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represents a Board, containing Lists, Cards, Labels, and related data.
    /// </summary>
    [DebuggerDisplay("{Name} (Id: {Id})")]
    public class Board
    {
        /// <summary>
        /// The Board ID (long form).
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Board.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Name)]
        [QueryParameter]
        public string Name { get; set; }

        /// <summary>
        /// Description of the Board.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Desc)]
        [QueryParameter]
        public string Description { get; set; }

        /// <summary>
        /// Full URL to the Board.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Url)]
        [JsonInclude]
        public string Url { get; private set; }

        /// <summary>
        /// Short URL to the Board.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.ShortUrl)]
        [JsonInclude]
        public string ShortUrl { get; private set; }

        /// <summary>
        /// True if the Board is archived (closed).
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Closed)]
        [JsonInclude]
        public bool Closed { get; private set; }

        /// <summary>
        /// Organization ID for the Board.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.IdOrganization)]
        [QueryParameter(false)]
        public string OrganizationId { get; set; }

        /// <summary>
        /// Organization for the Board. Only populated if 'IncludeOrganization' is set in GetBoardOptions.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Organization)]
        [JsonInclude]
        public Organization Organization { get; private set; }

        /// <summary>
        /// Enterprise ID for the Board.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.IdEnterprise)]
        [JsonInclude]
        public string EnterpriseId { get; private set; }

        /// <summary>
        /// True if the Board is pinned for the user.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Pinned)]
        [JsonInclude]
        public bool Pinned { get; private set; }

        /// <summary>
        /// Date and time the Board was created (UTC).
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Created => IdToCreatedHelper.GetCreatedFromId(Id);

        /// <summary>
        /// Actions on the Board. Only populated if included in GetBoardOptions.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Actions)]
        [JsonInclude]
        public List<TrelloAction> Actions { get; private set; }

        /// <summary>
        /// Cards on the Board. Only populated if 'IncludeCards' in GetBoardOptions is not None.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Cards)]
        [JsonInclude]
        public List<Card> Cards { get; internal set; }

        /// <summary>
        /// Labels on the Board. Only populated if 'IncludeLabels' in GetBoardOptions is true.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.LabelFields.Labels)]
        [JsonInclude]
        public List<Label> Labels { get; private set; }

        /// <summary>
        /// Lists on the Board. Only populated if 'IncludeLists' in GetBoardOptions is set.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Lists)]
        [JsonInclude]
        public List<List> Lists { get; private set; }

        /// <summary>
        /// Plugin data for the Board. Only populated if GetBoardOptions.IncludePluginData is used.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.PluginFields.PluginData)]
        [JsonInclude]
        public List<PluginData> PluginData { get; private set; }

        /// <summary>
        /// Preferences and settings for the Board.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Prefs)]
        [JsonInclude]
        public BoardPreferences Preferences { get; private set; }

        /// <summary>
        /// If the Token-use is Subscribed (Watching) the board. (Only populated if BoardFields.Subscribed is requested in GetBoardOptions)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Subscribed)]
        [QueryParameter]
        public bool Subscribed { get; set; }

        /// <summary>
        /// Initializes a new Board instance for serialization.
        /// </summary>
        public Board()
        {
            //Serialization
        }

        /// <summary>
        /// Initializes a new Board with the specified name and optional description.
        /// </summary>
        /// <param name="name">Name of the Board.</param>
        /// <param name="description">Description of the Board.</param>
        public Board(string name, string description = null)
        {
            Name = name;
            Description = description;
        }
    }
}





