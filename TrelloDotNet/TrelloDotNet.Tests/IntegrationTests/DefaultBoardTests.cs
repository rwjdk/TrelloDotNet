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

            //Member Tests
            var members = await TrelloClient.GetMembersOfBoardAsync(BoardId);
            Assert.Single(members);
            var member = await TrelloClient.GetMemberAsync(members.First().Id);
            Assert.Equal(member.FullName, members.First().FullName);
            Assert.Equal(member.Username, members.First().Username);

            //Default Lists
            var lists = await TrelloClient.GetListsOnBoardAsync(BoardId);
            var todoList = lists.FirstOrDefault(x => x.Name == "To Do");
            var doingList = lists.FirstOrDefault(x => x.Name == "Doing");
            var doneList = lists.FirstOrDefault(x => x.Name == "Done");
            Assert.NotNull(todoList);
            Assert.NotNull(doingList);
            Assert.NotNull(doneList);

            //No closed lists
            var listsFiltered = await TrelloClient.GetListsOnBoardFilteredAsync(BoardId, ListFilter.Closed);
            Assert.Empty(listsFiltered);

            //No Cards on the lists
            Assert.Empty(await TrelloClient.GetCardsInListAsync(todoList.Id));
            Assert.Empty(await TrelloClient.GetCardsInListAsync(doingList.Id));
            Assert.Empty(await TrelloClient.GetCardsInListAsync(doneList.Id));

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

            //Checklist Defaults
            var checklists = await TrelloClient.GetChecklistsOnBoardAsync(BoardId);
            Assert.Empty(checklists);

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

            //Raw JSON
            var rawGet = await TrelloClient.GetAsync($"boards/{BoardId}");
            Assert.NotNull(rawGet);

            //Raw
            var rawGetBoard = await TrelloClient.GetAsync<Board>($"boards/{BoardId}");
            Assert.Equal(BoardId, rawGetBoard.Id);

            var closedBoard = await TrelloClient.CloseBoardAsync(BoardId);
            Assert.True(closedBoard.Closed);

            var reopenedBoard = await TrelloClient.ReOpenBoardAsync(BoardId);
            Assert.False(reopenedBoard.Closed);

            var tokenInformation = await TrelloClient.GetTokenInformationAsync();
            Assert.NotNull(tokenInformation);

            var tokenMember = await TrelloClient.GetTokenMemberAsync();
            Assert.NotNull(tokenMember);
        }
        finally
        {
            await DeleteBoard();
        }
    }
}