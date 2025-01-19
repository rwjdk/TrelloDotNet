using System.Collections.Generic;

namespace TrelloDotNet.Model.Options.CopyCardOptions
{
    /// <summary>
    /// Options for 
    /// </summary>
    public class CopyCardOptions
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
        /// Name of the new card-copy (or leave blank if you want the same name as copy)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Position 
        /// </summary>
        public decimal? Position { get; set; }

        /// <summary>
        /// Named Position
        /// </summary>
        public NamedPosition? NamedPosition { get; set; }

        /// <summary>
        /// What to Keep from the Source Keep (or leave blank if you wish to copy everything)
        /// </summary>
        public CopyCardOptionsToKeep? Keep { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CopyCardOptions()
        {
            //Empty
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sourceCardId">Id of the source card</param>
        /// <param name="targetListId">Id of the List that card should be added</param>
        public CopyCardOptions(string sourceCardId, string targetListId)
        {
            SourceCardId = sourceCardId;
            TargetListId = targetListId;
        }
    }
}