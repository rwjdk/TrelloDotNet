using System.Reflection;
using TrelloDotNet.AutomationEngine;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.Tests.AutomationEngineTests.SampleActions;
using TrelloDotNet.Tests.AutomationEngineTests.SampleConditions;
using TrelloDotNet.Tests.AutomationEngineTests.SampleTriggers;

namespace TrelloDotNet.Tests.AutomationEngineTests;

public class BasicFlowTests : TestBase
{
    [Fact]
    public async Task TrueTrigger()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, [], [new DummyAction()]);
        var result = await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json")));
        Assert.Equal(2, result.AutomationsProcessed);
        Assert.Equal(0, result.AutomationsSkipped);
        Assert.Equal(2, result.ActionsExecuted);
        Assert.Equal(0, result.ActionsSkipped);
        Assert.NotNull(result.Log);
        Assert.NotNull(result.Log[0].Message);
        Assert.NotEmpty(result.Log[0].Timestamp.ToString());
    }

    [Fact]
    public async Task TrueTriggersTrueFalse()
    {
        var trueTrigger = new AlwaysTrueTrigger();
        var falseTrigger = new AlwaysFalseTrigger();
        var automationController = CreateAutomationController([trueTrigger, falseTrigger], [], [new DummyAction()]);
        var result = await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json")));
        Assert.Equal(2, result.AutomationsProcessed);
        Assert.Equal(0, result.AutomationsSkipped);
        Assert.Equal(2, result.ActionsExecuted);
        Assert.Equal(0, result.ActionsSkipped);
        Assert.NotNull(result.Log);
        Assert.NotNull(result.Log[0].Message);
        Assert.NotEmpty(result.Log[0].Timestamp.ToString());
    }

    [Fact]
    public async Task TrueTriggersFalseTrue()
    {
        var falseTrigger = new AlwaysFalseTrigger();
        var trueTrigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController([falseTrigger, trueTrigger], [], [new DummyAction()]);
        var result = await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json")));
        Assert.Equal(2, result.AutomationsProcessed);
        Assert.Equal(0, result.AutomationsSkipped);
        Assert.Equal(2, result.ActionsExecuted);
        Assert.Equal(0, result.ActionsSkipped);
        Assert.NotNull(result.Log);
        Assert.NotNull(result.Log[0].Message);
        Assert.NotEmpty(result.Log[0].Timestamp.ToString());
    }

    [Fact]
    public async Task SkipAction()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, [], [new SkipAction()]);
        var result = await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json")));
        Assert.Equal(2, result.AutomationsProcessed);
        Assert.Equal(0, result.AutomationsSkipped);
        Assert.Equal(0, result.ActionsExecuted);
        Assert.Equal(2, result.ActionsSkipped);
    }

    [Fact]
    public async Task TrueTriggerFalseCondition()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, [new AlwaysFalseCondition()], [new DummyAction()]);
        var result = await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json")));
        Assert.Equal(0, result.AutomationsProcessed);
        Assert.Equal(2, result.AutomationsSkipped);
        Assert.Equal(0, result.ActionsExecuted);
        Assert.Equal(0, result.ActionsSkipped);
    }

    [Fact]
    public async Task TrueTriggerTrueCondition()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, [new AlwaysTrueCondition()], [new DummyAction()]);
        var result = await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json")));
        Assert.Equal(2, result.AutomationsProcessed);
        Assert.Equal(0, result.AutomationsSkipped);
        Assert.Equal(2, result.ActionsExecuted);
        Assert.Equal(0, result.ActionsSkipped);
    }

    [Fact]
    public async Task TrueTrigger2TrueConditions()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, [new AlwaysTrueCondition(), new AlwaysTrueCondition()], [new DummyAction()]);
        var result = await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json")));
        Assert.Equal(2, result.AutomationsProcessed);
        Assert.Equal(0, result.AutomationsSkipped);
        Assert.Equal(2, result.ActionsExecuted);
        Assert.Equal(0, result.ActionsSkipped);
    }

    [Fact]
    public async Task TrueTrigger2ConditionsOneTrueAndOneFalse()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, [new AlwaysTrueCondition(), new AlwaysFalseCondition()], [new DummyAction()]);
        var result = await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json")));
        Assert.Equal(0, result.AutomationsProcessed);
        Assert.Equal(2, result.AutomationsSkipped);
        Assert.Equal(0, result.ActionsExecuted);
        Assert.Equal(0, result.ActionsSkipped);
    }

    [Fact]
    public async Task FalseTrigger()
    {
        var triggeer = new AlwaysFalseTrigger();
        var automationController = CreateAutomationController(triggeer, [], [new DummyAction()]);
        var result = await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json")));
        Assert.Equal(0, result.AutomationsProcessed);
        Assert.Equal(2, result.AutomationsSkipped);
        Assert.Equal(0, result.ActionsExecuted);
        Assert.Equal(0, result.ActionsSkipped);
    }

    [Fact]
    public async Task FalseTriggers()
    {
        var falseTrigger1 = new AlwaysFalseTrigger();
        var falseTrigger2 = new AlwaysFalseTrigger();
        var automationController = CreateAutomationController([falseTrigger1, falseTrigger2], [], [new DummyAction()]);
        var result = await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json")));
        Assert.Equal(0, result.AutomationsProcessed);
        Assert.Equal(2, result.AutomationsSkipped);
        Assert.Equal(0, result.ActionsExecuted);
        Assert.Equal(0, result.ActionsSkipped);
    }

    [Fact]
    public async Task TrueTriggerAndCondition()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, [new AlwaysTrueCondition()], [new DummyAction(), new DummyAction()]);
        var result = await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json")));
        Assert.Equal(2, result.AutomationsProcessed);
        Assert.Equal(0, result.AutomationsSkipped);
        Assert.Equal(4, result.ActionsExecuted);
        Assert.Equal(0, result.ActionsSkipped);
    }

    [Fact]
    public async Task ExceptionTrigger()
    {
        var trigger = new ExceptionTrigger();
        var automationController = CreateAutomationController(trigger, [], [new ExceptionAction()]);
        await Assert.ThrowsAsync<AutomationException>(async () => await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json"))));
    }

    [Fact]
    public async Task ExceptionCondition()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, [new ExceptionCondition()], [new ExceptionAction()]);
        await Assert.ThrowsAsync<AutomationException>(async () => await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json"))));
    }

    [Fact]
    public async Task ExceptionAction()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, [], [new ExceptionAction()]);
        await Assert.ThrowsAsync<AutomationException>(async () => await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json"))));
    }


    private AutomationController CreateAutomationController(IAutomationTrigger trigger, List<IAutomationCondition> conditions, List<IAutomationAction> actions)
    {
        const string name = "X";
        var automation1 = new Automation(name, trigger, conditions, actions);
        var automation2 = new Automation(name, trigger, conditions, actions);
        var configuration = new Configuration(TrelloClient, automation1, automation2);
        var automationController = new AutomationController(configuration);
        return automationController;
    }

    private AutomationController CreateAutomationController(List<IAutomationTrigger> triggers, List<IAutomationCondition> conditions, List<IAutomationAction> actions)
    {
        const string name = "X";
        var automation1 = new Automation(name, triggers, conditions, actions);
        var automation2 = new Automation(name, triggers, conditions, actions);
        var configuration = new Configuration(TrelloClient, automation1, automation2);
        var automationController = new AutomationController(configuration);
        return automationController;
    }

    private string GetJsonFromSampleFile(string filename)
    {
        const string sampleJsonFolder = "SampleJson";
        const string webhookEventsFolder = "WebhookEvents";
        var assemblyLocation = Assembly.GetExecutingAssembly().Location;
        string path = Path.GetDirectoryName(assemblyLocation) +
                      Path.DirectorySeparatorChar +
                      sampleJsonFolder +
                      Path.DirectorySeparatorChar +
                      webhookEventsFolder +
                      Path.DirectorySeparatorChar +
                      filename;
        return File.ReadAllText(path);
    }
}