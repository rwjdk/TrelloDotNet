namespace TrelloDotNet.Model.Options.MoveCardToBoardOptions
{
    /// <summary>
    /// Label options when moving a Card to another board
    /// </summary>
    public enum MoveCardToBoardOptionsLabelOptions
    {
        /// <summary>
        /// Migrate Labels with the same color and name, and create those missing as labels on the new board
        /// </summary>
        MigrateToLabelsOfSameNameAndColorAndCreateMissing,

        /// <summary>
        /// Migrate Labels with the same color and name, and remove all labels that does not exist on the new board
        /// </summary>
        MigrateToLabelsOfSameNameAndColorAndRemoveMissing,

        /// <summary>
        /// Migrate Labels with the same name (allow color change), and create those missing as labels on the new board
        /// </summary>
        MigrateToLabelsOfSameNameAndCreateMissing,

        /// <summary>
        /// Migrate Labels with the same name (allow color change), and remove all labels that does not exist on the new board
        /// </summary>
        MigrateToLabelsOfSameNameAndRemoveMissing,

        /// <summary>
        /// Remove all labels before move to new board
        /// </summary>
        RemoveAllLabelsOnCard,
    }
}