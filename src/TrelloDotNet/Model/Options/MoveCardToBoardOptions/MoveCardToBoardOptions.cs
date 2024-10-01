namespace TrelloDotNet.Model.Options.MoveCardToBoardOptions
{
    /// <summary>
    /// Options when moving a card to a new board
    /// </summary>
    public class MoveCardToBoardOptions
    {
        /// <summary>
        /// Id of a List on the new Board (if not specified card will be moved to the first list on the board)
        /// </summary>
        public string NewListId { get; set; }

        /// <summary>
        /// Position of the card on the new list (Will not be used if NamedPosition is used)
        /// </summary>
        public decimal? PositionOnNewList { get; set; }

        /// <summary>
        /// Named position of the card on the new list (will ignore position given in these options if any)
        /// </summary>
        public NamedPosition? NamedPositionOnNewList { get; set; }

        /// <summary>
        /// Define what should happen to Labels on the Card (Default = 'MigrateToLabelsOfSameNameAndColorAndCreateMissing')
        /// </summary>
        public MoveCardToBoardOptionsLabelOptions LabelOptions { get; set; } = MoveCardToBoardOptionsLabelOptions.MigrateToLabelsOfSameNameAndColorAndCreateMissing;

        /// <summary>
        /// Define what should happen to Members on the Card (Default = 'KeepMembersAlsoOnNewBoardAndRemoveRest')
        /// </summary>
        public MoveCardToBoardOptionsMemberOptions MemberOptions { get; set; } = MoveCardToBoardOptionsMemberOptions.KeepMembersAlsoOnNewBoardAndRemoveRest;

        /// <summary>
        /// If the Start Date of the Card should be removed (Default = False)
        /// </summary>
        public bool RemoveStartDate { get; set; }

        /// <summary>
        /// If the Due Date of the Card should be removed (Default = False)
        /// </summary>
        public bool RemoveDueDate { get; set; }
    }
}