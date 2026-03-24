using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.GetCardOptions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class OrderCardsTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly Board _board = fixture.Board!;
#pragma warning disable xUnit2013 // Do not use equality check to check for collection size.
    [Fact]
    public async Task OrderCards()
    {
        List list1 = await AddDummyList(_board.Id, "List 1");

        Card card1 = await AddDummyCardToList(list1, "B", start: DateTimeOffset.UtcNow, due: DateTimeOffset.UtcNow.AddDays(3));
        await Task.Delay(1000, TestCancellationToken);
        Card card2 = await AddDummyCardToList(list1, "X", start: DateTimeOffset.UtcNow.AddDays(1), due: DateTimeOffset.UtcNow.AddDays(2));
        await Task.Delay(1000, TestCancellationToken);
        Card card3 = await AddDummyCardToList(list1, "A", start: DateTimeOffset.UtcNow.AddDays(-20), due: DateTimeOffset.UtcNow.AddDays(1));

        // ReSharper disable once JoinDeclarationAndInitializer
        List<Card> cards;

        //********************************************************************************************************

        CardFields cardFields = new CardFields(CardFieldsType.Name);
        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            OrderBy = CardsOrderBy.CreateDateAsc
        }, cancellationToken: TestCancellationToken);

        Assert.Equal(card1.Id, cards[0].Id);
        Assert.Equal(card2.Id, cards[1].Id);
        Assert.Equal(card3.Id, cards[2].Id);

        //********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            OrderBy = CardsOrderBy.CreateDateDesc
        }, cancellationToken: TestCancellationToken);

        Assert.Equal(card3.Id, cards[0].Id);
        Assert.Equal(card2.Id, cards[1].Id);
        Assert.Equal(card1.Id, cards[2].Id);

        //********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            OrderBy = CardsOrderBy.NameAsc
        }, cancellationToken: TestCancellationToken);

        Assert.Equal(card3.Id, cards[0].Id);
        Assert.Equal(card1.Id, cards[1].Id);
        Assert.Equal(card2.Id, cards[2].Id);

        //********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            OrderBy = CardsOrderBy.NameDesc
        }, cancellationToken: TestCancellationToken);

        Assert.Equal(card2.Id, cards[0].Id);
        Assert.Equal(card1.Id, cards[1].Id);
        Assert.Equal(card3.Id, cards[2].Id);

        //********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            OrderBy = CardsOrderBy.DueDateAsc
        }, cancellationToken: TestCancellationToken);

        Assert.Equal(card3.Id, cards[0].Id);
        Assert.Equal(card2.Id, cards[1].Id);
        Assert.Equal(card1.Id, cards[2].Id);

        //********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            OrderBy = CardsOrderBy.DueDateDesc
        }, cancellationToken: TestCancellationToken);

        Assert.Equal(card1.Id, cards[0].Id);
        Assert.Equal(card2.Id, cards[1].Id);
        Assert.Equal(card3.Id, cards[2].Id);

        //********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            OrderBy = CardsOrderBy.StartDateAsc
        }, cancellationToken: TestCancellationToken);

        Assert.Equal(card3.Id, cards[0].Id);
        Assert.Equal(card1.Id, cards[1].Id);
        Assert.Equal(card2.Id, cards[2].Id);

        //********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            OrderBy = CardsOrderBy.StartDateDesc
        }, cancellationToken: TestCancellationToken);

        Assert.Equal(card2.Id, cards[0].Id);
        Assert.Equal(card1.Id, cards[1].Id);
        Assert.Equal(card3.Id, cards[2].Id);
    }
}