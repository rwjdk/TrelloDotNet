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
            //todo - add better system that also work in pipelines (added this for now)
            var config = new ConfigurationBuilder()
                .AddUserSecrets<TestBase>()
                .Build();

            var apiKey = config["TrelloApiKey"];
            var token = config["TrelloToken"];
            return new TrelloClient(apiKey, token);
        }
        catch (Exception)
        {
            throw new Exception("In order to run Unit-tests you need to add a user secrets 'TrelloApiKey' and 'TrelloToken' (both strings). See more here: https://bartwullems.blogspot.com/2022/06/using-secrets-in-your-unit-tests.html and here: https://itbackyard.com/how-to-manage-secrets-in-net-locally-and-on-github/");
        }
    }

    protected void AssertTimeIsNow(DateTimeOffset? objectCreationTime)
    {
        Assert.True(objectCreationTime < DateTimeOffset.Now && objectCreationTime > DateTimeOffset.Now.AddMinutes(-1));
    }

    protected void WaitToAvoidRateLimits(int waitSeconds = 1)
    {
        Thread.Sleep(waitSeconds * 1000);
    }
}