using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.AutomationEngine.Model.Conditions;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Webhook;
using Xunit.Abstractions;

namespace TrelloDotNet.Tests.AutomationEngineTests.ConditionTests;

public class ConditionTests : TestBaseWithNewBoard
{
    private readonly ITestOutputHelper _output;

    public ConditionTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async void ConditionsTests()
    {
        int step = 1;
        int totalSteps = 4;
        try
        {
            await CreateNewBoard();
            var lists = await TrelloClient.GetListsOnBoardAsync(BoardId);

            AddOutput("TestListCondition", ref step, totalSteps);
            await TestListCondition(lists);

            WaitToAvoidRateLimits(3);

            AddOutput("TestChecklistItemsIncompleteCondition", ref step, totalSteps);
            await TestChecklistItemsIncompleteCondition(lists);

            WaitToAvoidRateLimits(3);

            AddOutput("TestChecklistNotStartedCondition", ref step, totalSteps);
            await TestChecklistNotStartedCondition(lists);

            WaitToAvoidRateLimits(3);

            AddOutput("TestCardCoverCondition", ref step, totalSteps);
            await TestCardCoverCondition(lists);

            
        }
        finally
        {
            await DeleteBoard();
        }
    }

    private async Task TestListCondition(List<List> lists)
    {
        var aList = lists.First();
        var webhookActionWithList = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.ListUpdated, listToSimulate: aList);
        await TestListConditionForSpecificWebhookAction(aList, webhookActionWithList);
        
        WaitToAvoidRateLimits(3);

