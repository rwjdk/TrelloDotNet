using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests
{

    public class TestSandbox : TestBase
    {
        [FactManualOnly]
        public async Task GetWebhooks()
        {
            await Task.CompletedTask;
            var webhooksForCurrentToken = await TrelloClient.GetWebhooksForCurrentTokenAsync();
        }

        [FactManualOnly]
        public async Task DeleteAllWebhooks()
        {
            await Task.CompletedTask;
            var webhooksForCurrentToken = await TrelloClient.GetWebhooksForCurrentTokenAsync();
            foreach (var webhook in webhooksForCurrentToken)
            {
                await TrelloClient.DeleteWebhookAsync(webhook.Id);
            }
        }

        [FactManualOnly]
        public async Task UpdateWebhook()
        {
            await Task.CompletedTask;
            //Webhook webhook = await TrelloClient.GetWebhookAsync("63e2892778670f4f7b7ffa2e");
            //webhook.CallbackUrl = "https://4cf8-185-229-154-225.eu.ngrok.io/api/FunctionTrelloWebhookEndpointReceiver";
            //var updatedWebhook = await TrelloClient.UpdateWebhookAsync(webhook);
        }
        
        [FactManualOnly]
        public async Task TestTask()
        {
            await Task.CompletedTask;
            var cardId = "63e216e15baa8f45ae87948b";
            var boardId = "63e1096da4ecf28dcb763ba9";
            //var card = await TrelloClient.GetCardAsync(cardId);
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

            //await TrelloClient.AddCommentAsync(cardId, new Comment("My first cool comment! @rasmus58348007 mention"));
        }
    }
}
