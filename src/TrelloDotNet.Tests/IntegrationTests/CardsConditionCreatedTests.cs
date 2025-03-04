using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.GetCardOptions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class CardsConditionCreatedTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly Board _board = fixture.Board!;
#pragma warning disable xUnit2013 // Do not use equality check to check for collection size.
    [Fact]
    public async Task CreatedFilter()
    {
        List list1 = await AddDummyList(_board.Id, "List 1");
        List list2 = await AddDummyList(_board.Id, "List 2");
        List list3 = await AddDummyList(_board.Id, "List 3");

        Card card1 = await AddDummyCardToList(list1, "Card 1");
        await Task.Delay(1000);
        Card card2 = await AddDummyCardToList(list2, "Card 2");
        await Task.Delay(1000);
        Card card3 = await AddDummyCardToList(list3, "Card 3");
        await Task.Delay(1000);

        // ReSharper disable once JoinDeclarationAndInitializer
        List<Card> cards;
        var cardFields = new CardFields(CardFieldsType.Name);
        //*********************************************************
        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.AnyOfThese, card1.Created!.Value)]
        });
        Assert.Equal(1, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.AnyOfThese, card1.Created!.Value, card2.Created!.Value)]
        });
        Assert.Equal(2, cards.Count);
        //*********************************************************
        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.Equal, card1.Created!.Value)]
        });
        Assert.Equal(1, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.Equal, card1.Created!.Value, card2.Created!.Value)]
        });
        Assert.Equal(0, cards.Count);

        //*********************************************************
        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.NotEqual, card1.Created!.Value)]
        });
        Assert.Equal(2, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.NotEqual, card1.Created!.Value, card2.Created!.Value)]
        });
        Assert.Equal(3, cards.Count);

        //*********************************************************
        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.Between, card1.Created!.Value, card3.Created!.Value)]
        });
        Assert.Equal(3, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.NotBetween, card1.Created!.Value, card2.Created!.Value)]
        });
        Assert.Equal(1, cards.Count);

        //*********************************************************
        await Assert.ThrowsAsync<TrelloApiException>(async () =>
            cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
            {
                CardFields = cardFields,
                FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.DoNotHaveAnyValue)]
            })
        );

        await Assert.ThrowsAsync<TrelloApiException>(async () =>
            cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
            {
                CardFields = cardFields,
                FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.HasAnyValue)]
            })
        );

        //*********************************************************
        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.GreaterThan, card1.Created!.Value)]
        });
        Assert.Equal(2, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.GreaterThanOrEqual, card1.Created!.Value)]
        });
        Assert.Equal(3, cards.Count);

        //*********************************************************
        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.LessThan, card1.Created!.Value)]
        });
        Assert.Equal(0, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.LessThanOrEqual, card1.Created!.Value)]
        });
        Assert.Equal(1, cards.Count);

        //*********************************************************
        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.NoneOfThese, card1.Created!.Value)]
        });
        Assert.Equal(2, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = cardFields,
            FilterConditions = [CardsFilterCondition.Created(CardsConditionDate.NoneOfThese, card1.Created!.Value, card2.Created!.Value)]
        });
        Assert.Equal(1, cards.Count);
    }
}