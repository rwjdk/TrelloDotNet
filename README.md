[![NuGet](https://img.shields.io/badge/NuGet-blue?style=for-the-badge)](https://www.nuget.org/packages/TrelloDotNet)
[![WIKI](https://img.shields.io/badge/Wiki-brown?style=for-the-badge)](https://github.com/rwjdk/TrelloDotNet/wiki)
[![Changelog](https://img.shields.io/badge/-Changelog-darkgreen?style=for-the-badge)](https://github.com/rwjdk/TrelloDotNet/blob/main/Changelog.md)
[![YouTube](https://img.shields.io/badge/-YouTube-darkred?style=for-the-badge)](https://www.youtube.com/playlist?list=PLhGl0l5La4saguVChJ3jmlAXqFDkmYjdC)
[![Rest API](https://img.shields.io/badge/The_Trello_REST_API-gray?style=for-the-badge)](https://developer.atlassian.com/cloud/trello/rest/)
[![API Keys](https://img.shields.io/badge/Power--Ups_administration-purple?style=for-the-badge)](https://trello.com/power-ups/admin/)

# TrelloDotNet
_Welcome to TrelloDotNet - A .NET Implementation of the [Trello REST API](https://developer.atlassian.com/cloud/trello/rest)_

[![GitHub Release Date](https://img.shields.io/github/release-date/rwjdk/TrelloDotNet?style=for-the-badge&label=Last%20Release)](https://www.nuget.org/packages/TrelloDotNet)
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/rwjdk/TrelloDotNet/Build.yml?style=for-the-badge)](https://github.com/rwjdk/TrelloDotNet/actions)
[![GitHub Issues or Pull Requests by label](https://img.shields.io/github/issues/rwjdk/TrelloDotNet/bug?style=for-the-badge&label=Bugs)](https://github.com/rwjdk/TrelloDotNet/issues?q=is%3Aissue%20state%3Aopen%20label%3Abug)
[![Libraries.io dependency status for GitHub repo](https://img.shields.io/librariesio/github/rwjdk/TrelloDotNet?style=for-the-badge)](https://github.com/rwjdk/TrelloDotNet/network/dependencies)
[![Coveralls](https://img.shields.io/coverallsCoverage/github/rwjdk/TrelloDotNet?style=for-the-badge)](https://coveralls.io/github/rwjdk/TrelloDotNet)

## Features
- A [TrelloClient](https://github.com/rwjdk/TrelloDotNet/wiki/TrelloClient) for CRUD operations on the Trello features
- An [Automation Engine](https://github.com/rwjdk/TrelloDotNet/wiki/Automation-Engine) and [Webhook Data Receiver](https://github.com/rwjdk/TrelloDotNet/wiki/Webhook-Data-Receiver) for handling Webhook Events

## Getting Started
1. Install the '[TrelloDotNet](https://www.nuget.org/packages/TrelloDotNet)' NuGet Package (`dotnet add package TrelloDotNet`)
2. Retrieve your [API-Key and Token](https://youtu.be/ndLSAD3StH8) from the [PowerUps Administration](https://trello.com/power-ups/admin)
3. Create a new instance of the `TrelloClient` _(located in the namespace 'TrelloDotNet')_
4. Locate you IDs of your Boards, List, and Cards (see video [here](https://youtu.be/es84INLIiKI) or at the end of this ReadMe)
5. Use the TrelloClient based on the examples below and/or the [Wiki](https://github.com/rwjdk/TrelloDotNet/wiki).

### Examples of Usage:

```cs
TrelloClient client = new TrelloDotNet.TrelloClient("APIKEY", "TOKEN"); //IMPORTANT: Remember to NOT leave Key and Token in clear text!

//Get all boards that the Token Owner can Access
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
  - [Version 2.0 Migration Guide](https://github.com/rwjdk/TrelloDotNet/issues/51)
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
