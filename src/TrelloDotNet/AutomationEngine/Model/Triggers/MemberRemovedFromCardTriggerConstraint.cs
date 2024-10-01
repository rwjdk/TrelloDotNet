namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// The Constraint links to a Member Removed Trigger
    /// </summary>
    public enum MemberRemovedFromCardTriggerConstraint
    {
        /// <summary>
        /// Any of these Members are removed
        /// </summary>
        AnyOfTheseMembersAreRemoved = 1,

        /// <summary>
        /// Any except these Members are removed
        /// </summary>
        AnyButTheseMembersAreRemoved = 2,

        /// <summary>
        /// Any Member is removed
        /// </summary>
        AnyMember = 3
    }
}