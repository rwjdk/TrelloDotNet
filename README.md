[![NuGet](https://img.shields.io/badge/Code-NuGet-blue)](https://www.nuget.org/packages/TrelloDotNet)
[![WIKI](https://img.shields.io/badge/Documentation-Wiki-darkred)](https://github.com/rwjdk/TrelloDotNet/wiki)
[![Changelog](https://img.shields.io/badge/-Changelog-darkred)](https://github.com/rwjdk/TrelloDotNet/blob/main/Changelog.md)
[![YouTube](https://img.shields.io/badge/-YouTube-darkred)](https://www.youtube.com/playlist?list=PLhGl0l5La4saguVChJ3jmlAXqFDkmYjdC)
[![Rest API](https://img.shields.io/badge/Trello_API-Reference-red)](https://developer.atlassian.com/cloud/trello/rest/)
[![API Keys](https://img.shields.io/badge/-Admin-red)](https://trello.com/power-ups/admin/)

TrelloDotNet - a .NET wrapper of the Trello REST API.

## Features
- A [TrelloClient](https://github.com/rwjdk/TrelloDotNet/wiki/TrelloClient) for CRUD operations on most Trello features
- An [Automation Engine](https://github.com/rwjdk/TrelloDotNet/wiki/Automation-Engine) and [Webhook Data Reciver](https://github.com/rwjdk/TrelloDotNet/wiki/Webhook-Data-Reciver) for handling Webhook Events from a Trello Board

## Getting Started
1. Install the '[TrelloDotNet](https://www.nuget.org/packages/TrelloDotNet)' NuGet Package (dotnet add package TrelloDotNet)
2. Retrieve your [API-Key and Token](https://youtu.be/ndLSAD3StH8)
3. Create new instance of the `TrelloClient` (located in namespace 'TrelloDotNet')
4. Locate you Ids of your Boards, List, and Cards (see video [here](https://youtu.be/es84INLIiKI) or at the end on this ReadMe)
5. Use the TrelloClient based on the examples below and/or the [Wiki](https://github.com/rwjdk/TrelloDotNet/wiki).

### Examples of Usage:

```cs
TrelloClient client = new TrelloDotNet.TrelloClient("APIKey", "TOKEN"); //IMPORTANT: Remember to NOT leave Key and Token in clear text!

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
Card input = new Card("<listId>", "My Card", "My Card description");
//todo - add more about the card 
Card newCard = await client.AddCardAsync(input);

//Add a Checklist to a card
var checklistItems = new List<ChecklistItem>
{
    new("ItemA"),
    new("ItemB"),
    new("ItemC")
};
Checklist newChecklist = new Checklist("Sample Checklist", checklistItems);
Checklist addedChecklist = await client.AddChecklistAsync("<cardId>", newChecklist);

```

## Video Guides
- Trello Developer Fundamentals
  - [How to get your API-Key and Token](https://youtu.be/ndLSAD3StH8)
  - [How to Find ids on a Trello Board](https://youtu.be/es84INLIiKI)
- TrelloDotNet
  - [How to use the TrelloDotNet NuGet Package](https://youtu.be/tf47BCkieus)
  - [E-Learning Playlist](https://www.youtube.com/playlist?list=PLhGl0l5La4sZJxSCNYl0AfCagdRB_c8CD)
  - Webhook
    - [How to work with Webhooks (Part 1: Setup)](https://youtu.be/A3_B-SLBm_0)
    - [How to work with Webhooks (Part 2: Receiving Events)](https://youtu.be/GsGKDDvuq40)

## Handy links
- [Wiki](https://github.com/rwjdk/TrelloDotNet/wiki)
- [Changelog](https://github.com/rwjdk/TrelloDotNet/blob/main/Changelog.md)
- [Report an issue](https://github.com/rwjdk/TrelloDotNet/issues)
- [Report a security concern](https://github.com/rwjdk/TrelloDotNet/security)
- [TrelloDotNet Nuget Package](https://www.nuget.org/packages/TrelloDotNet) 
- [Developers LinkedIn Group](https://www.linkedin.com/groups/12847286/) 
- [Power-Up to locate Ids](https://trello.com/power-ups/646cc3622176aebf713bb7f8/api-developer-id-helper)
- [Trello API YouTube Playlist](https://www.youtube.com/playlist?list=PLhGl0l5La4saguVChJ3jmlAXqFDkmYjdC)
- [Power-Ups Admin Center for API Keys and Tokens](https://trello.com/power-ups/admin/)
- [Trello API Documentation](https://developer.atlassian.com/cloud/trello/rest)
- [Trello Changelog](https://developer.atlassian.com/cloud/trello/changelog/)
- [How to build your first Power-Up](https://www.youtube.com/watch?v=dLCkcQnwAQk&ab_channel=TrelloDevelopers)

## On the subject of getting Ids from Trello
The easiest way to get Ids in Trello is to use this [Power-Up](https://trello.com/power-ups/646cc3622176aebf713bb7f8/api-developer-id-helper) to copy/paste them (Recommended)

![API Developer ID Helper Power-Up](https://i.imgur.com/4FR6K2t.gif)

Alternative use the share buttons in the project (require no Power-Up but more cumbersome)

![Trello Board](https://i.imgur.com/D6vxkrm.png)

The Export looks like this (search for id or use a tool to pretty-print the JSON to get a better view)

![JSON Example](https://i.imgur.com/qDJgzNz.png)

*Have Fun* :-)
