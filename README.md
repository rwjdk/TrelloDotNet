[![NuGet](https://img.shields.io/badge/NuGet-blue)](https://www.nuget.org/packages/TrelloDotNet)
[![WIKI](https://img.shields.io/badge/Wiki-brown)](https://github.com/rwjdk/TrelloDotNet/wiki)
[![Changelog](https://img.shields.io/badge/-Changelog-darkgreen)](https://github.com/rwjdk/TrelloDotNet/blob/main/Changelog.md)
[![YouTube](https://img.shields.io/badge/-YouTube-darkred)](https://www.youtube.com/playlist?list=PLhGl0l5La4saguVChJ3jmlAXqFDkmYjdC)
[![Rest API](https://img.shields.io/badge/The_Trello_REST_API-gray)](https://developer.atlassian.com/cloud/trello/rest/)
[![API Keys](https://img.shields.io/badge/Power--Ups_administration-purple)](https://trello.com/power-ups/admin/)

# TrelloDotNet: .NET Implementation of the Trello REST API

## Features
- A [TrelloClient](https://github.com/rwjdk/TrelloDotNet/wiki/TrelloClient) for CRUD operations on the Trello features
- An [Automation Engine](https://github.com/rwjdk/TrelloDotNet/wiki/Automation-Engine) and [Webhook Data Reciver](https://github.com/rwjdk/TrelloDotNet/wiki/Webhook-Data-Reciver) for handling Webhook Events

## Getting Started
1. Install the '[TrelloDotNet](https://www.nuget.org/packages/TrelloDotNet)' NuGet Package (`dotnet add package TrelloDotNet`)
2. Retrieve your [API-Key and Token](https://youtu.be/ndLSAD3StH8) from the [PowerUps Administration](https://trello.com/power-ups/admin)
3. Create new instance of the `TrelloClient` _(located in namespace 'TrelloDotNet')_
4. Locate you Ids of your Boards, List, and Cards (see video [here](https://youtu.be/es84INLIiKI) or at the end on this ReadMe)
5. Use the TrelloClient based on the examples below and/or the [Wiki](https://github.com/rwjdk/TrelloDotNet/wiki).

### Examples of Usage:

```cs
TrelloClient client = new TrelloDotNet.TrelloClient("APIKEY", "TOKEN"); //IMPORTANT: Remember to NOT leave Key and Token in clear text!

//Get all boards that Token Owner can Access
List<Board> boards = await client.GetBoardsCurrentTokenCanAccessAsync();

//Get a specific board
Board board = await client.GetBoardAsync("<boardId>");

//Get Lists on a board
List<List> lists = await client.GetListsOnBoardAsync("<boardId>");

//Get Cards on Board
List<Card> cardsOnBoard = await trelloClient.GetCardsOnBoardAsync("<boardId>");

//Get Cards in a specific List
List<Card> cardsInList = await trelloClient.GetCardsInListAsync("<listId>");

//Get a specific card
Card card = await client.GetCardAsync("<cardId>");

//Add a card (Simple)
AddCardOptions newCardOptions = new AddCardOptions("<listId>", "My Card", "My Card description");
Card newCard = await client.AddCardAsync(newCardOptions);

//Add a Card (Advanced with all options set)
Card newAdvancedCard = await client.AddCardAsync(new AddCardOptions
{
    //Mandatory options
    ListId = "<listId>",
    Name = "My Card",

    //Optional options
    Description = "Description of My Card",
    Start = DateTimeOffset.Now,
    Due = DateTimeOffset.Now.AddDays(3),
    Cover = new CardCover(CardCoverColor.Blue, CardCoverSize.Normal),
    LabelIds = new List<string>
    {
        "<labelId1>",
        "<labelId2>",
    },
    MemberIds = new List<string>
    {
        "<memberId1>",
        "<memberId2>"
    },
    Checklists = new List<Checklist>
    {
        new Checklist("Checklist 1", new List<ChecklistItem>
        {
            new ChecklistItem("Item 1"),
            new ChecklistItem("Item 2"),
            new ChecklistItem("Item 3")
        }),
        new Checklist("Checklist 2", new List<ChecklistItem>
        {
            new ChecklistItem("Item A"),
            new ChecklistItem("Item B"),
            new ChecklistItem("Item C")
        }),
    },
    AttachmentUrlLinks = new List<AttachmentUrlLink>
    {
        new AttachmentUrlLink("https://www.google.com", "Google")
    },
    AttachmentFileUploads = new List<AttachmentFileUpload>
    {
        new AttachmentFileUpload(File.OpenRead(@"<pathToFile>"), "<Filename>", "<FileDescription>")
    },
    CustomFields = new List<AddCardOptionsCustomField>
    {
        new AddCardOptionsCustomField(customField1OnBoard, "ABC"),
        new AddCardOptionsCustomField(customField2OnBoard, 123),
    }
});

//Update a Card (with new name and description and removal of Due Date)
var updateCard = await TrelloClient.UpdateCardAsync("<cardId>", [
    CardUpdate.Name("New Name"),
    CardUpdate.Description("New Description"),
    CardUpdate.DueDate(null),
]);

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