        var card = await TrelloClient.AddCardAsync(new Card(aList.Id, "Some Card"));
        var webhookActionWithCard = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardUpdated, cardToSimulate: card);
        await TestListConditionForSpecificWebhookAction(aList, webhookActionWithCard);
    }

    private static async Task TestListConditionForSpecificWebhookAction(List aList, WebhookAction webhookActionWithList)
    {
        var conditionTrueName = new ListCondition(ListConditionConstraint.AnyOfTheseLists, aList.Name) { TreatListNameAsId = true };
        Assert.True(await conditionTrueName.IsConditionMetAsync(webhookActionWithList));

        var conditionTrueId = new ListCondition(ListConditionConstraint.AnyOfTheseLists, aList.Id) { TreatListNameAsId = false };
        Assert.True(await conditionTrueId.IsConditionMetAsync(webhookActionWithList));

        var conditionFalseName = new ListCondition(ListConditionConstraint.AnyOfTheseLists, "SomeNameThatDoesNotExist") { TreatListNameAsId = true };
        Assert.False(await conditionFalseName.IsConditionMetAsync(webhookActionWithList));

        var conditionFalseId = new ListCondition(ListConditionConstraint.AnyOfTheseLists, Guid.NewGuid().ToString()) { TreatListNameAsId = false };
        Assert.False(await conditionFalseId.IsConditionMetAsync(webhookActionWithList));

        var noneConditionTrueName = new ListCondition(ListConditionConstraint.NoneOfTheseLists, aList.Name) { TreatListNameAsId = true };
        Assert.False(await noneConditionTrueName.IsConditionMetAsync(webhookActionWithList));

        var noneConditionTrueId = new ListCondition(ListConditionConstraint.NoneOfTheseLists, aList.Id) { TreatListNameAsId = false };
        Assert.False(await noneConditionTrueId.IsConditionMetAsync(webhookActionWithList));

        var noneConditionFalseName = new ListCondition(ListConditionConstraint.NoneOfTheseLists, "SomeNameThatDoesNotExist") { TreatListNameAsId = true };
        Assert.True(await noneConditionFalseName.IsConditionMetAsync(webhookActionWithList));

        var noneConditionFalseId = new ListCondition(ListConditionConstraint.NoneOfTheseLists, Guid.NewGuid().ToString()) { TreatListNameAsId = false };
        Assert.True(await noneConditionFalseId.IsConditionMetAsync(webhookActionWithList));
    }

    private async Task TestCardCoverCondition(List<List> lists)
    {
        var aList = lists.First();
        var card = await TrelloClient.AddCardAsync(new Card(aList.Id, "Some Card"));
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardCreated, cardToSimulate: card);
        
        Assert.True(await new CardCoverCondition(CardCoverConditionConstraint.DoesNotHaveACover).IsConditionMetAsync(webhookAction));
        Assert.True(await new CardCoverCondition(CardCoverConditionConstraint.DoesNotHaveACoverOfTypeColor).IsConditionMetAsync(webhookAction));
        Assert.True(await new CardCoverCondition(CardCoverConditionConstraint.DoesNotHaveACoverOfTypeImage).IsConditionMetAsync(webhookAction));

        Assert.False(await new CardCoverCondition(CardCoverConditionConstraint.HaveACover).IsConditionMetAsync(webhookAction));
        Assert.False(await new CardCoverCondition(CardCoverConditionConstraint.HaveACoverOfTypeColor).IsConditionMetAsync(webhookAction));
        Assert.False(await new CardCoverCondition(CardCoverConditionConstraint.HaveACoverOfTypeImage).IsConditionMetAsync(webhookAction));

        card.Cover = new CardCover(CardCoverColor.Blue, CardCoverSize.Full);
        await TrelloClient.UpdateCardAsync(card);

        Assert.False(await new CardCoverCondition(CardCoverConditionConstraint.DoesNotHaveACover).IsConditionMetAsync(webhookAction));
        Assert.False(await new CardCoverCondition(CardCoverConditionConstraint.DoesNotHaveACoverOfTypeColor).IsConditionMetAsync(webhookAction));
        Assert.True(await new CardCoverCondition(CardCoverConditionConstraint.DoesNotHaveACoverOfTypeImage).IsConditionMetAsync(webhookAction));

        Assert.True(await new CardCoverCondition(CardCoverConditionConstraint.HaveACover).IsConditionMetAsync(webhookAction));
        Assert.True(await new CardCoverCondition(CardCoverConditionConstraint.HaveACoverOfTypeColor).IsConditionMetAsync(webhookAction));
        Assert.False(await new CardCoverCondition(CardCoverConditionConstraint.HaveACoverOfTypeImage).IsConditionMetAsync(webhookAction));

        var webhookActionNoCard = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.BoardUpdated);
        await Assert.ThrowsAsync<AutomationException>(async () => await new CardCoverCondition(CardCoverConditionConstraint.HaveACover).IsConditionMetAsync(webhookActionNoCard));

    }

    private async Task TestChecklistNotStartedCondition(List<List> lists)
    {
        var aList = lists.First();
        var card = await TrelloClient.AddCardAsync(new Card(aList.Id, "Some Card"));

        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardCreated, card);
        var webhookActionNoCard = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.BoardUpdated);

        var checklistNameToCheck = "Test";
        IAutomationCondition condition = new ChecklistNotStartedCondition(checklistNameToCheck);
        Assert.False(await condition.IsConditionMetAsync(webhookAction));
        Assert.False(await condition.IsConditionMetAsync(webhookActionNoCard));

        var checklist = await TrelloClient.AddChecklistAsync(card.Id, new Checklist(checklistNameToCheck, new List<ChecklistItem> { new ChecklistItem("Item A"), new ChecklistItem("Item B") }));
        Assert.True(await condition.IsConditionMetAsync(webhookAction));

        checklist.Items[0].State = ChecklistItemState.Complete;
        await TrelloClient.UpdateChecklistItemAsync(card.Id, checklist.Items[0]);
        Assert.False(await condition.IsConditionMetAsync(webhookAction));
    }

    private async Task TestChecklistItemsIncompleteCondition(List<List> lists)
    {
        var aList = lists.First();
        var card = await TrelloClient.AddCardAsync(new Card(aList.Id, "Some Card"));
        
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardCreated, cardToSimulate: card);
        IAutomationCondition condition = new ChecklistItemsIncompleteCondition();
        Assert.False(await condition.IsConditionMetAsync(webhookAction));

        await TrelloClient.AddChecklistAsync(card.Id, new Checklist("CheckList1"));
        Assert.False(await condition.IsConditionMetAsync(webhookAction));

        var checklist = await TrelloClient.AddChecklistAsync(card.Id, new Checklist("CheckList2", new List<ChecklistItem> { new ChecklistItem("Item A"), new ChecklistItem("Item B") }));
        Assert.True(await condition.IsConditionMetAsync(webhookAction));

        checklist.Items[0].State = ChecklistItemState.Complete;
        await TrelloClient.UpdateChecklistItemAsync(card.Id, checklist.Items[0]);
        Assert.True(await condition.IsConditionMetAsync(webhookAction));

        checklist.Items[1].State = ChecklistItemState.Complete;
        await TrelloClient.UpdateChecklistItemAsync(card.Id, checklist.Items[1]);
        Assert.False(await condition.IsConditionMetAsync(webhookAction));

        Assert.False(await condition.IsConditionMetAsync(WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.BoardUpdated)));
    }

    private void AddOutput(string description, ref int step, int totalSteps)
    {
        _output.WriteLine($"ConditionTests - Step {step}/{totalSteps} - {description}");
        step++;
    }
}