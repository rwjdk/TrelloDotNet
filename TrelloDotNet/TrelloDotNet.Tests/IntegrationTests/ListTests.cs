using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

public class ListTests : TestBaseWithNewBoard
{
    [Fact]
    public async Task ListCrud()
    {
        try
        {
            WaitToAvoidRateLimits();
            await CreateNewBoard();
            var listsBefore = await TrelloClient.GetListsOnBoardAsync(BoardId);

            //Add List Test
            WaitToAvoidRateLimits();
            var newListName = Guid.NewGuid().ToString();
            var addedList = await TrelloClient.AddListAsync(new List(newListName, BoardId));
            var listId = addedList.Id;
            AssertTimeIsNow(addedList.Created);
            Assert.False(addedList.Closed);
            Assert.Equal(listId, addedList.Id);
            Assert.Equal(newListName, addedList.Name);
            Assert.False(addedList.Subscribed);
            Assert.Null(addedList.SoftLimit);

            //There are now one more list
            WaitToAvoidRateLimits();
            var listsAfter = await TrelloClient.GetListsOnBoardAsync(BoardId);
            Assert.Equal(listsBefore.Count + 1, listsAfter.Count);

            //New list is there
            var newList = listsAfter.FirstOrDefault(x => x.Name == newListName);
            Assert.NotNull(newList);

            //Update List
            WaitToAvoidRateLimits();
            var updatedName = Guid.NewGuid().ToString();
            newList.Name = updatedName;
            var newListUpdated = await TrelloClient.UpdateListAsync(newList);
            var getnewListViaId = await TrelloClient.GetListAsync(listId);
            Assert.Equal(updatedName, getnewListViaId.Name);
            Assert.Equal(newListUpdated.Name, getnewListViaId.Name);

            //Archive List
            WaitToAvoidRateLimits();
            var archivedList = await TrelloClient.ArchiveListAsync(newListUpdated.Id);
            var listsAfterArchive = await TrelloClient.GetListsOnBoardAsync(BoardId);
            Assert.True(archivedList.Closed);
            Assert.Equal(listsBefore.Count, listsAfterArchive.Count);
            Assert.True(listsAfterArchive.TrueForAll(x => x.Id != listId));
            Assert.True(listsAfterArchive.TrueForAll(x => x.Name != updatedName));

            //Check that there are now one closed list
            WaitToAvoidRateLimits();
            var closedLists = await TrelloClient.GetListsOnBoardFilteredAsync(BoardId, ListFilter.Closed);
            Assert.Single(closedLists);
            Assert.Equal(listId, closedLists.First().Id);
            Assert.Equal(updatedName, closedLists.First().Name);

            //Reopen
            WaitToAvoidRateLimits();
            var reopenedList = await TrelloClient.ReOpenListAsync(listId);
            Assert.False(reopenedList.Closed);
            Assert.Equal(listId, reopenedList.Id);
            Assert.Equal(updatedName, reopenedList.Name);

            WaitToAvoidRateLimits();
            var listsAfterReopen = await TrelloClient.GetListsOnBoardAsync(BoardId);
            Assert.Equal(listsAfterArchive.Count + 1, listsAfterReopen.Count);

            //Test: ArchiveAllCardsInListAsync
            //Add some cards so we can test Archive All Cards In List
            WaitToAvoidRateLimits();
            await TrelloClient.AddCardAsync(new Card(reopenedList.Id, "C1"));
            WaitToAvoidRateLimits();
            await TrelloClient.AddCardAsync(new Card(reopenedList.Id, "C2"));
            WaitToAvoidRateLimits();
            await TrelloClient.AddCardAsync(new Card(reopenedList.Id, "C3"));
            WaitToAvoidRateLimits();
            var cardsOnListAfterAdd = await TrelloClient.GetCardsInListAsync(reopenedList.Id);
            Assert.Equal(3, cardsOnListAfterAdd.Count);
            WaitToAvoidRateLimits();
            await TrelloClient.ArchiveAllCardsInListAsync(reopenedList.Id);
            var cardsOnListAfterArchive = await TrelloClient.GetCardsInListAsync(reopenedList.Id);
            Assert.Empty(cardsOnListAfterArchive);

            //Test: Move All Cards In List
            //Add some cards so we can test Archive All Cards In List
            WaitToAvoidRateLimits();
            await TrelloClient.AddCardAsync(new Card(reopenedList.Id, "C1"));
            WaitToAvoidRateLimits();
            await TrelloClient.AddCardAsync(new Card(reopenedList.Id, "C2"));
            WaitToAvoidRateLimits();
            await TrelloClient.AddCardAsync(new Card(reopenedList.Id, "C3"));
            WaitToAvoidRateLimits();
            //Add new list to move cards to
            WaitToAvoidRateLimits();
            var listToMoveTo = await TrelloClient.AddListAsync(new List("List to move to", BoardId));
            WaitToAvoidRateLimits();
            await TrelloClient.MoveAllCardsInListAsync(reopenedList.Id, listToMoveTo.Id);
            WaitToAvoidRateLimits();
            var cardsOnListAfterMove = await TrelloClient.GetCardsInListAsync(listToMoveTo.Id);
            Assert.Equal(3, cardsOnListAfterMove.Count);
        }
        finally
        {
            await DeleteBoard();
        }
    }
}