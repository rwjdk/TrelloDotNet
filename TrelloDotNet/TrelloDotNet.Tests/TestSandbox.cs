using TrelloDotNet.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests
{
    public class TestSandbox
    {
        [RunnableInDebugOnly]
        public async Task AddBoardWithOptions()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            var addBoardOptions = new AddBoardOptions
            {
                DefaultLabels = false,
                DefaultLists = false,
                WorkspaceId = "other60483948"
            };
            //await client.AddBoardAsync(new Board("Test"), addBoardOptions);
        }
        
        [RunnableInDebugOnly]
        public async Task CustomDelete()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            await client.DeleteAsync("cards/63de84fd6ec529e05977a5ab");
        }
        
        [RunnableInDebugOnly]
        public async Task CloseCard()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            await client.ArchiveCardAsync("63de89f086c564af5be296ba");
        }
        
        [RunnableInDebugOnly]
        public async Task ReOpenCard()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            await client.ReOpenCardAsync("63de89f086c564af5be296ba");
        }
        
        [RunnableInDebugOnly]
        public async Task CloseBoard()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            await client.CloseBoardAsync("SCPjg8ON");
        }
        
        [RunnableInDebugOnly]
        public async Task ReOpenBoard()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            await client.ReOpenBoardAsync("SCPjg8ON");
        }
        
        [RunnableInDebugOnly]
        public async Task CloseList()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            await client.ArchiveListAsync("63de3eb58d0d357abc562b02");
        }
        
        [RunnableInDebugOnly]
        public async Task ReOpenList()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            await client.ReOpenListAsync("63de3eb58d0d357abc562b02");
        }
        
        [RunnableInDebugOnly]
        public async Task ArchiveAllCardsOnList()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            await client.ArchiveAllCardsInList("63de3eb58d0d357abc562b02");
        }

        [RunnableInDebugOnly]
        public async Task MoveList()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            var boardAsync = await client.GetBoardAsync("uPB5YWto");
            await client.MoveListToBoardAsync("63dbef756685986201365c13", boardAsync.Id);
        }

        [RunnableInDebugOnly]
        public async Task MoveAllCardsOnList()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            var boardAsync = await client.GetBoardAsync("SCPjg8ON");
            await client.MoveAllCardsInList("63de3eb58d0d357abc562b02", "63dbef756685986201365c13");
        }
        
        [RunnableInDebugOnly]
        public async Task GetWebhooks()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            var webhooksForCurrentToken = await client.GetWebhooksForCurrentToken();
        }
        
        [RunnableInDebugOnly]
        public async Task DeleteAllWebhooks()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            var webhooksForCurrentToken = await client.GetWebhooksForCurrentToken();
            foreach (var webhook in webhooksForCurrentToken)
            {
                await client.DeleteWebhook(webhook.Id);
            }
        }
        
        [RunnableInDebugOnly]
        public async Task AddWebhook()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            var webHook = new Webhook("My Webhook", "https://4cf8-185-229-154-225.eu.ngrok.io/api/FunctionTrelloWebhookEndpointReceiver", "63d128787441d05619f44dbe");
            var webhooksForCurrentToken = await client.AddWebhookAsync(webHook);
        }
        
        [RunnableInDebugOnly]
        public async Task UpdateWebhook()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            Webhook webhook = await client.GetWebhookAsync("63e2892778670f4f7b7ffa2e");
            webhook.CallbackUrl = "https://4cf8-185-229-154-225.eu.ngrok.io/api/FunctionTrelloWebhookEndpointReceiver";
            var updatedWebhook = await client.UpdateWebhookAsync(webhook);
        }
        













        [RunnableInDebugOnly]
        public async Task RemoveMembers()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            var removeMemberFromCard = await client.RemoveMembersFromCardAsync("63daa5bdef6d121000749728", "63d1239e857afaa8b003c633", "a", "c");
            var cardAsync = await client.GetCardAsync("63daa5bdef6d121000749728");
        }
        
        [RunnableInDebugOnly]
        public async Task RemoveAllMembers()
        {
            await Task.CompletedTask;
            var client = new TestHelper().GetClient();
            var removeMemberFromCard = await client.RemoveAllMembersFromCardAsync("63daa5bdef6d121000749728");
        }
        
        [RunnableInDebugOnly]
        public async Task AddTestNotification()
        {
            string json = "{\r\n  \"model\": {\r\n    \"id\": \"63d128787441d05619f44dbe\",\r\n    \"name\": \"New Name\",\r\n    \"desc\": \"New description\",\r\n    \"descData\": { \"emoji\": {} },\r\n    \"closed\": false,\r\n    \"idOrganization\": \"63d123a6472de44f1ea2801c\",\r\n    \"idEnterprise\": null,\r\n    \"pinned\": false,\r\n    \"url\": \"https://trello.com/b/SCPjg8ON/new-name\",\r\n    \"shortUrl\": \"https://trello.com/b/SCPjg8ON\",\r\n    \"prefs\": {\r\n      \"permissionLevel\": \"org\",\r\n      \"hideVotes\": false,\r\n      \"voting\": \"disabled\",\r\n      \"comments\": \"members\",\r\n      \"invitations\": \"members\",\r\n      \"selfJoin\": true,\r\n      \"cardCovers\": true,\r\n      \"isTemplate\": false,\r\n      \"cardAging\": \"regular\",\r\n      \"calendarFeedEnabled\": false,\r\n      \"hiddenPluginBoardButtons\": [],\r\n      \"switcherViews\": [\r\n        {\r\n          \"viewType\": \"Board\",\r\n          \"enabled\": true,\r\n          \"_id\": \"63d128787441d05619f44dbf\"\r\n        },\r\n        {\r\n          \"viewType\": \"Table\",\r\n          \"enabled\": true,\r\n          \"_id\": \"63d128787441d05619f44dc0\"\r\n        },\r\n        {\r\n          \"viewType\": \"Calendar\",\r\n          \"enabled\": false,\r\n          \"_id\": \"63d128787441d05619f44dc1\"\r\n        },\r\n        {\r\n          \"viewType\": \"Dashboard\",\r\n          \"enabled\": false,\r\n          \"_id\": \"63d128787441d05619f44dc2\"\r\n        },\r\n        {\r\n          \"viewType\": \"Timeline\",\r\n          \"enabled\": false,\r\n          \"_id\": \"63d128787441d05619f44dc3\"\r\n        },\r\n        {\r\n          \"viewType\": \"Map\",\r\n          \"enabled\": false,\r\n          \"_id\": \"63d128787441d05619f44dc4\"\r\n        }\r\n      ],\r\n      \"background\": \"63d11a5feca7f18c987726db\",\r\n      \"backgroundColor\": null,\r\n      \"backgroundImage\": \"https://trello-backgrounds.s3.amazonaws.com/SharedBackground/original/1094ad29b86d1eafdf30bb2e692760cd/photo-1674413146454-41e62f015153\",\r\n      \"backgroundImageScaled\": [\r\n        {\r\n          \"width\": 140,\r\n          \"height\": 93,\r\n          \"url\": \"https://trello-backgrounds.s3.amazonaws.com/SharedBackground/140x93/d384b0d786af0d3aac1a58031a66058a/photo-1674413146454-41e62f015153.jpg\"\r\n        },\r\n        {\r\n          \"width\": 256,\r\n          \"height\": 171,\r\n          \"url\": \"https://trello-backgrounds.s3.amazonaws.com/SharedBackground/256x171/d384b0d786af0d3aac1a58031a66058a/photo-1674413146454-41e62f015153.jpg\"\r\n        },\r\n        {\r\n          \"width\": 480,\r\n          \"height\": 320,\r\n          \"url\": \"https://trello-backgrounds.s3.amazonaws.com/SharedBackground/480x320/d384b0d786af0d3aac1a58031a66058a/photo-1674413146454-41e62f015153.jpg\"\r\n        },\r\n        {\r\n          \"width\": 960,\r\n          \"height\": 641,\r\n          \"url\": \"https://trello-backgrounds.s3.amazonaws.com/SharedBackground/960x641/d384b0d786af0d3aac1a58031a66058a/photo-1674413146454-41e62f015153.jpg\"\r\n        },\r\n        {\r\n          \"width\": 1024,\r\n          \"height\": 683,\r\n          \"url\": \"https://trello-backgrounds.s3.amazonaws.com/SharedBackground/1024x683/d384b0d786af0d3aac1a58031a66058a/photo-1674413146454-41e62f015153.jpg\"\r\n        },\r\n        {\r\n          \"width\": 1280,\r\n          \"height\": 854,\r\n          \"url\": \"https://trello-backgrounds.s3.amazonaws.com/SharedBackground/1280x854/d384b0d786af0d3aac1a58031a66058a/photo-1674413146454-41e62f015153.jpg\"\r\n        },\r\n        {\r\n          \"width\": 1920,\r\n          \"height\": 1281,\r\n          \"url\": \"https://trello-backgrounds.s3.amazonaws.com/SharedBackground/1920x1281/d384b0d786af0d3aac1a58031a66058a/photo-1674413146454-41e62f015153.jpg\"\r\n        },\r\n        {\r\n          \"width\": 2048,\r\n          \"height\": 1366,\r\n          \"url\": \"https://trello-backgrounds.s3.amazonaws.com/SharedBackground/2048x1366/d384b0d786af0d3aac1a58031a66058a/photo-1674413146454-41e62f015153.jpg\"\r\n        },\r\n        {\r\n          \"width\": 2398,\r\n          \"height\": 1600,\r\n          \"url\": \"https://trello-backgrounds.s3.amazonaws.com/SharedBackground/2398x1600/d384b0d786af0d3aac1a58031a66058a/photo-1674413146454-41e62f015153.jpg\"\r\n        },\r\n        {\r\n          \"width\": 2560,\r\n          \"height\": 1708,\r\n          \"url\": \"https://trello-backgrounds.s3.amazonaws.com/SharedBackground/original/1094ad29b86d1eafdf30bb2e692760cd/photo-1674413146454-41e62f015153\"\r\n        }\r\n      ],\r\n      \"backgroundTile\": false,\r\n      \"backgroundBrightness\": \"dark\",\r\n      \"backgroundBottomColor\": \"#0a232b\",\r\n      \"backgroundTopColor\": \"#9db0bf\",\r\n      \"canBePublic\": false,\r\n      \"canBeEnterprise\": false,\r\n      \"canBeOrg\": false,\r\n      \"canBePrivate\": false,\r\n      \"canInvite\": true\r\n    },\r\n    \"labelNames\": {\r\n      \"green\": \"Hello\",\r\n      \"yellow\": \"\",\r\n      \"orange\": \"\",\r\n      \"red\": \"\",\r\n      \"purple\": \"\",\r\n      \"blue\": \"\",\r\n      \"sky\": \"\",\r\n      \"lime\": \"\",\r\n      \"pink\": \"\",\r\n      \"black\": \"\",\r\n      \"green_dark\": \"\",\r\n      \"yellow_dark\": \"\",\r\n      \"orange_dark\": \"\",\r\n      \"red_dark\": \"\",\r\n      \"purple_dark\": \"\",\r\n      \"blue_dark\": \"\",\r\n      \"sky_dark\": \"\",\r\n      \"lime_dark\": \"\",\r\n      \"pink_dark\": \"\",\r\n      \"black_dark\": \"\",\r\n      \"green_light\": \"\",\r\n      \"yellow_light\": \"World\",\r\n      \"orange_light\": \"\",\r\n      \"red_light\": \"\",\r\n      \"purple_light\": \"\",\r\n      \"blue_light\": \"\",\r\n      \"sky_light\": \"\",\r\n      \"lime_light\": \"\",\r\n      \"pink_light\": \"\",\r\n      \"black_light\": \"\"\r\n    }\r\n  },\r\n  \"action\": {\r\n    \"id\": \"63e28add8b06d26bf065c16a\",\r\n    \"idMemberCreator\": \"63d1239e857afaa8b003c633\",\r\n    \"data\": {\r\n      \"card\": {\r\n        \"idList\": \"63d128787441d05619f44dc6\",\r\n        \"id\": \"63d387e699609c7f4bf0e0ce\",\r\n        \"name\": \"new name\",\r\n        \"idShort\": 1,\r\n        \"shortLink\": \"Yd9ScoJS\"\r\n      },\r\n      \"old\": { \"idList\": \"63d9942f12260e27b257d067\" },\r\n      \"board\": {\r\n        \"id\": \"63d128787441d05619f44dbe\",\r\n        \"name\": \"New Name\",\r\n        \"shortLink\": \"SCPjg8ON\"\r\n      },\r\n      \"listBefore\": {\r\n        \"id\": \"63d9942f12260e27b257d067\",\r\n        \"name\": \"My New List!!!\"\r\n      },\r\n      \"listAfter\": {\r\n        \"id\": \"63d128787441d05619f44dc6\",\r\n        \"name\": \"Doing\"\r\n      }\r\n    },\r\n    \"appCreator\": null,\r\n    \"type\": \"updateCard\",\r\n    \"date\": \"2023-02-07T17:31:09.942Z\",\r\n    \"limits\": null,\r\n    \"display\": {\r\n      \"translationKey\": \"action_move_card_from_list_to_list\",\r\n      \"entities\": {\r\n        \"card\": {\r\n          \"type\": \"card\",\r\n          \"idList\": \"63d128787441d05619f44dc6\",\r\n          \"id\": \"63d387e699609c7f4bf0e0ce\",\r\n          \"shortLink\": \"Yd9ScoJS\",\r\n          \"text\": \"new name\"\r\n        },\r\n        \"listBefore\": {\r\n          \"type\": \"list\",\r\n          \"id\": \"63d9942f12260e27b257d067\",\r\n          \"text\": \"My New List!!!\"\r\n        },\r\n        \"listAfter\": {\r\n          \"type\": \"list\",\r\n          \"id\": \"63d128787441d05619f44dc6\",\r\n          \"text\": \"Doing\"\r\n        },\r\n        \"memberCreator\": {\r\n          \"type\": \"member\",\r\n          \"id\": \"63d1239e857afaa8b003c633\",\r\n          \"username\": \"rasmus58348007\",\r\n          \"text\": \"Rasmus\"\r\n        }\r\n      }\r\n    },\r\n    \"memberCreator\": {\r\n      \"id\": \"63d1239e857afaa8b003c633\",\r\n      \"activityBlocked\": false,\r\n      \"avatarHash\": \"f07a53f609386b8f57a767bceb8a8a26\",\r\n      \"avatarUrl\": \"https://trello-members.s3.amazonaws.com/63d1239e857afaa8b003c633/f07a53f609386b8f57a767bceb8a8a26\",\r\n      \"fullName\": \"Rasmus\",\r\n      \"idMemberReferrer\": null,\r\n      \"initials\": \"R\",\r\n      \"nonPublic\": {},\r\n      \"nonPublicAvailable\": true,\r\n      \"username\": \"rasmus58348007\"\r\n    }\r\n  }\r\n}";

            //var deserializeAsync = JsonSerializer.Deserialize<WebhookBoardNotication>(json);
            await Task.CompletedTask;
        }


    }
}
