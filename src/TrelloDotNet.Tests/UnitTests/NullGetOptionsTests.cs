using System.Net;
using System.Text;
using TrelloDotNet.Model.Options.GetActionsOptions;
using TrelloDotNet.Model.Options.GetBoardOptions;
using TrelloDotNet.Model.Options.GetCardOptions;
using TrelloDotNet.Model.Options.GetLabelOptions;
using TrelloDotNet.Model.Options.GetListOptions;
using TrelloDotNet.Model.Options.GetMemberOptions;
using TrelloDotNet.Model.Options.GetOrganizationOptions;

namespace TrelloDotNet.Tests.UnitTests;

public class NullGetOptionsTests
{
    [Fact]
    public async Task GetMethodsAllowNullOptionsWithoutAddingQueryParameters()
    {
        RecordingHandler handler = new RecordingHandler();
        using HttpClient httpClient = new HttpClient(handler);
        TrelloClient client = new TrelloClient("key", "token", httpClient: httpClient);
        CancellationToken cancellationToken = TestContext.Current.CancellationToken;

        await client.GetBoardAsync("boardId", (GetBoardOptions)null!, cancellationToken);
        await client.GetBoardsForMemberAsync("memberId", (GetBoardOptions)null!, cancellationToken);
        await client.GetBoardsInOrganizationAsync("orgId", (GetBoardOptions)null!, cancellationToken);

        await client.GetCardAsync("cardId", (GetCardOptions)null!, cancellationToken);
        await client.GetCardsOnBoardAsync("boardId", (GetCardOptions)null!, cancellationToken);
        await client.GetCardsInListAsync("listId", (GetCardOptions)null!, cancellationToken);
        await client.GetCardsForMemberAsync("memberId", (GetCardOptions)null!, cancellationToken);

        await client.GetListAsync("listId", (GetListOptions)null!, cancellationToken);
        await client.GetListsOnBoardAsync("boardId", (GetListOptions)null!, cancellationToken);

        await client.GetLabelsOfBoardAsync("boardId", (GetLabelOptions)null!, cancellationToken);

        await client.GetMembersOfBoardAsync("boardId", (GetMemberOptions)null!, cancellationToken);
        await client.GetMembersOfCardAsync("cardId", (GetMemberOptions)null!, cancellationToken);
        await client.GetMembersWhoVotedOnCardAsync("cardId", (GetMemberOptions)null!, cancellationToken);
        await client.GetTokenMemberAsync((GetMemberOptions)null!, cancellationToken);
        await client.GetMembersOfOrganizationAsync("orgId", (GetMemberOptions)null!, cancellationToken);

        await client.GetOrganizationAsync("orgId", (GetOrganizationOptions)null!, cancellationToken);
        await client.GetOrganizationsForMemberAsync("memberId", (GetOrganizationOptions)null!, cancellationToken);

        await client.GetActionsOfBoardAsync("boardId", (GetActionsOptions)null!, cancellationToken);
        await client.GetActionsOnCardAsync("cardId", (GetActionsOptions)null!, cancellationToken);
        await client.GetActionsForListAsync("listId", (GetActionsOptions)null!, cancellationToken);
        await client.GetActionsForMemberAsync("memberId", (GetActionsOptions)null!, cancellationToken);
        await client.GetActionsForOrganizationsAsync("orgId", (GetActionsOptions)null!, cancellationToken);

        Assert.NotEmpty(handler.RequestedUris);
        Assert.All(handler.RequestedUris, uri => Assert.Equal("?key=key&token=token", uri.Query));
    }

    private sealed class RecordingHandler : HttpMessageHandler
    {
        public List<Uri> RequestedUris { get; } = new();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri != null)
            {
                RequestedUris.Add(request.RequestUri);
            }

            string json = GetResponseJson(request.RequestUri);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            return Task.FromResult(response);
        }

        private static string GetResponseJson(Uri? uri)
        {
            if (uri == null)
            {
                return "{}";
            }

            string path = uri.AbsolutePath;

            if (path.EndsWith("/actions", StringComparison.OrdinalIgnoreCase))
            {
                return "[]";
            }

            if (path.EndsWith("/labels", StringComparison.OrdinalIgnoreCase))
            {
                return "[]";
            }

            if (path.EndsWith("/lists", StringComparison.OrdinalIgnoreCase))
            {
                return "[]";
            }

            if (path.EndsWith("/cards", StringComparison.OrdinalIgnoreCase) || path.EndsWith("/cards/", StringComparison.OrdinalIgnoreCase))
            {
                return "[]";
            }

            if (path.EndsWith("/boards", StringComparison.OrdinalIgnoreCase) || path.EndsWith("/boards/", StringComparison.OrdinalIgnoreCase))
            {
                return "[]";
            }

            if (path.EndsWith("/organizations", StringComparison.OrdinalIgnoreCase))
            {
                return "[]";
            }

            if (path.EndsWith("/members/", StringComparison.OrdinalIgnoreCase) || path.EndsWith("/membersVoted", StringComparison.OrdinalIgnoreCase))
            {
                return "[]";
            }

            return "{}";
        }
    }
}
