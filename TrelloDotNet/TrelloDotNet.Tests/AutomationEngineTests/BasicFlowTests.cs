using System.Reflection;
using TrelloDotNet.AutomationEngine;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.Tests.AutomationEngineTests.SampleActions;
using TrelloDotNet.Tests.AutomationEngineTests.SampleConditions;
using TrelloDotNet.Tests.AutomationEngineTests.SampleTriggers;

namespace TrelloDotNet.Tests.AutomationEngineTests;

[Collection("Automation Engine Tests")]
public class BasicFlowTests : TestBase
{
    [Fact]
    public async Task TrueTrigger()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, new List<IAutomationCondition>(), new List<IAutomationAction>() { new DummyAction() });
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
        var automationController = CreateAutomationController(trigger, new List<IAutomationCondition>(), new List<IAutomationAction>() { new SkipAction() });
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
        var automationController = CreateAutomationController(trigger, new List<IAutomationCondition>() { new AlwaysFalseCondition() }, new List<IAutomationAction>() { new DummyAction() });
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
        var automationController = CreateAutomationController(trigger, new List<IAutomationCondition>() { new AlwaysTrueCondition() }, new List<IAutomationAction>() { new DummyAction() });
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
        var automationController = CreateAutomationController(trigger, new List<IAutomationCondition>() { new AlwaysTrueCondition(), new AlwaysTrueCondition() }, new List<IAutomationAction>() { new DummyAction() });
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
        var automationController = CreateAutomationController(trigger, new List<IAutomationCondition>() { new AlwaysTrueCondition(), new AlwaysFalseCondition() }, new List<IAutomationAction>() { new DummyAction() });
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
        var automationController = CreateAutomationController(triggeer, new List<IAutomationCondition>(), new List<IAutomationAction>() { new DummyAction()});
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
        var automationController = CreateAutomationController(trigger, new List<IAutomationCondition>() { new AlwaysTrueCondition() }, new List<IAutomationAction>() { new DummyAction(), new DummyAction()});
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
        var automationController = CreateAutomationController(trigger, new List<IAutomationCondition>(), new List<IAutomationAction>() { new ExceptionAction() });
        await Assert.ThrowsAsync<AutomationException>(async () => await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json"))));
    }

    [Fact]
    public async Task ExceptionCondition()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, new List<IAutomationCondition>() { new ExceptionCondition()}, new List<IAutomationAction>() { new ExceptionAction() });
        await Assert.ThrowsAsync<AutomationException>(async () => await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json"))));
    }

    [Fact]
    public async Task ExceptionAction()
    {
        var trigger = new AlwaysTrueTrigger();
        var automationController = CreateAutomationController(trigger, new List<IAutomationCondition>(), new List<IAutomationAction>() { new ExceptionAction() });
        await Assert.ThrowsAsync<AutomationException>(async () => await automationController.ProcessJsonFromWebhookAsync(new ProcessingRequest(GetJsonFromSampleFile("MoveCardFromListToList.json"))));
    }



    private AutomationController CreateAutomationController(IAutomationTrigger trigger, List<IAutomationCondition> conditions, List<IAutomationAction> actions)
    {
        var name = "X";
        var automation1 = new Automation(name, trigger, conditions, actions);
        var automation2 = new Automation(name, trigger, conditions, actions);
        var configuration = new Configuration(TrelloClient, automation1, automation2);
        var automationController = new AutomationController(configuration);
        return automationController;
    }

    private string GetJsonFromSampleFile(string filename)
    {
        string sampleJsonFolder = "SampleJson";
        string webhookEventsFolder = "WebhookEvents";
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