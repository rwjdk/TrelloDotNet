# TrelloDotNet
Welcome to TrelloDotNet; a modern .NET Implementation of the Trello API.

## How to get started
1. Install the '[TrelloDotNet](https://www.nuget.org/packages/TrelloDotNet)' NuGet Package (dotnet add package TrelloDotNet)
2. Retrieve your [API-Key and Token](https://youtu.be/ndLSAD3StH8)
3. Create new instance of `TrelloDotNet.TrelloClient`
4. Locate you Ids of you Boards, List and Cards (see video [here](https://youtu.be/aWYEg1wPVYY) or at the end on this ReadMe)
5. Use the TrelloClient based on the examples below and/or the [Wiki](https://github.com/rwjdk/TrelloDotNet/wiki).

### Examples of Usage:

```cs
TrelloClient client = new TrelloClient("APIKey", "TOKEN");

//Get a board
Board board = await client.GetBoardAsync("<boardId>");

//Get Lists on a board
List<List> lists = await client.GetCardsOnBoardAsync("<boardId>");

//Get a card
Card card = await client.GetCardAsync("<cardId>");

//Get Cards on Board
List<Card> cardsOnBoard = await trelloClient.GetCardsOnBoardAsync("<boardId>");

//Get Cards in List
List<Card> cardsInList = await trelloClient.GetCardsInListAsync("<listId>");

//Add a card
var input = new Card("<listId>", "My Card", "My Card description");
//todo - add more about the card 
var newCard = await client.AddCardAsync(input);

//Add a Checklist to a card
var checklistItems = new List<ChecklistItem>
{
    new("ItemA"),
    new("ItemB"),
    new("ItemC")
};
var newChecklist = new Checklist("Sample Checklist", checklistItems);
var addedChecklist = await client.AddChecklistAsync("<cardId>", newChecklist);

```

## Version History
- [Changelog](https://github.com/rwjdk/TrelloDotNet/blob/main/Changelog.md)

## Video Guides
- [How to get your API-Key and Token](https://youtu.be/ndLSAD3StH8)
- [How to Find ids on a Trello Board](https://youtu.be/aWYEg1wPVYY)
- [How to use the TrelloDotNet NuGet Package](https://youtu.be/tf47BCkieus)
- [How to work with Webhooks (Part 1: Setup)](https://youtu.be/A3_B-SLBm_0)
- [How to work with Webhooks (Part 2: Receiving Events)](https://youtu.be/GsGKDDvuq40)

## Other handy links
- Admin Center for API Keys and Tokens: https://trello.com/power-ups/admin/
- REST API Documentation from Trello: https://developer.atlassian.com/cloud/trello/rest


## On the subject of getting Ids from Trello
- The easiest way to get Ids in Trello is to use the API itself or use the share buttons in the project

![Trello Board](https://i.imgur.com/D6vxkrm.png)

The Export looks like this (search for id or use a tool to pretty-print the JSON to get a better view)

![JSON Example](https://i.imgur.com/qDJgzNz.png)

## More info, bugs, or questions?
Visit the Github Page: https://github.com/rwjdk/TrelloDotNet

*Have Fun* :-)
