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


        private async Task MindlessApiWork()
        {
            try
            {
                for (int i = 0; i < 150; i++)
                {
                    const string boardId = "63e1096da4ecf28dcb763ba9";
                    _output.WriteLine(i.ToString());
                    var board = await TrelloClient.GetBoardAsync(boardId);
                }
            }
            catch (Exception e)
            {
                _output.WriteLine(e.Message);
                throw;
            }
            
        }

        [FactManualOnly]
        public async Task PlaygroundTest()
        {
            _output.WriteLine("Hello");
            await Task.CompletedTask;
            var boardId = "63e1096da4ecf28dcb763ba9";
            var membershipsOfBoardAsync = await TrelloClient.GetMembershipsOfBoardAsync(boardId);

#pragma warning disable CS0219
            int debug = 0;
#pragma warning restore CS0219
            /*
            var t1 = await Task.Factory.StartNew(async () => await MindlessApiWork());
            var t2 = await Task.Factory.StartNew(async () => await MindlessApiWork());
            var t3 = await Task.Factory.StartNew(async () => await MindlessApiWork());

            Task.WaitAll(t1, t2, t3);
            */
            /*
            TrelloClient.Options.IncludeAttachmentsInCardGetMethods = true;

            string boardId = "czwRjhWF";
            boardId = "63e1096da4ecf28dcb763ba9";

            var labelsOfBoardAsync = await TrelloClient.GetLabelsOfBoardAsync(boardId);

            Label label = await TrelloClient.AddLabelAsync(new Label(boardId, name: "Mine!!!", color: "blue"));

            //await TrelloClient.DeleteLabelAsync(labelsOfBoardAsync[0].Id);


            var cardId = "63fb5126051edb121d91b4aa";

            var card = await TrelloClient.GetCardAsync(cardId);
            card.Cover = new CardCover(CardCoverColor.Blue, CardCoverSize.Normal);
            card.AttachmentCover = null;
            var updateCardAsync = await TrelloClient.UpdateCardAsync(card);

            updateCardAsync.Cover = null;
            updateCardAsync.AttachmentCover = card.Attachments[0].Id;

            var updateCardAsync2 = await TrelloClient.UpdateCardAsync(updateCardAsync);
            //var updateCardAsync2 = await TrelloClient.UpdateCardAsync(updateCardAsync);

            int debug = 0;

            /*
            await TrelloClient.AddAttachmentToCardAsync(cardId, new AttachmentFileUpload(File.OpenRead(@"C:\Users\rasmu\OneDrive\Pictures\workout.png"), "MyFile.png", "my attachment name"), true);

            //var addAttachmentToCardAsync = await TrelloClient.AddAttachmentToCardAsync(cardId, new AttachmentUrlLink("https://upload.wikimedia.org/wikipedia/commons/b/b6/Image_created_with_a_mobile_phone.png", "My Link Attachment"));

            List<Attachment> att = await TrelloClient.GetAttachmentsOnCardAsync(cardId);

            //await TrelloClient.DeleteAttachmentOnCardAsync(cardId, addAttachmentToCardAsync.Id);
            */


            /*            
            var cardId = "63e216e15baa8f45ae87948b";
            var boardId = "63e1096da4ecf28dcb763ba9";

            //var card = await TrelloClient.GetCardAsync(cardId);

            var addCoverToCardAsync = await TrelloClient.AddCoverToCardAsync(cardId, new CardCover(CardCoverColor.Orange, CardCoverSize.Normal));

            var uaddCoverToCardAsync = await TrelloClient.UpdateCoverOnCardAsync(cardId, new CardCover(CardCoverColor.Blue, CardCoverSize.Full));

            //card.Cover = new CardCover(CardCoverColor.Orange, CardCoverSize.Full);
            //card.Cover.Color = CardCoverColor.Green;
            //card.Cover.Size = CardCoverSize.Normal;
            //card.Cover.Brightness = CardCoverBrightness.Dark;
            //card.Cover.BackgroundImageId = "6425aff85c2e0abab405e043";
            //await TrelloClient.UpdateCardAsync(card);


            //var removeCoverFromCardAsync = await TrelloClient.RemoveCoverFromCardAsync(cardId);

            int debug = 0;

            //var cards = await TrelloClient.GetCardsOnBoardAsync(boardId);
            /*
            var addStickerToCard = await TrelloClient.AddStickerToCardAsync(cardId, new Sticker(StickerDefaultImageId.Clock, 20, 10, 0, 45));

            var stickerAsync = await TrelloClient.GetStickerAsync(cardId, addStickerToCard.Id);

            stickerAsync.Left = 50;
            var u = await TrelloClient.UpdateStickerAsync(cardId, stickerAsync);

            var stickersOnCard = await TrelloClient.GetStickersOnCardAsync(cardId);

            foreach (var s in stickersOnCard)
            {
                await TrelloClient.DeleteStickerAsync(cardId, s.Id);
            }*/

            /*
            for (int i = 0; i < 50; i++)
            {
              var comment = await TrelloClient.AddCommentAsync(cardId, new Comment("My first cool comment! @rasmus58348007 mention"));
            }*/

            //comment.Data.Text = "New text!";
            //var commentAction = await TrelloClient.UpdateCommentActionAsync(comment);

            //var commentsOnCardAsync = await TrelloClient.GetAllCommentsOnCardAsync(cardId);
            //var commentsOnCardAsync2 = await TrelloClient.GetPagedCommentsOnCardAsync(cardId, page: 1);
            //var commentsOnCardAsync3 = await TrelloClient.GetPagedCommentsOnCardAsync(cardId, page: 2);
            //var commentsOnCardAsync4 = await TrelloClient.GetPagedCommentsOnCardAsync(cardId, page: 3);
        }
    }
}
