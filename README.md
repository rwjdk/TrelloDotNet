[![NuGet Badge](https://img.shields.io/nuget/v/TrelloDotNet)](https://www.nuget.org/packages/TrelloDotNet) 
[![WIKI](https://img.shields.io/badge/ℹ️-Wiki-green)](https://github.com/rwjdk/TrelloDotNet/wiki)
[![Changelog](https://img.shields.io/badge/ℹ️-Changelog-orange)](https://github.com/rwjdk/TrelloDotNet/blob/main/Changelog.md)
[![YouTube](https://img.shields.io/badge/ℹ️-YouTube-red)](https://www.youtube.com/playlist?list=PLhGl0l5La4saguVChJ3jmlAXqFDkmYjdC)
[![Rest API](https://img.shields.io/badge/Trello-Rest_API-lightgray)](https://developer.atlassian.com/cloud/trello/rest/)
[![API Keys](https://img.shields.io/badge/Trello-API_Key_+_Token-blueviolet)](https://trello.com/power-ups/admin/)
[![Trello Developers LinkedIn Group](https://img.shields.io/badge/LinkedIn-Trello_Developers-0077B5)](https://www.linkedin.com/groups/12847286/)



# TrelloDotNet
Welcome to TrelloDotNet; a modern .NET Implementation of the Trello API.

## Features
- A [TrelloClient](https://github.com/rwjdk/TrelloDotNet/wiki/TrelloClient) implementing the Trello API for CRUD operations on most Trello Features
- An [Automation Engine](https://github.com/rwjdk/TrelloDotNet/wiki/Automation-Engine) and [Webhook Data Reciver](https://github.com/rwjdk/TrelloDotNet/wiki/Webhook-Data-Reciver) for handling Webhook Events from a Trello Board

## How to get started
1. Install the '[TrelloDotNet](https://www.nuget.org/packages/TrelloDotNet)' NuGet Package (dotnet add package TrelloDotNet)
2. Retrieve your [API-Key and Token](https://youtu.be/ndLSAD3StH8)
3. Create new instance of `TrelloDotNet.TrelloClient`
4. Locate you Ids of you Boards, List and Cards (see video [here](https://youtu.be/aWYEg1wPVYY) or at the end on this ReadMe)
5. Use the TrelloClient based on the examples below and/or the [Wiki](https://github.com/rwjdk/TrelloDotNet/wiki).

### Examples of Usage:

```cs
TrelloClient client = new TrelloClient("APIKey", "TOKEN"); //IMPORTANT: Remember to not leave Key and Token in clear text!

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
- The easiest way to get Ids in Trello is to use this [Power-Up](https://trello.com/power-ups/646cc3622176aebf713bb7f8/api-developer-id-helper) to copy/paste them (Recommended)... 

![API Developer ID Helper Power-Up](https://i.imgur.com/4FR6K2t.gif)

- or use the API itself or use the share buttons in the project (require no Power-Up but more cumbersome)

![Trello Board](https://i.imgur.com/D6vxkrm.png)

The Export looks like this (search for id or use a tool to pretty-print the JSON to get a better view)

![JSON Example](https://i.imgur.com/qDJgzNz.png)

## More info, bugs, or questions?
Visit the Github Page: https://github.com/rwjdk/TrelloDotNet

*Have Fun* :-)
