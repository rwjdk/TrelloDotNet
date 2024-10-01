using TrelloDotNet.Model;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// Interface of a CardField to set
    /// </summary>
    public interface ISetCardFieldValue
    {
        /// <summary>
        /// Set the Field (if needed based on criteria you define)
        /// </summary>
        /// <param name="card">The Card to set the values on</param>
        /// <returns>If card was modified (aka need to be updated against the API)</returns>
        // ReSharper disable once UnusedMemberInSuper.Global
        bool SetIfNeeded(Card card);

        /// <summary>
        /// Get a Query Parameter representing the change (or null if not needed)
        /// </summary>
        /// <param name="card">Card to apply the change to</param>
        /// <returns>Query parameter of null</returns>
        QueryParameter GetQueryParameter(Card card);
    }
}