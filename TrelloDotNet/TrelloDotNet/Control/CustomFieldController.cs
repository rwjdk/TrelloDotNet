using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    /// <inheritdoc />
    public class CustomFieldController : ICustomFieldController
    {
        private readonly ApiRequestController _apiRequestController;

        internal CustomFieldController(ApiRequestController apiRequestController)
        {
            _apiRequestController = apiRequestController;
        }

        /// <inheritdoc />
        public async Task<CustomField> GetCustomFieldAsync(string customFieldId)
        {
            return await _apiRequestController.Get<CustomField>($"{Constants.UrlSuffixGroup.CustomFields}/{customFieldId}");
        }

        /// <inheritdoc />
        public async Task<CustomField> AddCustomFieldAsync(Board board, string name, CustomFieldType type, bool displayOnTheFrontOfCard = true, List<CustomFieldOption> options = null)
        {
            return await AddCustomFieldAsync(board.Id, name, type, displayOnTheFrontOfCard, options);
        }

        /// <inheritdoc />
        public async Task<CustomField> AddCustomFieldAsync(string boardId, string name, CustomFieldType type, bool displayOnTheFrontOfCard = true, List<CustomFieldOption> options = null)
        {
            List<UriParameter> parameters = new List<UriParameter>
            {
                new UriParameter("idModel", boardId),
                new UriParameter("modelType", "board"),
                new UriParameter("name", name),
                new UriParameter("type", type.GetJsonPropertyName()),
                new UriParameter("display_cardFront", displayOnTheFrontOfCard)
            };

            //todo - options for Custom fields does not work and should be removed (Not even their own sample works: https://developer.atlassian.com/cloud/trello/guides/rest-api/getting-started-with-custom-fields/)
            if (type == CustomFieldType.List && options != null && options.Count > 0)
            {
                StringBuilder optionsArray = new StringBuilder();
                optionsArray.Append("[");
                foreach (var customFieldOption in options)
                {
                    string optionAsString = $"{{ color:\"{customFieldOption.Color}\", value: {{ text: \"{customFieldOption.Name}\" }} }}";
                    optionsArray.Append(optionAsString);
                }
                optionsArray.Append("]");
                parameters.Add(new UriParameter("options", optionsArray.ToString()));
            }

            return await _apiRequestController.Post<CustomField>(Constants.UrlSuffixGroup.CustomFields, parameters.ToArray());
        }
    }
}