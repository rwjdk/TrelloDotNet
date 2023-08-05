using TrelloDotNet.Model;
using Xunit.Abstractions;

namespace TrelloDotNet.Tests;

[Collection("Manual Tests")] //In own collection to not overlap other tests
public class TestSandbox : TestBase
{
    private readonly ITestOutputHelper _output;

    public TestSandbox(ITestOutputHelper output)
    {
        _output = output;
    }

    [FactManualOnly]
    public async Task GetWebhooks()
    {
        await Task.CompletedTask;
        var webhooksForCurrentToken = await TrelloClient.GetWebhooksForCurrentTokenAsync();
        foreach (var webhook in webhooksForCurrentToken)
        {
            _output.WriteLine("- Webhook: {0} ({1})", webhook.Description, webhook.CallbackUrl);
        }
    }

    [FactManualOnly]
    public async Task CleanUpEverythingFromUnitTests()
    {
        // ReSharper disable once ConvertToConstant.Local
        bool executeForReal = false;
        //executeForReal = true; //Comment these lines out before commit

        if (!executeForReal)
        {
            return;
        }

        //Remove test-boards
        TrelloClient.Options.AllowDeleteOfBoards = true;
        List<Board> boards = await TrelloClient.GetBoardsCurrentTokenCanAccessAsync();
        var unitTestBoards = boards.Where(x => x.Name.StartsWith("UnitTestBoard")).ToList();
        foreach (var unitTestBoard in unitTestBoards)
        {
            await TrelloClient.DeleteBoardAsync(unitTestBoard.Id);
        }

        TrelloClient.Options.AllowDeleteOfBoards = false;

        //Remove test-workspaces
        TrelloClient.Options.AllowDeleteOfOrganizations = true;
        List<Organization> organizations = await TrelloClient.GetOrganizationsCurrentTokenCanAccessAsync();
        var unitTestOrganizations = organizations.Where(x => x.DisplayName.StartsWith("UnitTestOrganization")).ToList();
        foreach (var unitTestOrganization in unitTestOrganizations)
        {
            await TrelloClient.DeleteOrganizationAsync(unitTestOrganization.Id);
        }

        TrelloClient.Options.AllowDeleteOfOrganizations = false;
    }

    [FactManualOnly]
    public async Task DeleteAllWebhooks()
    {
        // ReSharper disable once ConvertToConstant.Local
        bool executeForReal = false;
        //executeForReal = true; //Comment these lines out before commit

        if (!executeForReal)
        {
            return;
        }
        //Delete all Webhooks (comment in execution)
        var webhooksForCurrentToken = await TrelloClient.GetWebhooksForCurrentTokenAsync();
        foreach (var webhook in webhooksForCurrentToken)
        {
            await TrelloClient.DeleteWebhookAsync(webhook.Id);
        }
    }

    [FactManualOnly]
    public async Task CustomFieldsTests()
    {
        await Task.CompletedTask;
        //NB: These are not part of the automated test-suite as that is linked to a free account that does not support custom fields
        /*
        var boardId = "641ddde2e37dc99ab1ccc988";
        List<CustomField> customFieldsOnBoardAsync = await TrelloClient.GetCustomFieldsOnBoardAsync(boardId);
        var card = (await TrelloClient.GetCardsOnBoardAsync(boardId)).First(); //Grab random card

        //Sample set all custom fields on the board
        foreach (var customField in customFieldsOnBoardAsync)
        {
            switch (customField.Type)
            {
                case CustomFieldType.Checkbox:
                    await TrelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, true); //Update Bool
                    bool? boolean = card.CustomFieldItems.GetCustomFieldValueAsBoolean(customField); //Get Bool
                    await TrelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear Bool
                    break;
                case CustomFieldType.Date:
                    await TrelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, DateTimeOffset.Now); //Update Date
                    DateTimeOffset? dateTimeOffset = card.CustomFieldItems.GetCustomFieldValueAsDateTimeOffset(customField); // Get Date
                    await TrelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear Date
                    break;
                case CustomFieldType.List:
                    await TrelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, customField.Options[0]); //Update ListOption
                    CustomFieldOption? listOption = card.CustomFieldItems.GetCustomFieldValueAsOption(customField); //Get ListOption (as Option)
                    string listOptionString = card.CustomFieldItems.GetCustomFieldValueAsString(customField); //Get ListOption as String value
                    await TrelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear List Option
                    break;
                case CustomFieldType.Number:
                    await TrelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, 42); //Update Integer
                    await TrelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, 42M); //Update Decimal
                    int? numberAsInteger = card.CustomFieldItems.GetCustomFieldValueAsInteger(customField); //Get Integer
                    decimal? numberAsDecimal = card.CustomFieldItems.GetCustomFieldValueAsDecimal(customField); //Get Decimal
                    await TrelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear number
                    break;
                case CustomFieldType.Text:
                    await TrelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, "Hello World"); //Update String
                    var stringValue = card.CustomFieldItems.GetCustomFieldValueAsString(customField); //Get String
                    await TrelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear String
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }*/
    }
}