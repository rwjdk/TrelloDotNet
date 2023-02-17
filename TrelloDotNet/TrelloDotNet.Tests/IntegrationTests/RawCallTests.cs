using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

public class RawCallTests : TestBase
{
    [Fact]
    public async Task RawExceptionThrowsIncludeUrlButMaskCredentials()
    {
        try
        {
            TrelloClient.Options.ApiCallExceptionOption = ApiCallExceptionOption.IncludeUrlButMaskCredentials;
            await TrelloClient.GetAsync("xyz");
        }
        catch (TrelloApiException e)
        {
            //Empty
            Assert.Contains("XXXXXXXXXX", e.DataSentToTrello);
        }
    }

    [Fact]
    public async Task RawExceptionThrowsErrorWithoutUrl()
    {
        try
        {
            TrelloClient.Options.ApiCallExceptionOption = ApiCallExceptionOption.DoNotIncludeTheUrl;
            await TrelloClient.GetAsync("xyz");
        }
        catch (TrelloApiException e)
        {
            //Empty
            Assert.Equal("XXXXX", e.DataSentToTrello);
        }
    }

    [Fact]
    public async Task RawExceptionThrowsErrorWithUrlAndCredentials()
    {
        try
        {
            TrelloClient.Options.ApiCallExceptionOption = ApiCallExceptionOption.IncludeUrlAndCredentials;
            await TrelloClient.GetAsync("xyz");
        }
        catch (TrelloApiException e)
        {
            //Empty
            Assert.DoesNotContain("XXXXXXXXXX", e.DataSentToTrello);
        }
    }
}