using TrelloDotNet.Model;
using TrelloDotNet.Model.Actions;
using Xunit.Abstractions;

namespace TrelloDotNet.Tests
{
    public class TestSandbox : TestBase
    {
        private readonly ITestOutputHelper _output;

        public TestSandbox(ITestOutputHelper output)
        {
            _output = output;
        }

        [FactManualOnly]
        public async Task GetWebhooks()
        {
            await Task.CompletedTask;
            var webhooksForCurrentToken = await TrelloClient.GetWebhooksForCurrentTokenAsync();
            foreach (var webhook in webhooksForCurrentToken)
            {
                _output.WriteLine("- Webhook: {0} ({1})", webhook.Description, webhook.CallbackUrl);
            }
        }

        [FactManualOnly]
        public async Task DeleteAllBoardsWithUnitTestBoardPrefix()
        {
            await Task.CompletedTask;
            /*
            TrelloClient.Options.AllowDeleteOfBoards = true;
            List<Board> boards = await TrelloClient.GetBoardsCurrentTokenCanAccessAsync();
            foreach (var unitTestBoard in boards.Where(x => x.Name.StartsWith("UnitTestBoard")))
            {
                await TrelloClient.DeleteBoardAsync(unitTestBoard.Id);
            }

            TrelloClient.Options.AllowDeleteOfBoards = false;
            }*/
        }

        [FactManualOnly]
        public async Task DeleteAllWebhooks()
        {
            await Task.CompletedTask;
            /*
            var webhooksForCurrentToken = await TrelloClient.GetWebhooksForCurrentTokenAsync();
            foreach (var webhook in webhooksForCurrentToken)
            {
                await TrelloClient.DeleteWebhookAsync(webhook.Id);
            }*/
        }

        [FactManualOnly]
        public async Task UpdateWebhook()
        {
            await Task.CompletedTask;
            //Webhook webhook = await TrelloClient.GetWebhookAsync("63e2892778670f4f7b7ffa2e");
            //webhook.CallbackUrl = "https://4cf8-185-229-154-225.eu.ngrok.io/api/FunctionTrelloWebhookEndpointReceiver";
            //var updatedWebhook = await TrelloClient.UpdateWebhookAsync(webhook);
            //or
            //await TrelloClient.UpdateWebhookByCallbackUrlAsync("https://old", "https://new");
        }

        [FactManualOnly]
        public async Task CustomFieldsTests()
        {
            await Task.CompletedTask;
            /*
            int debug = 0;
            /*card.AttachmentCover = null;
            await TrelloClient.UpdateCardAsync(card);*/
            //await TrelloClient.AddCoverToCardAsync(card.Id, new CardCover(card.Attachments[0].Id, CardCoverBrightness.Light));

            //NB: These are not part of the automated test-suite as that is linked to a free account that does not support custom fields
            var _trelloClient = TrelloClient;

var boardId = "641ddde2e37dc99ab1ccc988";
List<CustomField> customFieldsOnBoardAsync = await _trelloClient.GetCustomFieldsOnBoardAsync(boardId);
var card = (await _trelloClient.GetCardsOnBoardAsync(boardId)).First(); //Grab random card

//Sample set all custom fields on the board
foreach (var customField in customFieldsOnBoardAsync)
{
    switch (customField.Type)
    {
        case CustomFieldType.Checkbox:
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, true); //Update Bool
            bool? boolean = card.CustomFieldItems.GetCustomFieldValueAsBoolean(customField); //Get Bool
            await _trelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear Bool
            break;
        case CustomFieldType.Date:
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, DateTimeOffset.Now); //Update Date
            DateTimeOffset? dateTimeOffset = card.CustomFieldItems.GetCustomFieldValueAsDateTimeOffset(customField); // Get Date
            await _trelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear Date
            break;
        case CustomFieldType.List:
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, customField.Options[0]); //Update ListOption
            CustomFieldOption? listOption = card.CustomFieldItems.GetCustomFieldValueAsOption(customField); //Get ListOption (as Option)
            string listOptionString = card.CustomFieldItems.GetCustomFieldValueAsString(customField); //Get ListOption as String value
            await _trelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear List Option
            break;
        case CustomFieldType.Number:
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, 42); //Update Integer
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, 42M); //Update Decimal
            int? numberAsInteger = card.CustomFieldItems.GetCustomFieldValueAsInteger(customField); //Get Integer
            decimal? numberAsDecimal = card.CustomFieldItems.GetCustomFieldValueAsDecimal(customField); //Get Decimal
            await _trelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear number
            break;
        case CustomFieldType.Text:
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, "Hello World"); //Update String
            var stringValue = card.CustomFieldItems.GetCustomFieldValueAsString(customField); //Get String
            await _trelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear String
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }
}
        }

        [FactManualOnly]
        public async Task PlaygroundTest()
        {
            _output.WriteLine("PlaygroundTest");
            await Task.CompletedTask;


            var _trelloClient = TrelloClient;

var cardId = "63c939a5cea0cb006dc9e9dd";
var customFieldItemsForCardAsync = await _trelloClient.GetCustomFieldItemsForCardAsync(cardId);
foreach (var customFieldItem in customFieldItemsForCardAsync)
{
    //Use 'customFieldItem.CustomFieldId' to determine type of field and then use below to get that values
    string checkboxValue = customFieldItem.Value.CheckedAsString;
    string dateValue = customFieldItem.Value.DateAsString;
    string numberValue = customFieldItem.Value.NumberAsString;
    string textvalue = customFieldItem.Value.TextAsString;
    string? listValueId = customFieldItem.ValueId;
}

//Alternative access via the Card itself
var boardId = "63c939a5cea0cb006dc9e88b";
List<CustomField>? customFieldsOnBoard = await _trelloClient.GetCustomFieldsOnBoardAsync(boardId);
CustomField customField = customFieldsOnBoard.First(x => x.Name == "Priority");
_trelloClient.Options.IncludeCustomFieldsInCardGetMethods = true;
Card? card = await _trelloClient.GetCardAsync(cardId);
CustomFieldOption option = card.CustomFieldItems.GetCustomFieldValueAsOption(customField);
var priorityDescription = option.Value.Text;


            /*
            var boardId = "63c939a5cea0cb006dc9e88b";
            var cardId = "63c939a5cea0cb006dc9e9dd";
    
            var memberId = "63d1239e857afaa8b003c633";*/
        }
    }
}