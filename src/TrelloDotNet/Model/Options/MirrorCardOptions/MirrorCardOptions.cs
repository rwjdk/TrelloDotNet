using TrelloDotNet.Model.Options.CopyCardOptions;

namespace TrelloDotNet.Model.Options.MirrorCardOptions
{
    /// <summary>
    /// Options for mirroring a Card
    /// </summary>
    public class MirrorCardOptions
    {
        /// <summary>
        /// Id of the source card
        /// </summary>
        public string SourceCardId { get; set; }

        /// <summary>
        /// Id of the List that card should be added
        /// </summary>
        public string TargetListId { get; set; }

        /// <summary>
        /// Position 
        /// </summary>
        public decimal? Position { get; set; }

        /// <summary>
        /// Named Position
        /// </summary>
        public NamedPosition? NamedPosition { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MirrorCardOptions()
        {
            //Empty
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sourceCardId">Id of the source card</param>
        /// <param name="targetListId">Id of the List that card should be added</param>
        public MirrorCardOptions(string sourceCardId, string targetListId)
        {
            SourceCardId = sourceCardId;
            TargetListId = targetListId;
        }
    }
}