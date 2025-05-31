using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TrelloDotNet.Control;
using TrelloDotNet.Model;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Updates the value of a custom field of type 'Checkbox' on a card.
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">ID of the card to update the custom field on</param>
        /// <param name="customField">The custom field to update (must be of type 'Checkbox')</param>
        /// <param name="newValue">The new boolean value to set</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, bool newValue, CancellationToken cancellationToken = default)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Checkbox:
                    var valueAsString = newValue ? "true" : "false";
                    payload = $"{{\"value\": {{ \"checked\": \"{HttpUtility.JavaScriptStringEncode(valueAsString)}\" }}}}";
                    break;
                case CustomFieldType.Date:
                case CustomFieldType.List:
                case CustomFieldType.Number:
                case CustomFieldType.Text:
                default:
                    throw new ArgumentOutOfRangeException(nameof(customField), "Only a custom field of type 'Checkbox' can be set with a bool value");
            }

            await SendCustomFieldChangeRequestAsync(cardId, customField, payload, cancellationToken);
        }

        /// <summary>
        /// Updates the value of a custom field of type 'Date' on a card.
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">ID of the card to update the custom field on</param>
        /// <param name="customField">The custom field to update (must be of type 'Date')</param>
        /// <param name="newValue">The new date value to set</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, DateTimeOffset newValue, CancellationToken cancellationToken = default)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Date:
                    string valueAsString = newValue.UtcDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
                    payload = $"{{\"value\": {{ \"date\": \"{HttpUtility.JavaScriptStringEncode(valueAsString)}\" }}}}";
                    break;
                case CustomFieldType.Checkbox:
                case CustomFieldType.List:
                case CustomFieldType.Number:
                case CustomFieldType.Text:
                default:
                    throw new ArgumentOutOfRangeException(nameof(customField), "Only a custom field of type 'Date' can be set with a DateTimeOffset value");
            }

            await SendCustomFieldChangeRequestAsync(cardId, customField, payload, cancellationToken);
        }

        /// <summary>
        /// Updates the value of a custom field of type 'Number' on a card using an integer value.
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">ID of the card to update the custom field on</param>
        /// <param name="customField">The custom field to update (must be of type 'Number')</param>
        /// <param name="newValue">The new integer value to set</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, int newValue, CancellationToken cancellationToken = default)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Number:
                    var valueAsString = newValue.ToString(CultureInfo.InvariantCulture);
                    payload = $"{{\"value\": {{ \"number\": \"{HttpUtility.JavaScriptStringEncode(valueAsString)}\" }}}}";
                    break;
                case CustomFieldType.Checkbox:
                case CustomFieldType.Date:
                case CustomFieldType.List:
                case CustomFieldType.Text:
                default:
                    throw new ArgumentOutOfRangeException(nameof(customField), "Only a custom field of type 'Number' can be set with a integer value");
            }

            await SendCustomFieldChangeRequestAsync(cardId, customField, payload, cancellationToken);
        }

        /// <summary>
        /// Updates the value of a custom field of type 'Number' on a card using a decimal value.
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">ID of the card to update the custom field on</param>
        /// <param name="customField">The custom field to update (must be of type 'Number')</param>
        /// <param name="newValue">The new decimal value to set</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, decimal newValue, CancellationToken cancellationToken = default)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Number:
                    var valueAsString = newValue.ToString(CultureInfo.InvariantCulture);
                    payload = $"{{\"value\": {{ \"number\": \"{HttpUtility.JavaScriptStringEncode(valueAsString)}\" }}}}";
                    break;
                case CustomFieldType.Checkbox:
                case CustomFieldType.Date:
                case CustomFieldType.List:
                case CustomFieldType.Text:
                default:
                    throw new ArgumentOutOfRangeException(nameof(customField), "Only a custom field of type 'Number' can be set with a decimal value");
            }

            await SendCustomFieldChangeRequestAsync(cardId, customField, payload, cancellationToken);
        }

        /// <summary>
        /// Updates the value of a custom field of type 'List' on a card using a custom field option.
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">ID of the card to update the custom field on</param>
        /// <param name="customField">The custom field to update (must be of type 'List')</param>
        /// <param name="newValue">The new custom field option to set</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, CustomFieldOption newValue, CancellationToken cancellationToken = default)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.List:
                    string valueAsString = string.Empty;
                    if (newValue != null)
                    {
                        valueAsString = newValue.Id;
                    }

                    payload = $"{{\"idValue\": \"{HttpUtility.JavaScriptStringEncode(valueAsString)}\" }}";
                    break;
                case CustomFieldType.Checkbox:
                case CustomFieldType.Date:
                case CustomFieldType.Number:
                case CustomFieldType.Text:
                default:
                    throw new ArgumentOutOfRangeException(nameof(customField), "Only a custom field of type 'List' can be set with a CustomFieldOption value");
            }

            await SendCustomFieldChangeRequestAsync(cardId, customField, payload, cancellationToken);
        }

        /// <summary>
        /// Updates the value of a custom field on a card using a string value. The type of the custom field determines how the value is interpreted.
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">ID of the card to update the custom field on</param>
        /// <param name="customField">The custom field to update</param>
        /// <param name="newValue">The new string value to set</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, string newValue, CancellationToken cancellationToken = default)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Checkbox:
                    payload = $"{{\"value\": {{ \"checked\": \"{HttpUtility.JavaScriptStringEncode(newValue)}\" }}}}";
                    break;
                case CustomFieldType.Date:
                    payload = $"{{\"value\": {{ \"date\": \"{HttpUtility.JavaScriptStringEncode(newValue)}\" }}}}";
                    break;
                case CustomFieldType.List:
                    payload = $"{{\"idValue\": \"{HttpUtility.JavaScriptStringEncode(newValue)}\" }}";
                    break;
                case CustomFieldType.Number:
                    payload = $"{{\"value\": {{ \"number\": \"{HttpUtility.JavaScriptStringEncode(newValue)}\" }}}}";
                    break;
                case CustomFieldType.Text:
                    payload = $"{{\"value\": {{ \"text\": \"{HttpUtility.JavaScriptStringEncode(newValue)}\" }}}}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await SendCustomFieldChangeRequestAsync(cardId, customField, payload, cancellationToken);
        }

        /// <summary>
        /// Clears the value of a custom field on a card, removing any value that was previously set.
        /// </summary>
        /// <param name="cardId">ID of the card to clear the custom field on</param>
        /// <param name="customField">The custom field to clear</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task ClearCustomFieldValueOnCardAsync(string cardId, CustomField customField, CancellationToken cancellationToken = default)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Checkbox:
                case CustomFieldType.Date:
                case CustomFieldType.Number:
                case CustomFieldType.Text:
                    payload = "{\"value\": \"\" }";
                    break;
                case CustomFieldType.List:
                    payload = "{\"idValue\": \"\" }";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await SendCustomFieldChangeRequestAsync(cardId, customField, payload, cancellationToken);
        }

        private async Task SendCustomFieldChangeRequestAsync(string cardId, CustomField customField, string payload, CancellationToken cancellationToken)
        {
            await _apiRequestController.PutWithJsonPayload($"{UrlPaths.Cards}/{cardId}/customField/{customField.Id}/item", cancellationToken, payload, 0);
        }

        /// <summary>
        /// Retrieves all custom field item values for a specific card.
        /// </summary>
        /// <remarks>Tip: Use extension methods GetCustomFieldValueAsXYZ for a handy way to get values</remarks>
        /// <param name="cardId">ID of the card to retrieve custom field items for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of custom field items for the card</returns>
        public async Task<List<CustomFieldItem>> GetCustomFieldItemsForCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<CustomFieldItem>>(GetUrlBuilder.GetCustomFieldItemsForCard(cardId), cancellationToken);
        }

        /// <summary>
        /// Retrieves all custom fields defined on a specific board.
        /// </summary>
        /// <param name="boardId">ID of the board (long version) to retrieve custom fields for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of custom fields on the board</returns>
        public async Task<List<CustomField>> GetCustomFieldsOnBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<CustomField>>(GetUrlBuilder.GetCustomFieldsOnBoard(boardId), cancellationToken);
        }
    }
}