using TrelloDotNet.AutomationEngine;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.Tests.AutomationEngineTests.SampleTriggers;

namespace TrelloDotNet.Tests.AutomationEngineTests;

[Collection("Automation Engine Tests")]
public class GuardTests : TestBase
{
    [Fact]
    public void ControllerNullConfiguration()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new AutomationController(null));
        Assert.StartsWith("You did not provide a configuration", exception.Message);
    }

    [Fact]
    public void ConfigurationNullTrelloClient()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Configuration(null));
        Assert.StartsWith("TrelloClient can't be null", exception.Message);
    }

    [Fact]
    public void ConfigurationNoAutomations()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Configuration(TrelloClient));
        Assert.StartsWith("You need to provide at least one Automation", exception.Message);
    }
    
    [Fact]
    public void AutomationNullTrigger()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Automation(null, null,null, null));
        Assert.StartsWith("Trigger can't be null", exception.Message);
    }

    [Fact]
    public void AutomationNullAction()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Automation(null, new AlwaysTrueTrigger(), null, null));
        Assert.StartsWith("Actions can't be null", exception.Message);
    }

    [Fact]
    public void AutomationNoActions()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Automation(null, new AlwaysTrueTrigger(), null, new List<IAutomationAction>()));
        Assert.StartsWith("You need at least one action", exception.Message);
    }
}