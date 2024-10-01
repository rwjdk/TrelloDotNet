namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// The Constraint links to a Member Added Trigger
    /// </summary>
    public enum MemberAddedToCardTriggerConstraint
    {
        /// <summary>
        /// Trigger is true if any of the provided Member are added
        /// </summary>
        AnyOfTheseMembersAreAdded = 1,

        /// <summary>
        /// Trigger is true if any but the provided Members are added
        /// </summary>
        AnyButTheseMembersAreAreAdded = 2,

        /// <summary>
        /// Trigger is true if any member is added (No matter who it is)
        /// </summary>
        AnyMember = 3,
    }
}