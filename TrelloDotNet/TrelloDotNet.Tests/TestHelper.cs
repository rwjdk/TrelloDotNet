using Microsoft.Extensions.Configuration;

namespace TrelloDotNet.Tests;

public class TestHelper
{
    public TrelloClient GetClient()
    {
        try
        {
            //todo - add better system that also work in pipelines (added this for now)
            var config = new ConfigurationBuilder()
                .AddUserSecrets<TestHelper>()
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
}