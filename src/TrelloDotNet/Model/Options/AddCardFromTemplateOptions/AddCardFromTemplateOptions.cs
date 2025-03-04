namespace TrelloDotNet.Model.Options.AddCardFromTemplateOptions
{
    /// <summary>
    /// Options for adding a card from a Template Card
    /// </summary>
    public class AddCardFromTemplateOptions
    {
        /// <summary>
        /// Id of the source Template card
        /// </summary>
        public string SourceTemplateCardId { get; set; }

        /// <summary>
        /// Id of the List that card should be added (at the bottom of)
        /// </summary>
        public string TargetListId { get; set; }

        /// <summary>
        /// Name of the new card (or leave blank if you want the same name as the template)
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
        public AddCardFromTemplateOptionsToKeep? Keep { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public AddCardFromTemplateOptions()
        {
            //Empty
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sourceTemplateCardId">Id of the source Template card</param>
        /// <param name="targetListId">Id of the List that card should be added (at the bottom of)</param>
        public AddCardFromTemplateOptions(string sourceTemplateCardId, string targetListId)
        {
            SourceTemplateCardId = sourceTemplateCardId;
            TargetListId = targetListId;
        }
    }
}