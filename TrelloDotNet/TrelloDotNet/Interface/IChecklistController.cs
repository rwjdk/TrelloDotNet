using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    /// <summary>
    /// Operations on Checklists
    /// </summary>
    public interface IChecklistController
    {
        //todo - Need to Have
        //- Get Check-items (https://developer.atlassian.com/cloud/trello/rest/api-group-checklists/#api-checklists-id-checkitems-get)

        //todo - Nice to have
        //- Get CheckItems of checklist
        //- Update Checklist (name + items)
        //- Delete Checklist

        /// <summary>
        /// Get a Checklist with a specific Id
        /// </summary>
        /// <param name="id">Id of the Checklist</param>
        /// <returns>The Checklist</returns>
        Task<Checklist> GetChecklistAsync(string id);

        /// <summary>
        /// Get list of Checklists that are used on cards on a specific Board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Checklists</returns>
        Task<List<Checklist>> GetChecklistsOnBoardAsync(string boardId);

        /// <summary>
        /// Get list of Checklists that are used on a specific card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The Checklists</returns>
        Task<List<Checklist>> GetChecklistsOnCardAsync(string cardId);

        /// <summary>
        /// Add a Checklist to the card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="checklist">The Checklist to add</param>
        /// <param name="ignoreIfAChecklistWithThisNameAlreadyExist">If true the card will be checked if a checklist with same name (case sensitive) exist and if so return that instead of creating a new</param>
        /// <returns>New or Existing Checklist with same name</returns>
        Task<Checklist> AddChecklistAsync(string cardId, Checklist checklist, bool ignoreIfAChecklistWithThisNameAlreadyExist = false);

        /// <summary>
        /// Add a Checklist to the card based on an existing checklist (as a copy)
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="existingChecklistIdToCopyFrom">Id of an existing Checklist that should be added to the card as a new copy</param>
        /// <param name="ignoreIfAChecklistWithThisNameAlreadyExist">If true the card will be checked if a checklist with same name (case sensitive) exist and if so return that instead of creating a new</param>
        /// <returns>New Checklist</returns>
        Task<Checklist> AddChecklistAsync(string cardId, string existingChecklistIdToCopyFrom, bool ignoreIfAChecklistWithThisNameAlreadyExist = false);
    }
}