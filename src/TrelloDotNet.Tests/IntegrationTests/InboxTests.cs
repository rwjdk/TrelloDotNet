using TrelloDotNet.Model;
using TrelloDotNet.Model.Options.AddCardToInboxOptions;
using TrelloDotNet.Model.Options.GetInboxCardOptions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class InboxTests : TestBase
{
    [Fact]
    public async Task GetInbox()
    {
        TokenMemberInbox memberInbox = await TrelloClient.GetTokenMemberInboxAsync();
        if (memberInbox != null) //Needed for now as it is a beta feature
        {
            Assert.NotNull(memberInbox.ListId);
            Assert.NotNull(memberInbox.BoardId);
            Assert.NotNull(memberInbox.OrganizationId);
        }
    }

    [Fact]
    public async Task AddCardToInbox()
    {
        TokenMemberInbox memberInbox = await TrelloClient.GetTokenMemberInboxAsync();

        if (memberInbox != null) //Needed for now as it is a beta feature
        {
            Card? card = null;
            try
            {
                const string name = "Hello";
                card = await TrelloClient.AddCardToInboxAsync(new AddCardToInboxOptions
                {
                    Name = name
                });
                Assert.Equal(name, card.Name);
            }
            finally
            {
                if (card != null)
                {
                    await TrelloClient.DeleteCardAsync(card.Id);
                }
            }
        }
    }

    [Fact]
    public async Task GetCardsToInbox()
    {
        TokenMemberInbox memberInbox = await TrelloClient.GetTokenMemberInboxAsync();
        if (memberInbox != null) //Needed for now as it is a beta feature
        {
            var cards = await TrelloClient.GetCardsInInboxAsync();
            Assert.NotNull(cards);
            cards = await TrelloClient.GetCardsInInboxAsync(new GetInboxCardOptions
            {
                Limit = 1
            });
            Assert.NotNull(cards);
        }
    }
}