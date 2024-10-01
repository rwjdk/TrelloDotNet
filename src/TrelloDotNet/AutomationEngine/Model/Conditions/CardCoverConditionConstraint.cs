namespace TrelloDotNet.AutomationEngine.Model.Conditions
{
    /// <summary>
    /// The constraint when checking a Cover
    /// </summary>
    public enum CardCoverConditionConstraint
    {
        /// <summary>
        /// Card does not have a cover at all
        /// </summary>
        DoesNotHaveACover,

        /// <summary>
        /// Card does not have a Cover or the cover is not a Color Cover
        /// </summary>
        DoesNotHaveACoverOfTypeColor,

        /// <summary>
        /// Card does not have a Cover or the cover is not an Image Cover
        /// </summary>
        DoesNotHaveACoverOfTypeImage,

        /// <summary>
        /// Card have a Cover
        /// </summary>
        HaveACover,

        /// <summary>
        /// Card have a Cover and it is of type Color
        /// </summary>
        HaveACoverOfTypeColor,

        /// <summary>
        /// Card have a Cover and it is of type Image
        /// </summary>
        HaveACoverOfTypeImage
    }
}