using Microsoft.Extensions.Configuration;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.AddCardOptions;
using TrelloDotNet.Model.Options.GetBoardOptions;

namespace TrelloDotNet.Tests;

public abstract class TestBase
{
    public TrelloClient TrelloClient;

    protected TestBase()
    {
        TrelloClient = GetClient();
    }

    private TrelloClient GetClient()
    {
        try
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<TestBase>()
                .Build();

            List<TrelloClient> clients = [];
            var apiKey = config["TrelloApiKey"];
            var token = config["TrelloToken"];
            if (!string.IsNullOrWhiteSpace(apiKey) && !string.IsNullOrWhiteSpace(token))
            {
                var trelloClientOptions = new TrelloClientOptions
                {
                    MaxRetryCountForTokenLimitExceeded = 10,
                    DelayInSecondsToWaitInTokenLimitExceededRetry = 2
                };
                clients.Add(new TrelloClient(apiKey, token, trelloClientOptions));
            }

            for (int i = 1; i < 10; i++)
            {
                apiKey = config["TrelloApiKey" + (i + 1)];
                token = config["TrelloToken" + (i + 1)];
                if (!string.IsNullOrWhiteSpace(apiKey) && !string.IsNullOrWhiteSpace(token))
                {
                    var trelloClientOptions = new TrelloClientOptions
                    {
                        MaxRetryCountForTokenLimitExceeded = 10,
                        DelayInSecondsToWaitInTokenLimitExceededRetry = 2
                    };
                    clients.Add(new TrelloClient(apiKey, token, trelloClientOptions));
                }
            }

            return clients[Random.Shared.Next(0, clients.Count - 1)];
        }
        catch (Exception)
        {
            throw new Exception("In order to run Unit-tests you need to add a user secrets 'TrelloApiKey' and 'TrelloToken' (both strings). See more here: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows#manage-user-secrets-with-visual-studio");
        }
    }

    protected async Task<List> AddDummyList(string boardId, string? name = null)
    {
        return await TrelloClient.AddListAsync(new List(name ?? Guid.NewGuid().ToString(), boardId));
    }

    protected async Task<Card> AddDummyCard(string boardId, string? name = null)
    {
        return (await AddDummyCardAndList(boardId, name)).Card;
    }

    protected async Task<Card> AddDummyCardToList(List list, string? name = null, string? description = null, DateTimeOffset? start = null, DateTimeOffset? due = null, bool? dueComplete = null)
    {
        var addCardOptions = new AddCardOptions(list.Id, name ?? Guid.NewGuid().ToString(), description ?? string.Empty);
        if (start.HasValue)
        {
            addCardOptions.Start = start.Value;
        }

        if (due.HasValue)
        {
            addCardOptions.Due = due.Value;
        }

        if (dueComplete.HasValue)
        {
            addCardOptions.DueComplete = dueComplete.Value;
        }

        return await TrelloClient.AddCardAsync(addCardOptions);
    }

    protected async Task<(List List, Card Card)> AddDummyCardAndList(string boardId, string? name = null)
    {
        List list = await AddDummyList(boardId, name);
        Card card = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, name ?? Guid.NewGuid().ToString()));
        return (list, card);
    }

    public void AssertTimeIsNow(DateTimeOffset? objectCreationTime)
    {
        var beforeNow = objectCreationTime < DateTimeOffset.UtcNow.AddMinutes(1);
        var afterAMinuteAgo = objectCreationTime > DateTimeOffset.UtcNow.AddMinutes(-1);
        Assert.True(beforeNow && afterAMinuteAgo);
    }

    public async Task<Board?> GetSpecialPaidSubscriptionBoard()
    {
        var availableBoards = await TrelloClient.GetBoardsCurrentTokenCanAccessAsync(new GetBoardOptions
        {
            BoardFields = new BoardFields(BoardFieldsType.Name)
        });

        const string specialSetupBoardsForTheseTests = "67c765705dc85a158981d888";
        return availableBoards.FirstOrDefault(x => x.Id == specialSetupBoardsForTheseTests);
    }
}