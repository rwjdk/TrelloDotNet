[Back to Card Features](TrelloClient#card-features)

Add a new Card to a board

## Signature
```cs
/// <summary>
/// Add a Card
/// </summary>
/// <param name="options">Add Card Options (Name, Dates, Checklist, etc.)</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Added Card</returns>
public async Task<Card> AddCardAsync(AddCardOptions options, CancellationToken cancellationToken = default)
```
### Examples

```cs
string listIdToAddTheCardTo = "63d128787441d05619f44dbe"; //Use 'TrelloClient.GetListsOnBoardAsync()' to find real list Ids

//Add a simple Card
var newSimpleCard = new AddCardOptions(listIdToAddTheCardTo, "My Card Name/Title");
var addedSimpleCard = await _trelloClient.AddCardAsync(newSimpleCard);

//Add a Card with All options
Card newAdvancedCard = await client.AddCardAsync(new AddCardOptions
{
    //Mandatory options
    ListId = listIdToAddTheCardTo,
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
});```