using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.GetCardOptions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class CardsConditionListTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly Board _board = fixture.Board!;
#pragma warning disable xUnit2013 // Do not use equality check to check for collection size.
    [Fact]
    public async Task ListFilter()
    {
        List list1 = await AddDummyList(_board.Id, "List 1");
        List list2 = await AddDummyList(_board.Id, "List 2");
        List list3 = await AddDummyList(_board.Id, "List 3");

        Card card1 = await AddDummyCardToList(list1);
        Card card2 = await AddDummyCardToList(list2);
        Card card3 = await AddDummyCardToList(list3);

        // ReSharper disable once JoinDeclarationAndInitializer
        List<Card> cards;
        //Equal *********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListId(CardsConditionIds.Equal, list1.Id)]
        });
        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListId(CardsConditionIds.Equal, list1.Id, list1.Id)]
        });
        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.Equal, list2.Name)]
        });
        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card2.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.Equal, list2.Name, list2.Name)]
        });
        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card2.Name);


        //NotEqual *********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListId(CardsConditionIds.NotEqual, list1.Id)]
        });

        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card2.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListId(CardsConditionIds.NotEqual, list1.Id, list1.Id)]
        });

        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card2.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.NotEqual, list2.Name)]
        });
        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.NotEqual, list2.Name, list2.Name)]
        });
        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);

        //Any of These *********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListId(CardsConditionIds.AnyOfThese, list1.Id)]
        });
        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListId(CardsConditionIds.AnyOfThese, list1.Id, list2.Id)]
        });
        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);
        Assert.Contains(cards, x => x.Name == card2.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.AnyOfThese, list1.Name)]
        });
        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.AnyOfThese, list1.Name, list2.Name)]
        });
        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);
        Assert.Contains(cards, x => x.Name == card2.Name);

        //None of These *********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListId(CardsConditionIds.NoneOfThese, list1.Id)]
        });
        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card2.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListId(CardsConditionIds.NoneOfThese, list1.Id, list2.Id)]
        });
        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card3.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.NoneOfThese, list1.Name)]
        });
        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card2.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.NoneOfThese, list1.Name, list2.Name)]
        });
        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card3.Name);

        //All of These *********************************************************************************************************

        await Assert.ThrowsAsync<TrelloApiException>(async () =>
        {
            cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
            {
                CardFields = new CardFields(CardFieldsType.Name),
                FilterConditions = [CardsFilterCondition.ListId(CardsConditionIds.AllOfThese, list1.Id)]
            });
        });

        await Assert.ThrowsAsync<TrelloApiException>(async () =>
        {
            cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
            {
                CardFields = new CardFields(CardFieldsType.Name),
                FilterConditions = [CardsFilterCondition.ListId(CardsConditionIds.AllOfThese, list1.Id, list2.Id)]
            });
        });


        await Assert.ThrowsAsync<TrelloApiException>(async () =>
        {
            cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
            {
                CardFields = new CardFields(CardFieldsType.Name),
                FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.AllOfThese, list1.Name)]
            });
        });

        await Assert.ThrowsAsync<TrelloApiException>(async () =>
        {
            cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
            {
                CardFields = new CardFields(CardFieldsType.Name),
                FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.AllOfThese, list1.Name, list2.Name)]
            });
        });

        //Other *****************************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.Contains, "Hello")]
        });
        Assert.Equal(0, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.Contains, "Hello", "World")]
        });
        Assert.Equal(0, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.DoNotContains, "Hello")]
        });
        Assert.Equal(3, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.DoNotContains, "Hello", "World")]
        });
        Assert.Equal(3, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.StartsWith, "Li")]
        });
        Assert.Equal(3, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.StartsWith, "Li", Guid.NewGuid().ToString())]
        });
        Assert.Equal(3, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.DoNotStartWith, "Li")]
        });
        Assert.Equal(0, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.DoNotStartWith, "Li", Guid.NewGuid().ToString())]
        });
        Assert.Equal(0, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.EndsWith, "1")]
        });
        Assert.Equal(1, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.EndsWith, "1", "2")]
        });
        Assert.Equal(2, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.DoNotEndWith, "1")]
        });
        Assert.Equal(2, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.DoNotEndWith, "1", "2")]
        });
        Assert.Equal(1, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.RegEx, "is")]
        });
        Assert.Equal(3, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.ListName(CardsConditionString.RegEx, "is", Guid.NewGuid().ToString())]
        });
        Assert.Equal(3, cards.Count);
    }
}