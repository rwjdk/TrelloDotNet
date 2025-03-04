using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.AddCardOptions;
using TrelloDotNet.Model.Options.GetCardOptions;
using TrelloDotNet.Model.Options.GetListOptions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class ListTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly string _boardId = fixture.BoardId!;

    [Fact]
    public async Task AddList()
    {
        var name = Guid.NewGuid().ToString();
        var addList = await TrelloClient.AddListAsync(new List(name, _boardId));
        AssertTimeIsNow(addList.Created);
        Assert.False(addList.Closed);
        Assert.Equal(name, addList.Name);
        Assert.False(addList.Subscribed);
        Assert.Null(addList.SoftLimit);
        var listsAfter = await TrelloClient.GetListsOnBoardAsync(_boardId);
        var foundList = listsAfter.FirstOrDefault(x => x.Id == addList.Id);
        Assert.NotNull(foundList);
        Assert.Equal(name, foundList.Name);
    }

    [Fact]
    public async Task UpdateList()
    {
        var name = Guid.NewGuid().ToString();
        var addList = await TrelloClient.AddListAsync(new List(name, _boardId));
        var updatedName = Guid.NewGuid().ToString();
        addList.Name = updatedName;
        var updateList = await TrelloClient.UpdateListAsync(addList);
        var getList = await TrelloClient.GetListAsync(addList.Id);
        Assert.Equal(updatedName, getList.Name);
        Assert.Equal(updateList.Name, getList.Name);
    }

    [Fact]
    public async Task ArchiveAndReopenList()
    {
        var name = Guid.NewGuid().ToString();
        var addList = await TrelloClient.AddListAsync(new List(name, _boardId));

        //Archive
        var archivedList = await TrelloClient.ArchiveListAsync(addList.Id);
        Assert.True(archivedList.Closed);
        var listsAfter = await TrelloClient.GetListsOnBoardAsync(_boardId);
        Assert.True(listsAfter.TrueForAll(x => x.Id != addList.Id));
        Assert.True(listsAfter.TrueForAll(x => x.Name != name));

        //Check that there are a closed list
        var closedLists = await TrelloClient.GetListsOnBoardAsync(_boardId, new GetListOptions
        {
            Filter = ListFilter.Closed
        });
        List foundList = closedLists.Single(x => x.Id == addList.Id);
        Assert.Equal(addList.Name, foundList.Name);

        //Re-open
        var reopenedList = await TrelloClient.ReOpenListAsync(foundList.Id);
        Assert.False(reopenedList.Closed);
        Assert.Equal(addList.Id, reopenedList.Id);
        Assert.Equal(name, reopenedList.Name);

        var listsAfterReopen = await TrelloClient.GetListsOnBoardAsync(_boardId);
        Assert.Contains(listsAfterReopen, x => x.Id == reopenedList.Id);
        Assert.Contains(listsAfterReopen, x => x.Name == name);
    }

    [Fact]
    public async Task DeleteList()
    {
        var listsBefore = await TrelloClient.GetListsOnBoardAsync(_boardId, new GetListOptions
        {
            Filter = ListFilter.All
        });
        var name = Guid.NewGuid().ToString();
        var addList = await TrelloClient.AddListAsync(new List(name, _boardId));

        //Delete
        await TrelloClient.DeleteListAsync(addList.Id);
        var listsAfter = await TrelloClient.GetListsOnBoardAsync(_boardId, new GetListOptions
        {
            Filter = ListFilter.All
        });

        Assert.Equal(listsAfter.Count, listsBefore.Count);
        Assert.Contains(listsAfter, x => x.Name != name);
    }

    [Fact]
    public async Task ArchiveAllCardsInList()
    {
        var name = Guid.NewGuid().ToString();
        var addList = await TrelloClient.AddListAsync(new List(name, _boardId));
        //Add some cards so we can test Archive All Cards In List
        await TrelloClient.AddCardAsync(new AddCardOptions(addList.Id, "C1"));
        await TrelloClient.AddCardAsync(new AddCardOptions(addList.Id, "C2"));
        await TrelloClient.AddCardAsync(new AddCardOptions(addList.Id, "C3"));
        var cardsOnListAfterAdd = await TrelloClient.GetCardsInListAsync(addList.Id);
        Assert.Equal(3, cardsOnListAfterAdd.Count);
        await TrelloClient.ArchiveAllCardsInListAsync(addList.Id);
        var cardsOnListAfterArchive = await TrelloClient.GetCardsInListAsync(addList.Id);
        Assert.Empty(cardsOnListAfterArchive);
    }

    [Fact]
    public async Task MoveCardToList()
    {
        var sourceList = await TrelloClient.AddListAsync(new List("Source", _boardId));
        var targetList = await TrelloClient.AddListAsync(new List("Target", _boardId));

        Card card1 = await TrelloClient.AddCardAsync(new AddCardOptions(sourceList.Id, "C1"));
        Card card2 = await TrelloClient.AddCardAsync(new AddCardOptions(sourceList.Id, "C2"));
        Card card3 = await TrelloClient.AddCardAsync(new AddCardOptions(sourceList.Id, "C3"));

        await TrelloClient.MoveCardToListAsync(card2.Id, targetList.Id);

        var sourceAfter = await TrelloClient.GetCardsInListAsync(sourceList.Id);
        Assert.Equal(2, sourceAfter.Count);
        Assert.Contains(sourceAfter, x => x.Id == card1.Id);
        Assert.Contains(sourceAfter, x => x.Id == card3.Id);


        var targetAfter = await TrelloClient.GetCardsInListAsync(targetList.Id);
        Assert.Single(targetAfter);
        Assert.Contains(targetAfter, x => x.Id == card2.Id);
    }

    [Fact]
    public async Task MoveAllCardsInList()
    {
        var name = Guid.NewGuid().ToString();
        var addList = await TrelloClient.AddListAsync(new List(name, _boardId));
        //Add some cards so we can test Move All Cards In List
        await TrelloClient.AddCardAsync(new AddCardOptions(addList.Id, "C1"));
        await TrelloClient.AddCardAsync(new AddCardOptions(addList.Id, "C2"));
        await TrelloClient.AddCardAsync(new AddCardOptions(addList.Id, "C3"));

        //Add new list to move cards to
        var listToMoveTo = await TrelloClient.AddListAsync(new List("List to move to", _boardId));
        await TrelloClient.MoveAllCardsInListAsync(addList.Id, listToMoveTo.Id);
        var cardsOnListAfterMove = await TrelloClient.GetCardsInListAsync(listToMoveTo.Id);
        Assert.Equal(3, cardsOnListAfterMove.Count);
    }

    [Fact]
    public async Task GetCardsInList()
    {
        List list = await AddDummyList(_boardId);

        await AddDummyCardToList(list, "Card 1");
        await AddDummyCardToList(list, "Card 2");
        await AddDummyCardToList(list, "Card 3");

        var cards = await TrelloClient.GetCardsInListAsync(list.Id, new GetCardOptions
        {
            IncludeBoard = true,
            IncludeList = true,
            ActionsTypes = ActionTypesToInclude.All,
            Limit = 10,
            CardFields = new CardFields(CardFieldsType.Name)
        });

        Assert.Equal(3, cards.Count);
    }
}