using TrelloDotNet.Model;
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
            /*
            var boardId = "641ddde2e37dc99ab1ccc988";
            List<CustomField> customFieldsOnBoardAsync = await TrelloClient.GetCustomFieldsOnBoardAsync(boardId);
            var cardsOnBoardAsync = await TrelloClient.GetCardsOnBoardAsync(boardId);
            var card = cardsOnBoardAsync[0];
            List<CustomFieldItem>? customFields = await TrelloClient.GetCustomFieldItemsForCardAsync(card.Id);
            var customFieldValueAsString = customFields.GetCustomFieldValueAsString(customFieldsOnBoardAsync[0]);

            foreach (var customField in customFieldsOnBoardAsync)
            {
                switch (customField.Type)
                {
                    case CustomFieldType.Checkbox:
                        var b = card.CustomFieldItems.GetCustomFieldValueAsBoolean(customField);
                        await TrelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, true);
                        await TrelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField);
                        break;
                    case CustomFieldType.Date:
                        var d = card.CustomFieldItems.GetCustomFieldValueAsDateTimeOffset(customField);
                        await TrelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField);
                        break;
                    case CustomFieldType.List:
                        var l1 = card.CustomFieldItems.GetCustomFieldValueAsOption(customField);
                        var l2 = card.CustomFieldItems.GetCustomFieldValueAsString(customField);
                        await TrelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField);
                        break;
                    case CustomFieldType.Number:
                        var n1 = card.CustomFieldItems.GetCustomFieldValueAsDecimal(customField);
                        var n2 = card.CustomFieldItems.GetCustomFieldValueAsInteger(customField);
                        await TrelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField);
                        break;
                    case CustomFieldType.Text:
                        var s = card.CustomFieldItems.GetCustomFieldValueAsString(customField);
                        await TrelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, s + "WOW");
                        await TrelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }*/
        }

        [FactManualOnly]
        public async Task PlaygroundTest()
        {
            _output.WriteLine("PlaygroundTest");
            await Task.CompletedTask;

            TrelloClientOptions

            /*
            var boardId = "63c939a5cea0cb006dc9e88b";
            var cardId = "63c939a5cea0cb006dc9e9dd";
            var listId = "63c939a5cea0cb006dc9e89d";
            var memberId = "63d1239e857afaa8b003c633";*/
        }
    }
}
