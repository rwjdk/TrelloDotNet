using Microsoft.Extensions.Configuration;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests;

public abstract class TestBase
{
    protected TrelloClient TrelloClient;
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

            var apiKey = config["TrelloApiKey"];
            var token = config["TrelloToken"];
            var trelloClientOptions = new TrelloClientOptions(includeCustomFieldsInCardGetMethods: true);
            return new TrelloClient(apiKey, token, trelloClientOptions);
        }
        catch (Exception)
        {
            throw new Exception("In order to run Unit-tests you need to add a user secrets 'TrelloApiKey' and 'TrelloToken' (both strings). See more here: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows#manage-user-secrets-with-visual-studio");
        }
    }

    protected void AssertTimeIsNow(DateTimeOffset? objectCreationTime)
    {
        var beforeNow = objectCreationTime < DateTimeOffset.UtcNow.AddMinutes(1);
        var afterAMinuteAgo = objectCreationTime > DateTimeOffset.UtcNow.AddMinutes(-1);
        Assert.True(beforeNow && afterAMinuteAgo);
    }

    protected void WaitToAvoidRateLimits(int waitSeconds = 1)
    {
        Thread.Sleep(waitSeconds * 1000);
    }
}