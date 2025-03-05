using TrelloDotNet.Model;
using TrelloDotNet.Model.Options.GetCardOptions;
using TrelloDotNet.Model.Options.GetListOptions;

namespace TrelloDotNet.Tests.PaidPlanIntegrationTests;

public class VoteTests : TestBase
{
    [Fact]
    public async Task AddRemoveVotes()
    {
        var board = await GetSpecialPaidSubscriptionBoard();
        if (board == null)
        {
            return; //Special Test-board not available
        }

        const string listPrefix = "UnitTestVotes";
        await CleanUp(board, listPrefix);
        try
        {
            List listForVoteTests = await TrelloClient.AddListAsync(new List(listPrefix + Guid.NewGuid(), board.Id));
            Card card1 = await AddDummyCardToList(listForVoteTests, "Card 1");

            Member member = await TrelloClient.GetTokenMemberAsync();

            var votedCard = await TrelloClient.GetCardAsync(card1.Id, new GetCardOptions
            {
                IncludeMemberVotes = true
            });
            Assert.Empty(votedCard.MembersVotedIds);

            await TrelloClient.AddVoteToCardAsync(card1.Id, member.Id);

            votedCard = await TrelloClient.GetCardAsync(card1.Id, new GetCardOptions
            {
                IncludeMemberVotes = true
            });

            Assert.Single(votedCard.MembersVotedIds);
            Assert.Contains(votedCard.MembersVotedIds, x => x == member.Id);

            await TrelloClient.RemoveVoteFromCardAsync(card1.Id, member.Id);

            votedCard = await TrelloClient.GetCardAsync(card1.Id, new GetCardOptions
            {
                IncludeMemberVotes = true
            });
            Assert.Empty(votedCard.MembersVotedIds);
        }
        finally
        {
            await CleanUp(board, listPrefix);
        }
    }

    private async Task CleanUp(Board board, string listPrefix)
    {
        //Potential previous cleanup
        var lists = await TrelloClient.GetListsOnBoardAsync(board.Id, new GetListOptions
        {
            Filter = ListFilter.All
        });
        foreach (List list in lists.Where(x => x.Name.StartsWith(listPrefix)))
        {
            await TrelloClient.DeleteListAsync(list.Id);
        }
    }
}