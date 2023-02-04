using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    /// <summary>
    /// The Main Client to communicate with the Trello API (aka everything is done via this)
    /// </summary>
    public interface ITrelloClient
    {
        //todo - Need To Have (Needed for v1)
        //- What about return limits (max records back?) - Investigate
        //- Determine if things should be called something else than "xxxController" (perhaps xxxOperations instead)
        //- Determine if I should get rid of the groupings (hard to group logically and make it less understandable) and have all method directly on the TrelloClient
        //- Actions
        //- Check other APIs for features that I might have missed
        //- Web-hooks (+ reaction to it)
        //- Get Actions on Board
        //- Get Cards on List (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-actions-get)
        //- Create new card
        //- Get Card Actions
        //- Update/Move List (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-idboard-put)
        //- Get Filtered Lists
        //- Get member (https://developer.atlassian.com/cloud/trello/rest/api-group-members/#api-members-id-get)
        
        //todo - Nice To Have (Not needed for v1)
        //- Common Scenario/Actions List (aka things that is not a one to one API call... Example: "Move Card to List with name" so user do not need to set everything up themselves)
        //- Create unit-test suite (borderline Need)
        //- Batch-system (why???)
        //- Organizations (how to gain access?)
        //- Create Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-post)
        //- Update Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-put)
        //- Manage Custom Fields on board (CRUD)
        //- Get Board Membership (Aka what roles the Token user have on the board) [https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-memberships-get]
        //- Invite members by mail or userId to board [https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-put + https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-idmember-put]
        //- Remove Members from board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-idmember-delete)
        //- Update Membership on board (make admin as an example)
        //- Card: Delete a card
        //- Card: Attachments CRUD
        //- Card: Support Stickers
        //- Card: Comments CRUD
        //- Card: Custom Fields CRUD
        //- Update List (+archive) (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-field-put)
        //- Position of list (research)
        //- Delete List
        //- Archive All Cards on list (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-archiveallcards-post)
        //- Move all Cards on list (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-moveallcards-post)
        //- Get actions on list (perhaps own controller)
        //- Get members of a Card
        //- Member Actions

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        Task<T> GetAsync<T>(string suffix, params QueryParameter[] parameters);

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        Task<string> GetAsync(string suffix, params QueryParameter[] parameters);

        /// <summary>
        /// Custom Post Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        Task<T> PostAsync<T>(string suffix, params QueryParameter[] parameters);

        /// <summary>
        /// Custom Post Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        Task<string> PostAsync(string suffix, params QueryParameter[] parameters);

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        Task<T> PutAsync<T>(string suffix, params QueryParameter[] parameters);

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        Task<string> PutAsync(string suffix, params QueryParameter[] parameters);

        /// <summary>
        /// Get a Board by its Id
        /// </summary>
        /// <param name="id">Id of the Board (in its long or short version)</param>
        /// <returns>The Board</returns>
        Task<Board> GetBoardAsync(string id);

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

        /// <summary>
        /// Get List of Labels defined for a board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Labels</returns>
        Task<List<Label>> GetLabelsOfBoardAsync(string boardId);

        /// <summary>
        /// Get a specific List (Column) based on it's Id
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <returns></returns>
        Task<List> GetListAsync(string listId);

        /// <summary>
        /// Add a List to a Board
        /// </summary>
        /// <remarks>
        /// The Provided BoardId the list should be added to need to be the long version of the BoardId as API does not support the short version
        /// </remarks>
        /// <param name="list">List to add</param>
        /// <returns>The Create list</returns>
        Task<List> AddListAsync(List list);

        /// <summary>
        /// Get Lists (Columns) on a Board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="filter">Filter Lists based on status</param>
        /// <returns>List of Lists (Columns)</returns>
        Task<List<List>> GetListsOnBoardAsync(string boardId, ListFilter filter = ListFilter.Open);

        /// <summary>
        /// Get the Members (users) of a board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Members</returns>
        Task<List<Member>> GetMembersOfBoardAsync(string boardId);
    }
}