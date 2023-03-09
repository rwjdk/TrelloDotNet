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
        bool SetIfNeeded(Card card);
    }
}