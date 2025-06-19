_NB: The Filter Condition System is a v 2.0 or higher feature only_

When you are returning a collection of cards from the API, you can via the [GetCardOptions](GetCardOptions) specify a filter on what cards you are interested in (ignoring the rest). Filters are always `AND` based

> Example: _give me all cards on board that have the Red Label, 1-2 Members and the Description contains the word 'Urgent'_

### Fields you can filter by:
- Name
- List (Id or Name)
- Label (Id or Name)
- Member (Id or Name)
- Description
- Due
- DueComplete
- DueWithNoDueComplete
- Start
- Created
- CustomField

### Conditions you can use (depending on the type of field (Example: it does not make sense to use GreaterThan on a Label-field))
- Equal
- NotEqual
- GreaterThan
- LessThan
- GreaterThanOrEqual
- LessThanOrEqual
- HasAnyValue
- DoNotHaveAnyValue
- Contains
- DoNotContains
- AnyOfThese
- AllOfThese
- NoneOfThese
- RegEx
- StartsWith
- EndsWith
- DoNotStartWith
- DoNotEndWith
- Between
- NotBetween

### Two ways to filter
In TrelloDotNet you have two ways to filter: 

1. Directly in the API-Call that returns multiple Cards (via the [GetCardOptions](GetCardOptions))
2. If you have a collection of cards, and you wish to have a specific subset of them (via the `Filter(...)` Extension Method)

### Important Note: Filtering is In-Memory
As the Trello REST API does not offer any advanced filtering capabilities, filtering will happen in memory, meaning that if you example have 100 cards on a board and you only wish to have the 10 cards that have the Red Label on them, the system will query all 100 cards and in memory reduce these to that 10 cards based on your specified condition (`CardsFilterCondition.LabelId(CardsConditionId.AnyOfThese, "<idOfTheRedLabel>")`)

For this reason, it is recommended that if you need to retrieve multiple sets of conditioned cards to:

- Step 1. Get all Cards (returning them to variable `allCardsOnBoard`)
- Step 2. Use `allCardsOnBoard.Filter(<first condition set>)` to get the first set of conditions
- Step 3. Use `allCardsOnBoard.Filter(<second condition set>)` to get the second set of conditions

... as it will result in only a single network call instead of two

### Important Note: Filtering vs CardFields Property
As filtering happens in-memory the data you filter needs to be there, the system will manipulate your `CardFields` property on `GetCardOptions`. So if you, for example, have a `CardsFields` that contains the Name, but a filter condition on the Start Date, the call will automate modify the `CardsFields` to also include the `Start` field. 

In cases where you use the Extension method, it is your responsibility to include the needed fields yourself

### Examples:
```cs

//Get all Cards that have the red label
var cards = await _trelloClient.GetCardsOnBoardAsync("644caf075b7fc5bd3f60830e", new GetCardOptions
{
    FilterConditions = [CardsFilterCondition.LabelId(CardsConditionId.AnyOfThese, "<idOfTheRedLabel>") ],
});

//Get all Cards that have no Description
var cards = await _trelloClient.GetCardsOnBoardAsync("644caf075b7fc5bd3f60830e", new GetCardOptions
{
    FilterConditions = [CardsFilterCondition.HasNoDescription() ],
});

///Get all Cards that have exactly one member, were created within the last 3 days, and are not yet started
var cards = await _trelloClient.GetCardsOnBoardAsync("644caf075b7fc5bd3f60830e", new GetCardOptions
{
    FilterConditions = [
        CardsFilterCondition.MemberCount(CardsConditionCount.Equal, 1), 
        CardsFilterCondition.Created(CardsConditionDate.GreaterThanOrEqual, DateTimeOffset.UtcNow.AddDays(-3)), 
        CardsFilterCondition.NotStarted()
    ],
});

//Example of filtering after the fact (note we specify the desired fields (Name) and the Fields needed for in-memory filtering)
var allCards = await _trelloClient.GetCardsOnBoardAsync("644caf075b7fc5bd3f60830e", new GetCardOptions
{
    CardFields = new CardFields(
      CardFieldsType.Name,  //Desired
      CardFieldsType.LabelIds, CardFieldsType.Due, CardFieldsType.DueComplete, CardFieldsType.MemberIds //Needed for filtering
    )
});

var cardsWithTheRedLabel = allCards.Filter(CardsFilterCondition.LabelId(CardsConditionId.AnyOfThese, "<idOfTheRedLabel>"));
var cardsCreatedToday = allCards.Filter(CardsFilterCondition.CreatedBetween(DateTimeOffset.UtcNow.Date, DateTimeOffset.UtcNow.Date.AddDays(1).AddSeconds(-1)));
var cardsDueTodayThatHaveNoMembers = allCards.Filter(
    CardsFilterCondition.DueToday(),
    CardsFilterCondition.MemberCount(CardsConditionCount.Equal, 0)
    );



```


