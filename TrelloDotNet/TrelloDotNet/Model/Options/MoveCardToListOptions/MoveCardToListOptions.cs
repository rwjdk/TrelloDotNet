namespace TrelloDotNet.Model.Options.MoveCardToListOptions
{
    /// <summary>
    /// Option of the Move
    /// </summary>
    public class MoveCardToListOptions
    {
        /// <summary>
        /// Position of the card on the new list (Will not be used if NamedPosition is used)
        /// </summary>
        public decimal? PositionOnNewList { get; set; }

        /// <summary>
        /// Named position of the card on the new list (will ignore position given in these options if any)
        /// </summary>
        public NamedPosition? NamedPositionOnNewList { get; set; }
    }
}