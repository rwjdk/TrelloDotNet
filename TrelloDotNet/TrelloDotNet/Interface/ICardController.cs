using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    /// <summary>
    /// Operations on Cards
    /// </summary>
    public interface ICardController
    {
        //todo - Need to have
        //- Get Cards on List (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-actions-get)
        //- Create new card
        //- Get Actions (perhaps it's own controller; most likely)
        
        //todo - Nice to have
        //- Delete a card
        //- Attachments CRUD
        //- Support Stickers
        //- Comments CRUD
        //- Custom Fields CRUD

        /// <summary>
        /// Get Card by its Id
        /// </summary>
        /// <param name="id">Id of the Card</param>
        /// <returns>The Card</returns>
        Task<Card> GetCardAsync(string id);

        /// <summary>
        /// Get all open cards on un-archived lists
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Cards</returns>
        Task<List<Card>> GetCardsOnBoardAsync(string boardId);

        /// <summary>
        /// The cards on board based on their status regardless if they are on archived lists
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="filter">The Selected Filter</param>
        /// <returns>List of Cards</returns>
        Task<List<Card>> GetCardsOnBoardFilteredAsync(string boardId, CardsFilter filter = CardsFilter.Open);

        /// <summary>
        /// Update a Card
        /// </summary>
        /// <remarks>
        /// Check description on each Card-property if it can be updated or not
        /// </remarks>
        /// <param name="card">The card with the changes</param>
        /// <returns>The Updated Card</returns>
        Task<Card> UpdateCardAsync(Card card);
    }
}