using System;

namespace TrelloDotNet.Model.Options.UpdateBoardPreferencesOptions
{
    /// <summary>
    /// Options to update for the board (options you do not define is left as is)
    /// </summary>
    public class UpdateBoardPreferencesOptions
    {
        /// <summary>
        /// What visibility should the board have
        /// </summary>
        public BoardPreferenceVisibility? Visibility { get; set; }

        /// <summary>
        /// Show Card Covers
        /// </summary>
        public BoardPreferenceCardCovers? CardCovers { get; set; }

        /// <summary>
        /// Show the Completed Status on the Front of the Cards
        /// </summary>
        public BoardPreferenceShowCompletedStatusOnCardFront? ShowCompletedStatusOnCardFront { get; set; }

        /// <summary>
        /// Hide the Votes Cast from other members
        /// </summary>
        public BoardPreferenceHideVotes? HideVotes { get; set; }

        /// <summary>
        /// Who can vote on cards (given the official PowerUp 'votes' is installed)
        /// </summary>
        public BoardPreferenceWhoCanVote? WhoCanVote { get; set; }

        /// <summary>
        /// Who can comment on cards
        /// </summary>
        public BoardPreferenceWhoCanComment? WhoCanComment { get; set; }

        /// <summary>
        /// Who can add/remove members to the board
        /// </summary>
        public BoardPreferenceWhoCanAddAndRemoveMembers? WhoCanAddAndRemoveMembers { get; set; }

        /// <summary>
        /// If Workspace Members Self-join this board (Require Board to be a workspace or public board)
        /// </summary>
        public BoardPreferenceSelfJoin? SelfJoin { get; set; }

        /// <summary>
        /// The Card Aging type (require Card Aging PowerUp)
        /// </summary>
        public BoardPreferenceCardAging? CardAging { get; set; }
    }
}