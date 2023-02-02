using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    /// <inheritdoc />
    public class ChecklistController : IChecklistController
    {
        private readonly ApiRequestController _apiRequestController;
        private readonly TrelloClient _parent;
        private readonly UriParameterBuilder _uriParameterBuilder;

        internal ChecklistController(ApiRequestController apiRequestController, TrelloClient parent)
        {
            _parent = parent;
            _apiRequestController = apiRequestController;
            _uriParameterBuilder = new UriParameterBuilder();
        }

        /// <inheritdoc />
        public Task<Checklist> GetChecklistAsync(string id)
        {
            return _apiRequestController.Get<Checklist>($"{Constants.UrlSuffixGroup.Checklists}/{id}");
        }
        
        /// <inheritdoc />
        public async Task<List<Checklist>> GetChecklistsOnBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<List<Checklist>>($"{Constants.UrlSuffixGroup.Boards}/{boardId}/checklists");
        }

        /// <inheritdoc />
        public async Task<List<Checklist>> GetChecklistsOnCardAsync(string cardId)
        {
            return await _apiRequestController.Get<List<Checklist>>($"{Constants.UrlSuffixGroup.Cards}/{cardId}/checklists");
        }


        /// <inheritdoc />
        public async Task<Checklist> AddChecklistAsync(string cardId, Checklist checklist, bool ignoreIfAChecklistWithThisNameAlreadyExist = false)
        {
            var template = checklist;
            if (ignoreIfAChecklistWithThisNameAlreadyExist)
            {
                //Check if card already have a list with same name
                var existingOnCard = await GetChecklistsOnCardAsync(cardId);
                var existing = existingOnCard.FirstOrDefault(x => x.Name == template.Name);
                if (existing != null)
                {
                    return existing;
                }
            }
            var checklistParameters = _uriParameterBuilder.GetViaUriParameterAttribute(template);
            var newChecklist = await _apiRequestController.Post<Checklist>($"{Constants.UrlSuffixGroup.Cards}/{cardId}/checklists", checklistParameters.ToArray());
            foreach (var item in template.Items)
            {
                List<UriParameter> checkItemParameters = _uriParameterBuilder.GetViaUriParameterAttribute(item);
                newChecklist.Items.Add(await _apiRequestController.Post<ChecklistItem>($"{Constants.UrlSuffixGroup.Checklists}/{newChecklist.Id}/checkItems", checkItemParameters.ToArray()));
            }

            return newChecklist;
        }

        /// <inheritdoc />
        public async Task<Checklist> AddChecklistAsync(string cardId, string existingChecklistIdToCopyFrom, bool ignoreIfAChecklistWithThisNameAlreadyExist = false)
        {
            if (ignoreIfAChecklistWithThisNameAlreadyExist)
            {
                //Find the name of the template (as we are only provided Id)
                var existingChecklist = await _parent.Checklists.GetChecklistAsync(existingChecklistIdToCopyFrom);
                //Check if card already have a list with same name
                var existingOnCard = await GetChecklistsOnCardAsync(cardId);
                var existing = existingOnCard.FirstOrDefault(x => x.Name == existingChecklist.Name);
                if (existing != null)
                {
                    return existing;
                }
            }

            UriParameter[] parameters =
            {
                new UriParameter("idChecklistSource", existingChecklistIdToCopyFrom)
            };
            return await _apiRequestController.Post<Checklist>($"{Constants.UrlSuffixGroup.Cards}/{cardId}/checklists", parameters);
        }
    }
}