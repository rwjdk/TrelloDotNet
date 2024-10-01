using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

public class MemberTests : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly Board _board;
    private readonly Organization _organization;

    public MemberTests(TestFixtureWithNewBoard fixture)
    {
        _board = fixture.Board!;
        _organization = fixture.Organization!;
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
        Assert.True(membersAfter.All(x=> x.Id != memberId));

        await TrelloClient.InviteMemberToBoardViaEmailAsync(_board.Id, "rwj_test1@outlook.com", MembershipType.Normal);

        var membersAfterInvite = await TrelloClient.GetMembersOfBoardAsync(_board.Id);
        Assert.Contains(membersAfterInvite, x => x.Id == memberId);
    }


}