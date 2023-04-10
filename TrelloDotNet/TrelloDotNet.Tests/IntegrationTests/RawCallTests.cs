using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

[Collection("Integration Tests")]
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

    [Fact]
    public async Task RawExceptionsThrowCorrectException()
    {
        await Assert.ThrowsAsync<TrelloApiException>(async () => await TrelloClient.GetAsync("xyz"));
        await Assert.ThrowsAsync<TrelloApiException>(async () => await TrelloClient.PostAsync("xyz"));
        await Assert.ThrowsAsync<TrelloApiException>(async () => await TrelloClient.PutAsync("xyz"));
        await Assert.ThrowsAsync<TrelloApiException>(async () => await TrelloClient.DeleteAsync("xyz"));
    }
}