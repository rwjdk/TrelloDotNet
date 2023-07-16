using System.Diagnostics;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

public class DefaultBoardTests : TestBaseWithNewBoard
{
    [Fact]
    public async Task BoardCrud()
    {
        try
        {
            await CreateNewBoard();
            //Check Create date
            AssertTimeIsNow(Board!.Created);

            Assert.False(Board.Closed);
            Assert.NotEmpty(Board.ShortUrl);
            Assert.NotEmpty(Board.Url);
            Assert.False(Board.Pinned);
            Assert.Null(Board.EnterpriseId);
            Assert.NotEmpty(Board.OrganizationId);

            await TestMembers();

            (List todoList, List doingList, List doneList) = await TestDefaultLists();

            await TestNoClosedLists();

            await TestDefaultCards(todoList, doingList, doneList);

            await TestBoardUpdate();

            await TestDefaultChecklists();

            await TestLabels();

            await TestRaw();

            await TestCloseAndReopen();

            await TestToken();

            await TestMembership();
        }
        finally
        {
            await DeleteBoard();
        }
    }

    private async Task TestMembers()
    {
        //Member Tests
        var members = await TrelloClient.GetMembersOfBoardAsync(BoardId);
        Assert.Single(members);
        var member = await TrelloClient.GetMemberAsync(members.First().Id);
        Assert.Equal(member.FullName, members.First().FullName);
        Assert.Equal(member.Username, members.First().Username);
    }

    private async Task<(List todoList, List doingList, List doneList)> TestDefaultLists()
    {
        //Default Lists
        var lists = await TrelloClient.GetListsOnBoardAsync(BoardId);
        var todoList = lists.FirstOrDefault(x => x.Name == "To Do");
        var doingList = lists.FirstOrDefault(x => x.Name == "Doing");
        var doneList = lists.FirstOrDefault(x => x.Name == "Done");
        Assert.NotNull(todoList);
        Assert.NotNull(doingList);
        Assert.NotNull(doneList);
        return (todoList, doingList, doneList);
    }

    private async Task TestNoClosedLists()
    {
        //No closed lists
        var listsFiltered = await TrelloClient.GetListsOnBoardFilteredAsync(BoardId, ListFilter.Closed);
        Assert.Empty(listsFiltered);
    }

    private async Task TestDefaultCards(List todoList, List doingList, List doneList)
    {
        //No Cards on the lists
        Assert.Empty(await TrelloClient.GetCardsInListAsync(todoList.Id));
        Assert.Empty(await TrelloClient.GetCardsInListAsync(doingList.Id));
        Assert.Empty(await TrelloClient.GetCardsInListAsync(doneList.Id));
    }

    private async Task TestBoardUpdate()
    {
        //Board Update
        Debug.Assert(Board != null, nameof(Board) + " != null");
        Board.Name = BoardName + "X";
        Board.Description = BoardDescription + "X";
        var updatedBoard = await TrelloClient.UpdateBoardAsync(Board);
        var getBoard = await TrelloClient.GetBoardAsync(BoardId);
        Assert.EndsWith("X", updatedBoard.Name);
        Assert.EndsWith("X", updatedBoard.Description);
        Assert.EndsWith("X", getBoard.Name);
        Assert.EndsWith("X", getBoard.Description);
        Assert.Equal(updatedBoard.Description, getBoard.Description);
        Assert.Equal(updatedBoard.Description, getBoard.Description);
    }

    private async Task TestDefaultChecklists()
    {
        //Checklist Defaults
        var checklists = await TrelloClient.GetChecklistsOnBoardAsync(BoardId);
        Assert.Empty(checklists);
    }

    private async Task TestLabels()
    {
        //Label Defaults
        var labels = await TrelloClient.GetLabelsOfBoardAsync(BoardId);
        Assert.Equal(6, labels.Count);
        foreach (var label in labels)
        {
            Assert.NotEmpty(label.Id);
            Assert.NotEmpty(label.BoardId);
            Assert.Empty(label.Name);
            AssertTimeIsNow(label.Created);
        }

        Label addedLabel = await TrelloClient.AddLabelAsync(new Label(BoardId, "MyLabel", "red"));
        Assert.NotNull(addedLabel.Id);
        Assert.Equal("red", addedLabel.Color);

        Label updateLabel = labels[0];
        updateLabel.Name = "Hello world";
        updateLabel.Color = "red";
        await TrelloClient.UpdateLabelAsync(updateLabel);

        await TrelloClient.DeleteLabelAsync(labels[1].Id);
        await TrelloClient.DeleteLabelAsync(labels[2].Id);

        var labelsAfterAddUpdateAndRemove = await TrelloClient.GetLabelsOfBoardAsync(BoardId);
        Assert.Equal(5, labelsAfterAddUpdateAndRemove.Count);
        Assert.Equal(3, labelsAfterAddUpdateAndRemove.Count(x=> x.Color == "red"));
    }

    private async Task TestRaw()
    {
        //Raw JSON
        var rawGet = await TrelloClient.GetAsync($"boards/{BoardId}");
        Assert.NotNull(rawGet);

        //Raw
        var rawGetBoard = await TrelloClient.GetAsync<Board>($"boards/{BoardId}");
        Assert.Equal(BoardId, rawGetBoard.Id);
    }

    private async Task TestCloseAndReopen()
    {
        var closedBoard = await TrelloClient.CloseBoardAsync(BoardId);
        Assert.True(closedBoard.Closed);

        var reopenedBoard = await TrelloClient.ReOpenBoardAsync(BoardId);
        Assert.False(reopenedBoard.Closed);
    }

    private async Task TestToken()
    {
        var tokenInformation = await TrelloClient.GetTokenInformationAsync();
        Assert.NotNull(tokenInformation);
        Assert.NotNull(tokenInformation.Created);
        Assert.Null(tokenInformation.Expires);
        Assert.NotNull(tokenInformation.Id);
        Assert.NotNull(tokenInformation.Identifier);
        Assert.NotNull(tokenInformation.MemberId);
        Assert.NotNull(tokenInformation.Permissions);
        Assert.NotNull(tokenInformation.Permissions[0].ModelId);
        Assert.NotNull(tokenInformation.Permissions[0].ModelType);
        Assert.True(tokenInformation.Permissions[0].Read);
        Assert.True(tokenInformation.Permissions[0].Write);

        var tokenMember = await TrelloClient.GetTokenMemberAsync();
        Assert.NotNull(tokenMember);
    }

    private async Task TestMembership()
    {
        var tokenMember = await TrelloClient.GetTokenMemberAsync();
        List<Membership> memberships = await TrelloClient.GetMembershipsOfBoardAsync(BoardId);
        Assert.Single(memberships);
        Membership membership = memberships.Single(x => x.MemberId == tokenMember.Id);
        Assert.Equal(MembershipType.Admin, membership.MemberType);
    }
}