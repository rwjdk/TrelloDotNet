using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The Defined Preferences of the Board
    /// </summary>
    public class BoardPreferences
    {
        /// <summary>
        /// What Visibility the Board have (Private, Workspace or Public)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.PermissionLevel)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<BoardPreferenceVisibility>))]
        public BoardPreferenceVisibility Visibility { get; private set; }

        /// <summary>
        /// If the Completed Status is shown on the Card Front
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.ShowCompleteStatus)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<BoardPreferenceShowCompletedStatusOnCardFront>))]
        public BoardPreferenceShowCompletedStatusOnCardFront ShowCompletedStatusOnCardFront { get; private set; }

        /// <summary>
        /// If Card Covers are enabled on the board
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.CardCovers)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<BoardPreferenceCardCovers>))]
        public BoardPreferenceCardCovers CardCovers { get; private set; }

        /// <summary>
        /// If Votes from the PowerUp Votes should be hidden from other Members or not (If a Member can only see their own votes)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.HideVotes)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<BoardPreferenceHideVotes>))]
        public BoardPreferenceHideVotes HideVotes { get; private set; }

        /// <summary>
        /// Who can vote (if the Vote PowerUp is enabled)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Voting)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<BoardPreferenceWhoCanVote>))]
        public BoardPreferenceWhoCanVote WhoCanVote { get; private set; }

        /// <summary>
        /// Who can Comment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Comments)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<BoardPreferenceWhoCanComment>))]
        public BoardPreferenceWhoCanComment WhoCanComment { get; private set; }

        /// <summary>
        /// Who can add/remove members to this board
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Invitations)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<BoardPreferenceWhoCanAddAndRemoveMembers>))]
        public BoardPreferenceWhoCanAddAndRemoveMembers WhoCanAddAndRemoveMembers { get; private set; }

        /// <summary>
        /// If Workspace Members Self-join this board
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.SelfJoin)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<BoardPreferenceSelfJoin>))]
        public BoardPreferenceSelfJoin SelfJoin { get; private set; }

        /// <summary>
        /// The Card Aging type (require Card Aging PowerUp)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.CardAging)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<BoardPreferenceCardAging>))]
        public BoardPreferenceCardAging CardAging { get; private set; }
    }
}



