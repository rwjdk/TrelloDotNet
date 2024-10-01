using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// A set of handy extension methods for Checklists
    /// </summary>
    public static class ChecklistExtensions
    {
        /// <summary>
        /// Get Number of Items on the Checklist regardless of state
        /// </summary>
        /// <returns>Number of Items</returns>
        public static int GetNumberOfItems(this Checklist checklist)
        {
            return checklist.Items.Count;
        }

        /// <summary>
        /// Get the Number of Completed items on the Checklist
        /// </summary>
        /// <returns>Number of Completed Items</returns>
        public static int GetNumberOfCompletedItems(this Checklist checklist)
        {
            return checklist.Items.Count(x => x.State == ChecklistItemState.Complete);
        }

        /// <summary>
        /// Get the Number of Incomplete items on the Checklist
        /// </summary>
        /// <returns>Number of Completed Items</returns>
        public static int GetNumberOfIncompleteItems(this Checklist checklist)
        {
            return checklist.Items.Count(x => x.State == ChecklistItemState.Incomplete);
        }

        /// <summary>
        /// Returns if all items on the Checklist is complete
        /// </summary>
        /// <returns>True if all Items are complete</returns>
        public static bool IsAllComplete(this Checklist checklist)
        {
            return checklist.Items.All(x => x.State == ChecklistItemState.Complete);
        }

        /// <summary>
        /// Returns if any of the checklist items ar not yet complete
        /// </summary>
        /// <returns>True if on or more is incomplete</returns>
        public static bool IsAnyIncomplete(this Checklist checklist)
        {
            return checklist.Items.Any(x => x.State != ChecklistItemState.Complete);
        }

        /// <summary>
        /// Get Number of Items across a collection of Checklists regardless of state
        /// </summary>
        /// <returns>Number of Items</returns>
        public static int GetNumberOfItems(this IEnumerable<Checklist> checklists)
        {
            return checklists.Sum(x => x.GetNumberOfItems());
        }

        /// <summary>
        /// Get the Number of Completed items across a collection of Checklists
        /// </summary>
        /// <returns>Number of Completed Items</returns>
        public static int GetNumberOfCompletedItems(this IEnumerable<Checklist> checklists)
        {
            return checklists.Sum(x => x.GetNumberOfCompletedItems());
        }

        /// <summary>
        /// Get the Number of Incomplete items across a collection of Checklists
        /// </summary>
        /// <returns>Number of Completed Items</returns>
        public static int GetNumberOfIncompleteItems(this IEnumerable<Checklist> checklists)
        {
            return checklists.Sum(x => x.GetNumberOfIncompleteItems());
        }

        /// <summary>
        /// Returns if all items on the Checklist is complete
        /// </summary>
        /// <returns>True if all Items are complete</returns>
        public static bool IsAllComplete(this IEnumerable<Checklist> checklists)
        {
            return checklists.All(x => x.IsAllComplete());
        }

        /// <summary>
        /// Returns if any of the checklist items ar not yet complete
        /// </summary>
        /// <returns>True if on or more is incomplete</returns>
        public static bool IsAnyIncomplete(this IEnumerable<Checklist> checklists)
        {
            return checklists.Any(x => x.IsAnyIncomplete());
        }
    }
}