using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.GetCardOptions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class CardExtensionsTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly Board _board = fixture.Board!;
#pragma warning disable xUnit2013 // Do not use equality check to check for collection size.
    [Fact]
    public async Task MemberFilter()
    {
        List list1 = await AddDummyList(_board.Id, "List 1");
        List list2 = await AddDummyList(_board.Id, "List 2");
        List list3 = await AddDummyList(_board.Id, "List 3");

        Card card1 = await AddDummyCardToList(list1);
        Card card2 = await AddDummyCardToList(list2);
        Card card3 = await AddDummyCardToList(list3);

        Member member = await TrelloClient.GetTokenMemberAsync();

        await TrelloClient.AddMembersToCardAsync(card1.Id, member.Id);
        await TrelloClient.AddMembersToCardAsync(card3.Id, member.Id);

        // ReSharper disable once JoinDeclarationAndInitializer
        List<Card> cards;
        //Equal *********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberId(CardsConditionIds.Equal, member.Id)]
        });
        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.Equal, member.FullName)]
        });
        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);


        //NotEqual *********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberId(CardsConditionIds.NotEqual, member.Id)]
        });

        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card2.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.NotEqual, member.FullName)]
        });
        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card2.Name);

        //Any of These *********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberId(CardsConditionIds.AnyOfThese, member.Id)]
        });
        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.AnyOfThese, member.FullName)]
        });
        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);

        //None of These *********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberId(CardsConditionIds.NoneOfThese, member.Id)]
        });
        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card2.Name);


        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.NoneOfThese, member.FullName)]
        });
        Assert.Equal(1, cards.Count);
        Assert.Contains(cards, x => x.Name == card2.Name);

        //All of These *********************************************************************************************************

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberId(CardsConditionIds.AllOfThese, member.Id)]
        });
        Assert.Equal(2, cards.Count);
        Assert.Contains(cards, x => x.Name == card1.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);


        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.AllOfThese, member.FullName)]
        });
        Assert.Contains(cards, x => x.Name == card1.Name);
        Assert.Contains(cards, x => x.Name == card3.Name);

        //Other *****************************************************************************************************************

        var firstName = member.FullName.Split([' '], StringSplitOptions.RemoveEmptyEntries)[0];
        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.Contains, firstName)]
        });
        Assert.Equal(2, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.DoNotContains, Guid.NewGuid().ToString())]
        });
        Assert.Equal(3, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.StartsWith, firstName)]
        });
        Assert.Equal(2, cards.Count);


        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.DoNotStartWith, firstName)]
        });
        Assert.Equal(1, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.EndsWith, Guid.NewGuid().ToString())]
        });
        Assert.Equal(0, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.DoNotEndWith, Guid.NewGuid().ToString())]
        });
        Assert.Equal(3, cards.Count);

        cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name),
            FilterConditions = [CardsFilterCondition.MemberName(CardsConditionString.RegEx, Guid.NewGuid().ToString())]
        });
        Assert.Equal(0, cards.Count);
    }
}