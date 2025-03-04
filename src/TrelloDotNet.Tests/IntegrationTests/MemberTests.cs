using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.GetMemberOptions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class MemberTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly Board _board = fixture.Board!;
    private readonly Organization _organization = fixture.Organization!;

    [Fact]
    public async Task GetTokenMember()
    {
        Member member = await TrelloClient.GetTokenMemberAsync(new GetMemberOptions
        {
            MemberFields = new MemberFields(MemberFieldsType.FullName)
        });
        Assert.NotNull(member);
        Assert.NotEmpty(member.Id);
        Assert.NotEmpty(member.FullName);
        Assert.Null(member.Username);
    }

    [Fact]
    public async Task GetCardsForMember()
    {
        Member member = await TrelloClient.GetTokenMemberAsync();
        (List list, Card card) = await AddDummyCardAndList(_board.Id, "GetCardsForMember");
        await TrelloClient.AddMembersToCardAsync(card.Id, member.Id);
        var cardForMember = await TrelloClient.GetCardsForMemberAsync(member.Id);
        Assert.Contains(cardForMember, x => x.Id == card.Id && x.ListId == list.Id);
    }

    [Fact]
    public async Task GetBoardsForMember()
    {
        Member member = await TrelloClient.GetTokenMemberAsync();
        var boards = await TrelloClient.GetBoardsForMemberAsync(member.Id);
        Assert.Contains(boards, x => x.Id == _board.Id);
    }

    [Fact]
    public async Task GetOrganizationsForMember()
    {
        Member member = await TrelloClient.GetTokenMemberAsync();
        var organizations = await TrelloClient.GetOrganizationsForMemberAsync(member.Id);
        Assert.Contains(organizations, x => x.Id == _organization.Id);
    }

    [Fact]
    public async Task AddRemoveChangeMemberOnBoard()
    {
        const string memberId = "64ce44c552fd41aa6937e866"; //Test_user rwj_test1@outlook.com
        await TrelloClient.AddMemberToBoardAsync(_board.Id, memberId, MembershipType.Normal);

        var members = await TrelloClient.GetMembersOfBoardAsync(_board.Id);
        Assert.Contains(members, x => x.Id == memberId);

        var memberships = await TrelloClient.GetMembershipsOfBoardAsync(_board.Id);
        Assert.Contains(memberships, x => x.MemberId == memberId && x.MemberType == MembershipType.Normal);
        Membership membership = memberships.Single(x => x.MemberId == memberId && x.MemberType == MembershipType.Normal);

        await TrelloClient.UpdateMembershipTypeOfMemberOnBoardAsync(_board.Id, membership.Id, MembershipType.Admin);

        var membershipsAfter = await TrelloClient.GetMembershipsOfBoardAsync(_board.Id);
        Assert.Contains(membershipsAfter, x => x.MemberId == memberId && x.MemberType == MembershipType.Admin);

        await TrelloClient.RemoveMemberFromBoardAsync(_board.Id, memberId);

        var membersAfter = await TrelloClient.GetMembersOfBoardAsync(_board.Id);
        Assert.True(membersAfter.All(x => x.Id != memberId));

        await TrelloClient.InviteMemberToBoardViaEmailAsync(_board.Id, "rwj_test1@outlook.com", MembershipType.Normal);

        var membersAfterInvite = await TrelloClient.GetMembersOfBoardAsync(_board.Id);
        Assert.Contains(membersAfterInvite, x => x.Id == memberId);
    }

    [Fact]
    public async Task GetMembersOfCard()
    {
        var list = await AddDummyList(_board.Id);
        var card = await AddDummyCardToList(list);
        var member = await TrelloClient.GetTokenMemberAsync();

        await TrelloClient.AddMembersToCardAsync(card.Id, member.Id);

        var membersOnCard = await TrelloClient.GetMembersOfCardAsync(card.Id);
        Assert.Contains(membersOnCard, x => x.Id == member.Id);

        // Test with options
        var membersWithOptions = await TrelloClient.GetMembersOfCardAsync(card.Id, new GetMemberOptions
        {
            MemberFields = new MemberFields(MemberFieldsType.FullName)
        });
        Assert.Contains(membersWithOptions, x => x.Id == member.Id);
    }

    [Fact]
    public async Task GetMembersWhoVotedOnCard()
    {
        var list = await AddDummyList(_board.Id);
        var card = await AddDummyCardToList(list);
        var member = await TrelloClient.GetTokenMemberAsync();

        var votingMembers = await TrelloClient.GetMembersWhoVotedOnCardAsync(card.Id);
        Assert.Empty(votingMembers);

        // Test with options
        var votingMembersWithOptions = await TrelloClient.GetMembersWhoVotedOnCardAsync(card.Id, new GetMemberOptions
        {
            MemberFields = new MemberFields(MemberFieldsType.FullName)
        });
        Assert.Empty(votingMembersWithOptions);
    }
}