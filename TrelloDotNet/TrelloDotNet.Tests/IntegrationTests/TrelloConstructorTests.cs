namespace TrelloDotNet.Tests.IntegrationTests;

[Collection("Integration Tests")]
public class TrelloConstructorTests
{
    [Fact]
    public void NoApiKeyThrowException()
    {
        Assert.Throws<ArgumentException>(() => new TrelloClient(null, "2"));
    }

    [Fact]
    public void NoTokenThrowException()
    {
        Assert.Throws<ArgumentException>(() => new TrelloClient("1", null));
    }

    [Fact]
    public void OptionWithOwnHttpClient()
    {
#pragma warning disable IDE0059
        var unused = new TrelloClient("1", "2", null, new HttpClient());
#pragma warning restore IDE0059

    }
}