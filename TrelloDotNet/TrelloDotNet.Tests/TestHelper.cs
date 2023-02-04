using Microsoft.Extensions.Configuration;

namespace TrelloDotNet.Tests;

public static class TestHelper
{
    public static TrelloClient GetClient()
    {
        try
        {
            //todo - add better system that also work in pipelines (added this for now)
            var config = new ConfigurationBuilder().AddJsonFile("client-secrets.json").Build();
            var apiKey = config["ApiKey"];
            var token = config["Token"];
            return new TrelloClient(apiKey, token);
        }
        catch (Exception)
        {
            throw new Exception("In order to run Unit-tests you need to add a client-secrets.json in the root of the Test-project, mark it as 'Copy if Newer' and add the following content: { \"ApiKey\": \"xyz\", \"Token\": \"xyz\" }");
        }
    }
}