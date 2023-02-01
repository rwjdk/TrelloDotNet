using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    public class CustomFieldController : ICustomFieldController
    {
        private readonly ApiRequestController _apiRequestController;

        internal CustomFieldController(ApiRequestController apiRequestController)
        {
            _apiRequestController = apiRequestController;
        }

        public async Task<CustomField> GetAsync(string customFieldId)
        {
            return await _apiRequestController.GetResponse<CustomField>($"{Constants.UrlSuffixGroup.CustomFields}/{customFieldId}");
        }

        public async Task<CustomField> AddAsync(Board board, string name, CustomFieldType type, CustomFieldPosition position, bool displayOnTheFrontOfCard = true, List<CustomFieldOption> options = null)
        {
            return await AddAsync(board.Id, name, type, position, displayOnTheFrontOfCard, options);
        }

        public async Task<CustomField> AddAsync(string longBoardId, string name, CustomFieldType type, CustomFieldPosition position, bool displayOnTheFrontOfCard = true, List<CustomFieldOption> options = null)
        {
            List<UriParameter> parameters = new List<UriParameter>
            {
                new UriParameter("idModel", longBoardId),
                new UriParameter("modelType", "board"),
                new UriParameter("name", name),
                new UriParameter("type", type.GetJsonPropertyName()),
                new UriParameter("pos", position.GetJsonPropertyName()), //todo - this seems a bit buggy (does not always end up the right place)
                new UriParameter("display_cardFront", displayOnTheFrontOfCard)
            };

            //todo - options for Custom fields does not work and should be removed (Not even their own sample works: https://developer.atlassian.com/cloud/trello/guides/rest-api/getting-started-with-custom-fields/)
            if (type == CustomFieldType.List && options != null && options.Count > 0)
            {
                StringBuilder optionsArray = new StringBuilder();
                optionsArray.Append("[");
                int postion = 0;
                foreach (var customFieldOption in options)
                {
                    string optionAsString = $"{{ color:\"{customFieldOption.Color}\", value: {{ text: \"{customFieldOption.Name}\" }}, pos:{position} }}";
                    optionsArray.Append(optionAsString);
                    position++;
                }
                optionsArray.Append("]");
                parameters.Add(new UriParameter("options", optionsArray.ToString()));
            }

            return await _apiRequestController.Post<CustomField>(Constants.UrlSuffixGroup.CustomFields, parameters.ToArray());
        }
    }
}