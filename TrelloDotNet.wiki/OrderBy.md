_NB: This is a v 2.0 and higher only feature_

When you return multiple cards from the API you are normally given them back in the 'latest card first' order.

If you wish to change this you can via the [GetCardOptions](GetCardOptions) parameter specify the `OrderBy` property

You have the following order by options:

- CreateDateAsc
- CreateDateDesc
- StartDateAsc
- StartDateDesc
- DueDateAsc
- DueDateDesc
- NameAsc
- NameDesc

### Example
```cs

//Get Cards On Board ordered by name
var cardOrderedByName = await _trelloClient.GetCardsOnBoardAsync("644caf075b7fc5bd3f60830e", new GetCardOptions
{
    OrderBy = CardsOrderBy.NameAsc
});
```
