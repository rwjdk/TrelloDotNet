using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Adds a new checklist to the specified card.
        /// </summary>
        /// <param name="cardId">ID of the card to which the checklist will be added</param>
        /// <param name="checklist">The checklist object to adds</param>
        /// <param name="ignoreIfAChecklistWithThisNameAlreadyExist">If true, checks for an existing checklist with the same name (case-sensitive) on the card and returns it if found; otherwise, creates a new checklist</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The newly created checklist, or the existing checklist with the same name if found</returns>
        public async Task<Checklist> AddChecklistAsync(string cardId, Checklist checklist, bool ignoreIfAChecklistWithThisNameAlreadyExist = false, CancellationToken cancellationToken = default)
        {
            var template = checklist;
            if (ignoreIfAChecklistWithThisNameAlreadyExist)
            {
                //Check if card already have a list with same name
                var existingOnCard = await GetChecklistsOnCardAsync(cardId, cancellationToken);
                var existing = existingOnCard.FirstOrDefault(x => x.Name == template.Name);
                if (existing != null)
                {
                    return existing;
                }
            }

            var checklistParameters = _queryParametersBuilder.GetViaQueryParameterAttributes(template);
            _queryParametersBuilder.AdjustForNamedPosition(checklistParameters, checklist.NamedPosition);
            var newChecklist = await _apiRequestController.Post<Checklist>($"{UrlPaths.Cards}/{cardId}/{UrlPaths.Checklists}", cancellationToken, checklistParameters);

            if (template.Items == null)
            {
                return newChecklist;
            }

            if (template.Items.TrueForAll(x => x.Position == 0)) //Give positions to have the system have same order as in list
            {
                int position = 1;
                foreach (var item in template.Items)
                {
                    item.Position = position;
                    position++;
                }
            }

            await AddCheckItemsAsync(newChecklist, template.Items.ToArray());

            return newChecklist;
        }

        /// <summary>
        /// Adds a new item to an existing checklist.
        /// </summary>
        /// <param name="checklistId">ID of the checklist to which the item will be added</param>
        /// <param name="checkItemToAdd">The checklist item to add</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The newly created checklist item</returns>
        public async Task<ChecklistItem> AddChecklistItemAsync(string checklistId, ChecklistItem checkItemToAdd, CancellationToken cancellationToken = default)
        {
            var parameters = _queryParametersBuilder.GetViaQueryParameterAttributes(checkItemToAdd);
            _queryParametersBuilder.AdjustForNamedPosition(parameters, checkItemToAdd.NamedPosition);
            return await _apiRequestController.Post<ChecklistItem>($"{UrlPaths.Checklists}/{checklistId}/{UrlPaths.CheckItems}", cancellationToken, parameters);
        }

        internal async Task AddCheckItemsAsync(Checklist existingChecklist, params ChecklistItem[] checkItemsToAdd)
        {
            foreach (var checkItemParameters in checkItemsToAdd.Select(item =>
                     {
                         var parameters = _queryParametersBuilder.GetViaQueryParameterAttributes(item);
                         _queryParametersBuilder.AdjustForNamedPosition(parameters, item.NamedPosition);
                         return parameters;
                     }))
            {
                existingChecklist.Items.Add(await _apiRequestController.Post<ChecklistItem>($"{UrlPaths.Checklists}/{existingChecklist.Id}/{UrlPaths.CheckItems}", CancellationToken.None, checkItemParameters));
            }
        }

        /// <summary>
        /// Adds a checklist to the specified card by copying an existing checklist.
        /// </summary>
        /// <param name="cardId">ID of the card to which the checklist will be added</param>
        /// <param name="existingChecklistIdToCopyFrom">ID of the existing checklist to copy</param>
        /// <param name="ignoreIfAChecklistWithThisNameAlreadyExist">If true, checks for an existing checklist with the same name (case-sensitive) on the card and returns it if found; otherwise, creates a new checklist</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The newly created checklist, or the existing checklist with the same name if found</returns>
        public async Task<Checklist> AddChecklistAsync(string cardId, string existingChecklistIdToCopyFrom, bool ignoreIfAChecklistWithThisNameAlreadyExist = false, CancellationToken cancellationToken = default)
        {
            return await AddChecklistAsync(cardId, existingChecklistIdToCopyFrom, ignoreIfAChecklistWithThisNameAlreadyExist, null, cancellationToken);
        }

        /// <summary>
        /// Adds a checklist to the specified card by copying an existing checklist, with optional named position.
        /// </summary>
        /// <param name="cardId">ID of the card to which the checklist will be added</param>
        /// <param name="existingChecklistIdToCopyFrom">ID of the existing checklist to copy</param>
        /// <param name="ignoreIfAChecklistWithThisNameAlreadyExist">If true, checks for an existing checklist with the same name (case-sensitive) on the card and returns it if found; otherwise, creates a new checklist</param>
        /// <param name="namedPosition">Optional named position (Top or Bottom) for the new checklist</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The newly created checklist, or the existing checklist with the same name if found</returns>
        public async Task<Checklist> AddChecklistAsync(string cardId, string existingChecklistIdToCopyFrom, bool ignoreIfAChecklistWithThisNameAlreadyExist = false, NamedPosition? namedPosition = null, CancellationToken cancellationToken = default)
        {
            if (ignoreIfAChecklistWithThisNameAlreadyExist)
            {
                //Find the name of the template (as we are only provided Id)
                var existingChecklist = await GetChecklistAsync(existingChecklistIdToCopyFrom, cancellationToken);
                //Check if card already have a list with same name
                var existingOnCard = await GetChecklistsOnCardAsync(cardId, cancellationToken);
                var existing = existingOnCard.FirstOrDefault(x => x.Name == existingChecklist.Name);
                if (existing != null)
                {
                    return existing;
                }
            }

            QueryParameter[] parameters =
            {
                new QueryParameter("idChecklistSource", existingChecklistIdToCopyFrom)
            };
            _queryParametersBuilder.AdjustForNamedPosition(parameters, namedPosition);
            return await _apiRequestController.Post<Checklist>($"{UrlPaths.Cards}/{cardId}/{UrlPaths.Checklists}", cancellationToken, parameters);
        }

        /// <summary>
        /// Updates the name and/or position of an existing checklist.
        /// </summary>
        /// <param name="checklistId">ID of the checklist to update</param>
        /// <param name="newName">The new name for the checklist, or null to leave unchanged</param>
        /// <param name="namedPosition">The new named position (Top or Bottom) for the checklist, or null to leave unchanged</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated checklist</returns>
        public async Task<Checklist> UpdateChecklistAsync(string checklistId, string newName = null, NamedPosition? namedPosition = null, CancellationToken cancellationToken = default)
        {
            var parameters = new List<QueryParameter>();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                parameters.Add(new QueryParameter("name", newName));
            }

            if (namedPosition.HasValue)
            {
                switch (namedPosition.Value)
                {
                    case NamedPosition.Top:
                        parameters.Add(new QueryParameter("pos", "top"));
                        break;
                    case NamedPosition.Bottom:
                        parameters.Add(new QueryParameter("pos", "bottom"));
                        break;
                    default:
                        parameters.Add(new QueryParameter("pos", Convert.ToInt32(namedPosition.Value)));
                        break;
                }
            }

            return await _apiRequestController.Put<Checklist>($"{UrlPaths.Checklists}/{checklistId}", cancellationToken, parameters.ToArray());
        }

        /// <summary>
        /// Updates an existing checklist item on a card.
        /// </summary>
        /// <param name="cardId">ID of the card containing the checklist item</param>
        /// <param name="item">The updated checklist item object</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated checklist item</returns>
        public async Task<ChecklistItem> UpdateChecklistItemAsync(string cardId, ChecklistItem item, CancellationToken cancellationToken = default)
        {
            var parameters = _queryParametersBuilder.GetViaQueryParameterAttributes(item);
            _queryParametersBuilder.AdjustForNamedPosition(parameters, item.NamedPosition);
            return await _apiRequestController.Put<ChecklistItem>($"{UrlPaths.Cards}/{cardId}/checkItem/{item.Id}", cancellationToken, parameters);
        }

        /// <summary>
        /// Retrieves a checklist by its ID.
        /// </summary>
        /// <param name="checkListId">ID of the checklist to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The checklist with the specified ID</returns>
        public Task<Checklist> GetChecklistAsync(string checkListId, CancellationToken cancellationToken = default)
        {
            return _apiRequestController.Get<Checklist>(GetUrlBuilder.GetChecklist(checkListId), cancellationToken);
        }

        /// <summary>
        /// Retrieves all checklists used on cards on a specific board.
        /// </summary>
        /// <param name="boardId">ID of the board (long or short version)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of checklists used on the board</returns>
        public async Task<List<Checklist>> GetChecklistsOnBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Checklist>>(GetUrlBuilder.GetChecklistsOnBoard(boardId), cancellationToken);
        }

        /// <summary>
        /// Retrieves all checklists attached to a specific card.
        /// </summary>
        /// <param name="cardId">ID of the card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of checklists attached to the card</returns>
        public async Task<List<Checklist>> GetChecklistsOnCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Checklist>>(GetUrlBuilder.GetChecklistsOnCard(cardId), cancellationToken);
        }

        /// <summary>
        /// Deletes a checklist from Trello. This operation is irreversible.
        /// </summary>
        /// <param name="checklistId">ID of the checklist to delete</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteChecklistAsync(string checklistId, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Delete($"{UrlPaths.Checklists}/{checklistId}", cancellationToken, 0);
        }

        /// <summary>
        /// Deletes a checklist item from a checklist. This operation is irreversible.
        /// </summary>
        /// <param name="checklistId">ID of the checklist containing the item</param>
        /// <param name="checklistItemId">ID of the checklist item to delete</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteChecklistItemAsync(string checklistId, string checklistItemId, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Delete($"{UrlPaths.Checklists}/{checklistId}/{UrlPaths.CheckItems}/{checklistItemId}", cancellationToken, 0);
        }
    }
}