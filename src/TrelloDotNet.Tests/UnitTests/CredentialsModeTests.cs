using System.Net;
using System.Text;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.UnitTests;

public class CredentialsModeTests
{
    [Fact]
    public async Task GetAsync_SendsCredentialsInAuthorizationHeader_WhenHeaderModeIsSelected()
    {
        var handler = new RecordingHandler();
        using var httpClient = new HttpClient(handler);
        var client = new TrelloClient("key", "token", new TrelloClientOptions
        {
            SendCredentialsMode = SendCredentialsMode.Header
        }, httpClient);

        _ = await client.GetAsync("members/me", cancellationToken: TestContext.Current.CancellationToken);

        var request = Assert.Single(handler.Requests);
        Assert.Equal(string.Empty, request.Query);
        Assert.Equal("OAuth", request.AuthorizationScheme);
        Assert.Contains("oauth_consumer_key=\"key\"", request.AuthorizationParameter, StringComparison.Ordinal);
        Assert.Contains("oauth_token=\"token\"", request.AuthorizationParameter, StringComparison.Ordinal);
    }

    private sealed class RecordingHandler : HttpMessageHandler
    {
        public List<RecordedRequest> Requests { get; } = new();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requests.Add(new RecordedRequest(
                request.RequestUri?.Query ?? string.Empty,
                request.Headers.Authorization?.Scheme,
                request.Headers.Authorization?.Parameter));

            const string json = "{}";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            return Task.FromResult(response);
        }
    }

    private sealed record RecordedRequest(string Query, string? AuthorizationScheme, string? AuthorizationParameter);
}
