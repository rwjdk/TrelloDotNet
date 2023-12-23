namespace TrelloDotNet.Model.Options.MoveCardToBoardOptions
{
    /// <summary>
    /// Member options when moving a Card to another board
    /// </summary>
    public enum MoveCardToBoardOptionsMemberOptions
    {
        /// <summary>
        /// Keep the members on the card that is also members of the new board, and remove the rest
        /// </summary>
        KeepMembersAlsoOnNewBoardAndRemoveRest,

        /// <summary>
        /// Remove all members on the board
        /// </summary>
        RemoveAllMembersOnCard
    }
}